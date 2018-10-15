$(document).ready(function () {
    var TblListaDoc = [
        { "data": "idDte", "title": "idDte" },
        { "data": "RutEmpresa", "title": "RutEmpresa" },
        { "data": "RutEmisor", "title": "RutEmisor" },
        { "data": "RutReceptor", "title": "RutReceptor" },
        { "data": "NombreReceptor", "title": "NombreReceptor" },
        { "data": "TipoDte", "title": "TipoDte" }, 
        { "data": "DescripcionTipoDte", "title": "DescripcionTipoDte" }, 
        { "data": "Folio", "title": "Folio" }, 
        { "data": "MontoTotal", "title": "MontoTotal" },
        { "data": "FechaEmision", "title": "FechaEmision" },
        { "data": "FechaCreacion", "title": "FechaCreacion" },
        { "data": "TipoOperacion", "title": "TipoOperacion" },
        {
            "title": "Accion",
            "mRender": function (data, type, row) {
                return '<center><a class="btn btn-success btn-sm OpenDoc" ><i class="" style="color:white" aria-hidden="true">Abrir</i></a></center>';
            }
        }
    ];
    var RutEmpresa = $("#_SES_RutEmpresa").val().replace('-', '');
    RutEmpresa=  RutEmpresa.substring(0, RutEmpresa.length - 1);
    var obj = new Object();
    obj.RutEmpresa = RutEmpresa;

    var objTblListaDoc = null;
    $.ajax({
        type: 'POST',
        url:  'ListaDocumentos.aspx/sp_sel_Documentos',
        data: JSON.stringify(obj),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (msg) {
          //  console.log(JSON.parse(msg.d));

        objTblListaDoc = MakeTable(
                10,
                JSON.parse(msg.d),
                TblListaDoc,
                "#tblDocumentos"
            );

        },
        error: function (request, status, error) {
            console.log(request.responseText);
        }
    });

    $('#tblDocumentos tbody').on('click', '.OpenDoc', function () {
        var me = this;
        var RowIndex = objTblListaDoc.row($(this).parents('tr')).index();
        var dataRow = objTblListaDoc.row($(me).parents('tr')).data();
        sessionStorage.setItem("DocumentoOrigen", JSON.stringify( dataRow));
        console.log(RowIndex, dataRow);
        window.location.href = 'EmisionDocumentos.aspx';    
        
     
    });

});