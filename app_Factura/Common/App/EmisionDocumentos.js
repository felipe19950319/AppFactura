
$(document).ready(function () {

   /* var x = [

    {
        "CodigoDet": "12345",
        "NombreDet": "Pendrive",
        "Descripcion": "Guardar Archivos",
        "UnidadMedida": "UN",
        "PrecioDet": "9990",
        "Iva": "SI",
        "Cantidad": "20",
        "Descuento": "20",
        "TipoDescuento": "+%",
        "Total": "9990"
    },
    {
        "CodigoDet": "12345",
        "NombreDet": "Pendrive",
        "Descripcion": "Guardar Archivos",
        "UnidadMedida": "UN",
        "PrecioDet": "9990",
        "Iva": "SI",
        "Cantidad": "20",
        "Descuento": "20",
        "TipoDescuento": "+%",
        "Total": "9990"
    },
    {
        "CodigoDet": "12345",
        "NombreDet": "Pendrive",
        "Descripcion": "Guardar Archivos",
        "UnidadMedida": "UN",
        "PrecioDet": "9990",
        "Iva": "SI",
        "Cantidad": "20",
        "Descuento": "20",
        "TipoDescuento": "+%",
        "Total": "9990"
    }
    ];*/
   var objTable= $("#TablaDetalles").DataTable({
        "destroy": true,
        "pageLength": 5,
        "lengthChange": false,
        "searching": false,
        "info": false,
      //  "data": x,
        "bAutoWidth": false,
        "columns": [
            { "data": "CodigoDet", "title": "Codigo" },
            { "data": "NombreDet", "title": "Nombre" },
            { "data": "Descripcion", "title": "Descripcion" },
            { "data": "UnidadMedida", "title": "U.Medida" },
            { "data": "PrecioDet", "title": "Precio" },
            { "data": "Iva", "title": "Iva" },
            { "data": "Cantidad", "title": "Cantidad" },
            { "data": "Descuento", "title": "Descuento" },
            { "data": "Total", "title": "Total" },

              {
                  "title": "Accion",
                  "mRender": function (data, type, row) {
                      return '<center><a class="btn btn-danger" ><i class="fa fa-trash" style="color:white" aria-hidden="true"></i></a></center>';
                  }
              }
        ], "language": {
            "emptyTable": "No hay registros para mostrar.",
            "info": "Mostrando registros _START_ al _END_ de _TOTAL_ registros totales",
            "infoEmpty": "Mostrando 0 registros",
            "infoFiltered": "(filtrados de _MAX_)",
            "infoPostFix": "",
            "thousands": ".",
            "loadingRecords": "Cargando grilla...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "No se encontraron registros para la búsqueda",
            "paginate": {
                "first": "Primer",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        }
    });


   $("#btnAddDetalle").on('click', function () {
       //cargamos el template 
       $(".TemplateZone").load("HtmlTemplates/ModalDetalles.html", function () {
           //...esparemos a que cargue
           //mostramos el modal 
           $("#ModalDetalles").modal('show');

           $.ajax({
               type: 'POST',
               url: 'EmisionDocumentos.aspx/sel_unidadmedida',
               //data: JSON.stringify({ Id_emp: id_emp, Razon_social: Empresa }),
               contentType: 'application/json; charset=utf-8',
               dataType: 'json',
               success: function (msg) {
                   console.log(msg);
    
                   $.each(JSON.parse(msg.d), function (i, item) {
                       console.log(i, item);
                       $('#UnidadMedidaDetModal').append($('<option>', {
                           value: item.IdUnidadMedida,
                           text: item.CodUnidadMedida
                       }));
                   });
               }
           });

           $("#btnAddNewDet").off().on('click', function () {

               var obj = new Object();
               var date = new Date();


               obj.p_ID_EMPRESA = $("#_SES_IdEmpresa").val();
               obj.p_NOMBRE = $("#txtNombreDetModal").val();
               obj.p_CODIGO = $("#txtCodigoDetModal").val();
               obj.p_STOCK = $("#txtCantidadDetModal").val();
               obj.p_DESCRIPCION_PRODUCTO = $("#txtDescripcionDetModal").val();
               obj.p_VALOR_UNITARIO = $("#txtPrecioDetModal").val();
               obj.p_FECHA_CREACION = date.yyyymmdd('-');
               obj.p_DESCUENTO_PCT = "0";
               obj.p_ESTADO = "CSTOCK";
               obj.p_IdUnidadMedida = $("#UnidadMedidaDetModal").val();

               ServerSide('EmisionDocumentos.aspx', 'InsertProducto', obj, function (r) {
                   console.log(r);
                   var response = JSON.parse(r.d);
                   var RowInserted = response[0].RowInserted;
                   console.log(RowInserted);

                   if (RowInserted != null || RowInserted != undefined || RowInserted != "" && RowInserted > 0) {
                       $("#ModalDetalles").modal('hide');
                       ModalElement.Create();
                       ModalElement.Class("success");
                       ModalElement.Header("Registro agregado");
                       ModalElement.Message("Se ha agregado el registro correctamente!");
                       ModalElement.Show();
                   }

               });
            
           });

       });
        /*
        console.log("hola");
        console.log(objTable);
        objTable.rows.add(
                         [{
                             "CodigoDet": "1",
                             "NombreDet": "2",
                             "Descripcion": "3",
                             "UnidadMedida": "4",
                             "PrecioDet": "5",
                             "Iva": "6",
                             "Cantidad": "7",
                             "Descuento": "8",
                             "Total":"9"
                         }]
                 ).draw();*/
   });

   $("#btnAddListaDetalle").on('click', function () {

       $(".TemplateZone").load("HtmlTemplates/ModalListaDetalles.html", function () {
           //...esparemos a que cargue
           //mostramos el modal 
           var Table = null;
           $("#ModalListaDetalles").modal('show');
           //ejecutamos la llamada a ajax para mostrar la tabla
           $.ajax({
               type: 'POST',
               url: 'EmisionDocumentos.aspx/GetDetalleProducto',
               data: JSON.stringify({ RegInicio: '0', RegFin: '10' }),
               contentType: 'application/json; charset=utf-8',
               dataType: 'json',
               success: function (msg) {
                   var  TblListaDetalles= $("#TblListaDetalles").DataTable({
                       "destroy": true,
                       "pageLength": 5,
                       "lengthChange": false,
                       "searching": false,
                       "info": false,
                       "responsive": true,
                       "data": JSON.parse(msg.d),
                       "bAutoWidth": false,
                       "columns": [
                           { "data": "CodigoDet", "title": "Codigo" },
                           { "data": "NombreDet", "title": "Nombre" },
                           { "data": "Descripcion", "title": "Descripcion" },
                           { "data": "CodUnidadMedida", "title": "U.Medida" },
                           { "data": "PrecioDet", "title": "Precio" },
                          // { "data": "Iva", "title": "Iva" },
                           { "data": "Cantidad", "title": "Stock" },

                          // { "data": "Descuento", "title": "Descuento" },
                         //  { "data": "Total", "title": "Total" },
                             {
                                 "title": "Cantidad",
                                 "mRender": function (data, type, row) {
                                     return '<input type="number" min="0" value="0" class="form-control">';
                                 }
                             },
                             {
                                 "title": "Accion",
                                 "mRender": function (data, type, row) {
                                     return '<center><a class="btn btn-success AddDetalleLista" ><i class="fa fa-plus" style="color:white" aria-hidden="true"></i></a></center>';
                                 }
                             }
                       ], "language": {
                           "emptyTable": "No hay registros para mostrar.",
                           "info": "Mostrando registros _START_ al _END_ de _TOTAL_ registros totales",
                           "infoEmpty": "Mostrando 0 registros",
                           "infoFiltered": "(filtrados de _MAX_)",
                           "infoPostFix": "",
                           "thousands": ".",
                           "loadingRecords": "Cargando grilla...",
                           "processing": "Procesando...",
                           "search": "Buscar:",
                           "zeroRecords": "No se encontraron registros para la búsqueda",
                           "paginate": {
                               "first": "Primer",
                               "last": "Último",
                               "next": "Siguiente",
                               "previous": "Anterior"
                           }
                       }
                   });


                   $('#TblListaDetalles tbody').on('click', '.AddDetalleLista', function () {
                       var data = TblListaDetalles.row($(this).parents('tr')).data();
                       console.log(data);
                   });

               }
           });

       });

   });


});

