$(document).ready(function () {
    checkUserRole("Patient");

    $.ajax({
        url: "/api/patient",
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + window.localStorage.getItem('token')
        },
        dataType: 'json',
        processData: false,
        contentType: false,
        success: function (data) {
            let patientDTO = data;

            if (patientDTO.imageName) {
                $('#profile_image').attr('src', patientDTO.imageName);
            }

            $('#name').append(patientDTO.name + ' ' + patientDTO.surname);
            $('#jmbg').append(patientDTO.jmbg);
            $('#dateOfBirth').append(patientDTO.dateOfBirth);
            $('#gender').append(patientDTO.gender);
            $('#phone').append(patientDTO.phone);
            $('#address').append(patientDTO.homeAddress + ', ' + patientDTO.cityName + ', ' + patientDTO.countryName);
            $('#email').append(patientDTO.email)

            let blood = '';
            if (patientDTO.bloodType == "0") {
                blood = 'A';
            } else if (patientDTO.bloodType == "1") {
                blood = 'B';
            } else if (patientDTO.bloodType == "2") {
                blood = 'AB';
            } else if (patientDTO.bloodType == "3") {
                blood = '0';
            } else {
                blood = 'Unknown';
            }

            if (patientDTO.rhFactor == "0") {
                blood = blood + '+';
            } else if (patientDTO.rhFactor == "1") {
                blood = blood + '-';
            }

            $('#blood').append(blood);

            if (patientDTO.hasInsurance == "1") {
                $('#insurance').empty();
                $('#insurance').append(patientDTO.lbo);
            }

            if (patientDTO.allergies) {
                $('#allergies').empty();
                $('#allergies').append(patientDTO.allergies);
            }

            if (patientDTO.medicalHistory) {
                $('#history').empty();
                $('#history').append(patientDTO.medicalHistory);
            }

            $('#loading').remove();

        },
        error: function () {
            let alert = $('<div class="alert alert-danger m-4" role="alert">Error fetching general info.</div >')
            $('#loading').remove();
            $('#container').prepend(alert);
        }
    });

    $.ajax({
        url: "/api/patient/medical-info",
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + window.localStorage.getItem('token')
        },
        dataType: 'json',
        processData: false,
        contentType: false,
        success: function (data) {
            let patientDTO = data;

            let blood = patientDTO.bloodType;
            if (patientDTO.rhFactor == "Positive") {
                blood = blood + '+';
            } else if (patientDTO.rhFactor == "Negative") {
                blood = blood + '-';
            }

            if (blood) {
                $('#blood').empty();
                $('#blood').append(blood);
            }            

            if (patientDTO.InsuranceNumber) {
                $('#insurance').empty();
                $('#insurance').append(patientDTO.InsuranceNumber);
            }

            if (patientDTO.allergies) {
                $('#allergies').empty();
                $('#allergies').append(patientDTO.allergies);
            }

            if (patientDTO.medicalHistory) {
                $('#history').empty();
                $('#history').append(patientDTO.medicalHistory);
            }

            $('#loadingmed').remove();

        },
        error: function () {
            let alert = $('<div class="alert alert-danger m-4" role="alert">Error fetching medical info.</div >')
            $('#loadingmed').remove();
            $('#container').prepend(alert);
        }
    });
});