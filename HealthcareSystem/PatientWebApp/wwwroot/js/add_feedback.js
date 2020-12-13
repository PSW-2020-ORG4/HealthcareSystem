$(document).ready(function () {

	$('#add_feedback_form').submit(function (event) {

		event.preventDefault();

		var msg = $('#text_area_id').val();
		var jmbg = "";
		var name = "";
		var surname = "";
		var allowed = true;

		if (!msg) {
			$('#ap_error_msg').text('Enter a feedback message!');
			$('#ap_error_msg').attr("hidden", false);

			setTimeout(function () {
				$('#ap_error_msg').attr("hidden", true);
			}, 2000);

			return;
		}

		//I take the certain patient from the database, otherwise the currently logged in patient will be taken
		$.ajax({
			url: "/api/patient/1309998755018",
			type: 'GET',
			dataType: 'json',
			processData: false,
			contentType: false,
			success: function (patient) {

				jmbg = patient.jmbg;
				name = patient.name;
				surname = patient.surname;

				if ($('#yes_anonymous').is(":checked")) {
					jmbg = null;
					name = "";
					surname = "";
				}

				if ($('#is_allowed').is(":checked")) {
					allowed = false;
				}

				var newData = {
					"Comment": msg,
					"CommentatorJmbg": jmbg,
					"CommentatorName": name,
					"CommentatorSurname": surname,
					"IsAllowedToPublish": allowed
				};

				$.ajax({
					url: "/api/feedback",
					type: 'POST',
					contentType: 'application/json',
					data: JSON.stringify(newData),
					success: function () {

						$('#ap_success_msg').text('You have successfully left a feedback!');
						$('#ap_success_msg').attr("hidden", false);

						setTimeout(function () {
							window.location.href = 'patients_home_page.html';
						}, 2000);

					},
					error: function (jqXHR) {

						alert(jqXHR.responseText);
					}
				});

			},
			error: function () {

				console.log("Error getting patients");
			}
		});

	});

});