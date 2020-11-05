$(document).ready(function () {

    $.ajax({
        url: '/api/feedback/published-feedbacks',
        type: 'GET',
        dataType: 'json',
        processData: false,
        contentType: false,
        success: function (data) {
            for (let i = 0; i < data.length; i++) {
                addFeedback(data[i]);
            }
        },
        error: function () {
            console.log("Error getting published feedbacks")
        }
    });
});

function addFeedback(feedback) {

    let name_surname = feedback.commentatorName + " " + feedback.commentatorSurname;

    if (feedback.commentatorName == "") {
        name_surname = "Anonymous";
    }

    let new_feedback = $('<div class="testimonial"><h5 class="font-weight-bold dark-grey-text mt-4">' + name_surname + '</h5 >'
        + '<p class="font-weight-normal dark-grey-text"><i class="fas fa-quote-left pr-2"></i>'
        + feedback.comment + ' </p></div >');

    $('div#view_feedbacks').append(new_feedback);
}