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
				$('#loading').remove();
				$('div#view_feedbacks').append(alert);
			}
			else {
				for (let i = 0; i < data.length; i++) {
					addCommentTable(data[i]);
				}
				$('#loading').remove();
			}
		},
		error: function () {
			let alert = $('<div class="alert alert-danger m-4" role="alert">Error fetching data.</div >')
			$('#loading').remove();
			$('div#view_feedbacks').prepend(alert);
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
			+ '<div class="card-body"><blockquote class="blockquote mb-0"><p>'
			+ feedback.comment + ' </p>'
			+ '<footer class="blockquote-footer text-info"><cite>'
			+ nameAndSurname + '</cite>'
			+ '<button type="button" class="btn btn-success float-right" id="'
			+ feedback.id
			+ '" onclick="approveComment(this.id)">Publish</button>'
			+ '</footer ></blockquote ></div >'
			+ '<div class="card-footer bg-transpartent border-top-0" id="a' + feedback.id + '">' 
			+ '</div ></div ></div ></div >');

		$('div#view_feedbacks').append(new_feedback);
	}
	else {
		let new_feedback = $('<div class="row"><div class="col p-4"><div class="card"><div class="card-header bg-info text-white">'
			+ feedback.sendingDate
			+ '</div>'
			+ '<div class="card-body"><blockquote class="blockquote mb-0"><p>'
			+ feedback.comment + ' </p>'
			+ '<footer class="blockquote-footer text-info"><cite>'
			+ nameAndSurname + '</cite></footer></blockquote></div></div></div></div>');

		$('div#view_feedbacks').append(new_feedback);
	}
}

function approveComment(feedbackId) {
	let loading = $('<div class="alert alert-info m-1" role="alert">Publishing...</div >')
	$('#' + feedbackId).prop("disabled", true);
	$('#a' + feedbackId).prepend(loading);

	$.ajax({
		type: "PUT",
		url: "/api/feedback/" + feedbackId,
		success: function () {
			let alert = $('<div class="alert alert-success m-1" role="alert">Feedback successfully published.</div >')
			$('#' + feedbackId).remove();
			$('#a' + feedbackId).empty();
			$('#a' + feedbackId).prepend(alert);
		},
		error: function (jqXHR) {
			let alert = $('<div class="alert alert-danger alert-dismissible fade show m-1" role="alert">Publishing was not successful.'
				+ '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >')
			$('#a' + feedbackId).empty();
			$('#' + feedbackId).prop("disabled", false);
			$('#a' + feedbackId).prepend(alert);
		}
	});
}