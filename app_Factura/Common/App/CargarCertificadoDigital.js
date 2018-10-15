$(document).ready(function () {

   

    $("#btnGetFile").on('click', function () {
        SaveFile();
    });

    SaveFile = function () {
        //se utiliza javascript nativo por incopatiblidad con algunas funciones al cargar archivos
        var files = document.getElementById("txtRutaCertificado").files;

        if (files.length > 0) {

            for (var i = 0 ; i < files.length; i++) {
                var datafile = FileToBase64(files[i], function (base64, name, type) {
                    console.log(base64, name, type);
                    var certificadoDigital = new Object();
                   
                    certificadoDigital.RutEmpresa = $("#_SES_RutEmpresa").val();
                    certificadoDigital.Password = $("#txtPassCertificado").val();
                    certificadoDigital.Path = "";
                    certificadoDigital.TypeFile = type;
                    certificadoDigital.Base64 = base64.split(",")[1];

                    var ObjCertificadoDigital = { certificadoDigital: {} };
                    ObjCertificadoDigital.certificadoDigital = certificadoDigital;
                    
                    result = fnSavePfx(ObjCertificadoDigital, function (r) {
                        console.log(r);
                      //  var Response = JSON.parse(r.ObjectResponse);

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
                            case 0:
                                ModalElement.Create();
                                ModalElement.Class("danger");
                                ModalElement.Header("Error!");
                                ModalElement.Message("ya existe un certificado asociado para la empresa seleccionada.");
                                ModalElement.Show();
                                break;

                        }

                  
                    });
              
                });
            }
        }
    }

});