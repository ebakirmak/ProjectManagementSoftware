$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "/User/GetUserNames",     
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $("#userNameSurname").text(response.UserName + " " + response.UserSurname);

        },
        error: function (response) {
            $("#userNameSurname").val(response.error);
        }
    });
});