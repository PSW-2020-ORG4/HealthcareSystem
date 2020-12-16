$(document).ready(function () {

    $.ajax({
        url: '/api/feedback/published-feedbacks',
        type: 'GET',
        dataType: 'json',
        processData: false,
        contentType: false,
        success: function (data) {
            if (data.length == 0) {
                let alert = $('<div class="alert alert-info m-4" role="alert">There is currently no feedback.</div >')
                $('div#view_feedbacks').append(alert);
            }
            else {
                for (let i = 0; i < data.length; i++) {
                    addFeedback(data[i]);
                }
            }
        },
        error: function () {
            let alert = $('<div class="alert alert-danger m-4" role="alert">Error fetching data.</div >')
            $('div#view_feedbacks').append(alert);
        }
    });
});

function addFeedback(feedback) {

    let name_surname = feedback.commentatorName + " " + feedback.commentatorSurname;

    if (feedback.commentatorName == "") {
        name_surname = "Anonymous";
    }    

    let new_feedback = $('<div class="row"><div class="col p-4"><div class="card"><div class="card-header bg-info text-white">'
        + feedback.sendingDate
        + '</div>'
        + '<div class="card-body bg-light"><blockquote class="blockquote mb-0"><p>'
        + feedback.comment + ' </p>'
        + '<footer class="blockquote-footer text-info"><cite>'
        + name_surname + '</cite></footer></blockquote></div></div></div></div>');

    $('div#view_feedbacks').append(new_feedback);
}