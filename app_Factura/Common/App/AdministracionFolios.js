$(document).ready(function () {



    $("#btnVerificar").on('click', function () {
        SaveFile();
    });

    $("#btnGuardar").attr("disabled", true);

    ObtieneGlosaTipoDoc =function (TipoDoc) {
        var x = "";
        switch (TipoDoc)
        {
            case "33":
                x = "Tipo (33) Factura Electrónica";
                break;
            case "34":
                x = "Tipo (34) Factura no Afecto o Exenta Electrónica";
                break;
            case "56":
                x = "Tipo (56) Nota de débito electrónica";
                break;
            case "61":
                x = "Tipo (61) Nota de crédito electrónica";
                break;
        }
        return x;
    }
    var ObjFolio = new Object();

    SaveFile = function () {
        //se utiliza javascript nativo por incopatiblidad con algunas funciones al cargar archivos
        var files = document.getElementById("txtRutaFolio").files;

        if (files.length > 0) {

            for (var i = 0; i < files.length; i++) {
 
                var datafile = FileToBase64(files[i], function (base64, name, type) {
                    //console.log(base64, name, type);

                    var Folio_xml = b64_to_utf8(base64.split(',')[1]);

                    var parser = new DOMParser();
                    var xmlDoc = parser.parseFromString(Folio_xml, "text/xml");
                    console.log(xmlDoc);
                    var FolioDesde = xmlDoc.getElementsByTagName("D")[0].childNodes[0].nodeValue;
                    var FolioHasta = xmlDoc.getElementsByTagName("H")[0].childNodes[0].nodeValue;
                    var TipoDoc = xmlDoc.getElementsByTagName("TD")[0].childNodes[0].nodeValue;
                    var FechaAutorizacion = xmlDoc.getElementsByTagName("FA")[0].childNodes[0].nodeValue;
                    var RutEmpresa = xmlDoc.getElementsByTagName("RE")[0].childNodes[0].nodeValue;

                    $("#txtRutEmpresa").val(RutEmpresa);
                    $("#txtFechaAut").val(FechaAutorizacion);
                    $("#txtTipoDoc").val(ObtieneGlosaTipoDoc(TipoDoc));
                    $("#txtFolioD").val(FolioDesde);
                    $("#txtFolioH").val(FolioHasta);

                    // ObjFolio.base64 = base64;
                    ObjFolio.NameFile = name;
                    ObjFolio.Type = type;
                    ObjFolio.TipoDocumento = TipoDoc;
                    ObjFolio.RutEmpresa = RutEmpresa;
                    ObjFolio.FolioDesde = FolioDesde;
                    ObjFolio.FolioHasta = FolioHasta;
                    ObjFolio.xml = Folio_xml;
                    ObjFolio.Ambiente = $("#_SES_Ambiente").val();;
                    if (RutEmpresa == $("#_SES_RutEmpresa").val()) {
                        $("#btnGuardar").attr("disabled", false);
                    } else
                    {
                        ModalElement.Create();
                        ModalElement.Class("danger");
                        ModalElement.Header("Error!");
                        ModalElement.Message("el folio no coincide con la empresa, verifique el rut de la empresa !");
                        ModalElement.Show();
                    }
                });
            }
        }
    }

    $("#btnGuardar").off().on('click', function () {
        var _ObjFolio = { folio: {} };
        _ObjFolio.folio = ObjFolio;

        fnSaveFolio(_ObjFolio, function (r) {
            console.log(r);
            switch (r.code) {
                case 200:
                    ModalElement.Create();
                    ModalElement.Class("info");
                    ModalElement.Header("Informacion");
                    ModalElement.Message(r.ObjectResponse);
                    ModalElement.Show();
                    break;
                case 500:
                    ModalElement.Create();
                    ModalElement.Class("danger");
                    ModalElement.Header("Error!");
                    ModalElement.Message(r.ObjectResponse);
                    ModalElement.Show();
                    break;
            }
        });
    });

});