$(document).ready(function () {

    let jmbg = "123";
    $.ajax({
        url: "/api/patient/" + jmbg,
        type: 'GET',
        dataType: 'json',
        processData: false,
        contentType: false,
        success: function (data) {
            let patientDTO = data

            var imagePath = "http://localhost:65117/uploads/" + patientDTO.imageName;
            $('#profile_image').attr('src', imagePath);

            $('#name').val(patientDTO.name)
            $('#surname').val(patientDTO.surname)
            $('#jmbg').val(patientDTO.jmbg)
            $('#dateOfBirth').val(patientDTO.dateOfBirth)
            if (patientDTO.gender == "0") {
                $('#gender').val("MALE")
            } else {
                $('#gender').val("FEMALE")
            }
            $('#phone').val(patientDTO.phone)
            $('#country').val(patientDTO.countryName)
            $('#city').val(patientDTO.cityName)
            $('#address').val(patientDTO.homeAddress)
            if (patientDTO.bloodType == "0") {
                $('#blood').val("A")
            } else if (patientDTO.bloodType == "1") {
                $('#blood').val("B")
            } else if (patientDTO.bloodType == "2") {
                $('#blood').val("AB")
            } else {
                $('#blood').val("0")
            }
            if (patientDTO.rhFactor == "0") {
                $('#rh').val("+")
            } else {
                $('#rh').val("-")
            }
            if (patientDTO.hasInsurance == "0") {
                $('#insurance').val("YES")
            } else {
                $('#insurance').val("NO")
            }

            $('#lbo').val(patientDTO.lbo)
            $('#allergies').val(patientDTO.allergies)
            $('#history').val(patientDTO.medicalHistory)
            $('#email').val(patientDTO.email)
            $('#password').val(patientDTO.password)


        },
        error: function () {
            console.log("Error getting patient")
        }
    });
});