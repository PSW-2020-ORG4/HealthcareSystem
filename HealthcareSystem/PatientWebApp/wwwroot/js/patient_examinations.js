$(document).ready(function () {

    let element_step_1 = '<label for="dateOfExam"><b>Date of examiantion</b></label>'
        + '<input class="form-control" type = "date" placeholder = "Enter date of examination" name = "dateOfExam" id = "dateOfExam" required><br />'
        + '<button class="btn btn-success" style="margin-left:200px;" id ="step_btn" onclick="step(1)">Next</button>';

    $('div#div_schedule_exam').append(element_step_1);
    var dtToday = new Date();

    var month = dtToday.getMonth() + 1;
    var day = dtToday.getDate();
    var year = dtToday.getFullYear();
    if (month < 10)
        month = '0' + month.toString();
    if (day < 10)
        day = '0' + day.toString();

    var minDate = year + '-' + month + '-' + day;

    $('#dateOfExam').attr('min', minDate);


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

function step(id) {

    $('#step_btn').click(function (event) {
        $("#div_schedule_exam").empty();

        event.preventDefault();
        if (id == 1) {
            let element_step_2 = '<label for="dateOfExam"><b>Specialty of doctor</b></label>'
                + '<select class="form-control" required>'
                + '<option value="previous">Kardiolog</option>'
                + '<option value="cancelled">Plumolog</option></select><br />'
                + '<button class="btn btn-warning" style ="margin-left:150px;" id ="step_previous_btn" onclick="step_previous(1)">Previous</button>'
                + '<button class="btn btn-success" style="margin-left:20px;" id ="step_btn" onclick="step(2)">Next</button>';

            $('div#div_schedule_exam').append(element_step_2);
        }
        if (id == 2) {
            let element_step_3 = '<label for="dateOfExam"><b>Doctor</b></label>'
                + '<select class="form-control" required>'
                + '<option value="previous">Marko Markovic</option>'
                + '<option value="cancelled">Darko Daric</option></select><br />'
                + '<button class="btn btn-warning" style ="margin-left:150px;" id ="step_previous_btn" onclick="step_previous(2)">Previous</button>'
                + '<button class="btn btn-success" style="margin-left:20px;" id ="step_btn" onclick="step(3)">Next</button>';

            $('div#div_schedule_exam').append(element_step_3);
        }
        if (id == 3) {
            let element_step_4 = '<label for="dateOfExam"><b>Available appointments</b></label>'   
                + '</br><table border="1" style="background: #ccffcc;"><tr><td>12.12.2020 12:00</td><td>dr Marko Markovic</td><td><button class="btn btn-success" style="margin-left:20px;" id ="step_btn" onclick="scheduleExamination()">Schedule</button></td></tr></table><br />'
                + '<button class="btn btn-warning" style ="margin-left:190px;" id ="step_previous_btn" onclick="step_previous(3)">Previous</button>';

            $('div#div_schedule_exam').append(element_step_4);
        }
              
    });
};

function step_previous(id) {

    $('#step_previous_btn').click(function (event) {
        $("#div_schedule_exam").empty();

        event.preventDefault();
        if (id == 1) {
            let element_step_1 = '<label for="dateOfExam"><b>Date of examiantion</b></label>'
                + '<input class="form-control" type = "date" placeholder = "Enter date of examination" name = "dateOfExam" id = "dateOfExam" required><br />'
                + '<button class="btn btn-success" style="margin-left:200px;" id ="step_btn" onclick="step(1)">Next</button>';

            $('div#div_schedule_exam').append(element_step_1);
        }
        if (id == 2) {
            let element_step_2 = '<label for="dateOfExam"><b>Specialty of doctor</b></label>'
                +'<select class="form-control" required>'
                + '<option value="previous">Kardiolog</option>'
                + '<option value="cancelled">Plumolog</option></select><br />'
                + '<button class="btn btn-warning" style ="margin-left:150px;" id ="step_previous_btn" onclick="step_previous(1)">Previous</button>'
                + '<button class="btn btn-success" style="margin-left:20px;" id ="step_btn" onclick="step(2)">Next</button>';

            $('div#div_schedule_exam').append(element_step_2);
        }
        if (id == 3) {
            let element_step_3 = '<label for="dateOfExam"><b>Doctor</b></label>'
                + '<select class="form-control" required>'
                + '<option value="previous">Marko Markovic</option>'
                + '<option value="cancelled">Darko Daric</option></select><br />'
                + '<button class="btn btn-warning" style ="margin-left:150px;" id ="step_previous_btn" onclick="step_previous(2)">Previous</button>'
                + '<button class="btn btn-success" style="margin-left:20px;" id ="step_btn" onclick="step(3)">Next</button>';

            $('div#div_schedule_exam').append(element_step_3);
        }



    });
};


function addExaminationRow(examination) {

    let btn_type = '';
    var restrict_date = new Date(examination.dateAndTime);
    restrict_date.setDate(restrict_date.getDate() - 2);
    var current_date = new Date();
    if (examination.examinationStatus == 0 && current_date < restrict_date) {
        btn_type = '<button class="btn btn-danger" style="margin-left:80px;" id="' + examination.id + '" onclick="cancelExamination(this.id)">Cancel examination</button>';
    }

    if (examination.examinationStatus == 2 && examination.isSurveyCompleted == 0) {
        btn_type = '<button class="btn btn-info" style="margin-left:80px;" id="' + examination.id + '" onclick="FillOutTheSurvey(this.id)">Fill out the survey</button>';
    }

    let divElement = $('<div style="margin-top: 40px; margin:auto; margin-top:70px; border-style: solid; border-color: black; border-width: 1px;padding: 10px; background-color: #cce6ff;width:670px; height:250px;">'
        + '<table>'
        + '<tr><td><table style="height: 220px; margin-left: 40px; margin-bottom: 20px;  width: 350px;">'
        + '<tr><th>Date:</th><td>' + examination.dateAndTime + '</td></tr>'
        + '<tr><th>Examination type:</th><td>' + examination.type + '</td></tr>'
        + '<tr><th>Doctor:</th><td>' + examination.doctorName + ' ' + examination.doctorSurname + '</td></tr>'
        + '</table></td><td style="vertical-align: center;">'
        + btn_type + '</td></tr></table></div>');

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
