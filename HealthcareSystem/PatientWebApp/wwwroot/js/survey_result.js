$(document).ready(function () {

	$.ajax({
		url: "/api/survey/surveyResultAboutMedicalStaff",
		type: "GET",
		dataType: 'json',
		processData: false,
		contentType: false,
		success: function (surveyResult) {
			console.log('success - loading survey about medical staff from database');
			if (surveyResult.length == 0) {
				console.log("there are no survey results about medical staff");
			}
			else {
				let overallAverageRating = calculateOverallAverageRating(surveyResult);
				$('div#survey_result_MedicalStaff').append('<p class="text-center h5 mb-4">Overall average rating: ' + overallAverageRating + ' / 5</p>');
				for (let i = 0; i < surveyResult.length; i++) {
					let divElement = addOneSurveyResult(surveyResult[i]);
					$('div#survey_result_MedicalStaff').append(divElement);
				}
			}
		},
		error: function () {
			alert("error getting survey result medical staff");
			console.log('error getting survey result medical staff');
		}
	});

	$.ajax({
		url: "/api/survey/surveyResultAboutHospital",
		type: "GET",
		dataType: 'json',
		processData: false,
		contentType: false,
		success: function (surveyResult) {
			console.log('success - loading survey about hospital from database');
			if (surveyResult.length == 0) {
				console.log("there are no survey results about hospital");
			}
			else {
				let overallAverageRating = calculateOverallAverageRating(surveyResult);
				$('div#survey_result_Hospital').append('<p class="text-center h5 mb-4">Overall average rating: ' + overallAverageRating + ' / 5</p>');
				for (let i = 0; i < surveyResult.length; i++) {
					let divElement = addOneSurveyResult(surveyResult[i]);
					$('div#survey_result_Hospital').append(divElement);
				}
			}
		},
		error: function () {
			alert("error getting survey result about hospital");
			console.log('error getting survey result about hospital');
		}
	});


	$.ajax({
		url: "/api/doctor",
		type: "GET",
		dataType: 'json',
		processData: false,
		contentType: false,
		success: function (doctors) {
			console.log('success - loading doctors from database');
			if (doctors.length == 0) {
				console.log("there are no doctors");
			}
			else {
				for (let i = 0; i < doctors.length; i++) {
					$('select#doctors').append('<option value="' + doctors[i].jmbg + '">' + doctors[i].name + ' ' + doctors[i].surname + '</option>');
				}
			}
		},
		error: function () {
			alert("error getting doctors");
			console.log('error getting doctors');
		}
	});

});


function addOneSurveyResult(surveyResult) {

	let divElement = $(
		'<div style="background-color: #cce6ff; margin-bottom:28px;" class="container p-2 my-2 border"> '
		+ '<table border="0" style="margin-bottom:10px;">'
		+ '<tr>'
		+ '<th style = "width:350px;height:20px" > <p class=" h5"> ' + surveyResult.ratedItem + '</p></th> '
		+ '<td style="text-align:right;width:360px;"><p class=" h6"> Average rating: ' + (surveyResult.averageRating).toFixed(2) + '</p></td>'
		+ '<td>'
		+ '<div style = "width:250px;height:25px" class= "progress" > '
		+ '<div class="progress-bar bg-dark" style="width:' + surveyResult.averageRating * 100 / 5 + '%;height: 25px">  </div> '
		+ '</div>'
		+ '</td> '
		+ '</tr>'
		+ '</table>'
		+ '<table border="0" align="left"> '
		+ '<tr>'
		+ '<td style = "text-align:left;" > <p style="margin-right:30px;">Number of grade ONE: ' + surveyResult.numberOfGradesOne + '</p></td > '
		+ '<td style="text-align:left;"><p style="margin-right:30px;">Number of grade TWO: ' + surveyResult.numberOfGradesTwo + '</p></td>'
		+ '<td style="text-align:left;"><p style="margin-right:30px;">Number of grade THREE: ' + surveyResult.numberOfGradesThree + '</p></td>'
		+ '<td style="text-align:left;"><p style="margin-right:30px;">Number of grade FOUR: ' + surveyResult.numberOfGradesFour + '</p></td>'
		+ '<td style="text-align:left;"><p style="margin-right:30px;">Number of grade FIVE: ' + surveyResult.numberOfGradesFive + '</p></td>'
		+ '</tr>'
		+ '</table>'
		+ '</div> '
	);
	return divElement;
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

	$.ajax({
		url: "/api/survey/surveyResultAboutDoctor/" + doctorValue,
		type: "GET",
		dataType: 'json',
		processData: false,
		contentType: false,
		success: function (surveyResult) {
			console.log('success - doctor selected');
			if (surveyResult.length == 0) {
				alert("there are no survey results about doctor");
				console.log("there are no survey results about doctor");
			}
			else {
				document.getElementById('survey_result_Doctor').innerHTML = "";
				let overallAverageRating = calculateOverallAverageRating(surveyResult);
				$('div#survey_result_Doctor').append('<p class="text-center h5 mb-4">Overall average rating: ' + overallAverageRating + ' / 5</p>');
				for (let i = 0; i < surveyResult.length; i++) {
					let divElement = addOneSurveyResult(surveyResult[i]);
					$('div#survey_result_Doctor').append(divElement);
				}
			}
		},
		error: function (jqXHR) {
			document.getElementById('survey_result_Doctor').innerHTML = "";
			alert(jqXHR.responseText);
			console.log(jqXHR.responseText);
		}
	});
}
