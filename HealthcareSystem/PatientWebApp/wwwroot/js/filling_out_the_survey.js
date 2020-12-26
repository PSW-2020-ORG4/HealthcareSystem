var params = (new URL(window.location.href)).searchParams;
var id = params.get("id");

$(document).ready(function () {
	checkUserRole("Patient");

	$.ajax({
		url: '/api/examination/' + id,
		type: "GET",
		headers: {
			'Authorization': 'Bearer ' + window.localStorage.getItem('token')
		},
		dataType: 'json',
		processData: false,
		contentType: false,
		success: function (examination) {
			$('#doctor_name_and_surname').append(' ' + examination.doctorName + ' ' + examination.doctorSurname);

			$('#survey_form').submit(function (event) {
				$('#survey_form').find(":submit").prop('disabled', true);
				$('#loading').show();
				event.preventDefault();

				var behaviorOfDoctor = parseInt($('input[name=gradeBehaviorOfDoctor]:checked').val());
				var doctorProfessionalism = parseInt($('input[name=gradeProfessionalismDoctor]:checked').val());
				var gettingAdviceByDoctor = parseInt($('input[name=gradeGettingAdviceDoctor]:checked').val());
				var availabilityOfDoctor = parseInt($('input[name=gradeAvailabilityOfDoctor]:checked').val());

				var behaviorOfMedicalStaff = parseInt($('input[name=gradeBehaviorOfMedicalStaff]:checked').val());
				var medicalStaffProfessionalism = parseInt($('input[name=gradeProfessionalismMedicalStaff]:checked').val());
				var gettingAdviceByMedicalStaff = parseInt($('input[name=gradeGettingAdviceMedicalStaff]:checked').val());
				var easeInObtainingFollowUpInformation = parseInt($('input[name=gradeFollowUpInformationFromMedicalStaff]:checked').val());

				var nursing = parseInt($('input[name=gradeNursing]:checked').val());
				var cleanliness = parseInt($('input[name=gradeCleanliness]:checked').val());
				var overallRating = parseInt($('input[name=gradeOverallRating]:checked').val());
				var satisfiedWithDrugAndInstrument = parseInt($('input[name=gradeSatisfiedWithDrugAndInstrument]:checked').val());

				var newSurvey = {
					"BehaviorOfDoctor": behaviorOfDoctor,
					"DoctorProfessionalism": doctorProfessionalism,
					"GettingAdviceByDoctor": gettingAdviceByDoctor,
					"AvailabilityOfDoctor": availabilityOfDoctor,
					"BehaviorOfMedicalStaff": behaviorOfMedicalStaff,
					"MedicalStaffProfessionalism": medicalStaffProfessionalism,
					"GettingAdviceByMedicalStaff": gettingAdviceByMedicalStaff,
					"EaseInObtainingFollowUpInformation": easeInObtainingFollowUpInformation,
					"Nursing": nursing,
					"Cleanliness": cleanliness,
					"OverallRating": overallRating,
					"SatisfiedWithDrugAndInstrument": satisfiedWithDrugAndInstrument,
					"ExaminationId": examination.id
				};

				$.ajax({
					url: "/api/survey",
					type: 'POST',
					contentType: 'application/json',
					headers: {
						'Authorization': 'Bearer ' + window.localStorage.getItem('token')
					},
					data: JSON.stringify(newSurvey),
					success: function () {
						let alert = $('<div class="alert alert-success alert-dismissible fade show m-1" role="alert">Your response has been recored. Thank you for filling out the survey!'
							+ '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >')
						$('#loading').hide();
						$('#survey_form').find(":submit").prop('disabled', false);
						$('#alert').prepend(alert);
					},
					error: function (jqXHR) {
						let alert = $('<div class="alert alert-danger alert-dismissible fade show m-1" role="alert">Survey submission failed.'
							+ '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >')
						$('#loading').hide();
						$('#survey_form').find(":submit").prop('disabled', false);
						$('#alert').prepend(alert);
					}
				});
			});

		},
		error: function () {
			let alert = $('<div class="alert alert-danger m-1" role="alert">Error fetching data.</div >')
			$('#alert').prepend(alert);
		}
	});
});