$(document).ready(function () {	

	/*Login patient on submit*/
	$('form#logging_in').submit(function (event) {

		event.preventDefault();
		$('#div_alert').empty();

		let username = $('#username').val();
		let password = $('#password').val();

		var userCredentialsDTO = {
			"Username": username,
			"Password": password,
		};

		if ((username == "") || (password == "")) {
			return;
		}
		else {
			$("form#logging_in").removeClass("unsuccessful");
			$.ajax({
				url: "/api/user/authenticate",
				type: 'POST',
				contentType: 'application/json',
				data: JSON.stringify(userCredentialsDTO),
				success: function (tokenUser) {
					localStorage.setItem('token', tokenUser);

					const jwt = localStorage.getItem('token');
					var decoded = parseJwt(jwt);
					console.log(decoded);
					
					if (decoded.role == "Patient") {
						window.location.href = "patients_home_page.html";
					}
					if (decoded.role == "Admin") {
						window.location.href = "admins_home_page.html";
					}
				},
				error: function (jqXHR) {
					let alert = $('<div class="alert alert-danger alert-dismissible fade show m-1" role="alert"> Incorrect username and password'
						+ '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >')
					$('#div_alert').append(alert);
					return;
				}
			});
		}
	});
});


function parseJwt(token) {
	var base64Url = token.split('.')[1];
	var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
	var jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
		return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
	}).join(''));

	return JSON.parse(jsonPayload);
};
