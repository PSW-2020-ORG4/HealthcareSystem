﻿$(document).ready(function () {
    $('body').prepend($(
        '<nav class="navbar navbar-expand-lg navbar-dark bg-dark fixed-top">'
        + '<a class="navbar-brand" href="/html/admins_home_page.html">HOSPITAL</a>'
        + ' <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">'
        + '<span class="navbar-toggler-icon"></span>'
        + ' </button>'
        + '<div class="collapse navbar-collapse" id="navbarNav">'
        + ' <ul class="navbar-nav">'
        + '  <li class="nav-item">'
        + '  <a class="nav-link" href="/html/survey_result.html">Survey result</a>'
        + '  </li>'
        + '  <li class="nav-item">'
        + '  <a class="nav-link" href="/html/malicious_patients.html">Malicious patients</a>'
        + '  </li>'
        + ' <li class="nav-item">'
        + '  <a href="javascript:logOut();" class="nav-link">Log out</a>'
        + '  </li>'
        + ' </ul>'
        + ' </div>'
        + ' </nav>'
    ));
});