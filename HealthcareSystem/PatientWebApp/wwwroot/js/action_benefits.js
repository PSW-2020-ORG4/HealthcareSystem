$(document).ready(function () {

    $.ajax({
        url: '/api/action',
        type: 'GET',
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
    var pharmacyName = "";
    let pharmacyId = action.pharmacyId;
    $.ajax({
        url: '/api/action/' + pharmacyId,
        type: 'GET',
        dataType: 'json',
        processData: false,
        contentType: false,
        success: function (data) {
            pharmacyName = data.name;

            let new_action = $('<h1 style="margin-left:140px;">APOTEKA ' + pharmacyName + '</h1><h1 style="margin-left:160px;">' + action.subject + '</h1><h3 style="margin-left:290px;">' + action.message + '<h3>');

            if (i == 0) {
                $('div#action_first_div').append(new_action);
            }
            if (i == 1) {
                $('div#action_second_div').append(new_action);
            }
            if (i == 2) {
                $('div#action_third_div').append(new_action);
            }
        },
        error: function () {
            console.log('error getting pharmacy');
        }
    });

    
}