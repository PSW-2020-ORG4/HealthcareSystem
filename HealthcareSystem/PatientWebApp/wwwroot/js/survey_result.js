$(document).ready(function () {
	checkUserRole("Admin");

	$.ajax({
		url: "/api/survey/surveyResultAboutMedicalStaff",
		type: "GET",
		headers: {
			'Authorization': 'Bearer ' + window.localStorage.getItem('token')
		},
		dataType: 'json',
		processData: false,
		contentType: false,
		success: function (medicalStaffSurveyReport) {
			if (medicalStaffSurveyReport.items.length == 0) {
				let alert = '<div class="alert alert-info mb-0" role="alert">'
					+ 'No information available.'
					+ '</div>';
				$('#loadingStaff').remove();
				$('#survey_result_MedicalStaff').append(alert);
			}
			else {
				$('#avgStaff').text(medicalStaffSurveyReport.totalAverage.toFixed(2));

				let table = '<table class="table table-bordered mb-0">' + tableHeader() + '<tbody>';
				for (let i = 0; i < medicalStaffSurveyReport.items.length; i++) {
					table = table + tableRow(medicalStaffSurveyReport.items[i]);
				}
				table = table + '</tbody></table>';

				$('#loadingStaff').remove();
				$('#survey_result_MedicalStaff').append(table);
			}
		},
		error: function (jqXHR) {
			let alert = '<div class="alert alert-info mb-0" role="alert">'
				+ jqXHR.responseJSON
				+ '</div>';
			$('#loadingStaff').remove();
			$('#survey_result_MedicalStaff').append(alert);
		}
	});

	$.ajax({
		url: "/api/survey/surveyResultAboutHospital",
		type: "GET",
		headers: {
			'Authorization': 'Bearer ' + window.localStorage.getItem('token')
		},
		dataType: 'json',
		processData: false,
		contentType: false,
		success: function (hospitalSurveyReport) {
			if (hospitalSurveyReport.items.length == 0) {
				let alert = '<div class="alert alert-info mb-0" role="alert">'
					+ 'No information available.'
					+ '</div>';
				$('#loadingHospital').remove();
				$('#survey_result_Hospital').append(alert);
			}
			else {
				$('#avgHospital').text(hospitalSurveyReport.totalAverage.toFixed(2));

				let table = '<table class="table table-bordered mb-0">' + tableHeader() + '<tbody>';
				for (let i = 0; i < hospitalSurveyReport.items.length; i++) {
					table = table + tableRow(hospitalSurveyReport.items[i]);
				}
				table = table + '</tbody></table>';
				$('#loadingHospital').remove();
				$('#survey_result_Hospital').append(table);
			}
		},
		error: function (jqXHR) {
			let alert = '<div class="alert alert-info mb-0" role="alert">'
				+ jqXHR.responseJSON
				+ '</div>';
			$('#loadingHospital').remove();
			$('#survey_result_Hospital').append(alert);
		}
	});


	$.ajax({
		url: "/api/doctor",
		type: "GET",
		headers: {
			'Authorization': 'Bearer ' + window.localStorage.getItem('token')
		},
		dataType: 'json',
		processData: false,
		contentType: false,
		success: function (doctors) {
			if (doctors.length == 0) {
				let alert = '<div class="alert alert-info mb-0" role="alert">'
					+ 'No doctors found.'
					+ '</div>';
				$('#survey_result_Doctor').append(alert);
			}
			else {
				for (let i = 0; i < doctors.length; i++) {
					$('select#doctors').append('<option value="' + doctors[i].jmbg + '">' + doctors[i].name + ' ' + doctors[i].surname + '</option>');
				}
			}
		},
		error: function (jqXHR) {
			let alert = '<div class="alert alert-danger mb-0" role="alert">'
				+ jqXHR.responseJSON
				+ '</div>';
			$('#survey_result_Doctor').append(alert);
		}
	});

});

function tableHeader() {
	let header = '<thead class="thead-light">'
		+ '<tr>'
		+ '<th scope="col" rowspan="2"></th>'
		+ '<th scope="col" colspan="5" class="text-center">Number of grades</th>'
		+ '<th scope="col" rowspan="2" class="text-center" style="vertical-align:middle">Average grade</th>'
		+ '</tr>'
		+ '<tr>'
		+ '<th scope="col" class="text-center">One</th>'
		+ '<th scope="col" class="text-center">Two</th>'
		+ '<th scope="col" class="text-center">Three</th>'
		+ '<th scope="col" class="text-center">Four</th>'
		+ '<th scope="col" class="text-center">Five</th>'
		+ '</tr>'
		+ '</thead>';

	return header;
};

function tableRow(surveyResult) {
	let row = '<tr>'
		+ '<th scope="row" class="text-left" style="vertical-align:middle;width:30%">' + surveyResult.ratedItem + '</th>'
		+ '<td class="text-center" style="vertical-align:middle">' + surveyResult.numberOfGradesOne + '</td>'
		+ '<td class="text-center" style="vertical-align:middle">' + surveyResult.numberOfGradesTwo + '</td>'
		+ '<td class="text-center" style="vertical-align:middle">' + surveyResult.numberOfGradesThree + '</td>'
		+ '<td class="text-center" style="vertical-align:middle">' + surveyResult.numberOfGradesFour + '</td>'
		+ '<td class="text-center" style="vertical-align:middle">' + surveyResult.numberOfGradesFive + '</td>'
		+ '<td class="text-center" style="vertical-align:middle"><span class="badge badge-primary">' + (surveyResult.averageRating).toFixed(2) + '/5</span></td>'
		+ '</tr>';

	return row;
};


function changeDoctor(event) {

	var selectElement = document.getElementById("doctors");
	var doctorValue = selectElement.options[selectElement.selectedIndex].value;
	$('#loadingDoctor').show();
	$('#survey_result_Doctor').empty();

	$.ajax({
		url: "/api/survey/surveyResultAboutDoctor/" + doctorValue,
		type: "GET",
		headers: {
			'Authorization': 'Bearer ' + window.localStorage.getItem('token')
		},
		dataType: 'json',
		processData: false,
		contentType: false,
		success: function (doctorSurveyReport) {
			if (doctorSurveyReport.items.length == 0) {
				let alert = '<div class="alert alert-info mb-0" role="alert">'
					+ 'No information available.'
					+ '</div>';
				$('#loadingDoctor').hide();
				$('#survey_result_Doctor').append(alert);
				$('#avgDoctor').text('');
			}
			else {
				$('#avgDoctor').text(doctorSurveyReport.totalAverage.toFixed(2));

				let table = '<table class="table table-bordered mb-0">' + tableHeader() + '<tbody>';
				for (let i = 0; i < doctorSurveyReport.items.length; i++) {
					table = table + tableRow(doctorSurveyReport.items[i]);
				}
				table = table + '</tbody></table>';
				$('#loadingDoctor').hide();
				$('#survey_result_Doctor').append(table);
			}
		},
		error: function (jqXHR) {
			let alert = '<div class="alert alert-info mb-0" role="alert">'
				+ jqXHR.responseJSON
				+ '</div>';
			$('#loadingDoctor').hide();
			$('#survey_result_Doctor').append(alert);
			$('#avgDoctor').text('');
		}
	});
}