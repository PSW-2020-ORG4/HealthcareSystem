$(document).ready(function () {

    $('#start_date').val("");
    $('#end_date').val("");
    $('#doctor_surname').val("");
    $('#drug_exam_name').val("");

    $('#examination_search').prop("selected", true);
    $("#drug_exam_name").attr("placeholder", "Anamnesis").blur();

    var dtToday = new Date();

    var month = dtToday.getMonth() + 1;
    var day = dtToday.getDate();
    var year = dtToday.getFullYear();
    if (month < 10)
        month = '0' + month.toString();
    if (day < 10)
        day = '0' + day.toString();

    var maxDate = year + '-' + month + '-' + day;

    $('#start_date').attr('max', maxDate);
    $('#end_date').attr('max', maxDate);

    $('#doc_type').change(function () {

        let doc_type = $('#doc_type option:selected').val();
        if (doc_type == "prescription") {
            $("#drug_exam_name").attr("placeholder", "Drug name").blur();
        } else {
            $("#drug_exam_name").attr("placeholder", "Anamnesis").blur();
        }
    });

    var jmbg = "1309998775018"

    $.ajax({
        url: '/api/examination/' + jmbg,
        type: 'GET',
        dataType: 'json',
        processData: false,
        contentType: false,
        success: function (data) {
            if (data.length == 0) {
                $('#not_found').text('There are no such examination reports.');
                $('#not_found').attr("hidden", false);
            }
            for (let i = 0; i < data.length; i++) {
                addReportTable(data[i]);
            }
        },
        error: function () {
            console.log("Error getting examination reports")
        }
    });

    $('#search_prescription').submit(function (event) {

        event.preventDefault();

        let start_date = $('#start_date').val();
        let end_date = $('#end_date').val();
        let doctor = $('#doctor_surname').val();
        let drug_anamnesis = $('#drug_exam_name').val();

        let first_operator = $('#operator_type_1 option:selected').val();
        let second_operator = $('#operator_type_2 option:selected').val();
        let third_operator = $('#operator_type_3 option:selected').val();

        if (!start_date) {
            start_date = null;
        }
        if (!end_date) {
            end_date = null;
        }
        if (!doctor) {
            doctor = null;
        }
        if (!drug_anamnesis) {
            drug_anamnesis = null;
        }

        let doc_type = $('#doc_type option:selected').val();
        if (doc_type == "prescription") {

            var newData = {
                "Jmbg": jmbg,
                "StartDate": start_date,
                "EndDateOperator": parseInt(first_operator),
                "EndDate": end_date,
                "DoctorSurnameOperator": parseInt(second_operator),
                "DoctorSurname": doctor,
                "DrugNameOperator": parseInt(third_operator),
                "DrugName": drug_anamnesis
            };

            $.ajax({
                url: "/api/therapy/advance-search",
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(newData),
                success: function (data) {
                    $('div#div_report').empty();
                    $('#not_found').attr("hidden", true);
                    if (data.length == 0) {
                        $('#not_found').text('There are no such prescriptions.');
                        $('#not_found').attr("hidden", false);
                    }
                    for (let i = 0; i < data.length; i++) {
                        addPrescriptionTable(data[i]);
                    }

                },
                error: function (jqXHR) {

                    alert(jqXHR.responseText);
                }
            });         

        } else {
            var newData = {
                "Jmbg": jmbg,
                "StartDate": start_date,
                "EndDateOperator": parseInt(first_operator),
                "EndDate": end_date,
                "DoctorSurnameOperator": parseInt(second_operator),
                "DoctorSurname": doctor,
                "AnamnesisOperator": parseInt(third_operator),
                "Anamnesis": drug_anamnesis
            };

            $.ajax({
                url: "/api/examination/advance-search",
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(newData),
                success: function (data) {
                    $('div#div_report').empty();
                    $('#not_found').attr("hidden", true);
                    if (data.length == 0) {
                        $('#not_found').text('There are no such examination reports.');
                        $('#not_found').attr("hidden", false);
                    }
                    for (let i = 0; i < data.length; i++) {
                        addReportTable(data[i]);
                    }

                },
                error: function (jqXHR) {

                    alert(jqXHR.responseText);
                }
            });

        }

    });

});

function addReportTable(examination) {
  
    let report = $('<div style="margin-top: 40px; margin-left: 21%; margin-bottom:20px; border-style: solid; border-color: black; border-width: 1px; background-color: #cce6ff; padding-top: 40px;left: 450px; top: 200px; width:600px;">'
        + '<table><tr><td>'
        + '<table style="height: 220px; margin-left: 30px; margin-bottom: 20px; width: 350px;">'
        + '<tr><td colspan="2"><h5>Examination report information</h5></td></tr>'
        + '<tr><th width="35%">Date:</th><td>' + examination.dateAndTime + '</td></tr>'
        + '<tr><th>Doctor:</th><td>' + examination.doctorName + ' ' + examination.doctorSurname + '</td></tr>'
        + '<tr><th>Type:</th><td>' + examination.type + '</td></tr>'
        + '<tr><th>Anamnesis:</th><td>' + examination.anamnesis + '</td></tr>'
        + '</table></td></tr></table></div >');

    $('div#div_report').append(report);
}

function addPrescriptionTable(therapy) {

    let prescription = $('<div style="margin-top: 40px; margin-left: 21%; margin-bottom:20px; border-style: solid; border-color: black; border-width: 1px; background-color: #cce6ff; padding-top: 40px;left: 450px; top: 200px; width:600px;">'
        + '<table><tr><td>'
        + '<table style="height: 220px; margin-left: 30px; margin-bottom: 20px; width: 350px;">'
        + '<tr><td colspan="2"><h5>Prescription information</h5></td></tr>'
        + '<tr><th width="35%">Date:</th><td>' + therapy.startDate + '</td></tr>'
        + '<tr><th>Doctor:</th><td>' + therapy.doctorName + ' ' + therapy.doctorSurname + '</td></tr>'
        + '<tr><th>Drug:</th><td>' + therapy.drugName + '</td></tr>'
        + '<tr><th>Daily dose:</th><td>' + therapy.dailyDose + '</td></tr>'
        + '<tr><th>Diagnosis:</th><td>' + therapy.diagnosis + '</td></tr>'
        + '</table></td></tr></table></div >');

    $('div#div_report').append(prescription);
}