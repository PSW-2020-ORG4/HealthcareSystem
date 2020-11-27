$(document).ready(function () {

	$.ajax({
		url: "/api/feedback/unpublished-feedbacks",
		type: "GET",
		dataType: 'json',
		processData: false,
		contentType: false,
		success: function (data) {

			if (data.length == 0) {
				$('#title-active').text("There is no unpublished feedback");
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

	let divElement = $('<div class="border_comment"><table class="table_comment">'
		+ ' <tr> <th style="width:250px;"><p style="margin-left:50px;">Patient:</p></th><td>' + nameAndSurname + '</td></tr > '
		+ ' <tr> <th style="width:250px;"><p style="margin-left:50px;">Date:</p></th><td>' + feedback.sendingDate + '</td></tr > '
		+ ' <tr> <th style="width:250px;"><p  style="margin-left:50px;">Comment:</p></th><td><p style="margin-right:10px;"><i class="fas fa-quote-left pr-2"></i> ' + feedback.comment + '<p/></td></tr> '
		+ ' </table ></div > ');

	let trElement = $('<tr> <td style="width:250px;"><span></span></td> <td style = "width:250px;"><span></span></td> </tr>');

	if (feedback.isAllowedToPublish) {
		let tdElement = $('<td> <button class="submit_btn" style="margin-bottom:10px;" id="' + feedback.id + '" onclick="approveComment(this.id)"> Publish </button> </td>');
		trElement.append(tdElement);
	}

	divElement.append(trElement);
	$('div#div_comments').append(divElement);
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