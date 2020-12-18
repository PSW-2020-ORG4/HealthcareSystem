$(document).ready(function () {

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

	$('#dateFrom').attr('min', minDate);
	$('#dateTo').attr('min', minDate);

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

	$('#btn_close').click(function () {
		location.reload();
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


function findRecommendedAppointments() {

	let doctorJmbg = $('#doctors option:selected').val();
	let earliestDateTime = $('#dateFrom').val();
	let latestDateTime = $('#dateTo').val();
	let priority = parseInt($('input[name=priority]:checked').val());

	if (!earliestDateTime || !latestDateTime) {
		alert("Pick a date!");
		return;
	}

	let specialtyId = parseInt($('#specialties option:selected').val());

	var initialParameters= {
		"PatientCardId": 2,
		"DoctorJmbg": doctorJmbg,
		"RequiredEquipmentTypes": [],
		"EarliestDateTime": earliestDateTime,
		"LatestDateTime": latestDateTime
	};

	var newData = {
		"SpecialtyId": specialtyId,
		"Priority": priority,
		"InitialParameters": initialParameters
	};

	var i = 0;
	$.ajax({
		url: "/api/appointment/priority-search",
		type: 'POST',
		contentType: 'application/json',
		data: JSON.stringify(newData),
		success: function (appointments) {
			newAppointments = appointments;
			if (appointments.length == 0) {
				document.getElementById('div_appointments').innerHTML = "";
				$('#div_appointments').append('<p> There are no free appointment </p>');
            }

			let doctorNameAndSurname = "";		
			for (let a of appointments) {
				if (parseInt($('input[name=priority]:checked').val())) {
					doctorNameAndSurname = a.doctorName + ' ' + a.doctorSurname;
				}
				let appointment = $('<option value="' + i + '">' + a.dateAndTime + ',  dr ' + doctorNameAndSurname + '</option>');
				$('#free_appointments').append(appointment);
				i = i + 1;
			}
		},
		error: function (jqXHR) {
			console.log("Error getting appointments");
			alert(jqXHR.responseText);
		}
	});

	document.getElementById("div_appointments").style.display = "initial";
}



function scheduleExamination() {

	let doctorJmbg = $('#doctors option:selected').val();
	let a = $('#free_appointments option:selected').val();

	var appointment = newAppointments[a];
	var newData = {
		"Type": appointment.type,
		"DateAndTime": appointment.dateAndTime,
		"DoctorJmbg": doctorJmbg,
		"IdRoom": appointment.idRoom,
		"Anamnesis": "",
		"PatientCardId": appointment.patientCardId,
		"PatientJmbg": "1309998775018",
		"ExaminationStatus": 0,
		"IsSurveyCompleted": false
	};

	$.ajax({
		url: "/api/examination",
		type: 'POST',
		contentType: 'application/json',
		data: JSON.stringify(newData),
		success: function () {
			setTimeout(function () {
				window.location.href = 'appointment_recommendation.html';
			}, 2000);
		},
		error: function (jqXHR) {
			alert(jqXHR.responseText);
		}
	});
};
