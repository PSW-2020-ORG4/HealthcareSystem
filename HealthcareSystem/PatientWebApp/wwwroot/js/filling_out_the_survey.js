$(document).ready(function () {

	$('#survey_form').submit(function (event) {

		event.preventDefault();

		var jmbgDoctor = "111";

		var behaviorOfDoctor = parseInt($('input[name=gradeBehaviorOfDoctor]:checked').val());
		var doctorProfessionalism = parseInt($('input[name=gradeProfessionalismDoctor]:checked').val());
		var gettingAdviceByDoctor = parseInt($('input[name=gradeGettingAdviceDoctor]:checked').val());
		var availabilityOfDoctor = parseInt($('input[name=gradeAvailabilityOfDoctor]:checked').val());

		var behaviorOfMedicalStaff = parseInt($('input[name=gradeBehaviorOfMedicalStaff]:checked').val());
		var medicalStaffProfessionalism = parseInt($('input[name=gradeProfessionalismMedicalStaff]:checked').val());
		var gettingAdviceByMedicalStaff = parseInt($('input[name=gradeGettingAdviceMedicalStaff]:checked').val());
		var easeInObtainingFollowupInformationAndCare = parseInt($('input[name=gradeFollowUpInformationFromMedicalStaff]:checked').val());

		var nursing = parseInt($('input[name=gradeNursing]:checked').val());
		var cleanliness = parseInt($('input[name=gradeCleanliness]:checked').val());
		var overallRating = parseInt($('input[name=gradeOverallRating]:checked').val());
		var satisfiedWithDrugAndInstrument = parseInt($('input[name=gradeSatisfiedWithDrugAndInstrument]:checked').val());

		var newSurvey = {
			"DoctorJmbg": jmbgDoctor,
			"BehaviorOfDoctor": behaviorOfDoctor,
			"DoctorProfessionalism": doctorProfessionalism,
			"GettingAdviceByDoctor": gettingAdviceByDoctor,
			"AvailabilityOfDoctor": availabilityOfDoctor,
			"BehaviorOfMedicalStaff": behaviorOfMedicalStaff,
			"MedicalStaffProfessionalism": medicalStaffProfessionalism,
			"GettingAdviceByMedicalStaff": gettingAdviceByMedicalStaff,
			"EaseInObtainingFollowupInformationAndCare": easeInObtainingFollowupInformationAndCare,
			"Nursing": nursing,
			"Cleanliness": cleanliness,
			"OverallRating": overallRating,
			"SatisfiedWithDrugAndInstrument": satisfiedWithDrugAndInstrument
		};

		$.ajax({
			url: "/api/survey",
			type: 'POST',
			contentType: 'application/json',
			data: JSON.stringify(newSurvey),
			success: function () {
				alert("success");
				setTimeout(function () {
					window.location.href = 'patients_home_page.html';
				}, 2000);
			},
			error: function (jqXHR) {
				alert(jqXHR.responseText);
			}
		});

	});
});
