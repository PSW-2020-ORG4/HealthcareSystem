﻿$(document).ready(function () {

	var newAppointments = [];

	var dtToday = new Date();

	var month = dtToday.getMonth() + 1;
	var day = dtToday.getDate() + 1;
	var year = dtToday.getFullYear();
	if (month < 10)
		month = '0' + month.toString();
	if (day < 10)
		day = '0' + day.toString();

	var minDate = year + '-' + month + '-' + day;

	$('#dateOfExam').attr('min', minDate);

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
				for (let specialty of specialties) {
					let specialtyOption = $('<option value="' + specialty.id + '">' + specialty.name + '</option>');
					$('select#specialties').append(specialtyOption);
				}
				let select_specialty = $('#specialties option:selected').val();

				$.ajax({
					url: '/api/doctor/doctor-specialty/' + select_specialty,
					type: 'GET',
					dataType: 'json',
					processData: false,
					contentType: false,
					success: function (doctorsSpecialtes) {
						for (let doctorSpecialist of doctorsSpecialtes) {
							let doctorJmbg = doctorSpecialist.doctorJmbg;

							$.ajax({
								url: '/api/doctor/' + doctorJmbg,
								type: 'GET',
								dataType: 'json',
								processData: false,
								contentType: false,
								success: function (doctor) {
									let doctorName = $('<option value="' + doctor.jmbg + '">' + doctor.name + ' ' + doctor.surname + '</option>');
									$('#doctors').append(doctorName);
								},
								error: function () {
									console.log("Error getting doctor")
								}
							});
						}
					},
					error: function () {
						console.log("Error getting specialist doctors")
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

	var select = document.getElementById("doctors");
	var length = select.options.length;
	for (i = length - 1; i >= 0; i--) {
		select.options[i] = null;
	}

	let select_specialty = $('#specialties option:selected').val();

	$.ajax({
		url: '/api/doctor/doctor-specialty/' + select_specialty,
		type: 'GET',
		dataType: 'json',
		processData: false,
		contentType: false,
		success: function (specialistDoctors) {
			for (let specialistDoctor of specialistDoctors) {
				let doctorJmbg = specialistDoctor.doctorJmbg;

				$.ajax({
					url: '/api/doctor/' + doctorJmbg,
					type: 'GET',
					dataType: 'json',
					processData: false,
					contentType: false,
					success: function (doctor) {
						let doctorName = $('<option value="' + doctor.jmbg + '">' + doctor.name + ' ' + doctor.surname + '</option>');
						$('#doctors').append(doctorName);
					},
					error: function () {
						console.log("Error getting specialist doctor")
					}
				});
			}
		},
		error: function () {
			console.log("Error getting specialist doctors")
		}
	});
}