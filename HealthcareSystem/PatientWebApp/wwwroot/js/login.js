$(document).ready(function () {	

	/*Login patient on submit*/
	$('form#logging_in').submit(function (event) {

		event.preventDefault();
		$('#div_alert').empty();

		let email = $('#email').val();
		let password = $('#password').val();

		var userInfoDTO = {
			"Email": email,
			"Password": password
		};

		if ((email == "") || (password == "")) {
			return;
		}
		else {
			$("form#logging_in").removeClass("unsuccessful");
			$.ajax({
				url: "/api/user/authenticate",
				type: 'POST',
				contentType: 'application/json',
				data: JSON.stringify(userInfoDTO),
				success: function (token) {
					localStorage.setItem('token', token);
					redirectUser(window.localStorage.getItem('token'));
				},
				error: function (jqXHR) {
					let alert = $('<div class="alert alert-danger alert-dismissible fade show m-1" role="alert">'
						+ jqXHR.responseJSON + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >')
					$('#div_alert').append(alert);
					return;
				}
			});
		}
	});
});
