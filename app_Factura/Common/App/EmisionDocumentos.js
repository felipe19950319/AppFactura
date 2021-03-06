﻿$(document).ready(function () {

    var ObjEmpresa = new Object();
    ObjEmpresa.ID_EMPRESA = $("#_SES_IdEmpresa").val();

    ServerSide('EmisionDocumentos.aspx', 'GetEmpresa', ObjEmpresa, function (r) {
        var emp = JSON.parse(r.d)[0];
        console.log(emp);
        $("#txtRutEmisor").val(emp.RUT_EMPRESA);
        $("#txtFechaResolucion").val(emp.FECHA_RESOLUCION);
        $("#txtRazonSocial").val(emp.RAZON_SOCIAL);
        $("#txtGiroEmisor").val(emp.NombreGiro);
        $("#txtActecoEmisor").val(emp.Acteco);
        $("#txtDireccionEmisor").val(emp.DIRECCION);
        $("#txtComunaEmisor").val(emp.COMUNA);
        $("#txtCiudadEmisor").val(emp.CIUDAD);
  

    });

    fnControlDesc = function (row,meta) {
        var ControlDesc = '';
        ControlDesc = ControlDesc + '<div class="input-group mb-3" style="top:8px;">';
        ControlDesc = ControlDesc + '<input type ="number" class="form-control form-control-sm _txtDescRecrgo" id = "txtDctoRcrgoDet' + meta.row+'" style="width:30px"/>';
        ControlDesc = ControlDesc + '<div class="input-group-append">';
        ControlDesc = ControlDesc + '<select id="ListaDctoRcrgoDet'+meta.row+'" class="form-control form-control-sm _ListaDescRecrgo" >';
        ControlDesc = ControlDesc + '<option value="0">NO</option>';
        ControlDesc = ControlDesc + '<option value="1">+%</option>';
        ControlDesc = ControlDesc + '<option value="2">-%</option>';
        ControlDesc = ControlDesc + '<option value="3">+$</option>';
        ControlDesc = ControlDesc + '<option value="4">-$</option></select></div></div>';
        console.log(ControlDesc);
        return ControlDesc;
    }

    var TblListaDetCols = [
            { "data": "CodigoDet", "title": "Codigo" },
            { "data": "NombreDet", "title": "Nombre" },
            { "data": "Descripcion", "title": "Descripcion" },
            { "data": "UnidadMedida", "title": "U.Medida" },
            { "data": "PrecioDet", "title": "Precio" },
            { "data": "HasIva", "title": "HasIva" }, //para saber si el detalle tiene iva o no
            { "data": "HasDesc", "title": "HasDesc" }, //para saber tiene Descuento o recargo
            { "data": "DescValue", "title": "DescValue" }, //valor del descuento o recargo
            { "data": "DescTipo", "title": "DescTipo" }, //tipo de descuento o recargo
           
              {
                  "title": "Iva",
                  "mRender": function (data, type, row,meta) {
                      return '<select id="SelectIva' + meta.row +'" class="form-control form-control-sm _iva" oldValue="'+row.Total+'"><option value="na" selected="selected">Seleccione</option><option value="si">SI</option><option value="no">NO</option></select>';
                  }
              },
            { "data": "Cantidad", "title": "Cantidad" },
        {
            "title": "Descuento/Recargo",
            "mRender": function (data, type, row,meta) {
                return fnControlDesc(row,meta);
            },
            "width":"10%"
        },
        { "data": "Total", "title": "Total" },
        { "data": "TotalOriginal", "title": "TotalOriginal" },//total original

              {
                  "title": "Accion",
                  "mRender": function (data, type, row) {
                      return '<center><a class="btn btn-danger btn-sm DeleteDetalle" ><i class="fa fa-trash" style="color:white" aria-hidden="true"></i></a></center>';
                  }
        },
        { "data": "Id_Detalle", "title": "Id_Detalle" }
    ];
    //tabla que contiene los detalles del formulario principal
    var objTableListaDetalles = MakeTable(
                    5,
                    null,
                    TblListaDetCols,
                    "#TablaDetalles"
                    );

    objTableListaDetalles.column(5).visible(false);
    objTableListaDetalles.column(6).visible(false);
    objTableListaDetalles.column(7).visible(false);
    objTableListaDetalles.column(8).visible(false);
    objTableListaDetalles.column(13).visible(false);
    objTableListaDetalles.column(15).visible(false);


    CalculaDescuentoTotal = function (PrecioDet,DescTipo,DescValue)
    {
        var Calculo=0;
        switch (DescTipo)
        {
            case "0":
                Calculo = PrecioDet;
                break;
            case "1"://+%
                Calculo = PrecioDet+( PrecioDet * FloatTryParse(DescValue)/100);
                break;
            case "2"://-%
                Calculo = PrecioDet - (PrecioDet * FloatTryParse(DescValue) / 100);
                break;
            case "3"://+$
                Calculo = PrecioDet + DescValue;
                break;
            case "4"://-$
                Calculo = PrecioDet - DescValue;
                break;
        }
         return Calculo;
    }

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
            var PrecioDsc = CalculaDescuentoTotal((data.PrecioDet * parseInt(data.Cantidad)), data.DescTipo, data.DescValue);// precio con descuento o recargo correspondiente
            switch (data.HasIva)
            {
                case "SI":             
                    AcumNeto = AcumNeto + PrecioDsc;
                    AcumConIva = AcumConIva + data.Total;
                    break;
                case "NO":
                    AcumExento = AcumExento + PrecioDsc;
                    break;
            }                
        });

        AcumDiffIva = AcumConIva-AcumNeto;
        AcumTotal = AcumExento + AcumConIva;
        $("#txtIvaTot").val(RoundDecimal(AcumDiffIva));
        $("#txtExentoTot").val(AcumExento);
        $("#txtNetoTot").val(AcumNeto);
        $("#txtDetTotal").val(AcumTotal);
    }

    $("#PruebaTotal").off().on('click', function () {

        SumDetails(objTableListaDetalles);
    });

    //_ListaDescRecrgo
    $('#TablaDetalles tbody').on('change', '._ListaDescRecrgo', function () {
        var me = this;
        var RowIndex = objTableListaDetalles.row($(this).parents('tr')).index();
        $("#txtDctoRcrgoDet" + RowIndex).val(0);
       // $("#ListaDctoRcrgoDet" + RowIndex).val(0);
        FnCalculoIvaDescuento(me);
        SumDetails(objTableListaDetalles);
    });

    $('#TablaDetalles tbody').on('change', '._iva', function () {
        var me = this;
        console.log(me);
        var RowIndex = objTableListaDetalles.row($(this).parents('tr')).index();
        $("#txtDctoRcrgoDet" + RowIndex).val(0);
        $("#ListaDctoRcrgoDet" + RowIndex).val(0);
        FnCalculoIvaDescuento(me);
        SumDetails(objTableListaDetalles);
    });


    //asignacion de descuento o recargo ademas con iva
    $('#TablaDetalles tbody').on('change', '._txtDescRecrgo', function () {
        var me = this;
        FnCalculoIvaDescuento(me);
        SumDetails(objTableListaDetalles);
    });

    FnCalculoIvaDescuento = function (me)
    {
        var dataRow = objTableListaDetalles.row($(me).parents('tr')).data();
        var RowIndex = objTableListaDetalles.row($(me).parents('tr')).index();
        var elem = $(me).parents('tr');
        var _txtDescRecrgo = elem.find('._txtDescRecrgo');
        var _iva = elem.find('._iva');

        var Operation = $("#ListaDctoRcrgoDet" + RowIndex).val();
        var OpValue = $("#txtDctoRcrgoDet" + RowIndex).val();
        if (OpValue.length==0)
        {
            OpValue = 0;
        }

        switch (Operation) {
            case "0":
                if (_iva.val() == 'si') {
                    dataRow.HasIva = 'SI';
                    dataRow.HasDesc = 'NO';
                    dataRow.DescTipo = "0";
                    //guardamos el valor original para utilizarlo despues
                    dataRow.Total = dataRow.TotalOriginal;
                    dataRow.Total=(dataRow.Total * 1.19);
                }
                else {
                    dataRow.HasIva = 'NO';
                    dataRow.HasDesc = 'NO';
                    dataRow.DescTipo = "0";
                    //corresponde al valor original
                    dataRow.Total = dataRow.TotalOriginal;
                }
                break;
            case "1"://+%
                if (_iva.val() == 'si') {
                    dataRow.HasIva = 'SI';
                    dataRow.HasDesc = 'SI';
                    dataRow.DescTipo = "1";                  
                    dataRow.Total = dataRow.TotalOriginal;
                    dataRow.Total = RoundDecimal((dataRow.Total + (dataRow.Total * (FloatTryParse( OpValue) / 100))) * 1.19);
                }
                else {
                    dataRow.HasIva = 'NO';
                    dataRow.HasDesc = 'SI';
                    dataRow.DescTipo = "1";
                    dataRow.Total = dataRow.TotalOriginal;
                    dataRow.Total = RoundDecimal(dataRow.Total + (dataRow.Total * (FloatTryParse( OpValue) / 100)));
                }
                dataRow.DescValue = OpValue;
                break;
            case "2"://-%
                if (_iva.val() == 'si') {
                    dataRow.HasIva = 'SI';
                    dataRow.HasDesc = 'SI';
                    dataRow.DescTipo = "2";
                    dataRow.Total = dataRow.TotalOriginal;
                    dataRow.Total = RoundDecimal((dataRow.Total - (dataRow.Total * (FloatTryParse( OpValue) / 100))) * 1.19);
                }
                else {
                    dataRow.HasIva = 'NO';
                    dataRow.HasDesc = 'SI';
                    dataRow.DescTipo = "2";
                    dataRow.Total = dataRow.TotalOriginal;
                    dataRow.Total = RoundDecimal(dataRow.Total - (dataRow.Total * (FloatTryParse( OpValue) / 100)));
                }
                dataRow.DescValue = OpValue;
                break;
            case "3"://+$
                if (_iva.val() == 'si') {
                    dataRow.HasIva = 'SI';
                    dataRow.HasDesc = 'SI';
                    dataRow.DescTipo = "3";
                    dataRow.Total = dataRow.TotalOriginal;
                    dataRow.Total = RoundDecimal((dataRow.Total + FloatTryParse(OpValue)) * 1.19);
                }
                else {
                    dataRow.HasIva = 'NO';
                    dataRow.HasDesc = 'SI';
                    dataRow.DescTipo = "3";
                    dataRow.Total = dataRow.TotalOriginal;
                    dataRow.Total = RoundDecimal((dataRow.Total + FloatTryParse( OpValue)));
                }
                dataRow.DescValue = OpValue;
                break;
            case "4"://-$
                if (_iva.val() == 'si') {
                    dataRow.HasIva = 'SI';
                    dataRow.HasDesc = 'SI';
                    dataRow.DescTipo = "4";
                    dataRow.Total = dataRow.TotalOriginal;
                    dataRow.Total = RoundDecimal((dataRow.Total - FloatTryParse( OpValue)) * 1.19);
                }
                else {
                    dataRow.HasIva = 'NO';
                    dataRow.HasDesc = 'SI';
                    dataRow.DescTipo = "4";
                    dataRow.Total = dataRow.TotalOriginal;
                    dataRow.Total = RoundDecimal((dataRow.Total - FloatTryParse( OpValue)));
                }
                dataRow.DescValue = OpValue;
                break;
        }

        SetCellValue(
            objTableListaDetalles,
            RowIndex,
            GetCellIndexByName(objTableListaDetalles, 'Total'),
            dataRow.Total
        );

    }

    

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
                SumDetails(objTableListaDetalles);
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
                      },
                      { "data": "ID_DETALLE", "title": "ID_DETALLE" }
                  ];

              
                   //construimos la tabla
                  var TblListaDetalles = MakeTable(
                      5,
                      JSON.parse(msg.d),
                      ColumnDefs,
                      "#TblListaDetalles"
                      );
                   TblListaDetalles.column(8).visible(false);
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
                                    "HasIva": 'NO',
                                    "HasDesc": 'NO',
                                    "DescValue": 0,
                                    "DescTipo":"0",
                                   // "Iva": "6",
                                    "Cantidad": CantidadProd,
                                    "Descuento": "8",
                                    "Total": aux,
                                    "TotalOriginal": aux,
                                    "Id_Detalle": data.ID_DETALLE
                                }]
                           ).draw();
                           SumDetails(objTableListaDetalles);
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
                                { "data": "RutReceptor", "title": "Rut", "width": "50px" },
                                { "data": "NombreReceptor", "title": "Nombre" },
                                { "data": "EmailReceptor", "title": "Email" },
                                /*{ "data": "DireccionReceptor", "title": "Direccion", "width": "20%" },*/
                                /*{ "data": "TelefonoReceptor", "title": "TelefonoReceptor", "width": "20%" },*/
                                { "data": "Comuna", "title": "Comuna" },
                                { "data": "Ciudad", "title": "Ciudad"},
                                { "data": "NombreGiro", "title": "Giro"},
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
                           //console.log(data);
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


    fnValidateDoc = function ()
    {
        var error = "";

        return error;
    }

    fnGetReceptor = function ()
    {
        var Receptor = new Object();
    }

    fnGetDetallesDte = function (Doc)
    {
     
        objTableListaDetalles.rows().every(function (rowIdx) {
            var data = this.data();
            var det = new Object();
            count = rowIdx + 1;
            det.NroLinDet = count;
            det.NmbItem = data.NombreDet;
            det.DscItem = data.Descripcion;
            det.QtyItem = data.Cantidad;
            det.PrcItem =parseInt( data.PrecioDet);
            det.MontoItem = parseInt(data.Total);
            det.HasIva = data.HasIva;
            det.CdgItem =[{
                TpoCodigo : "INT1",
                VlrCodigo: data.CodigoDet,
                Id_Detalle: data.Id_Detalle
            }];
            console.log("detalle seleccionado");
            console.log(data);

            Doc.dte.documento.detalle.push(det);
        });
    }

    fnGetDataDte = function ()
    {
        var Doc = fnGetJsonDte();
        var x = new Date();
        Doc.dte.TipoOperacion = "";
        Doc.dte.documento.ID = "";
        Doc.dte.documento.encabezado.iddoc.TipoDTE = 34;
        Doc.dte.documento.encabezado.iddoc.Folio = 1;
        Doc.dte.documento.encabezado.iddoc.FchEmis = "2018-07-25";
        Doc.dte.documento.encabezado.iddoc.FmaPago = 1;

        Doc.dte.documento.encabezado.emisor.RUTEmisor = $("#txtRutEmisor").val();
        Doc.dte.documento.encabezado.emisor.RznSoc = $("#txtRazonSocial").val();
        Doc.dte.documento.encabezado.emisor.GiroEmis = $("#txtGiroEmisor").val();
        Doc.dte.documento.encabezado.emisor.Acteco = $("#txtActecoEmisor").val();
        Doc.dte.documento.encabezado.emisor.DirOrigen = $("#txtDireccionEmisor").val();
        Doc.dte.documento.encabezado.emisor.CmnaOrigen = $("#txtComunaEmisor").val();
        Doc.dte.documento.encabezado.emisor.CiudadOrigen = $("#txtCiudadEmisor").val();

        Doc.dte.documento.encabezado.receptor.RUTRecep = $("#txtRutReceptor").val();
        Doc.dte.documento.encabezado.receptor.RznSocRecep = $("#txtRazonSocialReceptor").val();
        Doc.dte.documento.encabezado.receptor.GiroRecep = $("#txtGiroReceptor").val();
        Doc.dte.documento.encabezado.receptor.DirRecep = $("#txtDireccionReceptor").val();
        Doc.dte.documento.encabezado.receptor.CmnaRecep = $("#txtComunaReceptor").val();
        Doc.dte.documento.encabezado.receptor.CiudadRecep = $("#txtCiudadReceptor").val();

        Doc.dte.documento.encabezado.totales.MntNeto = $("#txtNetoTot").val();
        Doc.dte.documento.encabezado.totales.MntExe = $("#txtExentoTot").val();
        Doc.dte.documento.encabezado.totales.TasaIVA =parseInt( $("#txtTasaIvaTot").val());
        Doc.dte.documento.encabezado.totales.IVA = $("#txtIvaTot").val();
        Doc.dte.documento.encabezado.totales.MntTotal = $("#txtDetTotal").val();

        fnGetDetallesDte(Doc);

        return Doc;
    }

    $("#btnPrevisualizar").off().on('click', function() {
        $(".TemplateZone").load("HtmlTemplates/ModalPreviewDte.html", function() {
            $("#ModalPreviewDte").modal('show');
            var ObjDte = fnGetDataDte();//borrador
            fnGetPreviewDte(ObjDte, function (r) {

                switch (r.code)
                {
                    case 200:
                        //ok
                        GeneratePdf(r.ObjectResponse, "previewDTE");
                        break;
                    case 500:
                        //error
                        console.log(r.ex);
                        break;
                }            
            });
        });
    });

    fnSaveDte = function (TipoOperacion,Mensaje)
    {
        var ObjDte = fnGetDataDte();
        //SI VIENE LA OPERACION DESDE ESTE BOTON EL ESTADO ES BORRADOR
        ObjDte.dte.TipoOperacion = TipoOperacion;
        ObjDte.dte.Ambiente = $("#_SES_Ambiente").val();
        ObjDte.dte.documento.ID = TipoOperacion + $("#txtRutEmisor").val().replace("-", "");

        fnSaveDocDte(ObjDte, function (r) {
            switch (r.code) {
                case 200:
                    ModalElement.Create();
                    ModalElement.Class("info");
                    ModalElement.Header("Informacion");
                    ModalElement.Message(Mensaje);
                    ModalElement.Show();
                    //3 segundos de espera para redireccionar a lista documentos
                    setTimeout(function () {
                        window.location.href = 'ListaDocumentos.aspx';
                    }, 3000);

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
    }
    $("#btnGuardarDocumento").off().on('click', function () {
        fnSaveDte('BORR',"Se ha guardado el borrador del documento correctamente!");
    });
    $("#btnGuardarEmitir").off().on('click', function () {
        fnSaveDte('ENV', "Se ha guardado el documento correctamente y se enviara dentro de unos momentos al SII!");
    });



    /*****************SI SE QUIERE VISUALIZAR UN DOCUMENTO GUARDADO******************/
    if (sessionStorage.getItem("DocumentoOrigen") != null && sessionStorage.getItem("DocumentoOrigen") != '' ) {
        var SessionDoc = JSON.parse(sessionStorage.getItem("DocumentoOrigen"));
        //SessionDoc.idDte;
        console.log(SessionDoc);

        //Receptor
        var ObjReceptor = new Object();
        var RutReceptor = SessionDoc.RutReceptor.replace('-', '');
        RutReceptor = RutReceptor.substring(0, RutReceptor.length - 1);
        ObjReceptor.RutReceptor = RutReceptor;
        $.ajax({
            type: 'POST',
            url: 'EmisionDocumentos.aspx/GetReceptorByRut',
            data: JSON.stringify(ObjReceptor),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (msg) {
                var Receptor = JSON.parse(msg.d);
                $("#txtRutReceptor").val(Receptor[0].RutReceptor);
                $("#txtRazonSocialReceptor").val(Receptor[0].NombreReceptor);
                $("#txtGiroReceptor").val(Receptor[0].NombreGiro);
                $("#txtComunaReceptor").val(Receptor[0].Comuna);
                $("#txtDireccionReceptor").val(Receptor[0].DireccionReceptor);
                $("#txtCiudadReceptor").val(Receptor[0].Ciudad);
            },
            error: function (request, status, error) {
                console.log(request.responseText);
            }
        });
        //detalles
        var ObjDetalle = new Object();
        ObjDetalle.IdDte = SessionDoc.idDte;
        $.ajax({
            type: 'POST',
            url: 'EmisionDocumentos.aspx/GetProductoByIdDte',
            data: JSON.stringify(ObjDetalle),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (msg) {
                var Detalles = JSON.parse(msg.d);
                var Count = 0;

                $.each(Detalles, function (i, r) {
                    console.log(r.ID_DETALLE);
                    var aux = (r.CantidadComprada * r.PrecioDet);
                    objTableListaDetalles.rows.add(
                        [{
                            "CodigoDet": r.CodigoDet,
                            "NombreDet": r.NombreDet,
                            "Descripcion": r.Descripcion,
                            "UnidadMedida": r.IdUnidadMedida,
                            "PrecioDet": r.PrecioDet,
                            "HasIva": r.HasIva,
                            "HasDesc": 'NO',
                            "DescValue": 0,
                            "DescTipo": "0",
                            // "Iva": "6",
                            "Cantidad": r.CantidadComprada,
                            "Descuento": "0",
                            "Total": aux,
                            "TotalOriginal": aux,
                            "Id_Detalle": r.ID_DETALLE
                        }]
                    ).draw();
                    $("#SelectIva" + Count.toString()).val(r.HasIva.toLowerCase());
                    console.log($("#SelectIva" + Count.toString()));
                    FnCalculoIvaDescuento($("#SelectIva" + Count.toString())[0]);
                    SumDetails(objTableListaDetalles);

                    Count = Count + 1;
                });


               
           
            },
            error: function (request, status, error) {
                console.log(request.responseText);
            }
        });
        sessionStorage.setItem("DocumentoOrigen", '');
    }

    /*****************SI SE QUIERE VISUALIZAR UN DOCUMENTO GUARDADO******************/

});

