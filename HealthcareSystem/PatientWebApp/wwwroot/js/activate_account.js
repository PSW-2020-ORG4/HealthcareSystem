$(document).ready(function () {

        let url_split = window.location.href.split("?")[1];

        var encryptedJmbg = url_split.split("=")[1];

        jmbg = encryptJmbg(encryptedJmbg);

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

function encryptJmbg(jmbg) {

    var chars = jmbg.split('');
    var decryptedJmbg = '';
    for (let c of chars) {
        if (c == 'A') {
            decryptedJmbg += '0';
        } else if (c == 'p') {
            decryptedJmbg += '1';
        } else if (c == 't') {
            decryptedJmbg += '2';
        } else if (c == 'o') {
            decryptedJmbg += '3';
        } else if (c == 'g') {
            decryptedJmbg += '4';
        } else if (c == 'e') {
            decryptedJmbg += '5';
        } else if (c == 'x') {
            decryptedJmbg += '6';
        } else if (c == 'w') {
            decryptedJmbg += '7';
        } else if (c == 'y') {
            decryptedJmbg += '8';
        } else if (c == 'K') {
            decryptedJmbg += '9';
        }
    }
    return decryptedJmbg;
}