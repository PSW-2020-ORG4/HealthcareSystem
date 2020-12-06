$(document).ready(function () {

    $.ajax({
        url: '/api/patient/malicious-patients',
        type: 'GET',
        dataType: 'json',
        processData: false,
        contentType: false,
        success: function (data) {
            for (let i = 0; i < data.length; i++) {

                
                $.ajax({
                    url: '/api/patient/' + data[i].jmbg +'/canceled-examinations',
                    type: 'GET',
                    dataType: 'json',
                    processData: false,
                    contentType: false,
                    success: function (number) {
                        addPatient(data[i],number);
                    },
                    error: function () {
                        console.log("Error getting number of canceled examinations")
                    }
                });
            }
        },
        error: function () {
            console.log("Error getting malicious patients")
        }
    });
});

function addPatient(patient,number) {

    let new_patient = $('<div style="margin-top: 20px; margin-left: 21%; margin-bottom:20px; border-style: solid; border-color: black; border-width: 1px; background-color: #cce6ff; padding-top: 20px;left: 450px; top: 200px; width:600px;">'
        + '<table style="height: 100px; margin-left: 30px; margin-bottom: 20px; width: 350px;">'
        + '<tr><td><th>Name:</th></td><td>' + ' ' + patient.name + '</td></tr>'
        + '<tr><td><th>Surname:</th></td><td>' + ' ' + patient.surname + '</td></tr>'
        + '<tr><td><th>Email:</th></td><td>' + ' ' + patient.email + '</td></tr>'
        + '<tr><td><th>Number of canceled examinations:</th></td><td width="30px">' + ' ' + number + '</td></tr>'
        + '<tr><td><th>Phone:</th></td><td width="30px">' + ' ' + patient.phone + '</td></tr></br>' + ' </table ></div > ');

    $('div#div_patients').append(new_patient);
}