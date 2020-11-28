$(document).ready(function () {

	$.ajax({
		url: "/api/survey/surveyResultAboutMedicalStaff",
		type: "GET",
		dataType: 'json',
		processData: false,
		contentType: false,
		success: function (surveyResult) {

			alert("success  medical staff  ")

			if (surveyResult.length == 0) {
				console.log("there are no survey results");
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
			alert("error");
			console.log('error getting survey result');
		}
	});


	$.ajax({
		url: "/api/survey/surveyResultAboutHospital",
		type: "GET",
		dataType: 'json',
		processData: false,
		contentType: false,
		success: function (surveyResult) {

			alert("success Hospital ")

			if (surveyResult.length == 0) {
				console.log("there are no survey results");
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
			alert("error");
			console.log('error getting survey result');
		}
	});


	$.ajax({
		url: "/api/survey/surveyResultAboutDoctor/1308987105625",
		type: "GET",
		dataType: 'json',
		processData: false,
		contentType: false,
		success: function (surveyResult) {

			alert("success doctor ")

			if (surveyResult.length == 0) {
				console.log("there are no survey results");
			}
			else {
				let overallAverageRating = calculateOverallAverageRating(surveyResult);
				$('div#survey_result_Doctor').append('<p class="text-center h5 mb-4">Overall average rating: ' + overallAverageRating + ' / 5</p>');
				for (let i = 0; i < surveyResult.length; i++) {
					let divElement = addOneSurveyResult(surveyResult[i]);
					$('div#survey_result_Doctor').append(divElement);
				}
			}
		},
		error: function () {
			alert("error");
			console.log('error getting survey result');
		}
	});
});


function addOneSurveyResult(surveyResult) {

	let divElement = $(
		'<div style="background-color: rgb(0, 204, 212);margin-bottom:28px;" class="container p-2 my-2 border"> '
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

