$(document).ready(function () {

    var ObjEmpresa = new Object();
    ObjEmpresa.ID_EMPRESA = $("#_SES_IdEmpresa").val();
    ServerSide('EmisionDocumentos.aspx', 'GetEmpresa', ObjEmpresa, function (r) {
        console.log(r);
    });

    var TblListaDetCols = [
            { "data": "CodigoDet", "title": "Codigo" },
            { "data": "NombreDet", "title": "Nombre" },
            { "data": "Descripcion", "title": "Descripcion" },
            { "data": "UnidadMedida", "title": "U.Medida" },
            { "data": "PrecioDet", "title": "Precio" },
            { "data": "HasIva", "title": "HasIva" }, //para saber si el detalle tiene iva o no
              {
                  "title": "Iva",
                  "mRender": function (data, type, row,meta) {
                   //   console.log(data);
                      return '<select id="SelectIva' + meta.row +'" class="form-control form-control-sm _iva" oldValue="'+row.Total+'"><option value="na" selected="selected">Seleccione</option><option value="si">SI</option><option value="no">NO</option></select>';
                  }
              },
            { "data": "Cantidad", "title": "Cantidad" },
            { "data": "Descuento", "title": "Descuento" },
            { "data": "Total", "title": "Total" },

              {
                  "title": "Accion",
                  "mRender": function (data, type, row) {
                      return '<center><a class="btn btn-danger btn-sm DeleteDetalle" ><i class="fa fa-trash" style="color:white" aria-hidden="true"></i></a></center>';
                  }
              }
    ];
    //tabla que contiene los detalles del formulario principal
    var objTableListaDetalles = MakeTable(
                    5,
                    null,
                    TblListaDetCols,
                    "#TablaDetalles"
                    );

    objTableListaDetalles.column(5).visible(false);//desabilitamos la visibilidad de HasIva

    SumDetails = function (TablaDetalles)
    {
        var AcumTotal = 0;
        var AcumExento = 0;
        var AcumTasaIva = 0;
        var AcumSinIva = 0;
        var AcumConIva = 0;
        var AcumDiffIva = 0;
        var AcumNeto = 0;

        TablaDetalles.rows().every(function () {
            var data = this.data();
            console.log(data);       
            switch (data.HasIva)
            {
                case "SI":
                    AcumNeto = AcumNeto + (data.PrecioDet * parseInt(data.Cantidad));
                    AcumConIva = AcumConIva + data.Total;
                   // AcumDiffIva = AcumDiffIva + (data.Total - data.PrecioDet);
                    break;

                case "NO":
                    AcumExento = AcumExento + data.Total;
                    break;
            } 
                
        });

        AcumDiffIva = AcumConIva-AcumNeto;
        AcumTotal = AcumExento + AcumConIva;
        $("#txtIvaTot").val(RoundDecimal(AcumDiffIva));
        $("#txtExentoTot").val(AcumExento);
        $("#txtNetoTot").val(AcumNeto);
        $("#txtDetTotal").val(AcumTotal);
       // console.log("TotalExento", AcumExento);
       // console.log("TotalConIva", AcumConIva);
    }

    $("#PruebaTotal").off().on('click', function () {

        SumDetails(objTableListaDetalles);
    });

    //asignacion de iva
    $('#TablaDetalles tbody').on('change', '._iva', function () {
        var dataRow = objTableListaDetalles.row($(this).parents('tr')).data();
        var RowIndex = objTableListaDetalles.row($(this).parents('tr')).index();
        var elem = $(this).parents('tr');       
        var _iva = elem.find('._iva');

        var valueWithIva = "";

        switch (_iva.val())
        {
            case "si":
                //asigno
               // $("#SelectIva" + RowIndex + "").attr("oldValue", dataRow.Total);
                valueWithIva = (dataRow.Total * (1.19));
                dataRow.Total =valueWithIva;
                dataRow.HasIva = 'SI';
                break;
            case "no":
                //obtengo
                dataRow.Total =parseFloat( $("#SelectIva" + RowIndex + "").attr("oldValue"));
                dataRow.HasIva = 'NO';
                break;
            default:
                break;
        }
       // console.log(GetCellIndexByName(objTableListaDetalles, 'Total'));
        SetCellValue(
            objTableListaDetalles,
            RowIndex,
            GetCellIndexByName(objTableListaDetalles, 'Total'),
            dataRow.Total
        );
        //objTableListaDetalles.row(RowIndex).data(dataRow).draw();    
    });

    //eliminacion de data de la grilla de detalles principal
    $('#TablaDetalles tbody').on('click', '.DeleteDetalle', function () {
        ModalElement.Create();
        ModalElement.Class("danger");
        ModalElement.Header("Eliminar detalle");
        ModalElement.Message("Desea quitar este detalle?");
  
        ModalElement.Confirm(true, function (r) {

            if (r == true) {
                var RowIndex = objTableListaDetalles.row($(this).parents('tr')).index();
                objTableListaDetalles.row(RowIndex).remove().draw();
            }
        });

        ModalElement.Show();
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
                   //console.log(msg);
    
                   $.each(JSON.parse(msg.d), function (i, item) {
                     //  console.log(i, item);
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
                  // console.log(r);
                   var response = JSON.parse(r.d);
                   var RowInserted = response[0].RowInserted;
                 //  console.log(RowInserted);

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
                   //definimos las columnas 
                  var ColumnDefs = [
                           { "data": "CodigoDet", "title": "Codigo" },
                           { "data": "NombreDet", "title": "Nombre" },
                           { "data": "Descripcion", "title": "Descripcion" },
                           { "data": "CodUnidadMedida", "title": "U.Medida" },
                           { "data": "PrecioDet", "title": "Precio" },
                           { "data": "Cantidad", "title": "Stock" },
                         
                             {
                                 "title": "Cantidad",
                                 "mRender": function (data, type, row) {
                                     return '<input type="number" min="0" value="0" class="form-control form-control-sm CantidadProd">';
                                 }
                             },
                             {
                                 "title": "Accion",
                                 "mRender": function (data, type, row) {
                                     return '<center><a class="btn btn-success btn-sm AddDetalleLista" ><i class="fa fa-plus" style="color:white" aria-hidden="true"></i></a></center>';
                                 }
                             }];

              
                   //construimos la tabla
                  var TblListaDetalles = MakeTable(
                      5,
                      JSON.parse(msg.d),
                      ColumnDefs,
                      "#TblListaDetalles"
                      );

                    //eventos de la tabla
                   $('#TblListaDetalles tbody').on('click', '.AddDetalleLista', function () {
                       var data = TblListaDetalles.row($(this).parents('tr')).data();
                      // console.log(data);
                       var elem = $(this).parents('tr');
                       var CantidadProd = elem.find('.CantidadProd').val();

                       if (CantidadProd > data.Cantidad)
                       {
                           ModalElement.Create();
                           ModalElement.Class("warning");
                           ModalElement.Header("Error en cantidad!");
                           ModalElement.Message("La cantidad no puede superar al stock!");
                           ModalElement.Show();
                       }
                       else
                       {
                        //   console.log(data);
                           var aux = (CantidadProd * data.PrecioDet);
                           objTableListaDetalles.rows.add(
                                [{
                                    "CodigoDet": data.CodigoDet,
                                    "NombreDet": data.NombreDet,
                                    "Descripcion": data.Descripcion,
                                    "UnidadMedida": data.IdUnidadMedida,
                                    "PrecioDet": data.PrecioDet,
                                    "HasIva":'NO',
                                   // "Iva": "6",
                                    "Cantidad": CantidadProd,
                                    "Descuento": "8",
                                    "Total": aux
                                }]
                                  ).draw();
                       }

                       //console.log(CantidadProd);
                   });

               }
           });

       });

   });

 
   $("#AddReceptor").on("click", function () {

       $(".TemplateZone").load("HtmlTemplates/ModalReceptor.html", function () {

           $("#ModalReceptor").modal('show');
           /*------------------------autocomplete-----------------------------*/
               $("#txtComunaRecep").autocomplete({
                   minLength: 2,
                   source: function (request,response) {
                       var object = new Object();
                       object.NombreComuna = request.term;

                       ServerSide('EmisionDocumentos.aspx', 'GetComuna', object, function (r) {
                          var response_ = JSON.parse(r.d);
                            response($.map(response_, function (val, i) {
                                return {
                                    label: val.Label,
                                    value: val.Label
                                };
                           }));
                       });
                   }
               });


               $("#txtCiudadRecep").autocomplete({
                   minLength: 2,
                   source: function (request, response) {
                       var object = new Object();
                       object.NombreRegion = request.term;

                       ServerSide('EmisionDocumentos.aspx', 'GetRegion', object, function (r) {
                           var response_ = JSON.parse(r.d);
                           response($.map(response_, function (val, i) {
                               return {
                                   label: val.Label,
                                   value: val.Label
                               };
                           }));
                       });
                   }
               });

               $("#txtGiroRecep").autocomplete({
                   minLength: 2,
                   source: function (request, response) {
                       var object = new Object();
                       object.GIRO = request.term;

                       ServerSide('EmisionDocumentos.aspx', 'GetGiro', object, function (r) {
                           var response_ = JSON.parse(r.d);
                           response($.map(response_, function (val, i) {
                               return {
                                   label:val.IdGiro+' '+ val.Label,
                                   value: val.Label
                               };
                           }));
                       });
                   },
                   select: function (event, ui) {
                      // var Id = ui.item.label.replace(/^\D+/g, '');
                       $("#txtGiroRecep").attr("IdGiro",parseInt(ui.item.label));
                   }
               }); 
         
           /*------------------------autocomplete-----------------------------*/

               $("#txtRutRecep").on('change', function () { 
                   var rut = $("#txtRutRecep").val();
                   $("#txtRutRecep").val( likeRut(rut, "", "-"));
               });

          
               ValidateRecep = function (obj) {
                   var Error = "";

                   if (isRut((obj.RutReceptor + obj.DvReceptor)) == false)
                   {
                       Error = Error + 'el <strong>RUT</strong> ingresado no es valido! \n';
                   }

                   if (obj.NombreReceptor == "" || obj.NombreReceptor == null)
                   {
                       Error = Error + 'el <strong>Nombre</strong> ingresado no puede estar vacio! \n';
                   }

                   if (obj.DireccionReceptor == "" || obj.DireccionReceptor == null) {
                       Error = Error + 'la <strong>Direccion</strong> ingresada no puede estar vacia! \n';
                   }

                   if (obj.IdGiro == "" || obj.IdGiro == null) {
                       Error = Error + 'debe seleccionar un <strong>Giro</strong> de la lista! \n';
                   }
                  
                   return Error;
               }

               $("#btnAddNewRecep").on('click', function () {
                   var obj = new Object();
                   obj.NombreReceptor = $("#txtNombreRecep").val();
                   obj.RutReceptor = $("#txtRutRecep").val().split('-')[0];
                   obj.DvReceptor = $("#txtRutRecep").val().split('-')[1];
                   obj.EmailReceptor = $("#txtEmailRecep").val();
                   obj.DireccionReceptor = $("#txtDireccionRecep").val();
                   obj.TelefonoReceptor = $("#txtTelefonoRecep").val();
                   obj.Ciudad = $("#txtCiudadRecep").val();
                   obj.Comuna = $("#txtComunaRecep").val();
                   obj.IdGiro = $("#txtGiroRecep").attr("IdGiro");

                   var Error = ValidateRecep(obj);
                   if (Error.length > 0) {

                       ModalElement.Create();
                       ModalElement.Class("danger");
                       ModalElement.Header("Error al Agregar Receptor");
                       ModalElement.Message(Error);
                       ModalElement.Show();
                   }
                   else
                   {
                       ServerSide('EmisionDocumentos.aspx', 'InsertReceptor', obj, function (r) {
                           var response_ = JSON.parse(r.d);
                           if (response_[0].RESPONSE == "OK")
                           {
                               $("#ModalReceptor").modal('hide');

                               ModalElement.Create();
                               ModalElement.Class("success");
                               ModalElement.Header("Operacion exitosa!");
                               ModalElement.Message("Se ha agregado el registro correctamente!");
                               ModalElement.Show();
                           }
                       
                       });
                   }
               });
      
       });
   });


   $("#btnModalReceptor").on('click', function () {
       $(".TemplateZone").load("HtmlTemplates/ModalListaReceptor.html", function () {
           $("#ModalListaReceptor").modal('show');

           $("#SearchReceptor").keyup(function () {
               if ($(this).val().length > 1) {
                   var obj = new Object();
                   obj.Parameter = $(this).val();
                   ServerSide('EmisionDocumentos.aspx', 'GetReceptor', obj, function (msg) {
                       //console.log(msg);


                       //definimos las columnas 
                       var ColumnDefs = [
                                { "data": "RutReceptor", "title": "Rut"},
                                { "data": "NombreReceptor", "title": "Nombre" },
                                { "data": "EmailReceptor", "title": "Email" },
                                /*{ "data": "DireccionReceptor", "title": "Direccion", "width": "20%" },*/
                                /*{ "data": "TelefonoReceptor", "title": "TelefonoReceptor", "width": "20%" },*/
                                { "data": "Comuna", "title": "Comuna" },
                                { "data": "Ciudad", "title": "Ciudad" },
                                { "data": "NombreGiro", "title": "Giro" },
                                {
                                      "title": "",
                                      "mRender": function (data, type, row) {
                                          return '<a class="btn btn-success btn-sm SelReceptor"><i class="fa fa-plus" style="color:white" aria-hidden="true"></i></a>';
                                      }
                                }
                             
                                ];
                       //construimos la tabla
                       var TblListaReceptor = MakeTable(
                           5,
                           JSON.parse(msg.d),
                           ColumnDefs,
                           "#TblListaReceptor"
                           );

                       $('#TblListaReceptor tbody').off().on('click', '.SelReceptor', function () {
                           var data = TblListaReceptor.row($(this).parents('tr')).data();
                         //  var elem = $(this).parents('tr');
                           console.log(data);
                           //var CantidadProd = elem.find('.CantidadProd').val();
                           $("#txtRutReceptor").val(data.RutReceptor);
                           $("#txtRazonSocialReceptor").val(data.NombreReceptor);
                           $("#txtGiroReceptor").val(data.NombreGiro);
                           $("#txtComunaReceptor").val(data.Comuna);
                           $("#txtDireccionReceptor").val(data.DireccionReceptor);
                           $("#txtCiudadReceptor").val(data.Ciudad);

                           $("#ModalListaReceptor").modal('hide');
                       });

                   });
               }
       
           });
       });
   });


});

