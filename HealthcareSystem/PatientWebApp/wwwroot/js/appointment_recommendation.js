var newAppointments = [];

$(document).ready(function () {
    checkUserRole("Patient");

    var dtToday = new Date();
    var month = dtToday.getMonth() + 1;
    var day = dtToday.getDate() + 1;
    var year = dtToday.getFullYear();
    if (month < 10)
        month = '0' + month.toString();
    if (day < 10)
        day = '0' + day.toString();

    var minDate = year + '-' + month + '-' + day;

    $('#dateFrom').attr('min', minDate);
    $('#dateTo').attr('min', minDate);

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
            let alert = $('<div class="alert alert-danger alert-dismissible fade show mb-0 mt-2" role="alert">' + jqXHR.responseJSON +  '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >')
            $('#alertSchedule').prepend(alert);
        }
    });

    $('#specialty_name').change(changeSpecialty);

    $('form#schedule').submit(function (event) {
        event.preventDefault();

        let doctorJmbg = $('#doctor_name option:selected').val();
        let earliestDateTime = $('#dateFrom').val();
        let latestDateTime = $('#dateTo').val();
        let priority = $('input[name=priority]:checked').val();
        let specialtyId = $('#specialty_name option:selected').val();

        if (!doctorJmbg || !earliestDateTime || !latestDateTime || !specialtyId || !priority)
            return;

        specialtyId = parseInt(specialtyId);
        priority = parseInt(priority);

        $('form#schedule').find(":submit").prop('disabled', true);
        $('#loadingSchedule').show();
        $('#free_appointments').empty();

        var initialParameters = {
            "DoctorJmbg": doctorJmbg,
            "RequiredEquipmentTypes": [],
            "EarliestDateTime": earliestDateTime,
            "LatestDateTime": latestDateTime
        };

        var newData = {
            "SpecialtyId": specialtyId,
            "Priority": priority,
            "InitialParameters": initialParameters
        };

        var i = 0;
        $.ajax({
            url: "/api/appointment/priority-search",
            type: 'POST',
            contentType: 'application/json',
            headers: {
                'Authorization': 'Bearer ' + window.localStorage.getItem('token')
            },
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
                        let doctorNameAndSurname = a.doctorName + ' ' + a.doctorSurname;
                        let appointment = $('<option value="' + i + '">' + a.startTime + ',  dr ' + doctorNameAndSurname + '</option>');
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


function scheduleExamination() {

    $('#modalButton').prop('disabled', true);
    $('#modalLoading').show();
    let a = $('#free_appointments option:selected').val();
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
                },
                error: function (jqXHR) {
                    let alert = $('<div class="alert alert-danger alert-dismissible fade show mb-0 mt-2" role="alert">Examination scheduling failed.'
                        + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >');
                    $('#alertModal').prepend(alert);
                    $('#modalLoading').hide();
                    $('#modalButton').prop('disabled', false);
                }
            });
        },
        error: function () {
            let alert = $('<div class="alert alert-danger alert-dismissible fade show m-1" role="alert">Error getting patient.'
                + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + '</div >')
            $('#loading').hide();
            $('#schedule').find(":submit").prop('disabled', false);
            $('#alert').prepend(alert);
        }
    });
}