var newAppointments = [];
var jmbg = "";
var gender = 0;
var age = 0;
var startEventTime = new Date();
var startEventId = 0;
var currentStep = 0;
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
        success: function (patient) {
            jmbg = patient.jmbg;
            if (patient.gender == "Female") {
                gender = 1;
            }
            let dateOfBirth = new Date(patient.dateOfBirth);
            let now = new Date();
            age = Math.floor((now - dateOfBirth) / 31536000000);
        },
        error: function () {
            let alert = $('<div class="alert alert-danger alert-dismissible fade show m-1" role="alert">Error getting patient.'
                + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >')
            $('#loading').hide();
            $('#add_feedback_form').find(":submit").prop('disabled', false);
            $('#alert').prepend(alert);
        }
    });

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
        url: '/api/specialty',
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + window.localStorage.getItem('token')
        },
        dataType: 'json',
        processData: false,
        contentType: false,
        success: function (specialtes) {
            if (specialtes) {
                for (let s of specialtes) {
                    let specialty = $('<option value="' + s.id + '">' + s.name + '</option>');
                    $('#specialty_name').append(specialty);
                }
                changeSpecialty();
            }
        },
        error: function (jqXHR) {
            let alert = $('<div class="alert alert-danger alert-dismissible fade show mb-0 mt-2" role="alert">' + jqXHR.responseJSON + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >')
            $('#alertSchedule').prepend(alert);
        }
    });

    $('#specialty_name').change(changeSpecialty);

    getExaminations('/api/examination/following');

    $('form#search_examinations').submit(function (event) {
        event.preventDefault();
        $('#search_examinations').find(":submit").prop('disabled', true);
        $('#loading').show();
        $('#div_examinations').empty();

        let exam_status = $('#examination_status option:selected').val();

        if (exam_status == "following") {
            getExaminations('/api/examination/following');
        }
        else if (exam_status == "previous") {
            getPreviousExaminations('/api/examination/previous');
        }
        else {
            getExaminations('/api/examination/cancelled');
        }
    });

    $('#btn_close').click(function () {

        var stepEvent = {
            "startSchedulingEventId": parseInt(startEventId),
            "userAge": parseInt(age),
            "userGender": parseInt(gender),
            "eventStep": parseInt(currentStep),
            "clickEvent": parseInt(2)
        };

        $.ajax({
            url: "/api/event/step",
            type: 'POST',
            contentType: 'application/json',
            headers: {
                'Authorization': 'Bearer ' + window.localStorage.getItem('token')
            },
            data: JSON.stringify(stepEvent),
            success: function () {
                console.log("Successful!");
                currentStep = 0;
            },
            error: function () {
                console.log("An error occurred while writing the step event");
            }
        });

        var endEvent = {
            "startSchedulingEventTime": startEventTime,
            "userAge": parseInt(age),
            "userGender": parseInt(gender),
            "reasonForEndOfAppointment": parseInt(1)
        };

        $.ajax({
            url: "/api/event/end",
            type: 'POST',
            contentType: 'application/json',
            headers: {
                'Authorization': 'Bearer ' + window.localStorage.getItem('token')
            },
            data: JSON.stringify(endEvent),
            success: function () {
                console.log("Successful!");
                currentStep = 0;
            },
            error: function () {
                console.log("An error occurred while writing the end event");
            }
        });

        location.reload();
    });

    $('#startScheduling').click(function () {

        var startEvent = {
            "userAge": parseInt(age),
            "userGender": parseInt(gender),
        };

        $.ajax({
            url: "/api/event/start",
            type: 'POST',
            contentType: 'application/json',
            headers: {
                'Authorization': 'Bearer ' + window.localStorage.getItem('token')
            },
            data: JSON.stringify(startEvent),
            success: function (startEvent) {
                console.log("Successful!");
                startEventTime = new Date();
                startEventId = parseInt(startEvent.id);
                currentStep = 0;
            },
            error: function () {
                console.log("An error occurred while writing the start event");
            }
        });
    });
});


