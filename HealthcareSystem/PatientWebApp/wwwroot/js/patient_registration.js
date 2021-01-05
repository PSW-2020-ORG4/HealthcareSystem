$(document).ready(function () {
	var dtToday = new Date();

	var month = dtToday.getMonth() + 1;
	var day = dtToday.getDate();
	var year = dtToday.getFullYear();
	if (month < 10)
		month = '0' + month.toString();
	if (day < 10)
		day = '0' + day.toString();

	var maxDate = year + '-' + month + '-' + day;

	$('#dateOfBirth').attr('max', maxDate);
	$('#image_row').empty();
	$('#confirm_pass_validation').attr("hidden", true);

	/* Display the image on the html page */
	var chosen_image = [];
	if (window.File && window.FileList && window.FileReader) {

		$("#file").on("change", function (e) {

			var images = e.target.files;
			var image = images[0];
			chosen_image.push(image);
			var fileReader = new FileReader();
			fileReader.onload = (function (event) {
				$('#profile_image').attr('src', event.target.result);
				$('#profile_image').show();
			});
			fileReader.readAsDataURL(image);
		});
	} else {
		alert("Your browser doesn't support chosen file")
	}

	/*Get all countries from database*/
	$.ajax({
		url: "/api/country",
		type: 'GET',
		dataType: 'json',
		processData: false,
		contentType: false,
		success: function (countries) {

			for (let i = 0; i < countries.length; i++) {
				addCountryInComboBox(countries[i]);
			}
		},
		error: function (jqXHR) {
			let alert = $('<div class="alert alert-danger alert-dismissible fade show m-1" role="alert">'
				+ jqXHR.responseJSON + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >')
			$('#div_alert').append(alert);
			return;
		}
	});

	/*Get all cities from database*/
	$('select#countries').on('change', function () {

		var id = $(this).children(":selected").attr("id");

		$.ajax({
			url: "/api/city/country/" + id,
			type: 'GET',
			dataType: 'json',
			processData: false,
			contentType: false,
			success: function (cities) {

				$('select#cities').empty();
				$('select#cities').append('<option value="" disabled selected hidden>Choose city</option>');
				for (let i = 0; i < cities.length; i++) {
					addCityInComboBox(cities[i]);
				}
			},
			error: function (jqXHR) {
				let alert = $('<div class="alert alert-danger alert-dismissible fade show m-1" role="alert">'
					+ jqXHR.responseJSON + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >')
				$('#div_alert').append(alert);
				return;
			}
		});

	});

	/*Select combo box about medical insurance*/
	$("#insurance").change(function () {
		if ($('#insurance').val() == 1) {
			$('#enter_lbo').text('Insurance number *');
			$('#lbo').attr('required', true);
			$('#insurance_validation').attr('hidden', false);
		}
		else {
			$('#enter_lbo').text('Insurance number');
			$('#lbo').attr('required', false);
			$('#insurance_validation').attr('hidden', true);
		}

	});

	/*Registrate patient on submit*/
	$('form#registration').submit(function (event) {

		event.preventDefault();
		$('#div_alert').empty();

		let name = $('#name').val();
		let surname = $('#surname').val();
		let jmbg = $('#jmbg').val();
		let dateOfBirth = $('#dateOfBirth').val();
		let gender = $("#gender option:selected").val();
		let phone = $('#phone').val();
		let countryId = $("#countries option:selected").attr("id");
		let countryName = $("#countries").val();
		let cityZipCode = $("#cities option:selected").attr("id");
		let cityName = $("#cities").val();
		let address = $('#address').val();
		let bloodType = $('#blood').val();
		let rhFactor = $('#rh').val();
		let hasInsurance = $('#insurance').val();
		let lbo = $('#lbo').val();
		let allergies = $('#allergies').val();
		let medHistory = $('#history').val();
		let email = $('#email').val();
		let password = $('#password').val();
		let passwordRepeat = $('#rpt_password').val();
		let file = $('#file').val()

		if (file == null || file == '') {
			let alert = $('<div class="alert alert-danger alert-dismissible fade show m-1" role="alert">Please, upload a profile image.'
				+ '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >')
			$('#div_alert').append(alert);
		}

		if (bloodType == null) {
			bloodType = -1;
		}
		if (rhFactor == null) {
			rhFactor = -1;
		}
		if (hasInsurance == 0) {
			hasInsurance = false;
		} else {
			hasInsurance = true;
		}

		if (password != passwordRepeat) {
			let alert = $('<div class="alert alert-danger alert-dismissible fade show m-1" role="alert">Password and confirm password don\'t match.'
				+ '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >')
			$('#div_alert').append(alert);
			return;
		}

		if (!$.isNumeric(jmbg) || jmbg.toString().length < 13) {
			let alert = $('<div class="alert alert-danger alert-dismissible fade show m-1" role="alert">JMBG must have 13 digits.'
				+ '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >')
			$('#div_alert').append(alert);
			return;
		}

		var newPatient = {
			"Jmbg": jmbg,
			"Name": name,
			"Surname": surname,
			"Gender": parseInt(gender),
			"DateOfBirth": dateOfBirth,
			"Phone": phone,
			"CountryId": parseInt(countryId),
			"CountryName": countryName,
			"CityZipCode": parseInt(cityZipCode),
			"CityName": cityName,
			"HomeAddress": address,
			"Email": email,
			"Password": password,
			"BloodType": parseInt(bloodType),
			"RhFactor": parseInt(rhFactor),
			"HasInsurance": hasInsurance,
			"Lbo": lbo,
			"Allergies": allergies,
			"MedicalHistory": medHistory
		};

		if ($("form#registration").hasClass("unsuccessful")) {
			return;
		}
		else {
			$("form#registration").removeClass("unsuccessful");
			$.ajax({
				url: "/api/patient",
				type: 'POST',
				contentType: 'application/json',
				data: JSON.stringify(newPatient),
				success: function () {
					var actionPath = '/api/patient/upload?patientJmbg=' + jmbg;
					$('#form_image').attr('action', actionPath)
					$('#form_image').submit();
				},
				error: function (jqXHR) {
					let alert = $('<div class="alert alert-danger alert-dismissible fade show m-1" role="alert">'
						+ jqXHR.responseText + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >')
					$('#div_alert').append(alert);
					return;
				}
			});
		}

	});

});

function addCountryInComboBox(country) {
	let country_option = $('<option id="' + country.id + '" value="' + country.name + '">' + country.name + '</option>');
	$('select#countries').append(country_option);
};

function addCityInComboBox(city) {
	let city_option = $('<option id="' + city.id + '" value = "' + city.name + '">' + city.name + '</option>');
	$('select#cities').append(city_option);
};