$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "/User/GetUserNames",     
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $("#userNameSurname").text(response.UserName + " " + response.UserSurname);

        },
        error: function (response) {
            $("#userNameSurname").text(response.error);
        }
    });

    $.ajax({
        type: "POST",
        url: "/UserRoles/RoleGet",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $("#userRole").text(response);

        },
        error: function (response) {
          
        }
    });
});