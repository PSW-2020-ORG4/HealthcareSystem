$(document).ready(function () {
    checkUserRole("Patient");
    $.ajax({
        url: '/api/action',
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + window.localStorage.getItem('token')
        },
        dataType: 'json',
        processData: false,
        contentType: false,
        success: function (data) {
            if (data.length == 0) {
            }
            else {
                for (let i = 0; i < data.length; i++) {
                    addAction(data[i], i);
                }
            }
        },
        error: function () {
            console.log('error getting actions');
        }
    });
});

function addAction(action, i) {

    let text_image; 

    if (i == 0) {
        text_image = $('<div class="carousel-item" style="background-image: url(/images/4.jpg);">'
            + '< img class= "d-block" src = "/images/4.jpg" alt = "First slide" >'
            + '<div class="carousel-caption trickcenter"><h1 style="margin-left:140px;"><b>APOTEKA ' + action.pharmacyName + '</b></h1><h1 style="margin-left:160px;"><b>' + action.subject + '</b></h1><h3 style="margin-left:290px;"><b>' + action.message + '</b><h3></div></div>');

        $('div#action_benefit_div').append(text_image);
    }
    if (i == 1) {
        text_image = $('<div class="carousel-item" style="background-image: url(/images/5.jpg);">'
            + '< img class= "d-block" src = "/images/5.jpg" alt = "First slide" >'
            + '<div class="carousel-caption trickcenter"><h1 style="margin-left:140px;"><b>APOTEKA ' + action.pharmacyName + '</b></h1><h1 style="margin-left:160px;"><b>' + action.subject + '</b></h1><h3 style="margin-left:290px;"><b>' + action.message + '</b><h3></div></div>');

        $('div#action_benefit_div').append(text_image);
    }
    if (i == 2) {
        text_image = $('<div class="carousel-item" style="background-image: url(/images/6.jpg);">'
            + '< img class= "d-block" src = "/images/6.jpg" alt = "First slide" >'
            + '<div class="carousel-caption trickcenter"><h1 style="margin-left:140px;"><b>APOTEKA ' + action.pharmacyName + '</b></h1><h1 style="margin-left:160px;"><b>' + action.subject + '</b></h1><h3 style="margin-left:290px;"><b>' + action.message + '</b><h3></div></div>');

        $('div#action_benefit_div').append(text_image);
    }
}