function scheduleExamination() {

    $('#modalButton').prop('disabled', true);
    $('#modalLoading').show();
    let a = $('#free_appointments option:selected').val();

    var appointment = newAppointments[a];
    var newData = {
        "startTime": appointment.startTime,
        "DoctorJmbg": appointment.doctorJmbg,
        "roomId": appointment.roomId
    };

    $.ajax({
        url: "/api/examination",
        type: 'POST',
        contentType: 'application/json',
        headers: {
            'Authorization': 'Bearer ' + window.localStorage.getItem('token')
        },
        data: JSON.stringify(newData),
        success: function () {
            let alert = $('<div class="alert alert-success alert-dismissible fade show mb-0 mt-2" role="alert">Examination successfully scheduled.'
                + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >');
            $('#alertSchedule').prepend(alert);
            $('#centralModalSuccess').modal('hide');
            $('#modalLoading').hide();
            $('#modalButton').prop('disabled', false);

            var endEvent = {
                "startSchedulingEventTime": startEventTime,
                "userAge": parseInt(age),
                "userGender": parseInt(gender),
                "reasonForEndOfAppointment": parseInt(0)
            };

            $.ajax({
                url: "/api/event/end",
                type: 'POST',
                contentType: 'application/json',
                headers: {
                    'Authorization': 'Bearer ' + window.localStorage.getItem('token')
                },
                data: JSON.stringify(endEvent),
                success: function () {
                    console.log("Successful!");
                    currentStep = 0;
                },
                error: function () {
                    console.log("An error occurred while writing the end event");
                }
            });

            window.location.reload();
        },
        error: function () {
            let alert = $('<div class="alert alert-danger alert-dismissible fade show mb-0 mt-2" role="alert">Examination scheduling failed.'
                + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >');
            $('#alertModal').prepend(alert);
            $('#modalLoading').hide();
            $('#modalButton').prop('disabled', false);

            var endEvent = {
                "startSchedulingEventTime": startEventTime,
                "userAge": parseInt(age),
                "userGender": parseInt(gender),
                "reasonForEndOfAppointment": parseInt(1)
            };

            $.ajax({
                url: "/api/event/end",
                type: 'POST',
                contentType: 'application/json',
                headers: {
                    'Authorization': 'Bearer ' + window.localStorage.getItem('token')
                },
                data: JSON.stringify(endEvent),
                success: function () {
                    console.log("Successful!");
                    currentStep = 0;
                },
                error: function () {
                    console.log("An error occurred while writing the end event");
                }
            });
        }
    });
};

function changeSpecialty() {
    var select = document.getElementById("doctor_name");
    var length = select.options.length;
    for (i = length - 1; i >= 0; i--) {
        select.options[i] = null;
    }

    let select_specialty = $('#specialty_name option:selected').val();

    $.ajax({
        url: '/api/doctor/doctor-specialty/' + select_specialty,
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + window.localStorage.getItem('token')
        },
        dataType: 'json',
        processData: false,
        contentType: false,
        success: function (doctorSpecialtes) {
            for (let doctor of doctorSpecialtes) {
                let doctorName = $('<option value="' + doctor.jmbg + '">' + doctor.name + ' ' + doctor.surname + '</option>');
                $('#doctor_name').append(doctorName);
            }
        },
        error: function () {
            let alert = $('<div class="alert alert-danger alert-dismissible fade show mb-0 mt-2" role="alert">Error fetching doctors.'
                + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >');
            $('#alertSchedule').prepend(alert);
        }
    });
}

function getExaminations(path) {
    $.ajax({
        url: path,
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + window.localStorage.getItem('token')
        },
        dataType: 'json',
        processData: false,
        contentType: false,
        success: function (examinations) {
            if (examinations.length == 0) {
                let alert = '<div id="loading" class="alert alert-info" role="alert" style="display:none">'
                    + 'No examinations found.'
                    + '</div>';
                $('#div_examinations').prepend(alert);
                $('#loading').hide();
                $('#search_examinations').find(":submit").prop('disabled', false);
            }
            else {
                for (let i = 0; i < examinations.length; i++) {
                    addExaminationRow(examinations[i]);
                }
                $('#loading').hide();
                $('#search_examinations').find(":submit").prop('disabled', false);
            }
        },
        error: function () {
            let alert = '<div id="loading" class="alert alert-danger" role="alert" style="display:none">'
                + 'Error fetching data.'
                + '</div>';
            $('#div_examinations').prepend(alert);
            $('#loading').hide();
            $('#search_examinations').find(":submit").prop('disabled', false);
        }
    });
}

function getPreviousExaminations(path) {
    $.ajax({
        url: path,
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + window.localStorage.getItem('token')
        },
        dataType: 'json',
        processData: false,
        contentType: false,
        success: function (examinations) {
            if (examinations.length == 0) {
                let alert = '<div id="loading" class="alert alert-info" role="alert" style="display:none">'
                    + 'No examinations found.'
                    + '</div>';
                $('#div_examinations').prepend(alert);
                $('#loading').hide();
                $('#search_examinations').find(":submit").prop('disabled', false);
            }
            else {
                $.ajax({
                    url: '/api/survey/permission',
                    type: 'GET',
                    headers: {
                        'Authorization': 'Bearer ' + window.localStorage.getItem('token')
                    },
                    dataType: 'json',
                    processData: false,
                    contentType: false,
                    success: function (permissions) {
                        for (let i = 0; i < examinations.length; i++) {
                            examinations[i].isSurveyCompleted = isCompleted(examinations[i], permissions)
                            addExaminationRow(examinations[i]);
                        }
                        $('#loading').hide();
                        $('#search_examinations').find(":submit").prop('disabled', false);
                    }
                });
            }
        },
        error: function () {
            let alert = '<div id="loading" class="alert alert-danger" role="alert" style="display:none">'
                + 'Error fetching data.'
                + '</div>';
            $('#div_examinations').prepend(alert);
            $('#loading').hide();
            $('#search_examinations').find(":submit").prop('disabled', false);
        }
    });
}

