var jmbg = "";
$(document).ready(function () {
    checkUserRole("Patient");

    $('#start_date').val("");
    $('#end_date').val("");
    $('#doctor_surname').val("");
    $('#drug_exam_name').val("");

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
            $("#drug_exam_name").attr("placeholder", "Enter drug name");
            $("#drug_exam_label").text("Drug name");
        } else {
            $("#drug_exam_name").attr("placeholder", "Enter anamnesis");
            $("#drug_exam_label").text("Anamnesis");
        }
    });

    $.ajax({
        url: '/api/examination/previous',
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + window.localStorage.getItem('token')
        },
        dataType: 'json',
        processData: false,
        contentType: false,
        success: function (data) {
            if (data.length == 0) {
                let alert = '<div id="loading" class="alert alert-info" role="alert">'
                    + 'No examinations found.'
                    + '</div>';
                $("#loading").hide();
                $("#div_prescriptions").prepend(alert);
            }
            else {
                for (let i = 0; i < data.length; i++) {
                    jmbg = data[i].patientJmbg;
                    addExaminationRow(data[i]);
                }
                $("#loading").hide();
            }
        },
        error: function () {
            let alert = '<div id="loading" class="alert alert-danger" role="alert">'
                + 'Error fetching data.'
                + '</div>';
            $("#loading").hide();
            $("#div_prescriptions").prepend(alert);
        }
    });
    $('form#search_prescription').submit(function (event) {
        $('#search_prescription').find(":submit").prop('disabled', true);
        event.preventDefault()
        $("#loading").show();
        $("#div_prescriptions").empty();

        let start_date = $('#start_date').val();
        let end_date = $('#end_date').val();
        let doctor = $('#doctor_surname').val();
        let anamnesis_or_drug = $('#drug_exam_name').val();
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
        if (!anamnesis_or_drug) {
            anamnesis_or_drug = null;
        }

        let operator = 0;
        let doc_type = $('#doc_type option:selected').val();
        if (doc_type == "report") {
            var newData = {
                "Jmbg": jmbg,
                "StartDate": start_date,
                "EndDateOperator": parseInt(first_operator),
                "EndDate": end_date,
                "DoctorSurnameOperator": parseInt(second_operator),
                "DoctorSurname": doctor,
                "AnamnesisOperator": parseInt(third_operator),
                "Anamnesis": anamnesis_or_drug
            };
            $.ajax({
                url: '/api/examination/advance-search',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(newData),
                success: function (data) {
                    if (data.length == 0) {
                        let alert = '<div id="loading" class="alert alert-info" role="alert">'
                            + 'No examinations found.'
                            + '</div>';
                        $("#loading").hide();
                        $("#div_prescriptions").prepend(alert);
                        $('#search_prescription').find(":submit").prop('disabled', false);
                    }
                    else {
                        for (let i = 0; i < data.length; i++) {
                            addExaminationRow(data[i]);
                        }
                        $("#loading").hide();
                        $('#search_prescription').find(":submit").prop('disabled', false);
                    }
                },
                error: function () {
                    let alert = '<div id="loading" class="alert alert-danger" role="alert">'
                        + 'Error fetching data.'
                        + '</div>';
                    $("#loading").hide();
                    $("#div_prescriptions").prepend(alert);
                    $('#search_prescription').find(":submit").prop('disabled', false);
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
                "DrugNameOperator": parseInt(third_operator),
                "DrugName": anamnesis_or_drug
            };

            $.ajax({
                url: "/api/therapy/advance-search",
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(newData),
                success: function (therapies) {
                    if (therapies.length == 0) {
                        let alert = '<div id="loading" class="alert alert-info" role="alert">'
                            + 'No therapies found.'
                            + '</div>';
                        $("#loading").hide();
                        $("#div_prescriptions").prepend(alert);
                        $('#search_prescription').find(":submit").prop('disabled', false);
                    }
                    else {
                        for (let i = 0; i < therapies.length; i++) {
                            addPrescriptionTable(therapies[i]);
                        }
                        $("#loading").hide();
                        $('#search_prescription').find(":submit").prop('disabled', false);
                    }
                },
                error: function (error) {
                    let alert = '<div id="loading" class="alert alert-danger" role="alert">'
                        + 'Error fetching data.'
                        + '</div>';
                    $("#loading").hide();
                    $("#div_prescriptions").prepend(alert);
                    $('#search_prescription').find(":submit").prop('disabled', false);
                }
            });

        }
    });
});
function addPrescriptionTable(therapy) {
    let divElement = $(
        '<div class="row">'
        + '<div class="col mb-4">'
        + '<div class="card">'
        + '<div class="card-header bg-info text-white">'
        + '<h4 class="card-title mb-0"><strong>'
        + therapy.drugName + '</strong> <span class="badge badge-light">' + therapy.dailyDose + ' daily</span>'
        + ' from <span class= "badge badge-light"> ' + therapy.startDate + ' </span >'
        + ' to <span class= "badge badge-light"> ' + therapy.endDate + '</span >'
        + '</h4> '
        + '</div>'
        + '<div class="card-body p-3">'
        + '<label class="text-secondary mb-0">Prescribed by</label><br>'
        + '<label id="jmbg">' + therapy.doctorName + ' ' + therapy.doctorSurname + '</label><br>'
        + ' </div></div></div></div>'
    );
    $('div#div_prescriptions').append(divElement);
}

function addExaminationRow(examination) {
    let type = '';
    if (examination.type == "GENERAL")
        type = "Examination";
    else
        type = "Surgery";

    let button = '';
    if (examination.examinationStatus == 2 && examination.isSurveyCompleted == 0) {
        button = '<div class="card-footer">'
            + '<button type = "button" class="btn btn-success float-right" '
            + 'onclick="window.location.href=\'/html/filling_out_the_survey.html?id=' + examination.id + '\'"'
            + '> Fill out the survey</button >'
            + '</div >';
    }

    let divElement = $(
        '<div class="row">'
        + '<div class="col mb-4">'
        + '<div class="card">'
        + '<div class="card-header bg-info text-white">'
        + '<h4 class="card-title mb-0">'
        + type + ' on <span class="badge badge-light">' + examination.dateAndTime + '</span> '
        + '</h4>'
        + '</div>'
        + '<div class="card-body p-3">'
        + '<label class="text-secondary mb-0">Doctor:</label><br>'
        + '<label>' + examination.doctorName + ' ' + examination.doctorSurname + '</label><br>'
        + '<label class="text-secondary mb-0">Anamnesis:</label><br>'
        + '<label>' + examination.anamnesis + '</label><br>'
        + '</div>' + button + '</div></div></div>'
    );
    $('div#div_prescriptions').append(divElement);

}