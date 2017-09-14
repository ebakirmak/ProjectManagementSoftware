$(document).ready(function () {
    $("#ProcessType").change(function () {
        ProcessType();
    });
    $("#ProcessManagerID").mouseover(function () {
        GetUsers();
    });
    function ProcessType() {
      
        if ($("#ProcessType").val() == "ANA SÜREÇ") {
          
            $('#ParentID').find('option').remove().end().append('<option value=""></option>').val('');
            $('#ParentID').append($('<option>', {
                text: "Ana Süreç",
                value: 0
            }));
        }
        else if ($("#ProcessType").val() == "SÜREÇ") {
           
            var projectID=$("#projectID").val();           
            $.post("/Process/MainProcessGet", { ProjectID: projectID }, function (data) {
                ////KonumID nesnesinin option değerleri kaldırılıyor.
                $('#ParentID').find('option').remove().end().append('<option value=""></option>').val('');
                //Yeni Option değerleri giriliyor.
                $.each(data, function (i, eleman) {
                    $('#ParentID').append($('<option>', {
                        text: eleman.ProcessName,
                        value: eleman.ProcessID
                    }));
                });

            });
        }
        else if ($("#ProcessType").val() == "ALT SÜREÇ") {
            var projectID = $("#projectID").val();
            $.post("/Process/ProcessGet", { ProjectID:projectID }, function (data) {
                ////KonumID nesnesinin option değerleri kaldırılıyor.
                $('#ParentID').find('option').remove().end().append('<option value=""></option>').val('');
                //Yeni Option değerleri giriliyor.
                $.each(data, function (i, eleman) {
                    $('#ParentID').append($('<option>', {
                        text: eleman.ProcessName,
                        value: eleman.ProcessID
                    }));
                });

            });
        }
      
    }
    
    function GetUsers() {
        $.post("/User/GetUsers", function (data) {
                ////KonumID nesnesinin option değerleri kaldırılıyor.
            $('#ProcessManagerID').find('option').remove().end();
                //Yeni Option değerleri giriliyor.
                $.each(data, function (i, eleman) {
                    $('#ProcessManagerID').append($('<option>', {
                        text: eleman.UserName+" "+eleman.UserSurname+" / "+eleman.UserNickname,
                        value: eleman.UsersID
                    }));
                });

            });
    }
    
});