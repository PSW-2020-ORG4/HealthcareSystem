$(document).ready(function () {

	$.ajax({
		url: "/api/feedback/unpublished-feedbacks",
		type: "GET",
		dataType: 'json',
		processData: false,
		contentType: false,
		success: function (data) {
			let length = data.length;
			for (let i = 0; i < length; i++) {
				addCommentTable(data[i]);
			}
		},
		error: function () {
			console.log('error getting comments');
		}
	});

});



function addCommentTable(c) {
	
	let divElement = $('<div class= "border_comment"> <table class="table_comment"> '
		+ ' <tr> <th style="width:350px;"><p style="margin-left:50px;">Patient:</p></th><td>' + c.commentatorName + ' ' + c.commentatorSurname + '</td></tr > '
		+ ' <tr><th><p  style="margin-left:50px;">Comment:</p></th><td>' + c.comment + '</td></tr> '
		+' </table ></div > ');

	let trElement = $('<tr> <td style="width:250px;"><span></span></td> <td style = "width:250px;"><span></span></td> </tr>');

	if (c.isAllowedToPublish) {
		let tdElement = $('<td> <button style="color:green;" id="' + c.id + '" onclick="approveComment(this.id)"> Approve </button> </td>');
		trElement.append(tdElement);
	}
	else {
		let tdElement = $('<td style="color:red;"> Publishing disabled </td>');
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
			$('#ap_success_msg').text('You have successfully approved a feedback!');
			$('#ap_success_msg').attr("hidden", false);

			setTimeout(function () {
				location.reload();
			}, 500);
		},
		error: function () {
			console.log("error approving comment");
		}
	});

}

