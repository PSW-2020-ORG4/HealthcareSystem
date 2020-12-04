$(document).ready(function () {

    $('#following_exam').prop("selected", true);

    let jmbg = "1309998775018";

    $.ajax({
        url: '/api/examination/following/' + jmbg,
        type: 'GET',
        dataType: 'json',
        processData: false,
        contentType: false,
        success: function (examinations) {
            if (examinations.length == 0) {
                $('p#not_found').text('There are no following examinations.');
                $('p#not_found').attr("hidden", false);
            }
            for (let i = 0; i < examinations.length; i++) {
                addExaminationRow(examinations[i]);
            }
        },
        error: function () {
            console.log("Error getting following examination")
        }
    });

    $('form#search_examinations').submit(function (event) {
        event.preventDefault()
        $("#div_examinations").empty();

        let exam_status = $('#examination_status option:selected').val();

        if (exam_status == "following") {
            $('p#not_found').attr("hidden", true);

            location.reload();
        }
        else if (exam_status == "previous") {
            $('p#not_found').attr("hidden", true);

            $.ajax({
                url: '/api/examination/previous/' + jmbg,
                type: 'GET',
                dataType: 'json',
                processData: false,
                contentType: false,
                success: function (examinations) {
                    if (examinations.length == 0) {
                        $('p#not_found').text('There are no previous examinations.');
                        $('p#not_found').attr("hidden", false);
                    }
                    for (let i = 0; i < examinations.length; i++) {
                        addExaminationRow(examinations[i]);
                    }
                },
                error: function () {
                    console.log("Error getting previous examination")
                }
            });
        }
        else {
            $('p#not_found').attr("hidden", true);

            $.ajax({
                url: '/api/examination/cancelled/' + jmbg,
                type: 'GET',
                dataType: 'json',
                processData: false,
                contentType: false,
                success: function (cancelled_exam) {
                    if (cancelled_exam.length == 0) {
                        $('p#not_found').text('There are no cancelled examinations.');
                        $('p#not_found').attr("hidden", false);
                    }
                    for (let i = 0; i < cancelled_exam.length; i++) {
                        addExaminationRow(cancelled_exam[i]);
                    }
                },
                error: function () {
                    console.log("Error getting cancelled examination")
                }
            });
        }
    });

});


function addExaminationRow(examination) {

    let btn_type = '';
    if (examination.examinationStatus == 0) {
        btn_type = '<button class="btn btn-danger" style="margin-bottom:10px;" id="' + examination.id + '" onclick="cancelExamination(this.id)">Cancel</button>';

    }

    if (examination.examinationStatus == 2 && examination.isSurveyCompleted == 0) {
        btn_type = '<button class="btn btn-info" style="margin-bottom:10px;" id="' + examination.id + '" onclick="FillOutTheSurvey(this.id)">Fill out the survey</button>';
    }

    let divElement = $('<div style="margin-top: 40px; margin:auto; margin-top:70px; border-style: solid; border-color: black; border-width: 1px;padding: 10px; background-color: #cce6ff;width:470px; height:220px;">'
        + '<table><tr><td>'
        + '<table style="height: 140px; margin-left: 30px; margin-bottom: 20px; width: 350px;">'
        + '<tr><th>Date:</th><td>' + examination.dateAndTime + '</td></tr>'
        + '<tr><th>Examination type:</th><td>' + examination.type + '</td></tr>'
        + '<tr><th>Doctor:</th><td>' + examination.doctorName + ' ' + examination.doctorSurname + '</td></tr>'
        + '<tr style="margin-top:20px;"><th></th><td><span style="width: 200px;"></span>' + btn_type + '</td></tr></br></table></div>');

    $('div#div_examinations').append(divElement);

};

function cancelExamination(id) {

    $.ajax({
        type: "PUT",
        url: "/api/examination/cancel/" + id,
        success: function () {

            console.log('You have successfully cancel the examination!');
            setTimeout(function () {
                location.reload();
            }, 500);
        },
        error: function (jqXHR) {

            alert(jqXHR.responseText);
        }
    });

};


function FillOutTheSurvey(examinationId) {

    window.location.href = 'filling_out_the_survey.html?id=' + examinationId;
};
