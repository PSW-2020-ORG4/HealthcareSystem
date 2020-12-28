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
            + '<div class="carousel-caption text-dark rounded"><h1 class="display-4" style="font-size:40px">APOTEKA ' + action.pharmacyName + '</h1><h1 class="display-4" style="font-size:40px">' + action.subject + '</h1><h3 class="lead"><b>' + action.message + '</b></h3></div></div>');

        $('div#action_benefit_div').append(text_image);
    }
    if (i == 1) {
        text_image = $('<div class="carousel-item" style="background-image: url(/images/5.jpg);">'
            + '< img class= "d-block" src = "/images/5.jpg" alt = "First slide" >'
            + '<div class="carousel-caption text-dark rounded"><h1 class="display-4" style="font-size:40px">APOTEKA ' + action.pharmacyName + '</h1><h1 class="display-4" style="font-size:40px">' + action.subject + '</h1><h3 class="lead"><b>' + action.message + '</b></h3></div></div>');

        $('div#action_benefit_div').append(text_image);
    }
    if (i == 2) {
        text_image = $('<div class="carousel-item" style="background-image: url(/images/6.jpg);">'
            + '< img class= "d-block" src = "/images/6.jpg" alt = "First slide" >'
            + '<div class="carousel-caption text-dark rounded"><h1 class="display-4" style="font-size:40px">APOTEKA ' + action.pharmacyName + '</h1><h1 class="display-4" style="font-size:40px">' + action.subject + '</h1><h3 class="lead"><b>' + action.message + '</b></h3></div></div>');

        $('div#action_benefit_div').append(text_image);
    }
}