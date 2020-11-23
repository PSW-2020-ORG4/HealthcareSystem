$(document).ready(function () {
    $('#no_type').prop("selected", true);

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
    $('form#search_prescription').submit(function (event) {
        event.preventDefault()
        let start_date = "2000-1-1 00:00"
        let end_date = "2000-1-1 00:00"
        let doctor = "Peric"
        let drug = "brufen"
        let id = 1
        let anamnesis = "anamnaza"
        let dose = 2
        let idDrug = 2
        let idExamination = 1
        let doctorName = "Ime"
        if ($('#end_date').val() != "") {
            start_date = $('#end_date').val()
        }
        if ($('#start_date').val() != "") {
            end_date = $('#start_date').val()
        }
        if ($('#doctor').val() != "") {
            doctor = $('#doctor').val()
        }
        if ($('#drug').val() != "") {
            drug = $('#drug').val()
        }

        let idPatientCard = 1;
        let jmbg = "1";
        $.ajax({
            url: "/api/patient/" + jmbg,
            type: 'GET',
            dataType: 'json',
            processData: false,
            contentType: false,
            success: function (patientDto) {
                jmbg = patientDto.jmbg;
                idPatientCard = patientDto.patientCardId;
                alert(idPatientCard);

                $.ajax({
                    url: "/api/examination/" + jmbg,
                    type: 'GET',
                    dataType: 'json',
                    processData: false,
                    contentType: false,
                    success: function (examinations) {
                        $('#border_prescriptions').empty();
                        for (let i = 0; i < examinations.length; i++) {
                            let examination = examinations[i].id;
                           
                            $.ajax({
                                type: "POST",
                                url: "/api/therapy/" + parseInt(examination),
                                contentType: "application/json",
                                data: JSON.stringify({
                                    StartDate: start_date,
                                    EndDate: end_date,
                                    DoctorSurname: doctor,
                                    DrugName: drug,
                                    PatientJmbg: jmbg,
                                    Id: id,
                                    Anamnesis: anamnesis,
                                    DailyDose: dose,
                                    IdDrug: idDrug,
                                    IdExamination: examination,
                                    DoctorName: doctorName
                                }),
                                success: function (therapies) {
                                    $('#border_prescriptions').empty();
                                    for (let i = 0; i < therapies.length; i++) {

                                        let divElement = $('<div class="border_perscription"><table class="table_perscription">'
                                            + ' <tr> <th> <p>Therapy  number  </p></th><td><p><b>' + i + '</b> :</p></td></tr > '
                                            + ' <tr> <th> <p>Start date:</p></th><td>' + therapies[i].id + '</td></tr > '
                                           // + ' <tr> <th> <p>End date:</p></th><td>' + therapies[i].jmbg + '</td></tr > '
                                            // + ' <tr> <th style="width:250px;"><p style="margin-left:50px;">End date:</p></th><td>' + therapies[i].EndDate + '</td></tr > '
                                            //  + ' <tr> <th style="width:250px;"><p style="margin-left:50px;">Daily dose:</p></th><td>' + therapies[i].DailyDose + '</td></tr > '
                                            //  + ' <tr> <th style="width:250px;"><p style="margin-left:50px;">Doctor:</p></th><td>' + therapies[i].Examination.DoctorJmbg + '</td></tr > '
                                            //  + ' <tr> <th style="width:250px;"><p style="margin-left:50px;">Drug:</p></th><td>' + therapies[i].Drug.Name + '</td></tr > '
                                            + ' </table ></div > ');
                                        $('div#border_prescriptions').append(divElement);

                                    }
                                },
                                error: function (error) {
                                    alert("Error getting therapies")
                                }
                            }); 




                           /* let divElement = $('<div class="border_perscription"><table class="table_perscription">'
                                + ' <tr> <th> <p>Therapy  number  </p></th><td><p><b>' + i + '</b> :</p></td></tr > '
                                + ' <tr> <th> <p>Start date:</p></th><td>' + examinations[i].id + '</td></tr > '
                                //  + ' <tr> <th> <p>End date:</p></th><td>' + exeminations[i].doctorSurname + '</td></tr > '
                                // + ' <tr> <th style="width:250px;"><p style="margin-left:50px;">End date:</p></th><td>' + therapies[i].EndDate + '</td></tr > '
                                //  + ' <tr> <th style="width:250px;"><p style="margin-left:50px;">Daily dose:</p></th><td>' + therapies[i].DailyDose + '</td></tr > '
                                //  + ' <tr> <th style="width:250px;"><p style="margin-left:50px;">Doctor:</p></th><td>' + therapies[i].Examination.DoctorJmbg + '</td></tr > '
                                //  + ' <tr> <th style="width:250px;"><p style="margin-left:50px;">Drug:</p></th><td>' + therapies[i].Drug.Name + '</td></tr > '
                                + ' </table ></div > ');
                            $('div#border_prescriptions').append(divElement); */

                         }
                    },
                    error: function (error) {
                        alert("Error getting examinations")
                    }
                });
            },
            error: function () {

                alert("Error getting patient");
            }
        });
    });
               /* $.ajax({
                    type: "POST",
                    url: "/api/therapy",
                    contentType: "application/json",
                    data: JSON.stringify({
                        StartDate: start_date,
                        EndDate: end_date,
                        DoctorSurname: doctor,
                        DrugName: drug,
                        PatientJmbg: jmbg,
                        Id: id,
                        Anamnesis: anamnesis,
                        DailyDose: dose,
                        IdDrug: idDrug,
                        IdExamination: idExamination,
                        DoctorName: doctorName
                    }),
                    success: function (therapies) {
                        $('#border_prescriptions').empty();
                        for (let i = 0; i < therapies.length; i++) {

                            let divElement = $('<div class="border_perscription"><table class="table_perscription">'
                                + ' <tr> <th> <p>Therapy  number  </p></th><td><p><b>' + i + '</b> :</p></td></tr > '
                                + ' <tr> <th> <p>Start date:</p></th><td>' + therapies[i].start_date + '</td></tr > '
                                + ' <tr> <th> <p>End date:</p></th><td>' + therapies[i].jmbg + '</td></tr > '
                                // + ' <tr> <th style="width:250px;"><p style="margin-left:50px;">End date:</p></th><td>' + therapies[i].EndDate + '</td></tr > '
                                //  + ' <tr> <th style="width:250px;"><p style="margin-left:50px;">Daily dose:</p></th><td>' + therapies[i].DailyDose + '</td></tr > '
                                //  + ' <tr> <th style="width:250px;"><p style="margin-left:50px;">Doctor:</p></th><td>' + therapies[i].Examination.DoctorJmbg + '</td></tr > '
                                //  + ' <tr> <th style="width:250px;"><p style="margin-left:50px;">Drug:</p></th><td>' + therapies[i].Drug.Name + '</td></tr > '
                                + ' </table ></div > ');
                            $('div#border_prescriptions').append(divElement);

                        }
                    },
                    error: function (error) {
                        alert("Error getting therapies")
                    }
                });  */

 });
