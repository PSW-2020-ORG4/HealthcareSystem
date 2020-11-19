﻿$(document).ready(function () {

	$('#image_row').empty();

	/* Display the image on the html page */
	var chosen_image = [];
	if (window.File && window.FileList && window.FileReader) {

		$("#add_image").on("change", function (e) {

			var images = e.target.files, filesLength = images.length;
			var image = images[0];
			chosen_image.push(image);
			var fileReader = new FileReader();
			fileReader.onload = (function (e) {
				var image_div = $("<div class=\"col-md-3\"><span class=\"pip\">" +
					"<img class=\"imageThumb\" style='width:100%; height:90%; margin-top:10px;' src=\"" + e.target.result + "\" title=\"" + image.name + "\"/>"
					+ "</span></div>");
				$('#image_row').append(image_div);
			});
			fileReader.readAsDataURL(image);
		});
	} else {
		alert("Your browser doesn't support chosen file")
	}

});