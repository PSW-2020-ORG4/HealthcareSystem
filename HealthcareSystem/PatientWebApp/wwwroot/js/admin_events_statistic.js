$(document).ready(function () {
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
		+ '<thead><tr><th scope="col">Description</th><th scope="col">Minimum</th><th scope="col">Average</th><th scope="col">Maximum</th></tr></thead >'
		+ '<tr><td>Successful scheduling duration</td><td>' + statistic.successfulSchedulingDuration.minimum + ' min</td>'
		+ '<td>' + statistic.successfulSchedulingDuration.average + ' min</td>'
		+ '<td>' + statistic.successfulSchedulingDuration.maximum + ' min</td></tr>'
		+ '<tr><td>Successful scheduling age statistic</td><td>' + statistic.successfulSchedulingAgeStatistic.minimum + ' years</td>'
		+ '<td>' + statistic.successfulSchedulingAgeStatistic.average + ' years</td>'
		+ '<td>' + statistic.successfulSchedulingAgeStatistic.maximum + ' years</td></tr>'
		+ '</table > ';

	let table2 = '<table class="table" style="margin-top:50px; margin-bottom:50px;">'
		+ '<thead><tr><th scope="col">Description</th><th scope="col">Date step</th><th scope="col">Specialty step</th>'
		+ '<th scope="col">Doctor step</th><th scope="col">Appointment step</th><th scope="col">Most used step</th></tr></thead > '
		+ '<tr><td>Closed scheduling step statistic</td><td>' + (statistic.closedSchedulingStepStatistic.numberOfClosuresOnDateStep / statistic.closedSchedulingStepStatistic.totalNumberOfClosures).toFixed(2) * 100 + ' %</td>'
		+ '<td>' + (statistic.closedSchedulingStepStatistic.numberOfClosuresOnSpecialtyStep / statistic.closedSchedulingStepStatistic.totalNumberOfClosures).toFixed(2) * 100 + ' %</td>'
		+ '<td>' + (statistic.closedSchedulingStepStatistic.numberOfClosuresOnDoctorStep / statistic.closedSchedulingStepStatistic.totalNumberOfClosures).toFixed(2) * 100 + ' %</td>'
		+ '<td>' + (statistic.closedSchedulingStepStatistic.numberOfClosuresOnAppointmentStep / statistic.closedSchedulingStepStatistic.totalNumberOfClosures).toFixed(2) * 100 + ' %</td>'
		+ '<td>' + statistic.closedSchedulingStepStatistic.mostClosedStep + ' </td></tr>'

		+ '<tr><td>Previous scheduling step statistic</td>'
		+ '<td></td>'
		+ '<td> ' + (statistic.previousSchedulingStepStatistic.numberOfPreviousOnSpecialtyStep / statistic.previousSchedulingStepStatistic.totalNumberOfPrevious).toFixed(2) * 100 + ' %</td> '
		+ '<td>' + (statistic.previousSchedulingStepStatistic.numberOfPrevoiusOnDoctorStep / statistic.previousSchedulingStepStatistic.totalNumberOfPrevious).toFixed(2) * 100 + ' %</td>'
		+ '<td>' + (statistic.previousSchedulingStepStatistic.numberOfPreviousOnAppointmentStep / statistic.previousSchedulingStepStatistic.totalNumberOfPrevious).toFixed(2) * 100 + ' %</td>'
		+ '<td>' + statistic.previousSchedulingStepStatistic.mostReturnedStep + ' </td></tr>'
		+ '</table>';

	let table3 = '<table class="table" style="margin-top:50px;">'
		+ '<thead><tr><th scope="col">Description</th><th scope="col">Younger</th><th scope="col">Elder</th></tr></thead >'
		+ '<tr><td>Successful scheduling by age</td><td>' + statistic.successfulSchedulingAgeDuration.durationFirst + '</td>'
		+ '<td>' + statistic.successfulSchedulingAgeDuration.durationSecond + '</td></tr>'
		+ '<tr><td>Unsuccessful scheduling by age</td><td>' + statistic.unsuccessfulSchedulingAgeDuration.durationFirst + '</td>'
		+ '<td>' + statistic.unsuccessfulSchedulingAgeDuration.durationSecond + '</td></tr>'
		+ '</table > ';

	let table4 = '<table class="table" style="margin-top:50px;">'
		+ '<thead><tr><th scope="col">Description</th><th scope="col">Male</th><th scope="col">Female</th></tr></thead >'
		+ '<tr><td>Successful scheduling by gender</td><td>' + statistic.successfulSchedulingGenderDuration.durationFirst + '</td>'
		+ '<td>' + statistic.successfulSchedulingGenderDuration.durationSecond + '</td></tr>'
		+ '<tr><td>Unsuccessful scheduling by gender</td><td>' + statistic.unsuccessfulSchedulingGenderDuration.durationFirst + '</td>'
		+ '<td>' + statistic.unsuccessfulSchedulingGenderDuration.durationSecond + '</td></tr>'
		+ '</table > ';

	let table5 = '<table class="table" style="margin-top:50px;">'
		+ '<thead><tr><th scope="col">Description</th><th scope="col">Successful</th><th scope="col">Unsuccess</th></tr></thead >'
		+ '<tr><td>Scheduling</td><td>' + statistic.successfulAndUnsuccessfulSchedulingDTO.numberOfSuccessfulScheduling / statistic.successfulAndUnsuccessfulSchedulingDTO.numberOfScheduling * 100 + ' %</td>'
		+ '<td>' + statistic.successfulAndUnsuccessfulSchedulingDTO.numberOfUnsuccessfulScheduling / statistic.successfulAndUnsuccessfulSchedulingDTO.numberOfScheduling * 100 + ' %</td></tr>'
		+ '</table > ';

	let table6 = '<table class="table" style="margin-top:50px;">'
		+ '<thead><tr><th scope="col">Description</th><th scope="col">Previous</th><th scope="col">Next</th><th scope="col">Close</th></tr></thead >'
		+ '<tr><td>Number of steps</td><td>' + (statistic.schedulingStepsStatistic.numberOfPreviousSteps / statistic.schedulingStepsStatistic.numberOfSteps).toFixed(2) * 100 + ' %</td>'
		+ '<td>' + (statistic.schedulingStepsStatistic.numberOfNextSteps / statistic.schedulingStepsStatistic.numberOfSteps).toFixed(2) * 100 + ' %</td>'
		+ '<td>' + (statistic.schedulingStepsStatistic.numberOfClosedScheduling / statistic.schedulingStepsStatistic.numberOfSteps).toFixed(2) * 100 + ' %</td></tr>'
		+ '</table > ';
	
	$('div#view_statistic').append(table1);
	$('div#view_statistic').append(table2);
	$('div#view_statistic').append(table3);
	$('div#view_statistic').append(table4);
	$('div#view_statistic').append(table5);
	$('div#view_statistic').append(table6);
};