function isCompleted(examination, permissions) {
    for (let i = 0; i < permissions.length; i++) {
        if (permissions[i].id == examination.id)
            return 0;
    }
    return 1;
}


function addExaminationRow(examination) {
    var restrict_date = new Date(examination.startTime);
    restrict_date.setDate(restrict_date.getDate() - 2);
    var current_date = new Date();
    let button = '';
 
    if (examination.examinationStatus == "Finished" && examination.isSurveyCompleted == 1) {
        button = '<div class="card-footer">'
            + '<button type = "button" class="btn btn-outline-primary float-left"'
            + 'id="' + examination.id + '" onclick="showReport(this.id)"'
            + '> Report</button >'
            + '<button type = "button" class="btn btn-outline-primary float-left"'
            + 'id="' + examination.id + '" onclick="showTherapies(this.id)"'
            + '> Prescriptions </button >'
            + '</div >'; 
    }
    else if (examination.examinationStatus == "Finished" && examination.isSurveyCompleted == 0) {
        button = '<div class="card-footer">'
            + '<button type = "button" class="btn btn-primary float-right" '
            + 'onclick="window.location.href=\'/html/filling_out_the_survey.html?id=' + examination.id + '\'"'
            + '> Fill out the survey</button >'
            + '<button type = "button" class="btn btn-outline-primary float-left"'
            + 'id="' + examination.id +'" onclick="showReport(this.id)"'
            + '> Report</button >'
            + '<button type = "button" class="btn btn-outline-primary float-left"'
            + 'id="' + examination.id + '" onclick="showTherapies(this.id)"'
            + '> Prescriptions </button >'
            + '</div >';
    }
    else if (examination.examinationStatus == "Created" && current_date < restrict_date) {
        button = '<div class="card-footer" id="f' + examination.id + '">'
            + '<button name="cancelButton" type = "button" class="btn btn-danger float-right" '
            + 'id="' + examination.id + '" onclick="cancelExamination(this.id)"'
            + '> Cancel examination</button >'
            + '</div >'
            + '<div name="alert_container" class="card-footer border-top-0 p-0" id="a' + examination.id + '"></div>';
    }

    let divElement = $(
        '<div class="row">'
        + '<div class="col mb-4">'
        + '<div class="card">'
        + '<div class="card-header bg-info text-white">'
        + '<h4 class="card-title mb-0">'
        + examination.examinationType + ' on <span class="badge badge-light">' + examination.startTime.split('T')[0] + " " + examination.startTime.split('T')[1] + '</span> '
        + '</h4>'
        + '</div>'
        + '<div class="card-body p-3">'
        + '<label class="text-secondary mb-0">Doctor:</label><br>'
        + '<label>' + examination.doctorName + ' ' + examination.doctorSurname + '</label><br>'
        + '<label class="text-secondary mb-0">Room:</label><br>'
        + examination.roomId
        + '</div>' + button + '</div></div></div>'
    );

    $('div#div_examinations').append(divElement);
};

