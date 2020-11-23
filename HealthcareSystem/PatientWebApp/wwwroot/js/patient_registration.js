$(document).ready(function () {

	$('#image_row').empty();

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
		error: function () {
			console.log("Error getting countries from database");
        }
	});

	/*Get all cities from database*/
	$('select#countries').on('change', function () {

		var id = $(this).children(":selected").attr("id");

		$.ajax({
			url: "/api/city/" + id,
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
			error: function () {
				console.log("Error getting cities from database");
			}
		});


	});

	/* Display the image on the html page */
	var chosen_image = [];
	if (window.File && window.FileList && window.FileReader) {

		$("#add_image").on("change", function (e) {

			var images = e.target.files, filesLength = images.length;
			var image = images[0];
			chosen_image.push(image);
			var fileReader = new FileReader();
			fileReader.onload = (function (event) {
				var image_div = $("<div class=\"col-md-3\"><span class=\"pip\">" +
					"<img class=\"imageThumb\" style='width:100%; height:90%; margin-top:10px;' src=\"" + event.target.result + "\" title=\"" + image.name + "\"/>"
					+ "</span></div>");
				$('#image_row').append(image_div);
			});
			fileReader.readAsDataURL(image);
		});
	} else {
		alert("Your browser doesn't support chosen file")
	}

	/*Registrate patient on submit*/
	$('form#registration').submit(function (event) {

		event.preventDefault();

		let name = $('#name').val();
		let surname = $('#name').val();
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

		if (hasInsurance == true && (lbo == null || lbo == "")) {
			alert("If You have medical insurance, fill personal number of the insured.");
			return;
		}

		if (password != passwordRepeat) {
			alert("Password and confirm password don't match.");
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


		$.ajax({
			url: "/api/patient",
			type: 'POST',
			contentType: 'application/json',
			data: JSON.stringify(newPatient),
			success: function () {

				alert('You registrate successfully! Check Your email inbox and activate profile.');

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

function addCountryInComboBox(country) {
	let country_option = $('<option id="' + country.id + '" value="' + country.name +'">' + country.name + '</option>');
	$('select#countries').append(country_option);
};

function addCityInComboBox(city) {
	let city_option = $('<option id="' + city.zipCode + '" value = "' + city.name +'">' + city.name + '</option>');
	$('select#cities').append(city_option);
};
