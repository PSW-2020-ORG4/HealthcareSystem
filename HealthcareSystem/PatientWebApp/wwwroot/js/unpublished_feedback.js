$(document).ready(function () {

	let comment1 = $('<div class="border_comment">'
		+ '<table class="table_comment">'
		+ '<tr><th style="width:350px;">Patient:</th><td>Pera Peric</td></tr>'
		+ '<tr><th>Comment:</th><td>Very good!</td></tr>'
		+ '<tr><td style="width:250px;"><span></span></td><td style="width:250px;"><span></span></td>'
		+ '<td><button class="btn yes"  onclick="approveComment(this.id)"><i class="fas fa-check"></i></button/></td>'
		+ '</tr></table></div>');
	$('div#div_comments').append(comment1);

	let comment2 = $('<div class="border_comment">'
		+ '<table class="table_comment">'
		+ '<tr><th style="width:350px;">Patient:</th><td>Petra Peric</td></tr>'
		+ '<tr><th>Comment:</th><td>Bad! :( </td></tr>'
		+ '<tr><td style="width:250px;"><span></span></td><td style="width:250px;"><span></span></td>'
		+ '<td><button class="btn yes"  onclick="approveComment(this.id)"><i class="fas fa-check"></i></button/></td>'
		+ '</tr></table></div>');
	$('div#div_comments').append(comment2);
	
});
