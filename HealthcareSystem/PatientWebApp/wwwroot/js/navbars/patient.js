﻿$(document).ready(function () {
    $('body').prepend($(
        '<nav class="navbar navbar-expand-lg navbar-dark bg-dark fixed-top">'
        + '<a class="navbar-brand" href="patients_home_page.html">HOSPITAL</a>'
        + ' <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">'
        + '<span class="navbar-toggler-icon"></span>'
        + ' </button>'
        + '<div class="collapse navbar-collapse" id="navbarNav">'
        + ' <ul class="navbar-nav">'
        + '  <li class="nav-item">'
        + '  <a class="nav-link" href="/html/add_feedback.html">Give feedback</a>'
        + ' </li>'
        + '  <li class="nav-item">'
        + '  <a class="nav-link" href="/html/search_documentation.html">Documentation</a>'
        + '  </li>'
        + '  <li class="nav-item">'
        + '  <a class="nav-link" href="/html/patient_examinations.html">Examinations</a>'
        + '  </li>'
        + ' <li class="nav-item">'
        + '  <a class="nav-link" href="/html/patient_profile.html">Profile</a>'
        + '  </li>'
        + ' <li class="nav-item">'
        + '  <a href="javascript:logOut();" class="nav-link">Log out</a>'
        + '  </li>'
        + ' </ul>'
        + ' </div>'
        + ' </nav>'
    ));
});