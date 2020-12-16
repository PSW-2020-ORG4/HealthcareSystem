$(document).ready(function () {

    let jmbg = "1309998775018";
    $.ajax({
        url: "/api/patient/" + jmbg,
        type: 'GET',
        dataType: 'json',
        processData: false,
        contentType: false,
        success: function (data) {
            let patientDTO = data;

            if (patientDTO.imageName) {
                var imagePath = "/uploads/" + patientDTO.imageName;
                $('#profile_image').attr('src', imagePath);
            }

            $('#name').append(patientDTO.name + ' ' + patientDTO.surname);
            $('#jmbg').append(patientDTO.jmbg);
            $('#dateOfBirth').append(patientDTO.dateOfBirth);
            if (patientDTO.gender == "0") {
                $('#gender').append("Male");
            } else {
                $('#gender').append("Female");
            }
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

            console.log("here");

        },
        error: function () {
            let alert = $('<div class="alert alert-danger m-4" role="alert">Error fetching data.</div >')
            $('#container').prepend(alert);
        }
    });
});