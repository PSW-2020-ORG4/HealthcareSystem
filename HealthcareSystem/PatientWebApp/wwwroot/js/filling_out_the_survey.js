$(document).ready(function () {

	var jmbg = "1309998775018";

	$.ajax({
		url: '/api/examination/' + jmbg,
		type: "GET",
		dataType: 'json',
		processData: false,
		contentType: false,
		success: function (examinations) {
			console.log('success - loading examinations from database');
			if (examinations.length == 0) {
				alert("There is no examinations for patient in the database");
				setTimeout(function () {
					window.location.href = 'patients_home_page.html';
				}, 1000);
			}
			else {
				let last_examination = examinations[examinations.length - 1];

				var jmbgDoctor = last_examination.doctorJmbg;
				$('p#doctor_name_and_surname').append(' ' + last_examination.doctorName + ' ' + last_examination.doctorSurname);

				$('#survey_form').submit(function (event) {

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
						"DoctorJmbg": jmbgDoctor,
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
						"ExaminationId" : last_examination.id
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
			}
		},
		error: function () {
			alert("error getting doctor");
			console.log('error getting doctor');
			setTimeout(function () {
				window.location.href = 'patients_home_page.html';
			}, 2000);
		}
	});

});
