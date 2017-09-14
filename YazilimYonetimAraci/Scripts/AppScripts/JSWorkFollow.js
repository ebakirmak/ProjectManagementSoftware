$(document).ready(function () {
    GetUsers();
    function GetUsers() {
        $.post("/User/GetAnalyzers", function (data) {
            ////KonumID nesnesinin option değerleri kaldırılıyor.
            //$('#AnalizUserID').find('option').remove().end();
            //Yeni Option değerleri giriliyor.
            $.each(data, function (i, eleman) {
                $('#AnalizUserID').append($('<option>', {
                    text: eleman.UserName + " " + eleman.UserSurname + " / " + eleman.UserNickname,
                    value: eleman.UsersID
                }));
            });

        });

        $.post("/User/GetDatabaseManagement", function (data) {
            ////KonumID nesnesinin option değerleri kaldırılıyor.
            //$('#TableUserID').find('option').remove().end();
            //Yeni Option değerleri giriliyor.
            $.each(data, function (i, eleman) {
                $('#TableUserID').append($('<option>', {
                    text: eleman.UserName + " " + eleman.UserSurname + " / " + eleman.UserNickname,
                    value: eleman.UsersID
                }));
            });

        });

        $.post("/User/GetProcedure", function (data) {
            ////KonumID nesnesinin option değerleri kaldırılıyor.
            //$('#ProcedureUserID').find('option').remove().end();
            //Yeni Option değerleri giriliyor.
            $.each(data, function (i, eleman) {
                $('#ProcedureUserID').append($('<option>', {
                    text: eleman.UserName + " " + eleman.UserSurname + " / " + eleman.UserNickname,
                    value: eleman.UsersID
                }));
            });

        });
        $.post("/User/GetDllList", function (data) {
            ////KonumID nesnesinin option değerleri kaldırılıyor.
            //$('#DllListUserID').find('option').remove().end();
            //Yeni Option değerleri giriliyor.
            $.each(data, function (i, eleman) {
                $('#DllListUserID').append($('<option>', {
                    text: eleman.UserName + " " + eleman.UserSurname + " / " + eleman.UserNickname,
                    value: eleman.UsersID
                }));
            });

        });
        $.post("/User/GetFrontEnd", function (data) {
            ////KonumID nesnesinin option değerleri kaldırılıyor.
            //$('#ArayüzUserID').find('option').remove().end();
            //Yeni Option değerleri giriliyor.
            $.each(data, function (i, eleman) {
                $('#ArayüzUserID').append($('<option>', {
                    text: eleman.UserName + " " + eleman.UserSurname + " / " + eleman.UserNickname,
                    value: eleman.UsersID
                }));
            });

        });
        $.post("/User/GetTest", function (data) {
            ////KonumID nesnesinin option değerleri kaldırılıyor.
            //$('#TestUserID').find('option').remove().end();
            //Yeni Option değerleri giriliyor.
            $.each(data, function (i, eleman) {
                $('#TestUserID').append($('<option>', {
                    text: eleman.UserName + " " + eleman.UserSurname + " / " + eleman.UserNickname,
                    value: eleman.UsersID
                }));
            });

        });
    };

});