function cancelExamination(id) {
    let loading = $('<div class="alert alert-info m-2" role="alert">Cancelling...</div >');
    $('#' + id).prop("disabled", true);
    $('#a' + id).prepend(loading);

    $.ajax({
        type: "POST",
        url: "/api/examination/cancel/" + id,
        contentType: 'application/json',
        headers: {
            'Authorization': 'Bearer ' + window.localStorage.getItem('token')
        },
        success: function () {
            let alert = $('<div name="alert_msg" class="alert alert-success m-2" role="alert">Examination successfully cancelled.</div >')
            $('#f' + id).remove();
            $('#a' + id).empty();
            $('#a' + id).prepend(alert);
        },
        error: function (jqXHR) {
            let alert = $('<div class="alert alert-danger alert-dismissible fade show m-2" role="alert">Cancelling was not successful.'
                + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >')
            $('#a' + id).empty();
            $('#' + id).prop("disabled", false);
            $('#a' + id).prepend(alert);
        }
    });

};

function FillOutTheSurvey(examinationId) {
    window.location.href = '/html/filling_out_the_survey.html?id=' + examinationId;
};

function first_step_next() {

    let date = $('#dateOfExam').val();

    if (!date) {
        let alert = $('<div class="alert alert-danger alert-dismissible fade show mb-0 mt-2" role="alert">Select a date.'
            + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >');

        $('#alertsModal').prepend(alert);
        return;
    }

    var stepEvent = {
        "startSchedulingEventId": parseInt(startEventId),
        "userAge": parseInt(age),
        "userGender": parseInt(gender),
        "eventStep": parseInt(0),
        "clickEvent": parseInt(1)
    };

    $.ajax({
        url: "/api/event/step",
        type: 'POST',
        contentType: 'application/json',
        headers: {
            'Authorization': 'Bearer ' + window.localStorage.getItem('token')
        },
        data: JSON.stringify(stepEvent),
        success: function () {
            console.log("Successful!");
            currentStep = 1;
        },
        error: function () {
            console.log("An error occurred while writing the step event");
        }
    });

    document.getElementById("div_date").style.display = "none";
    document.getElementById("div_specialty").style.display = "initial";
};

function second_step_next() {

    let specialtyId = $('#specialty_name option:selected').val();

    if (!specialtyId) {
        let alert = $('<div class="alert alert-danger alert-dismissible fade show mb-0 mt-2" role="alert">Select a specialty.'
            + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >');

        $('#alertsModal').prepend(alert);
        return;
    }

    var stepEvent = {
        "startSchedulingEventId": parseInt(startEventId),
        "userAge": parseInt(age),
        "userGender": parseInt(gender),
        "eventStep": parseInt(1),
        "clickEvent": parseInt(1)
    };

    $.ajax({
        url: "/api/event/step",
        type: 'POST',
        contentType: 'application/json',
        headers: {
            'Authorization': 'Bearer ' + window.localStorage.getItem('token')
        },
        data: JSON.stringify(stepEvent),
        success: function () {
            console.log("Successful!");
            currentStep = 2;
        },
        error: function () {
            console.log("An error occurred while writing the step event");
        }
    });

    document.getElementById("div_specialty").style.display = "none";
    document.getElementById("div_doctor").style.display = "initial";
};

function third_step_next() {

    var select = document.getElementById("free_appointments");
    var length = select.options.length;
    for (i = length - 1; i >= 0; i--) {
        select.options[i] = null;
    }

    let date = $('#dateOfExam').val();
    let doctorJmbg = $('#doctor_name option:selected').val();

    if (!doctorJmbg) {
        let alert = $('<div class="alert alert-danger alert-dismissible fade show mb-0 mt-2" role="alert">Select a doctor.'
            + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >');

        $('#alertsModal').prepend(alert);
        return;
    }

    var stepEvent = {
        "startSchedulingEventId": parseInt(startEventId),
        "userAge": parseInt(age),
        "userGender": parseInt(gender),
        "eventStep": parseInt(2),
        "clickEvent": parseInt(1)
    };

    $.ajax({
        url: "/api/event/step",
        type: 'POST',
        contentType: 'application/json',
        headers: {
            'Authorization': 'Bearer ' + window.localStorage.getItem('token')
        },
        data: JSON.stringify(stepEvent),
        success: function () {
            console.log("Successful!");
            currentStep = 3;
        },
        error: function () {
            console.log("An error occurred while writing the step event");
        }
    });

    $('#loadingSchedule').show();
    $('#free_appointments').empty();
    var newData = {
        "DoctorJmbg": doctorJmbg,
        "RequiredEquipmentTypes": [],
        "EarliestDateTime": date,
        "LatestDateTime": date
    };

    var i = 0;
    $.ajax({
        url: "/api/appointment/basic-search",
        type: 'POST',
        contentType: 'application/json',
        headers: {
            'Authorization': 'Bearer ' + window.localStorage.getItem('token')
        },
        data: JSON.stringify(newData),
        success: function (appointments) {
            var appointmentStartTimes = [];
            var multipleTimes = false;
            if (appointments.length == 0) {
                let alert = $('<div class="alert alert-info alert-dismissible fade show mb-0 mt-2" role="alert">No matching free appointments found.'
                    + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >');
                $('#loadingSchedule').hide();
                $('#alertSchedule').prepend(alert);
                $('form#schedule').find(":submit").prop('disabled', false);
            }
            else {
                for (let j = 0; j < appointments.length; j++) {
                    for (let k = 0; k < appointmentStartTimes.length; k++) {
                        if (appointments[j].startTime == appointmentStartTimes[k]) {
                            multipleTimes = true;
                            break;
                        }
                    }
                    if (multipleTimes == false) {
                        let appointment = $('<option value="' + i + '">' + appointments[j].startTime + '</option>');
                        appointmentStartTimes.push(appointments[j].startTime);
                        newAppointments.push(appointment[i]);
                        $('#free_appointments').append(appointment);
                        i = i + 1;
                    } else {
                        multipleTimes = false;
                    }
                }
                $('#loadingSchedule').hide();
            }
        },
        error: function (jqXHR) {
            let alert = $('<div class="alert alert-danger alert-dismissible fade show mb-0 mt-2" role="alert">Error fetching free appointments.'
                + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >');
            $('#loadingSchedule').hide();
            $('#alertSchedule').prepend(alert);
        }
    });

    document.getElementById("div_doctor").style.display = "none";
    document.getElementById("div_appointments").style.display = "initial";
};

function first_step_previous() {

    var stepEvent = {
        "startSchedulingEventId": parseInt(startEventId),
        "userAge": parseInt(age),
        "userGender": parseInt(gender),
        "eventStep": parseInt(1),
        "clickEvent": parseInt(0)
    };

    $.ajax({
        url: "/api/event/step",
        type: 'POST',
        contentType: 'application/json',
        headers: {
            'Authorization': 'Bearer ' + window.localStorage.getItem('token')
        },
        data: JSON.stringify(stepEvent),
        success: function () {
            console.log("Successful!");
            currentStep = 0;
        },
        error: function () {
            console.log("An error occurred while writing the step event");
        }
    });

    document.getElementById("div_date").style.display = "initial";
    document.getElementById("div_specialty").style.display = "none";
};

function second_step_previous() {

    var stepEvent = {
        "startSchedulingEventId": parseInt(startEventId),
        "userAge": parseInt(age),
        "userGender": parseInt(gender),
        "eventStep": parseInt(2),
        "clickEvent": parseInt(0)
    };

    $.ajax({
        url: "/api/event/step",
        type: 'POST',
        contentType: 'application/json',
        headers: {
            'Authorization': 'Bearer ' + window.localStorage.getItem('token')
        },
        data: JSON.stringify(stepEvent),
        success: function () {
            console.log("Successful!");
            currentStep = 1;
        },
        error: function () {
            console.log("An error occurred while writing the step event");
        }
    });

    document.getElementById("div_specialty").style.display = "initial";
    document.getElementById("div_doctor").style.display = "none";
};

function third_step_previous() {

    var stepEvent = {
        "startSchedulingEventId": parseInt(startEventId),
        "userAge": parseInt(age),
        "userGender": parseInt(gender),
        "eventStep": parseInt(3),
        "clickEvent": parseInt(0)
    };

    $.ajax({
        url: "/api/event/step",
        type: 'POST',
        contentType: 'application/json',
        headers: {
            'Authorization': 'Bearer ' + window.localStorage.getItem('token')
        },
        data: JSON.stringify(stepEvent),
        success: function () {
            console.log("Successful!");
            currentStep = 2;
        },
        error: function () {
            console.log("An error occurred while writing the step event");
        }
    });

    document.getElementById("div_doctor").style.display = "initial";
    document.getElementById("div_appointments").style.display = "none";
};

function showReport(examinationId) {

    $.ajax({
        url: '/api/patient/examination/' + examinationId,
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + window.localStorage.getItem('token')
        },
        dataType: 'json',
        processData: false,
        contentType: false,
        success: function (report) {

            $.ajax({
                url: '/api/patient/examination/' + examinationId + "/therapies",
                type: 'GET',
                headers: {
                    'Authorization': 'Bearer ' + window.localStorage.getItem('token')
                },
                dataType: 'json',
                processData: false,
                contentType: false,
                success: function (therapies) {

                    let table = '<table style="margin-left:50px; margin-right:50px; margin-top:30px; margin-bottom:30px; width:300px;">'
                        + '<tr> <td>Date:</td><td>' + report.dateAndTime.split('T')[0] + '</td> </tr>'
                        + '<tr> <td>Time:</td><td>' + report.dateAndTime.split('T')[1] + '</td> </tr>'
                        + '<tr> <td>Doctor:</td><td>' + report.doctorName + ' ' + report.doctorSurname + '</td> </tr>'
                        + '<tr> <td>Anamnesis:</td><td>' + report.anamnesis + '</td> </tr>'
                        + '<tr> <td>Prescribed prescriptions:</td><td>' + therapies.length + '</td> </tr>'
                        + '</table>';

                    $('#report_content').empty();
                    $('#report_content').append(table);
                    $('#bottomModalSuccess').modal('show');

                },
                error: function () {
                    console.log("Error fetching therapies");
                }
            });
        },
        error: function () {
            console.log("Error fetching report");
        }
    });
};

function showTherapies(examinationId) {

    $.ajax({
        url: '/api/patient/examination/' + examinationId + "/therapies",
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + window.localStorage.getItem('token')
        },
        dataType: 'json',
        processData: false,
        contentType: false,
        success: function (therapies) {
            $('#therapy_content').empty();
            if (therapies.length == 0) {
                let messagge = '<label class="text-secondary" style="margin-left:24px; margin-top:30px; margin-bottom:30px">There are no prescribed prescriptions.</label>';
                $('#therapy_content').append(messagge);
            } else {
                for (let i = 0; i < therapies.length; i++) {
                    let therapy = '<table style="margin-left:50px; margin-right:50px; margin-top:30px; margin-bottom:30px; width:300px;">'
                        + '<tr> <td>Start date:</td><td>' + therapies[i].startDate.split('T')[0] + '</td> </tr>'
                        + '<tr> <td>End date:</td><td>' + therapies[i].endDate.split('T')[0] + '</td> </tr>'
                        + '<tr> <td>Drug name:</td><td>' + therapies[i].drugName + '</td> </tr>'
                        + '<tr> <td>Diagnosis:</td><td>' + therapies[i].diagnosis + '</td> </tr>'
                        + '<tr> <td>Daily dose:</td><td>' + therapies[i].dailyDose + '</td> </tr>'
                        + '</table>';
                    $('#therapy_content').append(therapy);
                }
            }
        },
        error: function () {
            console.log("Error fetching therapies");
        }
    });

    $('#topModalSuccess').modal('show');

};
