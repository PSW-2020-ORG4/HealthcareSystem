$(document).ready(function () {

	$('#image_row').empty();

	//choose profile image
	var chosen_image = [];
	if (window.File && window.FileList && window.FileReader) {

		$("#add_image").on("change", function (e) {

			var files = e.target.files,
				filesLength = files.length;

			for (var i = 0; i < filesLength; i++) {
				var f = files[i];
				chosen_image.push(f);
				var fileReader = new FileReader();
				fileReader.onload = (function (e) {
					var column = $("<div  class=\"col-md-3\"><span class=\"pip\">" +
						"<img class=\"imageThumb\" style='width:100%; height:90%; margin-top:10px;' src=\"" + e.target.result + "\" title=\"" + f.name + "\"/>"
						+ "</span></div>");
					$('#image_row').append(column);


				});
				fileReader.readAsDataURL(f);
			}
		});
	} else {
		alert("Your browser doesn't support to File API")
	}

});
