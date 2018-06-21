$(document).ready(function () {
    /*FELIPE PASACHE*/
    //se crea esta clase para unificar la llamada de los modales a modo de ejemplo se puede ver el funcionamiento mas arriba 
    //la idea es evitar la redundancia de codigo tanto html como js para hacer el codigo mas legible y expedito de leer F.pasache
    //se generan en tiempo de ejecucion y se destruyen cuando ya no se ocupan
    //llamada de ejemplo
    /*
   ------------------------------- modal de alerta
    ModalElement.Create(); 
    ModalElement.Class("success"); puede ser info,warning o danger
    ModalElement.Header("hola"); 
    ModalElement.Message("hola mundo"); 
    ModalElement.Show(); 

    ------------------------------ modal de confirmacion
     ModalElement.Create();
     ModalElement.Class("success"); 
     ModalElement.Header("hola"); 
     ModalElement.Message("hola mundo"); 
     --agregamos el confirm por defecto esta en false si se pone en true activara el modal de confirmacion el cual tiene un callback para captar la respuesta del usuario
     
     ModalElement.Confirm(true , function(r){ 

     if (r==true){ console.log("estoy ok"); }

     else{ console.log("no estoy ok"); } 
     
     });
     
     ModalElement.Show();

    */
    var TmplId = "";
    var IdMessage = "";
    var IdButton = "";
    var Confirm = false;

    ModalElement =
     {
         Create: function () {
             var x = ModalTemplate.CreateModal();
             TmplId = x.IdTemplate;
             IdMessage = x.IdMessage;
             IdButton = x.IdButton;
             $("body").append(x.HtmlTemplate);
             $(".DeleteModal").on('click', function () {
                 var id_ = $(this).attr("id");
                 id_ = id_.replace("Button", "ModalComponent");
                 $("#" + id_).remove();
                 $(".modal-backdrop").attr("class", "modal-backdrop fade out");
                 window.setTimeout(function () {
                     $(".modal-backdrop").remove();
                     //se produce un bug por lo tanto forzamos a que el scroll sea visible
                     document.body.style.overflow = 'visible';
                 }, 400);
             });
         },

         Header: function (x) {
             $("#" + TmplId + " h4").html(x);
         },

         Class: function (x) {
             switch (x) {
                 case "success":
                     $("#" + IdMessage).attr("class", "alert alert-success");
                     break;
                 case "info":
                     $("#" + IdMessage).attr("class", "alert alert-info");
                     break;
                 case "warning":
                     $("#" + IdMessage).attr("class", "alert alert-warning");
                     break;
                 case "danger":
                     $("#" + IdMessage).attr("class", "alert alert-danger");
                     break;
             }
         },

         Message: function (x) {
             $("#" + IdMessage).html(x);
         },

         Show: function () {
             $("#" + TmplId).modal();
         },

         Confirm: function (x, callback) {
             if (x == true) {
                 $("#" + TmplId + " .confirmFooter").show();
                 $("#" + TmplId + " .defaultFooter").hide();
             }
             else {
                 $("#" + TmplId + " .confirmFooter").hide();
                 $("#" + TmplId + " .defaultFooter").show();
             }

             //generamos un callback 
             if (typeof callback == "function")
                 //callback();
                 //llamamos a los botones generados para saber su respuesta
                 var r = false;
             $("#" + TmplId + " .ok_").on('click', function () { callback(true); });
             $("#" + TmplId + " .cancel_").on('click', function () { callback(false); });


         }
     }


    ModalTemplate =
        {
            CreateModal: function () {

                var Id = Math.random();
                IdTemplate = "ModalComponent" + Id.toString().replace(".", "");
                IdHeader = "ModalHeader" + Id.toString().replace(".", "");
                IdMessage = "Message_" + Id.toString().replace(".", "");
                IdButton = "Button" + Id.toString().replace(".", "");

                var x = '<div class="modal fade" id="ModalComponent" role="dialog">';
                x = x + '<div class="modal-dialog">';
                x = x + '<div class="modal-content">';
                x = x + '<div class="modal-header">';
                x = x + '<h4 class="modal-title" id="ModalHeader">p</h4>';
                x = x + '<button type="button" class="close" data-dismiss="modal">&times;</button>';         
                x = x + '</div>'
                x = x + '<div class="modal-body" id="ModalText">';
                x = x + '<div class="p" id="Message_">';
                x = x + '</div>';
                x = x + '</div>';
                x = x + '<div class="modal-footer defaultFooter">';
                x = x + '<button type="button" class="btn btn-default DeleteModal" data-dismiss="modal" id="IdButton">Cerrar</button>';
                x = x + '</div>';

                x = x + '<div class ="modal-footer confirmFooter" style="display:none">';
                x = x + '<button type="button" class ="btn btn-primary ok_" data-dismiss="modal">Aceptar</button>';
                x = x + '<button type="button" class ="btn btn-default cancel_" data-dismiss="modal">Cancelar</button>';
                x = x + '</div>';
                x = x + '</div>';
                x = x + '</div>';
                x = x + '</div>';

                x = x.replace("ModalComponent", IdTemplate);
                x = x.replace("ModalHeader", IdHeader);
                x = x.replace("Message_", IdMessage);
                x = x.replace("IdButton", IdButton);


                var DataTemplate =
                    {
                        "HtmlTemplate": x,
                        "IdTemplate": IdTemplate,
                        "IdHeader": IdHeader,
                        "IdMessage": IdMessage,
                        "IdButton": IdButton
                    }

                return DataTemplate;
            }
        }


});