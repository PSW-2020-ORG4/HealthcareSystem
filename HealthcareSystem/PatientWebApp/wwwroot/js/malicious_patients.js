$(document).ready(function () {

    $.ajax({
        url: '/api/patient/malicious-patients',
        type: 'GET',
        dataType: 'json',
        processData: false,
        contentType: false,
        success: function (data) {
            for (let i = 0; i < data.length; i++) {
                addPatient(data[i]);
            }
        },
        error: function () {
            console.log("Error getting malicious patients")
        }
    });
});

function addPatient(patient) {
    
    let btn_type = '';
    if (patient.isBlocked == false) {
        btn_type = '<button name="button_block" class="btn btn-danger" style="margin-bottom:10px;" id="' + patient.jmbg
            + '" onclick="blockPatient(this.id)">Block</button>';
    }
    
    let new_patient = $('<div style="margin-top: 10px; margin-left: 21%; margin-bottom:20px; border-style: solid; border-color: black; border-width: 1px; background-color: #cce6ff; padding-top: 40px;left: 450px; top: 200px; width:600px;">'
        + '<table style="height: 10px; margin-left: 10px; margin-bottom: 20px; width: 350px;">'
        + '<tr><td width="25%"><th>Name:</th></td><td>' + ' ' + patient.name + '</td>'
        + '<td width="25%"><th>Surname:</th></td><td>' + ' ' + patient.surname + '</td></tr>'
        + '<tr><td width="25%"><th>Email:</th></td><td>' + ' ' + patient.email + '</td>'
        + '<td width="25%"><th>Phone:</th></td><td width="30px">' + ' ' + patient.phone + '</td></tr></br>' 

        + '<tr style="margin-top:20px;"><th></th><td><span style="width: 200px;"></span>' + btn_type + '</td></tr></br></table></div>');

    $('div#div_patients').append(new_patient);
}


function blockPatient(patientJmbg) {

    $.ajax({
        type: "PUT",
        url: "/api/patient/blocked/" + patientJmbg,
        success: function () {
            alert("You have successfully blocked the patient.");
            console.log('You have successfully blocked the patient.');
            setTimeout(function () {
                location.reload();
            }, 500);
        },
        error: function (jqXHR) {
            alert("Error");
            alert(jqXHR.responseText);
        }
    });
};
