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

    let jmbg = "1309998775018";

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
                addExaminationRow(data[i],i);
            }
        },
        error: function () {
            console.log("Error getting examination reports")
        }
    });
    $('form#search_prescription').submit(function (event) {
        event.preventDefault()
        $("#div_prescriptions").empty();

        var j = 0;
        let start_date = $('#start_date').val();
        let end_date = $('#end_date').val();
        let doctor = $('#doctor_surname').val();
        let anamnesis_or_drug = $('#drug_exam_name').val();

        if (!start_date) {
            start_date = null;
        }
        if (!end_date) {
            end_date = null;
        }
        if (!doctor) {
            doctor = null;
        }
        if (!anamnesis_or_drug) {
            anamnesis_or_drug = null;
        }

        let operator = 0;
        let doc_type = $('#doc_type option:selected').val();
        if (doc_type == "report") {
            var newData = {
                "Jmbg": jmbg,
                "StartDate": start_date,
                "EndDateOperator": operator,
                "EndDate": end_date,
                "DoctorSurnameOperator": operator,
                "DoctorSurname": doctor,
                "AnamnesisOperator": operator,
                "Anamnesis": anamnesis_or_drug
            };
            $.ajax({
                url: '/api/examination/advance-search',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(newData),
                success: function (data) {
                    for (let i = 0; i < data.length; i++) {
                        addExaminationRow(data[i], i);
                    }
                },
                error: function (error) {
                    alert("Error getting examinations")
                }
            }); 

        } else {
            var newData = {
                "Jmbg": jmbg,
                "StartDate": start_date,
                "EndDateOperator": operator,
                "EndDate": end_date,
                "DoctorSurnameOperator": operator,
                "DoctorSurname": doctor,
                "DrugNameOperator": operator,
                "DrugName": anamnesis_or_drug
            };
            
            $.ajax({
               url: "/api/therapy/advance-search",
               type: 'POST',
               contentType: 'application/json',
               data: JSON.stringify(newData),
               success: function (therapies) {
                    for (let i = 0; i < therapies.length; i++) {
                        j = j + 1;
                        addPrescriptionTable(therapies[i], j);
                    }
               },
               error: function (error) {
                    alert("Error getting therapies")
               }
             });

        }
    });
});
function addPrescriptionTable(therapy, j) {
    let divElement = $('<div style="margin-top: 40px; margin-left: 21%; margin-bottom:20px; border-style: solid; border-color: black; border-width: 1px; background-color: #cce6ff; padding-top: 40px;left: 450px; top: 200px; width:600px;">'
        + '<table><tr><td>'
        + '<table style="height: 220px; margin-left: 30px; margin-bottom: 20px; width: 350px;">'
        + '<tr><td colspan="2"><h5>Prescription information '+ j +'</h5></td></tr>'
        + ' <tr> <th>Start date:</th><td>' + therapy.startDate + '</td></tr > '
        + ' <tr> <th>End date:</th><td>' + therapy.endDate + '</td></tr > '
        + ' <tr> <th>Daily dose:</th><td>' + therapy.dailyDose + '</td></tr > '
        + '<tr><th >Doctor:</th><td>' + therapy.doctorName + ' ' + therapy.doctorSurname + '</td></tr>'
        + ' <tr> <th>Drug:</th><td>' + therapy.drugName + '</td></tr></br> '
        + ' </table ></div > ');
    $('div#div_prescriptions').append(divElement);

}
function addExaminationRow(examination, i) {
    i = i + 1;
    let divElement = $('<div style="margin-top: 40px; margin-left: 21%; margin-bottom:20px; border-style: solid; border-color: black; border-width: 1px; background-color: #cce6ff; padding-top: 40px;left: 450px; top: 200px; width:600px;">'
        + '<table><tr><td>'
        + '<table style="height: 220px; margin-left: 30px; margin-bottom: 20px; width: 350px;">'
        + '<tr><td colspan="2"><h5>Examination report information '+ i +'</h5></td></tr>'
        + '<tr><th>Date:</th><td>' + examination.dateAndTime + '</td></tr>'
        + '<tr><th>Doctor:</th><td>' + examination.doctorName + ' ' + examination.doctorSurname + '</td></tr>'
        + '<tr><th>Type:</th><td>' + examination.type + '</td></tr>'
        + '<tr><th>Anamnesis:</th><td>' + examination.anamnesis + '</td></tr></br>'+ ' </table ></div > ');
    $('div#div_prescriptions').append(divElement);
    
}
