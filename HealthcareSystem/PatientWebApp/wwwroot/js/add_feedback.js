$(document).ready(function () {

	$('#add_feedback_form').submit(function (event) {
		$('#loading').show();
		$('#add_feedback_form').find(":submit").prop('disabled', true);
		event.preventDefault();

		var msg = $('#text_area_id').val();
		var jmbg = "";
		var name = "";
		var surname = "";
		var allowed = true;

		if (!msg) {
			$('#loading').hide();
			let alert = $('<div class="alert alert-danger alert-dismissible fade show m-1" role="alert">Feedback cannot be empty.'
				+ '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >')
			$('#alert').prepend(alert);
			$('#add_feedback_form').find(":submit").prop('disabled', false);
			return;
		}

		//I take the certain patient from the database, otherwise the currently logged in patient will be taken
		$.ajax({
			url: "/api/patient/1309998775018",
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
						$('#text_area_id').val(null);
						let alert = $('<div class="alert alert-success alert-dismissible fade show m-1" role="alert">You have successfuly left a feedback.'
							+ '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >')
						$('#loading').hide();
						$('#add_feedback_form').find(":submit").prop('disabled', false);
						$('#alert').prepend(alert);

					},
					error: function (jqXHR) {
						let alert = $('<div class="alert alert-danger alert-dismissible fade show m-1" role="alert">Leaving feedback was not successful.'
							+ '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >')
						$('#loading').hide();
						$('#add_feedback_form').find(":submit").prop('disabled', false);
						$('#alert').prepend(alert);
					}
				});

			},
			error: function () {
				let alert = $('<div class="alert alert-danger alert-dismissible fade show m-1" role="alert">Error getting patient.'
					+ '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >')
				$('#loading').hide();
				$('#add_feedback_form').find(":submit").prop('disabled', false);
				$('#alert').prepend(alert);
			}
		});

	});

});