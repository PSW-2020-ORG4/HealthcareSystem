$(document).ready(function () {

    var dtToday = new Date();

    var month = dtToday.getMonth() + 1;
    var day = dtToday.getDate() + 1;
    var year = dtToday.getFullYear();
    if (month < 10)
        month = '0' + month.toString();
    if (day < 10)
        day = '0' + day.toString();

    var minDate = year + '-' + month + '-' + day;

    $('#dateOfExam').attr('min', minDate); 

    $.ajax({
        url: '/api/doctor/all-specialty',
        type: 'GET',
        dataType: 'json',
        processData: false,
        contentType: false,
        success: function (specialtes) {
            for (let s of specialtes) {        
                let specialty = $('<option value="' + s.id + '">' + s.name + '</option>');
                $('#specialty_name').append(specialty);              
            }

            let select_specialty = $('#specialty_name option:selected').val();
          
            $.ajax({
                url: '/api/doctor/doctor-specialty/' + select_specialty,
                type: 'GET',
                dataType: 'json',
                processData: false,
                contentType: false,
                success: function (doctorSpecialtes) {
                    for (let ds of doctorSpecialtes) {

                        let doctorJmbg = ds.doctorJmbg;

                        $.ajax({
                            url: '/api/doctor/' + doctorJmbg,
                            type: 'GET',
                            dataType: 'json',
                            processData: false,
                            contentType: false,
                            success: function (doctor) {

                                let doctorName = $('<option value="' + doctor.jmbg + '">' + doctor.name + ' ' + doctor.surname +'</option>');
                                $('#doctor_name').append(doctorName);

                            },
                            error: function () {
                                console.log("Error getting doctor")
                            }
                        });  

                        let specialty = $('<option value="' + s.id + '">' + s.name + '</option>');
                        $('#specialty_name').append(specialty);
                    }

                },
                error: function () {
                    console.log("Error getting doctors specialtes")
                }
            });  
        },
        error: function () {
            console.log("Error getting specialtes")
        }
    });  

    $('#specialty_name').change(function () {

        var select = document.getElementById("doctor_name");
        var length = select.options.length;
        for (i = length - 1; i >= 0; i--) {
            select.options[i] = null;
        }

        let select_specialty = $('#specialty_name option:selected').val();
        alert(select_specialty);

        $.ajax({
            url: '/api/doctor/doctor-specialty/' + select_specialty,
            type: 'GET',
            dataType: 'json',
            processData: false,
            contentType: false,
            success: function (doctorSpecialtes) {
                for (let ds of doctorSpecialtes) {
                    let doctorJmbg = ds.doctorJmbg;
                    alert(doctorJmbg);

                    $.ajax({
                        url: '/api/doctor/' + doctorJmbg,
                        type: 'GET',
                        dataType: 'json',
                        processData: false,
                        contentType: false,
                        success: function (doctor) {
                            let doctorName = $('<option value="' + doctor.jmbg + '">' + doctor.name + ' ' + doctor.surname + '</option>');
                            $('#doctor_name').append(doctorName);

                        },
                        error: function () {
                            console.log("Error getting doctor")
                        }
                    });
                }

            },
            error: function () {
                console.log("Error getting doctors specialtes")
            }
        }); 
    });

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

        if (id == 1) {
            document.getElementById("div_date").style.display = "none";
            document.getElementById("div_specialty").style.display = "initial";
        }
        if (id == 2) {
            document.getElementById("div_specialty").style.display = "none";
            document.getElementById("div_doctor").style.display = "initial";
        }
        if (id == 3) {
            document.getElementById("div_doctor").style.display = "none";
            document.getElementById("div_appointments").style.display = "initial";
            //to do call ajax get(get free appiontments)
        }
     
};

function step_previous(id) {

        if (id == 1) {
            document.getElementById("div_date").style.display = "initial";
            document.getElementById("div_specialty").style.display = "none";
        }
        if (id == 2) {
            document.getElementById("div_specialty").style.display = "initial";
            document.getElementById("div_doctor").style.display = "none";
        }
        if (id == 3) {
            document.getElementById("div_doctor").style.display = "initial";
            document.getElementById("div_appointments").style.display = "none";
        }

};

function scheduleExamination() {

    let date = $('#dateOfExam').val();  
    let specialty = $('#specialty_name option:selected').val();
    let doctor = $('#doctor_name option:selected').val();
    //to do call ajax post(add examiantion)
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
