$(document).ready(function () {

	checkUserRole("Patient");

	$('#add_feedback_form').submit(function (event) {
		$('#loading').show();
		$('#add_feedback_form').find(":submit").prop('disabled', true);
		event.preventDefault();

		var msg = $('#text_area_id').val();
		var allowed = true;
		var anonymous = false;

		if (!msg) {
			$('#loading').hide();
			let alert = $('<div class="alert alert-danger alert-dismissible fade show m-1" role="alert">Feedback cannot be empty.'
				+ '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >')
			$('#alert').prepend(alert);
			$('#add_feedback_form').find(":submit").prop('disabled', false);
			return;
		}
		if ($('#yes_anonymous').is(":checked")) {
			anonymous = true;
		}
		if ($('#is_allowed').is(":checked")) {
			allowed = false;
		}

		var newData = {
			"Comment": msg,
			"IsAnonymous": anonymous,
			"CommentatorJmbg": null,
			"IsAllowedToPublish": allowed
		};
		$.ajax({
			url: "/api/feedback",
			type: 'POST',
			contentType: 'application/json',
			headers: {
				'Authorization': 'Bearer ' + window.localStorage.getItem('token')
			},
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
				let alert = $('<div class="alert alert-danger alert-dismissible fade show m-1" role="alert">' + jqXHR.responseJSON
					+ '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >')
				$('#loading').hide();
				$('#add_feedback_form').find(":submit").prop('disabled', false);
				$('#alert').prepend(alert);
			}
		});
	});
});