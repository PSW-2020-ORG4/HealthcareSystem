$(document).ready(function () {


	$.ajax({
		url: "/api/doctor/all-specialty",
		type: "GET",
		dataType: 'json',
		processData: false,
		contentType: false,
		success: function (specialties) {
			console.log('success - loading specialties from database');
			if (specialties.length == 0) {
				console.log("there are no specialties");
			}
			else {
				for (let s of specialtes) {
					let specialty = $('<option value="' + s.id + '">' + s.name + '</option>');
					$('select#specialties').append(specialty);
				}
				let select_specialty = $('#specialties option:selected').val();

				$.ajax({
					url: '/api/doctor/doctor-specialty/' + select_specialty,
					type: 'GET',
					dataType: 'json',
					processData: false,
					contentType: false,
					success: function (doctorSpecialtes) {
						for (let ds of doctorSpecialtes) {

							let doctorJmbg = ds.doctorJmbg;

							$.ajax({
								url: '/api/doctor/' + doctorJmbg,
								type: 'GET',
								dataType: 'json',
								processData: false,
								contentType: false,
								success: function (doctor) {

									let doctorName = $('<option value="' + doctor.jmbg + '">' + doctor.name + ' ' + doctor.surname + '</option>');
									$('#doctor_name').append(doctorName);

								},
								error: function () {
									console.log("Error getting doctor")
								}
							});
						}
					},
					error: function () {
						console.log("Error getting doctors specialtes")
					}
				});


			}
		},
		error: function () {
			alert("error getting specialties");
			console.log('error getting specialties');
		}
	});





});


function changeSpecialty(event) {


	alert("function changeSpecialty");


	var selectElement = document.getElementById("specialty");
	var specialtyValue = selectElement.options[selectElement.selectedIndex].value;

	$.ajax({
		url: "/api/doctor/doctor-specialty/" + specialtyValue,
		type: "GET",
		dataType: 'json',
		processData: false,
		contentType: false,
		success: function (doctors) {
			console.log('success - get doctors');
			if (doctors.length == 0) {
				alert("there are no doctors");
				console.log("there are no doctors");
			}



			else {
				//document.getElementById('survey_result_Doctor').innerHTML = "";
				$('div#survey_result_Doctor').append('<p class="text-center h5 mb-4">Overall average rating: ' + overallAverageRating + ' / 5</p>');
				for (let i = 0; i < surveyResult.length; i++) {
					let divElement = addOneSurveyResult(surveyResult[i]);
					$('div#survey_result_Doctor').append(divElement);
				}
			}
		},
		error: function (jqXHR) {
			//document.getElementById('survey_result_Doctor').innerHTML = "";
			alert(jqXHR.responseText);
			console.log(jqXHR.responseText);
		}
	});
}

