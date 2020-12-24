var jmbg = "";
$(document).ready(function () {
	var token = window.localStorage.getItem('token');
	if (token != null) {
		$.ajax({
			url: "/api/user/logged",
			type: 'GET',
			dataType: 'json',
			processData: false,
			contentType: 'application/json',
			data: JSON.stringify(token),
			success: function (loggedUser) {
				if (loggedUser.role != "Admin") {
					alert('Access denied!');
					return;
				}
				jmbg = loggedUser.jmbg;
			},
			error: function () {
				alert('Error getting logged user!');
			}
		});
	}
	else {
		alert('Unlogged user!');
		return;
	}

	$.ajax({
		url: "/api/survey/surveyResultAboutMedicalStaff",
		type: "GET",
		dataType: 'json',
		processData: false,
		contentType: false,
		success: function (surveyResult) {
			if (surveyResult.length == 0) {
				let alert = '<div class="alert alert-info mb-0" role="alert">'
					+ 'No information available.'
					+ '</div>';
				$('#loadingStaff').remove();
				$('#survey_result_MedicalStaff').append(alert);
			}
			else {
				let overallAverageRating = calculateOverallAverageRating(surveyResult);
				$('#avgStaff').text(overallAverageRating);

				let table = '<table class="table table-bordered mb-0">' + tableHeader() + '<tbody>';
				for (let i = 0; i < surveyResult.length; i++) {
					table = table + tableRow(surveyResult[i]);
				}
				table = table + '</tbody></table>';

				$('#loadingStaff').remove();
				$('#survey_result_MedicalStaff').append(table);
			}
		},
		error: function () {
			let alert = '<div class="alert alert-info mb-0" role="alert">'
				+ 'No information available.'
				+ '</div>';
			$('#loadingStaff').remove();
			$('#survey_result_MedicalStaff').append(alert);
		}
	});

	$.ajax({
		url: "/api/survey/surveyResultAboutHospital",
		type: "GET",
		dataType: 'json',
		processData: false,
		contentType: false,
		success: function (surveyResult) {
			if (surveyResult.length == 0) {
				let alert = '<div class="alert alert-info mb-0" role="alert">'
					+ 'No information available.'
					+ '</div>';
				$('#loadingHospital').remove();
				$('#survey_result_Hospital').append(alert);
			}
			else {
				let overallAverageRating = calculateOverallAverageRating(surveyResult);
				$('#avgHospital').text(overallAverageRating);

				let table = '<table class="table table-bordered mb-0">' + tableHeader() + '<tbody>';
				for (let i = 0; i < surveyResult.length; i++) {
					table = table + tableRow(surveyResult[i]);
				}
				table = table + '</tbody></table>';
				$('#loadingHospital').remove();
				$('#survey_result_Hospital').append(table);
			}
		},
		error: function () {
			let alert = '<div class="alert alert-info mb-0" role="alert">'
				+ 'No information available.'
				+ '</div>';
			$('#loadingHospital').remove();
			$('#survey_result_Hospital').append(alert);
		}
	});


	$.ajax({
		url: "/api/doctor",
		type: "GET",
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
		error: function () {
			let alert = '<div class="alert alert-danger mb-0" role="alert">'
				+ 'Error fetching doctors.'
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


function calculateOverallAverageRating(surveyResult) {
	let overallAverageRating = 0;
	for (let i = 0; i < surveyResult.length; i++) {
		overallAverageRating += surveyResult[i].averageRating;
	}
	return (overallAverageRating / surveyResult.length).toFixed(2);
}


function changeDoctor(event) {

	var selectElement = document.getElementById("doctors");
	var doctorValue = selectElement.options[selectElement.selectedIndex].value;
	$('#loadingDoctor').show();
	$('#survey_result_Doctor').empty();

	$.ajax({
		url: "/api/survey/surveyResultAboutDoctor/" + doctorValue,
		type: "GET",
		dataType: 'json',
		processData: false,
		contentType: false,
		success: function (surveyResult) {
			if (surveyResult.length == 0) {
				let alert = '<div class="alert alert-info mb-0" role="alert">'
					+ 'No information available.'
					+ '</div>';
				$('#loadingDoctor').hide();
				$('#survey_result_Doctor').append(alert);
				$('#avgDoctor').text('');
			}
			else {
				let overallAverageRating = calculateOverallAverageRating(surveyResult);
				$('#avgDoctor').text(overallAverageRating);

				let table = '<table class="table table-bordered mb-0">' + tableHeader() + '<tbody>';
				for (let i = 0; i < surveyResult.length; i++) {
					table = table + tableRow(surveyResult[i]);
				}
				table = table + '</tbody></table>';
				$('#loadingDoctor').hide();
				$('#survey_result_Doctor').append(table);
			}
		},
		error: function (jqXHR) {
			let alert = '<div class="alert alert-info mb-0" role="alert">'
				+ 'No information available.'
				+ '</div>';
			$('#loadingDoctor').hide();
			$('#survey_result_Doctor').append(alert);
			$('#avgDoctor').text('');
		}
	});
}
