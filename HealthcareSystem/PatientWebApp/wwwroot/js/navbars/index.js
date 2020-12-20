﻿$(document).ready(function () {
    $('body').prepend($(
        '<nav class="navbar navbar-expand-lg navbar-dark bg-dark fixed-top">'
        + '<a class="navbar-brand" href="index.html">HOSPITAL</a>'
        + ' <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">'
        + '<span class="navbar-toggler-icon"></span>'
        + ' </button>'
        + '<div class="collapse navbar-collapse" id="navbarNav">'
        + ' <ul class="navbar-nav">'
        + '  <li class="nav-item">'
        + '  <a class="nav-link" href="/html/patient_registration.html">Register</a>'
        + ' </li>'
        + '  <li class="nav-item">'
        + '  <a class="nav-link" href="/html/patients_home_page.html">Patient</a>'
        + '  </li>'
        + '  <li class="nav-item">'
        + '  <a class="nav-link" href="/html/admins_home_page.html">Administrator</a>'
        + '  </li>'
        + ' </ul>'
        + ' </div>'
        + ' </nav>'
    ));
});