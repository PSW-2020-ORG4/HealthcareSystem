﻿$(document).ready(function () {
    checkUserRole("Admin");

	$.ajax({
		url: "/api/event",
		type: "GET",
		headers: {
			'Authorization': 'Bearer ' + window.localStorage.getItem('token')
		},
		dataType: 'json',
		processData: false,
		contentType: false,
		success: function (statistic) {
			$('#loading').remove();
			addStatisticTable(statistic);
		},
		error: function (jqXHR) {
			let alert = $('<div class="alert alert-danger alert-dismissible fade show m-1" role="alert">'
				+ jqXHR.responseJSON + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >')
			$('#loading').remove();
			$('div#view_statistic').prepend(alert);
		}
	});
});

function addStatisticTable(statistic) {

	let table1 = '<table class="table" style="margin-top:50px;">'
		+ '<thead><tr><th scope="col" style="width:350px;">Description</th><th scope="col">Minimum</th><th scope="col">Average</th><th scope="col">Maximum</th></tr></thead >'
		+ '<tr><td>Successful scheduling duration</td><td>' + statistic.successfulSchedulingDuration.minimum + ' min</td>'
		+ '<td>' + statistic.successfulSchedulingDuration.average + ' min</td>'
		+ '<td>' + statistic.successfulSchedulingDuration.maximum + ' min</td></tr>'
		+ '<tr><td>Successful scheduling age statistic</td><td>' + statistic.successfulSchedulingAgeStatistic.minimum + ' years</td>'
		+ '<td>' + statistic.successfulSchedulingAgeStatistic.average + ' years</td>'
		+ '<td>' + statistic.successfulSchedulingAgeStatistic.maximum + ' years</td></tr>'
		+ '</table > ';

	let closedStep = "Date step";
	let previousStep = "Date step";

	if (statistic.closedSchedulingStepStatistic.mostClosedStep == 1) {
		closedStep = "Specialty step";
	}
	else if (statistic.closedSchedulingStepStatistic.mostClosedStep == 2) {
		closedStep = "Doctor step";
	}
	else if (statistic.closedSchedulingStepStatistic.mostClosedStep == 3) {
		closedStep = "Appointment step";
	}

	if (statistic.previousSchedulingStepStatistic.mostReturnedStep == 1) {
		previousStep = "Specialty step";
	}
	else if (statistic.previousSchedulingStepStatistic.mostReturnedStep == 2) {
		previousStep = "Doctor step";
	}
	else if (statistic.previousSchedulingStepStatistic.mostReturnedStep == 3) {
		previousStep = "Appointment step";
    }

	let table2 = '<table class="table" style="margin-top:50px; margin-bottom:50px;">'
		+ '<thead><tr><th scope="col">Description</th><th scope="col">Date step</th><th scope="col">Specialty step</th>'
		+ '<th scope="col">Doctor step</th><th scope="col">Appointment step</th><th scope="col">Most used step</th></tr></thead > '
		+ '<tr><td>Closed scheduling step statistic</td><td>' + (statistic.closedSchedulingStepStatistic.numberOfClosuresOnDateStep / statistic.closedSchedulingStepStatistic.totalNumberOfClosures * 100).toFixed(2) + ' %</td>'
		+ '<td>' + (statistic.closedSchedulingStepStatistic.numberOfClosuresOnSpecialtyStep / statistic.closedSchedulingStepStatistic.totalNumberOfClosures * 100).toFixed(2) + ' %</td>'
		+ '<td>' + (statistic.closedSchedulingStepStatistic.numberOfClosuresOnDoctorStep / statistic.closedSchedulingStepStatistic.totalNumberOfClosures * 100).toFixed(2) + ' %</td>'
		+ '<td>' + (statistic.closedSchedulingStepStatistic.numberOfClosuresOnAppointmentStep / statistic.closedSchedulingStepStatistic.totalNumberOfClosures * 100).toFixed(2) + ' %</td>'
		+ '<td>' + closedStep + ' </td></tr>'

		+ '<tr><td>Previous scheduling step statistic</td>'
		+ '<td></td>'
		+ '<td> ' + (statistic.previousSchedulingStepStatistic.numberOfPreviousOnSpecialtyStep / statistic.previousSchedulingStepStatistic.totalNumberOfPrevious * 100).toFixed(2) + ' %</td> '
		+ '<td>' + (statistic.previousSchedulingStepStatistic.numberOfPrevoiusOnDoctorStep / statistic.previousSchedulingStepStatistic.totalNumberOfPrevious * 100).toFixed(2) + ' %</td>'
		+ '<td>' + (statistic.previousSchedulingStepStatistic.numberOfPreviousOnAppointmentStep / statistic.previousSchedulingStepStatistic.totalNumberOfPrevious * 100).toFixed(2) + ' %</td>'
		+ '<td>' + previousStep + ' </td></tr>'
		+ '</table>';

	let table3 = '<table class="table" style="margin-top:50px;">'
		+ '<thead><tr><th scope="col" style="width:500px;">Description</th><th scope="col">Younger</th><th scope="col">Elder</th></tr></thead >'
		+ '<tr><td>Successful scheduling duration by age</td><td>' + statistic.successfulSchedulingAgeDuration.durationFirst + ' min</td>'
		+ '<td>' + statistic.successfulSchedulingAgeDuration.durationSecond + ' min</td></tr>'
		+ '<tr><td>Unsuccessful scheduling duration by age</td><td>' + statistic.unsuccessfulSchedulingAgeDuration.durationFirst + ' min</td>'
		+ '<td>' + statistic.unsuccessfulSchedulingAgeDuration.durationSecond + ' min</td></tr>'
		+ '</table > ';

	let table4 = '<table class="table" style="margin-top:50px;">'
		+ '<thead><tr><th scope="col" style="width:500px;">Description</th><th scope="col">Female</th><th scope="col">Male</th></tr></thead >'
		+ '<tr><td>Successful scheduling duration by gender</td><td>' + statistic.successfulSchedulingGenderDuration.durationFirst + ' min</td>'
		+ '<td>' + statistic.successfulSchedulingGenderDuration.durationSecond + ' min</td></tr>'
		+ '<tr><td>Unsuccessful scheduling duration by gender</td><td>' + statistic.unsuccessfulSchedulingGenderDuration.durationFirst + ' min</td>'
		+ '<td>' + statistic.unsuccessfulSchedulingGenderDuration.durationSecond + ' min</td></tr>'
		+ '</table > ';

	let table5 = '<table class="table" style="margin-top:50px;">'
		+ '<thead><tr><th scope="col" style="width:500px;">Description</th><th scope="col">Successful</th><th scope="col">Unsuccessful</th></tr></thead >'
		+ '<tr><td>Successful/unsuccessful scheduling ratio</td><td>' + (statistic.successfulAndUnsuccessfulSchedulingDTO.numberOfSuccessfulScheduling / statistic.successfulAndUnsuccessfulSchedulingDTO.numberOfScheduling * 100).toFixed(2) + ' %</td>'
		+ '<td>' + (statistic.successfulAndUnsuccessfulSchedulingDTO.numberOfUnsuccessfulScheduling / statistic.successfulAndUnsuccessfulSchedulingDTO.numberOfScheduling * 100).toFixed(2) + ' %</td></tr>'
		+ '</table > ';

	let table6 = '<table class="table" style="margin-top:50px;">'
		+ '<thead><tr><th scope="col" style="width:350px;">Description</th><th scope="col">Previous</th><th scope="col">Next</th><th scope="col">Close</th></tr></thead >'
		+ '<tr><td>Number of steps \t \t</td><td>' + (statistic.schedulingStepsStatistic.numberOfPreviousSteps / statistic.schedulingStepsStatistic.numberOfSteps * 100).toFixed(2)  + ' %</td>'
		+ '<td>' + (statistic.schedulingStepsStatistic.numberOfNextSteps / statistic.schedulingStepsStatistic.numberOfSteps * 100).toFixed(2) + ' %</td>'
		+ '<td>' + (statistic.schedulingStepsStatistic.numberOfClosedScheduling / statistic.schedulingStepsStatistic.numberOfSteps * 100).toFixed(2) + ' %</td></tr>'
		+ '</table > ';
	
	$('div#view_statistic').append(table3);
	$('div#view_statistic').append(table4);
	$('div#view_statistic').append(table5);
	$('div#view_statistic').append(table1);
	$('div#view_statistic').append(table6);
	$('div#view_statistic').append(table2);
	
};