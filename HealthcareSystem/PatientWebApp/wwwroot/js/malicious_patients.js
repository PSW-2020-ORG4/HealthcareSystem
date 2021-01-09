$(document).ready(function () {
    checkUserRole("Admin");
    $.ajax({
        url: '/api/patient/malicious',
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + window.localStorage.getItem('token')
        },
        dataType: 'json',
        processData: false,
        contentType: false,
        success: function (data) {
            for (let i = 0; i < data.length; i++) {
                addPatient(data[i]);
            }
            $('#loading').remove();
        },
        error: function (jqXHR) {
            let alert = $('<div class="alert alert-danger alert-dismissible fade show m-1" role="alert">'
                + jqXHR.responseJSON + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >')
            $('#loading').remove();
            $('div#div_patients').prepend(alert);
        }
    });
});

function addPatient(patient) {
    let noCanc = '';
    if (patient.numberOfMaliciousActions)
        noCanc = '<strong>' + patient.numberOfMaliciousActions + '</strong> cancellations';
    else
        noCanc = 'Error fetching number of cancellations.';

    let patientName = patient.name + ' ' + patient.surname;
    let address = patient.homeAddress + ', ' + patient.cityName + ', ' + patient.countryName;

    let image = '';
    if (patient.imageName) {
        image = "/Uploads/" + patient.imageName;
    } else {
        image = "/images/Blank-profile.png";
    }

    let new_patient = '<div class="row"><div class="col p-4"><div class="card">'
        + '<div class="card-header bg-info text-white pt-0 pb-0">'
        + '<div class="row align-items-center"><div class="col-auto p-1">'
        + '<img width="75px" height="75px" class="rounded-circle border border-white p-1" alt="No image" src="' + image + '">'
        + '</div><div class="col"><h4 class="card-title mb-0">'
        + patientName + '</h4></div></div></div>'
        + '<div class="card-body p-3"><label class="text-secondary mb-0">JMBG:</label><label class="text-secondary float-right mb-0">Email:</label><br>'
        + '<label>' + patient.jmbg + '</label>'
        + '<label class="float-right">' + patient.email + '</label><br>'
        + '<label class="text-secondary mb-0">Address:</label><label class="text-secondary float-right mb-0">Phone number:</label><br>'
        + '<label>' + address + '</label>'
        + '<label class="float-right">' + patient.phone + '</label><br>'
        + '</div><div class="card-footer">'
        + '<span class="text-danger align-middle" style="font-size:18px">' + noCanc + '</span>';

    if (patient.isBlocked == false) {
        new_patient = new_patient + '<button type="button" name="block_malicious" class="btn btn-danger float-right" id="'
            + patient.jmbg + '" onclick="blockPatient(this.id)">Block</button></div>'
            + '<div name="alert_container" class="card-footer bg-transpartent border-top-0 p-0" id="a' + patient.jmbg + '">';
    }

    new_patient = new_patient + '</div></div></div></div>';

    $('div#div_patients').append($(new_patient));
}


function blockPatient(patientJmbg) {
    let loading = $('<div class="alert alert-info m-2" role="alert">Blocking...</div >');
    $('#' + patientJmbg).prop("disabled", true);
    $('#a' + patientJmbg).prepend(loading);

    $.ajax({
        type: "POST",
        url: "/api/patient/" + patientJmbg + "/block",
        contentType: 'application/json',
        headers: {
            'Authorization': 'Bearer ' + window.localStorage.getItem('token')
        },
        success: function () {
            let alert = $('<div name="alert_msg" class="alert alert-success alert-dismissible fade show m-2" role="alert">Patient was successfully blocked.'
                + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >')
            $('#' + patientJmbg).remove();
            $('#a' + patientJmbg).empty();
            $('#a' + patientJmbg).prepend(alert);
        },
        error: function (jqXHR) {
            let alert = $('<div class="alert alert-danger alert-dismissible fade show m-2" role="alert">' + jqXHR.responseJSON +
                + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >')
            $('#a' + patientJmbg).empty();
            $('#' + patientJmbg).prop("disabled", false);
            $('#a' + patientJmbg).prepend(alert);
        }
    });
};