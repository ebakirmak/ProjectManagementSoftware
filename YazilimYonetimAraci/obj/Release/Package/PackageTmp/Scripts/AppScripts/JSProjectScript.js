$(document).ready(function () {
    var para = $("#txtBudget").val();
    if (para != null){
        var paraDizi = para.split(',');
        para = paraDizi[0];
        $("#txtBudget").val(para);
    }

    var tarih = $("#txtFinishDate").val();
    if (tarih != null) {
        var tarihDizi = tarih.split(' ');
        tarih = tarihDizi[0];
        $("#txtFinishDate").val(tarih);
    }
   

    $.post("/Project/ProjectManagers", function (data) {
        ////KonumID nesnesinin option değerleri kaldırılıyor.
        $('#ManagerID').find('option').remove().end().append('<option value="">PROJE YÖNETİCİSİ SEÇİNİZ</option>').val('');
        //Yeni Option değerleri giriliyor.
        $.each(data, function (i, eleman) {
            $('#ManagerID').append($('<option>', {
                value: eleman.ID,
                text: eleman.Name
            }));
        });

    });

});