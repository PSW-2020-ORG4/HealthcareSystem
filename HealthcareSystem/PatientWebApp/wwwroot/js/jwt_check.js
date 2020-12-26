﻿var token = "";
$(document).ready(function () {
    token = window.localStorage.getItem('token');
    if (token == null && window.location.pathname != "/html/login.html") {
        window.location.href = "login.html";
    }
});
function getRoleFromToken() {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return (JSON.parse(jsonPayload)).role;
}
function checkUserRole(trueRole) {
    var role = getRoleFromToken();
    if (role != trueRole) {
        if (role == "Patient") {
            window.location.href = "patients_home_page.html";
        }
        else if (role == "Admin") {
            window.location.href = "admins_home_page.html";
        }
        else {
            window.location.href = "login.html";
        }
    }
}
function redirectUser(new_token) {
    token = new_token;
    var role = getRoleFromToken();

    if (role == "Patient") {
        window.location.href = "patients_home_page.html";
    }
    if (role == "Admin") {
        window.location.href = "admins_home_page.html";
    }
}

function logOut() {
    window.localStorage.clear();
    window.location.href = "index.html";
}