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
});