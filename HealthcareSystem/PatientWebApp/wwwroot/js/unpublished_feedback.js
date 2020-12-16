$(document).ready(function () {

	$.ajax({
		url: "/api/feedback/unpublished-feedbacks",
		type: "GET",
		dataType: 'json',
		processData: false,
		contentType: false,
		success: function (data) {

			if (data.length == 0) {
				let alert = $('<div class="alert alert-info m-4" role="alert">There is no unpublished feedback.</div >')
				$('div#view_feedbacks').append(alert);
			}
			else {
				for (let i = 0; i < data.length; i++) {
					addCommentTable(data[i]);
				}
			}
		},
		error: function () {
			console.log('error getting comments');
		}
	});

});

function addCommentTable(feedback) {
	let nameAndSurname = feedback.commentatorName + ' ' + feedback.commentatorSurname;
	if (feedback.commentatorName == '') {
		nameAndSurname = 'Anonymous';
	}

	if (feedback.isAllowedToPublish) {
		let new_feedback = $('<div class="row"><div class="col p-4"><div class="card"><div class="card-header bg-info text-white">'
			+ feedback.sendingDate
			+ '</div>'
			+ '<div class="card-body bg-light"><blockquote class="blockquote mb-0"><p>'
			+ feedback.comment + ' </p>'
			+ '<footer class="blockquote-footer text-info"><cite>'
			+ nameAndSurname + '</cite>'
			+ '<button type="button" class="btn btn-success float-right" id="'
			+ feedback.id
			+ '" onclick="approveComment(this.id)">Publish</button>'
			+ '</footer ></blockquote ></div ></div ></div ></div > ');

		$('div#view_feedbacks').append(new_feedback);
	}
	else {
		let new_feedback = $('<div class="row"><div class="col p-4"><div class="card"><div class="card-header bg-info text-white">'
			+ feedback.sendingDate
			+ '</div>'
			+ '<div class="card-body bg-light"><blockquote class="blockquote mb-0"><p>'
			+ feedback.comment + ' </p>'
			+ '<footer class="blockquote-footer text-info"><cite>'
			+ nameAndSurname + '</cite></footer></blockquote></div></div></div></div>');

		$('div#view_feedbacks').append(new_feedback);
	}
}

function approveComment(feedbackId) {

	$.ajax({
		type: "PUT",
		url: "/api/feedback/" + feedbackId,
		success: function () {

			console.log('You have successfully approved a feedback!');
			setTimeout(function () {
				location.reload();
			}, 500);
		},
		error: function (jqXHR) {

			alert(jqXHR.responseText);
		}
	});
}