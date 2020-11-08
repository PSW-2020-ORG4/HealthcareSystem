$(document).ready(function () {

	$.ajax({
		url: "/api/feedback/unpublished-feedbacks",
		type: "GET",
		dataType: 'json',
		processData: false,
		contentType: false,
		success: function (data) {
			for (let i = 0; i < data.length; i++) {
				addCommentTable(data[i]);
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

	let divElement = $('<div class="border_comment"><table class="table_comment">'
		+ ' <tr> <th style="width:250px;"><p style="margin-left:50px;">Patient:</p></th><td>' + nameAndSurname + '</td></tr > '
		+ ' <tr><th><p  style="margin-left:50px;">Comment:</p></th><td>' + feedback.comment + '</td></tr> '
		+ ' </table ></div > ');

	let trElement = $('<tr> <td style="width:250px;"><span></span></td> <td style = "width:250px;"><span></span></td> </tr>');

	if (feedback.isAllowedToPublish) {
		let tdElement = $('<td> <button style="color:white;background-color:#33adff;width:110px;margin-bottom:-60px;height:38px;" id="' + feedback.id + '" onclick="approveComment(this.id)"> Approve </button> </td>');
		trElement.append(tdElement);
	}

	divElement.append(trElement);
	$('div#div_comments').append(divElement);

}

function approveComment(feedbackId) {

	$.ajax({
		type: "PUT",
		url: "/api/feedback/" + feedbackId,
		data: feedbackId,
		success: function () {
			console.log('You have successfully approved a feedback!');

			setTimeout(function () {
				location.reload();
			}, 500);
		},
		error: function () {
			console.log("error approving comment");
		}
	});

}