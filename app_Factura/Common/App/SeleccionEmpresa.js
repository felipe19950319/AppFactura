$(document).ready(function () {
    $("#btnSelectEmpresa").on('click', function () {
        var Empresa = $("#ContentPlaceHolder1_DropEmpresa option:selected").text();
        var id_emp = $("#ContentPlaceHolder1_DropEmpresa").val();
        var ambiente = $("#ContentPlaceHolder1_DropAmbiente").val();

        $.ajax({
            type: 'POST',
            url: 'SeleccionEmpresa.aspx/ajax_SeleccionEmpresa',
            data: JSON.stringify({ Id_emp: id_emp, Razon_social: Empresa, Ambiente: ambiente }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (msg) {
                console.log(msg);
                window.location.href = "MenuPrincipal.aspx";
            }
        });

    });

});