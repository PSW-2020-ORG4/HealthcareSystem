var newAppointments = [];
var jmbg = "";
$(document).ready(function () {
    var token = window.localStorage.getItem('token');
    if (token != null) {
        $.ajax({
            url: "/api/user/logged",
            type: 'GET',
            dataType: 'json',
            processData: false,
            contentType: 'application/json',
            data: JSON.stringify(token),
            success: function (loggedUser) {
                if (loggedUser.role != "Patient") {
                    alert('Access denied!');
                    return;
                }
                jmbg = loggedUser.jmbg;
            },
            error: function () {
                alert('Error getting logged user!');
            }
        });
    }
    else {
        alert('Unlogged user!');
        return;
    }

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
            if (specialtes) {
                for (let s of specialtes) {
                    let specialty = $('<option value="' + s.id + '">' + s.name + '</option>');
                    $('#specialty_name').append(specialty);
                }
                changeSpecialty();
            }
        },
        error: function () {
            let alert = $('<div class="alert alert-danger alert-dismissible fade show mb-0 mt-2" role="alert">Error fetching specialties.'
                + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >')
            $('#alertSchedule').prepend(alert);
        }
    });

    $('#specialty_name').change(changeSpecialty);

    getExaminations('/api/examination/following/' + jmbg);

    $('form#search_examinations').submit(function (event) {
        event.preventDefault();
        $('#search_examinations').find(":submit").prop('disabled', true);
        $('#loading').show();
        $('#div_examinations').empty();

        let exam_status = $('#examination_status option:selected').val();

        if (exam_status == "following") {
            getExaminations('/api/examination/following/' + jmbg);
        }
        else if (exam_status == "previous") {
            getExaminations('/api/examination/previous/' + jmbg);
        }
        else {
            getExaminations('/api/examination/cancelled/' + jmbg);
        }
    });

    $('form#schedule').submit(function (event) {
        event.preventDefault();

        let date = $('#dateOfExam').val();
        let doctorJmbg = $('#doctor_name option:selected').val();
        let specialtyId = $('#specialty_name option:selected').val();

        if (!date || !doctorJmbg || !specialtyId)
            return;

        $('form#schedule').find(":submit").prop('disabled', true);
        $('#loadingSchedule').show();
        $('#free_appointments').empty();
        var newData = {
            "PatientCardId": 1,
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
            data: JSON.stringify(newData),
            success: function (appointments) {
                newAppointments = appointments;
                if (appointments.length == 0) {
                    let alert = $('<div class="alert alert-info alert-dismissible fade show mb-0 mt-2" role="alert">No matching free appointments found.'
                        + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >');
                    $('#loadingSchedule').hide();
                    $('#alertSchedule').prepend(alert);
                    $('form#schedule').find(":submit").prop('disabled', false);
                }
                else {
                    for (let a of appointments) {
                        let appointment = $('<option value="' + i + '">' + a.dateAndTime + '</option>');
                        $('#free_appointments').append(appointment);
                        i = i + 1;
                    }
                    $('#loadingSchedule').hide();
                    $('form#schedule').find(":submit").prop('disabled', false);
                    $('#centralModalSuccess').modal('show');
                }
            },
            error: function (jqXHR) {
                let alert = $('<div class="alert alert-danger alert-dismissible fade show mb-0 mt-2" role="alert">Error fetching free appointments.'
                    + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >');
                $('#loadingSchedule').hide();
                $('#alertSchedule').prepend(alert);
                $('form#schedule').find(":submit").prop('disabled', false);
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
        "Type": appointment.type,
        "DateAndTime": appointment.dateAndTime,
        "DoctorJmbg": appointment.doctorJmbg,
        "IdRoom": appointment.idRoom,
        "Anamnesis": "",
        "PatientCardId": appointment.patientCardId,
        "PatientJmbg": jmbg,
        "ExaminationStatus": 0,
        "IsSurveyCompleted": false
    };

    $.ajax({
        url: "/api/examination",
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(newData),
        success: function () {
            let alert = $('<div class="alert alert-success alert-dismissible fade show mb-0 mt-2" role="alert">Examination successfully scheduled.'
                + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >');
            $('#alertSchedule').prepend(alert);
            $('#centralModalSuccess').modal('hide');
            $('#modalLoading').hide();
            $('#modalButton').prop('disabled', false);
            window.location.reload();
        },
        error: function (jqXHR) {
            let alert = $('<div class="alert alert-danger alert-dismissible fade show mb-0 mt-2" role="alert">Examination scheduling failed.'
                + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >');
            $('#alertModal').prepend(alert);
            $('#modalLoading').hide();
            $('#modalButton').prop('disabled', false);
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
                        let doctorName = $('<option value="' + doctor.jmbg + '">' + doctor.name + ' ' + doctor.surname + '</option>');
                        $('#doctor_name').append(doctorName);
                    },
                    error: function () {
                        let alert = $('<div class="alert alert-danger alert-dismissible fade show mb-0 mt-2" role="alert">Error fetching doctors.'
                            + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >');
                        $('#alertSchedule').prepend(alert);
                    }
                });
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


function addExaminationRow(examination) {
    let type = '';
    if (examination.type == "GENERAL")
        type = "Examination";
    else
        type = "Surgery";

    var restrict_date = new Date(examination.dateAndTime);
    restrict_date.setDate(restrict_date.getDate() - 2);
    var current_date = new Date();
    let button = '';
    if (examination.examinationStatus == 2 && examination.isSurveyCompleted == 0) {
        button = '<div class="card-footer">'
            + '<button type = "button" class="btn btn-success float-right" '
            + 'onclick="window.location.href=\'/html/filling_out_the_survey.html?id=' + examination.id + '\'"'
            + '> Fill out the survey</button >'
            + '</div >';
    }
    else if (examination.examinationStatus == 0 && current_date < restrict_date) {
        button = '<div class="card-footer" id="f' + examination.id + '">'
            + '<button type = "button" class="btn btn-danger float-right" '
            + 'id="' + examination.id + '" onclick="cancelExamination(this.id)"'
            + '> Cancel examination</button >'
            + '</div >'
            + '<div class="card-footer border-top-0 p-0" id="a' + examination.id + '"></div>';
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
        + '</div>' + button + '</div></div></div>'
    );

    $('div#div_examinations').append(divElement);
};

function cancelExamination(id) {
    let loading = $('<div class="alert alert-info m-2" role="alert">Cancelling...</div >');
    $('#' + id).prop("disabled", true);
    $('#a' + id).prepend(loading);

    $.ajax({
        type: "PUT",
        url: "/api/examination/cancel/" + id,
        success: function () {
            let alert = $('<div class="alert alert-success m-2" role="alert">Examination successfully cancelled.</div >')
            $('#f' + id).remove();
            $('#a' + id).empty();
            $('#a' + id).prepend(alert);
            window.location.reload();
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
