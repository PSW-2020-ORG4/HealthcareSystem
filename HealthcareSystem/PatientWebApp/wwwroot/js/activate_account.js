$(document).ready(function () {
    $("#activate").click(function (e) {
        e.preventDefault();

        let url_split = window.location.href.split("?")[1];

        var jmbg = url_split.split("=")[1];

        $.ajax({
            type: "PUT",
            url: "/api/patient/activate/" + jmbg,
            success: function () {

                setTimeout(function () {
                    window.location.href = 'patients_home_page.html';
                }, 500);


            },
            error: function (jqXHR) {

                alert(jqXHR.responseText);
            }
        });
    });
});