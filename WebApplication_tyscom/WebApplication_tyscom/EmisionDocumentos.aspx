<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EmisionDocumentos.aspx.cs" Inherits="WebApplication_tyscom.EmisionDocumentos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style8 {
            width: 141px;
        }
        .auto-style10 {
            width: 76px;
        }
        .auto-style11 {
            width: 48px;
        }
        .auto-style12 {
            width: 115px;
        }
        .auto-style13 {
            width: 10px;
        }
        .auto-style14 {
            width: 127px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <header>
      <script src="js/jquery-1.9.1.min.js"></script>
      <nav>
        <div class="navbar navbar-default navbar-sub">
          <div class="container">
            <ul class="nav navbar-nav">
            </ul>
            <ul class="nav navbar-nav navbar-right">
            </ul>
          </div>
        </div>     
      </nav>
    </header>
   
     

        
              <h1>Emitir Factura Electrónica</h1>
     
    <h2><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h2>
                    <asp:DropDownList ID="List_Tipo_Doc" runat="server" CssClass="btn dropdown-toggle selectpicker btn-default" data-style="btn-primary" onchange="disable_iva()" Style="display: none;">
                    <asp:ListItem Text="Tipo (33) Factura Electrónica" Value="33" />
                    <asp:ListItem Text="Tipo (34) Factura no Afecto o Exenta Electrónica" Value="34" />
                    <asp:ListItem Text="Tipo (56) Nota de débito electrónica" Value="56" />
                     <asp:ListItem Text="Tipo (61) Nota de crédito electrónica" Value="61" />
                    </asp:DropDownList>
                 
              <div class="row-fluid ">
				<div class="box blue span12">
					<div class="box-header" data-original-title="">
						<h2><i class="halflings-icon edit"></i><span class="break"></span>Emisor</h2>
						<div class="box-icon">
					
							<a href="#" class="btn-minimize"><i class="fa fa-angle-down" style="color:white"></i></a>					
						</div>
					</div>
					<div class="box-content" >
                  
                    <div class="row-fluid">
                      <div class="span4">
                           <label class="col-xs-4 control-label p0right align-left" style="width: 215px"><a>RUT:</a></label>                         
                              <asp:TextBox ID="txt_rut_emisor" runat="server" CssClass="form-control input-sm" ReadOnly="true"></asp:TextBox>
                         </div>
                           
                       <div class="span4">  
                          <label class="col-xs-4 control-label p0right align-left" style="width: 215px"><a>RUT Envia:</a></label>     
                              <asp:TextBox ID="txt_rut_envia" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                    </div>

                     <div class="span4">   
                          <label class="col-xs-4 control-label p0right align-left" style="width: 215px"><a>Fecha Resolucion:</a></label>                               
                              <asp:TextBox ID="txt_fecha" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                      </div>
              </div>
                        <div class="row-fluid">
                          <div class="span4">
                          <label class="col-xs-4 control-label p0right align-left" style="width: 215px"><a>Razón Social:</a></label>                     
                            <asp:TextBox ID="txt_rzn_emis" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                       </div>
                       
                     <div class="span4">
                          <label class="col-xs-4 control-label p0right align-left" style="width: 215px"><a>Giro:</a></label>             
                              <asp:TextBox ID="txt_giro_emisor" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>                       
                        </div>
                       
                       <div class="span4">
                          <label class="col-xs-4 control-label p0right align-left" style="width: 215px"><a>Dirección:</a></label>                      
                            <asp:TextBox ID="txt_direccion_emisor" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                         </div>
                       </div>
                        <div class="row-fluid">
                     <div class="span4">
                          <label class="col-xs-4 control-label p0right align-left" style="width: 215px"><a>Comuna:</a></label>               
                         <asp:TextBox ID="txt_comuna_emisor" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                        </div>

                     <div class="span4">
                   <label class="col-xs-4 control-label p0right align-left" style="width: 215px"><a>Ciudad:</a></label>                              
                           <asp:TextBox ID="txt_ciudad_emisor" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                      </div> 
                         
                     </div>
                 </div>       </div>
  </div>

               <div class="row-fluid ">
				<div class="box red span12">
					<div class="box-header" data-original-title="">
						<h2><i class="halflings-icon edit"></i><span class="break"></span>Cliente</h2>
						<div class="box-icon">
							<a href="#" data-toggle="modal" data-target="#ModalClientes" ><i class="fa fa-plus" style="color:white"></i></a>
							<a href="#" class="btn-minimize"><i class="fa fa-angle-down" style="color:white"></i></a>					
						</div>
					</div>
					<div class="box-content" >
                         <div class="row-fluid">
                       <div class="span4">
                          <label class="col-xs-4 control-label p0right align-left"><a>RUT:</a></label>                   
                            <asp:TextBox ID="txt_rut_recep" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                        </div>
                       
                       <div class="span4">
                          <label class="col-xs-4 control-label p0right align-left"><a>Razón Social:</a></label>
                           <asp:TextBox ID="txt_rzn_recep" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                       </div>
                       
                       <div class="span4">
                          <label class="col-xs-4 control-label p0right align-left"><a>Giro:</a></label>                      
                         <asp:TextBox ID="txt_giro_recep" runat="server" CssClass="form-control input-sm"></asp:TextBox>              
                           </div>
</div>
                       <div class="row-fluid">
                       <div class="span4">
                          <label class="col-xs-4 control-label p0right align-left"><a>Comuna:</a></label>              
                           <asp:TextBox ID="txt_comuna_recep" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>

                       <div class="span4">
                          <label class="col-xs-4 control-label p0right align-left"><a>Direccion:</a></label>                      
                           <asp:TextBox ID="txt_dir_recep" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                  </div>

                       <div class="span4">
                          <label class="col-xs-4 control-label p0right align-left" style="left: 0px; top: 0px"><a>Ciudad:</a></label>                             
                                <asp:TextBox ID="txt_ciudad_recep" runat="server" CssClass="form-control input-sm"></asp:TextBox>
               </div>
                 </div>
                                              </div>
                     </div>
                  
                 

                 <div class="row-fluid ">
				<div class="box orangeDark span12">
					<div class="box-header" data-original-title="">
						<h2><i class="halflings-icon edit"></i><span class="break"></span>Referencias</h2>
						<div class="box-icon">
					
							<a href="#" class="btn-minimize"><i class="fa fa-angle-down" style="color:white"></i></a>					
						</div>
					</div>
					<div class="box-content" >
               <table class="table table-bordered table-condensed table-hover" style="width: 81%">
                        <tbody><tr>
                         
                          <th class="auto-style9"><a>Documento ref.</a></th>
                          <th class="auto-style5"><a>Folio Ref.&nbsp;</a></th>
                          <th class="auto-style8"><a>Fecha</a></th>                                        
                          <th><a>Razon Referencia</a></th>
                    
                        
                        
                        </tr>
                        <tr class="wrap-with-actions">
                          <td class="auto-style9">
                            <div class="form-inline">
                              <div class="form-group">
                                            <asp:DropDownList ID="list_documentos" runat="server"  CssClass="btn dropdown-toggle selectpicker btn-default" data-style="btn-primary">

                             <asp:ListItem Text="29 Factura de Inicio" Value="29" />
                             <asp:ListItem Text="30 Factura" Value="30" />
                             <asp:ListItem Text="33 Factura Electrónica" Value="33" />
                             <asp:ListItem Text="34 Factura no Afecto o Exenta Electrónica" Value="34" />
                             <asp:ListItem Text="40 Liquidación factura" Value="40" />
                           <asp:ListItem Text="43 Liquidación Factura Electrónica" Value="43" />
                             <asp:ListItem Text="45 Factura de Compra" Value="45" />
                              <asp:ListItem Text="46 Factura de Compra electrónica" Value="46" />
                               <asp:ListItem Text="55 Nota de débito" Value="55" />
                               <asp:ListItem Text="56 Nota de débito electrónica" Value="56" />
                                <asp:ListItem Text="60 Nota de crédito" Value="60" />
                              <asp:ListItem Text="61 Nota de crédito electrónica" Value="61" />
                             <asp:ListItem Text="101 Factura de exportación" Value="101" />
                          <asp:ListItem Text="110 Factura de Exportación Electrónica" Value="110" />
                        </asp:DropDownList>                  
                              </div>                             
                            </div>
                          </td>
                          <td class="auto-style5"><asp:TextBox ID="txt_folio_ref" runat="server" CssClass="form-control input-sm w90" Width="130px"></asp:TextBox></td>
                          <td class="auto-style8">
                         <asp:TextBox ID="txt_fecha_ref" runat="server" CssClass="form-control input-sm w90" Width="122px"></asp:TextBox>
                         </td>
                         
                       
                          <td>
                              
                        <asp:DropDownList ID="ListCodRef" runat="server"  CssClass="btn dropdown-toggle selectpicker btn-default" data-style="btn-primary">

                             <asp:ListItem Text="ANULA DOCUMENTO DE REFERENCIA" Value="1" />
                             <asp:ListItem Text="CORRIGE MONTO DOCUMENTO DE REFERENCIA" Value="3" />
                             <asp:ListItem Text="CORRIGE TEXTO DOCUMENTO DE REFERENCIA" Value="2" />

                        </asp:DropDownList> 
                          </td>  
                        </tr>
                      </tbody></table>
                        <asp:LinkButton ID="Link_referencia" runat="server" CssClass="btn orangeDark" Text="Agregar Nueva Referencia" OnClick="LinkButton_ref_Click" >Agregar Nueva Referencia</asp:LinkButton>
</div></div>
                        <br>
                     <div class="box orangeDark span12" style="width:80%">
                <div class="box-content">
                    <div class="box-header">
                      <h2>Referencias </h2>
                    </div> 
                           <asp:GridView ID="Grid_Referencia" runat="server"  AutoGenerateColumns="False" 
EmptyDataText="No se han agregado referencias de documentos." OnRowDeleting="Grid_Referencia_RowDeleting" 
       CssClass="table table-bordered table-condensed table-hover" PagerStyle-CssClass="pager"
 HeaderStyle-CssClass="header" RowStyle-CssClass="rows" Width="823px" >
<Columns>
    <asp:BoundField DataField="TpoDocRef" HeaderText="Documento Ref."  >

      </asp:BoundField>
    <asp:BoundField DataField="FolioRef" HeaderText="Folio Ref."  >
      </asp:BoundField>
     <asp:BoundField DataField="FchRef" HeaderText="Fecha"  >

      </asp:BoundField>
      <asp:BoundField DataField="RazonRef" HeaderText="Razon Ref."  >

      </asp:BoundField>
           <asp:CommandField ShowDeleteButton="True" ButtonType="Button"   />    
</Columns>
            
</asp:GridView>
                    </div></div>
                       
                        
                        </div>
                    <script type="text/javascript"> 
                   
                       function disable_iva() {

                            var tipo_documento =document.getElementById('<%=List_Tipo_Doc.ClientID%>');
                             var iva =document.getElementById('<%=List_iva.ClientID%>');
                            if (tipo_documento.options[tipo_documento.selectedIndex].value == "34") {

                                for (var i = 0; i < iva.options.length; i++) {
                                    if (iva.options[i].text == "NO") {
                                        iva.options[i].selected = true;
                                    }
                                    }
                                document.getElementById('<%=List_iva.ClientID%>').disabled = true;
                            } else {
                                           document.getElementById('<%=List_iva.ClientID%>').disabled = false;
                            }
                        }

                        function calculo_desc() {

                         //   var iva=1;
                            var aux = 0;
                            var desc_recarg = document.getElementById('<%=txt_desc_rec.ClientID%>').value;
                            var precio_u = document.getElementById('<%=txt_precio_u.ClientID%>').value;
                            var cantidad = document.getElementById('<%=txt_cantidad.ClientID%>').value;
                            var lista_dcto = document.getElementById('<%=list_dcto_recargo.ClientID%>');
                             var lista_iva = document.getElementById('<%=List_iva.ClientID%>');
                            var porciento = 0;
                            var resultado = 0;
                               
                     //*   if (lista_iva.options[lista_iva.selectedIndex].text == "SI") {
                     //           iva = 1.19;
                       //     }
                         //   if (lista_iva.options[lista_iva.selectedIndex].text == "NO") {
                          //      iva = 1;
                           // }
                        
                            if (lista_dcto.options[lista_dcto.selectedIndex].text == "+%") {

                                if (desc_recarg == null) {
                                    porciento = 1;
                                }
                                if (desc_recarg == "") {
                                    porciento = 1;
                                } else {
                                    porciento = parseFloat(desc_recarg) / 100;
                                    aux = ((parseFloat(precio_u) * parseFloat(cantidad)) * parseFloat(porciento));
                                    resultado = ((parseFloat(precio_u) * parseFloat(cantidad)) + parseFloat(aux));

                                    document.getElementById('<%=txt_sub_uni.ClientID%>').value = parseFloat(resultado);
                                }
                            }
                                   if (lista_dcto.options[lista_dcto.selectedIndex].text == "-%") {

                                if (desc_recarg == null) {
                                    porciento = 1;
                                }
                                if (desc_recarg == "") {
                                    porciento = 1;
                                } else {
                                    porciento = parseFloat(desc_recarg) / 100;
                                    aux = ((parseFloat(precio_u) * parseFloat(cantidad)) * parseFloat(porciento));
                                    resultado = ((parseFloat(precio_u) * parseFloat(cantidad)) - parseFloat(aux)) ;

                                    document.getElementById('<%=txt_sub_uni.ClientID%>').value = parseFloat(resultado);
                                }
                            }

                        }            
                    </script>
            <div class="row-fluid ">
				<div class="box green span12">
					<div class="box-header" data-original-title="">
						<h2><i class="halflings-icon edit"></i><span class="break"></span>Detalle</h2>
						<div class="box-icon">
					<a href="#" data-toggle="modal" data-target="#ModalDetalles" ><i class="fa fa-plus" style="color:white"></i></a>
							<a href="#" class="btn-minimize"><i class="fa fa-angle-down" style="color:white"></i></a>					
						</div>
					</div>
					<div class="box-content" >
                                             
                     <table>
                          <tr>
                         <td>
                             <label class="col-xs-4 control-label p0right align-left"><a>Codigo:</a></label>
                                   <asp:TextBox ID="txt_codigo" runat="server" CssClass="form-control input-sm w90" Width="117px" ></asp:TextBox>                      
                        </td>
                         <td>
                                        <label class="col-xs-4 control-label p0right align-left"><a>Nombre:</a></label>
                        <asp:TextBox ID="txt_nombre" runat="server" CssClass="form-control input-sm w90" Width="117px" ></asp:TextBox>
                         </td>
                            <td>
                                        <label class="col-xs-4 control-label p0right align-left"><a>Descripcion:</a></label>
                         <asp:TextBox ID="txt_descripcion" runat="server" CssClass="form-control input-sm w90" Width="117px" ></asp:TextBox>
                          </td>
                           <td>            
                                      <label class="col-xs-4 control-label p0right align-left"><a>U.Medida:</a></label>
                               <asp:DropDownList ID="list_u_medida" runat="server"  Height="30px" Width="75px">

                                    <asp:ListItem Text="KG" Value="0" />
                                   <asp:ListItem Text="HORA" Value="0" />
                                   <asp:ListItem Text="UN" Value="0" />
                                   <asp:ListItem Text="CLP" Value="0" />
                               </asp:DropDownList> 
                         </td>
                              <td>
                                        <label class="col-xs-4 control-label p0right align-left"><a>Precio:</a></label>                                         
                         <asp:TextBox ID="txt_precio_u" runat="server" CssClass="form-control input-sm w90"  onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" onchange=" calculo_desc()" PlaceHolder="0" Width="100px" ></asp:TextBox>             
                         </td>
                              <td>
                                         <label class="col-xs-4 control-label p0right align-left"><a>Iva:</a></label>
                                 <asp:DropDownList ID="List_iva" runat="server"   Height="30px" 
                                     OnClientClick="return false" onchange=" calculo_desc()" Width="70px">
                                <asp:ListItem Text="SI" Value="0" />
                                <asp:ListItem Text="NO" Value="1" />                          
                           </asp:DropDownList> 
                                  </td>
                             <td>
                                      <label class="col-xs-4 control-label p0right align-left"><a>Cantidad:</a></label>
                          <asp:TextBox ID="txt_cantidad" runat="server" CssClass="form-control input-sm w90" onchange=" calculo_desc()"  onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" PlaceHolder="0" Width="50px"></asp:TextBox>

                             </td>
                          <td>
                                                   <label class="col-xs-4 control-label p0right align-left">Dcto/Recargo</label>                
                                        
                                         <asp:TextBox ID="txt_desc_rec" Width="50" runat="server" CssClass="form-control "                        
                              onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" onchange="calculo_desc()" PlaceHolder="0"></asp:TextBox>
                        </td>
                                 <td>  
                                                   <label class="col-xs-4 control-label p0right align-left">&nbsp;</label>          
                           <asp:DropDownList ID="list_dcto_recargo" runat="server"  Height="30px" onchange=" calculo_desc()" Width="52px">
                                <asp:ListItem Text="+%" Value="0" />
                                <asp:ListItem Text="-%" Value="1" />
                                <asp:ListItem Text="+$" Value="2" />
                                <asp:ListItem Text="-$" Value="3" />
                           </asp:DropDownList> 
                   
                     </td>
                              <td>
                           <label class="col-xs-4 control-label p0right align-left"><a>Total:</a></label>
                          <asp:TextBox ID="txt_sub_uni" runat="server" CssClass="form-control input-sm w90" Width="72px" ></asp:TextBox>
                             </td>
                            </tr>
                         <tr>
                             <td colspan="10">
                                 <asp:LinkButton ID="LinkButton5" runat="server" CssClass="btn btn-success" Text="Agregar Nuevo Detalle" OnClick="LinkButton5_Click" >Agregar Nuevo Detalle</asp:LinkButton>
                             </td>
                         </tr>
                               </table>
                          </div>               
                       </div>
                   
                 
               
                        <br>
             <div class="box  span12" style="width:80%" >
                <div class="box-content">
                    <div class="box-header">
                      <h2>Detalles </h2>
                    </div> 
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>  
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                 
                       <ContentTemplate>
                         <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" 
EmptyDataText="No se han agregado detalles ni servicios." OnRowDeleting="GridView1_RowDeleting" 
       CssClass="table table-bordered table-condensed table-hover" PagerStyle-CssClass="pager"
 HeaderStyle-CssClass="header" RowStyle-CssClass="rows" Width="823px" >
<Columns>
      <asp:BoundField DataField="NroLinDet" HeaderText="Item" ItemStyle-Width="120" >
<ItemStyle Width="120px"></ItemStyle>
      </asp:BoundField>
    <asp:BoundField DataField="VlrCodigo" HeaderText="Codigo" ItemStyle-Width="120" >
<ItemStyle Width="120px"></ItemStyle>
      </asp:BoundField>
    <asp:BoundField DataField="NmbItem" HeaderText="Nombre" ItemStyle-Width="120" >
<ItemStyle Width="120px"></ItemStyle>
      </asp:BoundField>
     <asp:BoundField DataField="DscItem" HeaderText="Descripcion" ItemStyle-Width="120" >
<ItemStyle Width="120px"></ItemStyle>
      </asp:BoundField>   
     <asp:BoundField DataField="UnmdItem" HeaderText="U.Medida" ItemStyle-Width="120" >
<ItemStyle Width="120px"></ItemStyle>
      </asp:BoundField>
    <asp:BoundField DataField="QtyItem" HeaderText="Cantidad" ItemStyle-Width="120" >
<ItemStyle Width="90px"></ItemStyle>
      </asp:BoundField>
    <asp:BoundField DataField="PrcItem" HeaderText="Precio" ItemStyle-Width="120" >
<ItemStyle Width="90px"></ItemStyle>
      </asp:BoundField>
     <asp:BoundField DataField="DescuentoPct" HeaderText="Dcto" ItemStyle-Width="120" >
<ItemStyle Width="90px"></ItemStyle>
      </asp:BoundField>
      <asp:BoundField DataField="RecargoPct" HeaderText="Recargo" ItemStyle-Width="120" >
<ItemStyle Width="90px"></ItemStyle>
      </asp:BoundField>
      <asp:BoundField DataField="MontoItem" HeaderText="Total" ItemStyle-Width="120" >
<ItemStyle Width="70px"></ItemStyle>
      </asp:BoundField>
           <asp:CommandField ShowDeleteButton="True" ButtonType="Button"   />    
</Columns>          
</asp:GridView>
                       
                    </ContentTemplate>
                    
              
                        </asp:UpdatePanel>
      </div>
                      </div>
                 
                <div class="box span12" style="width:80%" >
                <div class="box-content">
                    <div class="box-header">
                      <h2>Totales </h2>
                    </div>   
                	<table class="table table-bordered table-condensed table-hover" style="width: 82%" >                   
                        <tr>
                            <td class="auto-style12">
                                <label class="col-xs-5 control-label" for="">
                                Dsc. Global</label></td>
                            <td class="auto-style10">
                                 <asp:DropDownList ID="list_desc_rec_glob" runat="server"  CssClass="btn dropdown-toggle selectpicker btn-default" data-style="btn-primary" Height="30px" onchange=" calculo_desc()" Width="56px">
                                <asp:ListItem Text="+%" Value="0" />
                                <asp:ListItem Text="-%" Value="1" />
                                <asp:ListItem Text="+$" Value="2" />
                                <asp:ListItem Text="-$" Value="3" />
                           </asp:DropDownList> 
                            </td>
                            <td class="auto-style14">                                 
                            <asp:TextBox ID="txt_desc_rec_glob" runat="server" CssClass="form-control input-sm" Enabled="true" Width="67%"></asp:TextBox>
                            </td>
                            <td class="auto-style11">
                                      <label for="" class="col-xs-5 control-label"> Excento</label>
                            </td>
                            <td class="auto-style8">                               
                            <asp:TextBox ID="_txt_excento" runat="server" CssClass="form-control input-sm" Enabled="false" Width="91%"></asp:TextBox>
                            </td>
                               <td class="auto-style13">
                                   <label for="" class="col-xs-5 control-label"> IVA</label>
                               </td>
                             <td>
                                 <asp:TextBox ID="txt_iva" runat="server" CssClass="form-control input-sm" Enabled="false" Width="75%"></asp:TextBox>
                             </td>
                        </tr>
                         <tr>
                                <td class="auto-style12">
                                    <label for="" class="col-xs-5 control-label"> Neto</label>
                                   </td>
                            <td colspan="2">
                                 <asp:TextBox ID="txt_mnt_neto" runat="server" CssClass="form-control input-sm" Enabled="false" Width="80%"></asp:TextBox>
                            </td>
                            <td class="auto-style11">
                                <label for="" class="col-xs-5 control-label ">Tasa Iva</label>
                            </td>
                            <td class="auto-style8">
                                 <asp:TextBox ID="_txt_tasa_iva" runat="server" CssClass="form-control input-sm" Enabled="false" Width="91%"></asp:TextBox>      
                            </td>
                                <td class="auto-style13">
                                     <label for="" class="col-xs-5 control-label"> Total</label>
                                </td>
                             <td>
                                  <asp:TextBox ID="txt_total" runat="server" CssClass="form-control input-sm" Enabled="false" Width="75%"></asp:TextBox>
                             </td>
                        </tr>
                	</table>  
                    
                     <asp:LinkButton ID="desc_recargo" runat="server" CssClass="btn btn-info" Text="Agregar Descuento/Recargo" OnClick="desc_recargo_Click"  >Aplicar Descuento/Recargo</asp:LinkButton>                                	
                </div>                 
              </div>
            </div>
            </div>
                       <div id="btn" class="row m30top">
                    <div class="col-xs-12">
                      <div class="row">
                        <div class="col-xs-2">
                    
                        </div>
                      </div>          
                         <table>
                      <tr>
                          <td> <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-info" Text="Previsualizar" ></asp:LinkButton>&nbsp; </td>
                          <td>  <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-success" Text="Guardar y Emitir" OnClick="LinkButton2_Click" ></asp:LinkButton>&nbsp; </td>
                          <td><asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn btn-primary" Text="Guardar" ></asp:LinkButton>&nbsp; </td>
                          <td>
                        <asp:LinkButton ID="LinkButton7" runat="server" CssClass="btn btn-warning" Text="Cancelar" ></asp:LinkButton> </td>
                          <td></td>
                      </tr>
                        </table>
                        </div></div>
  <style type="text/css">
      .width {
   width: 90%;
  left: 5%; 
  overflow: inherit;
  margin-left:auto;
  margin-right:auto; 
}
  </style>

   <div class="modal hide fade width" id="ModalClientes" style="display: none;" aria-hidden="true">
		<div class="modal-header width">
			<button type="button" class="close" data-dismiss="modal">×</button>
			<h3>Clientes</h3>
		</div>
		<div class="modal-body width">
 
         <textarea id="TextArea_clientes" rows="2" cols="20" runat="server" ></textarea>
            <table id="tabla_clientes" class="table table-bordered table-condensed table-hover">
           <thead>
                   <tr>
                    <th>ID</th>
                    <th>RUT</th>              
                    <th>Razon Social</th>   
                    <th>Ciudad</th>     
                    <th>Comuna</th> 
                    <th>Giro</th>   
                    <th>Direccion</th>       
                    <th>telefono</th>
                    <th>Email</th> 
                    <th></th>    
                    </tr>
            </thead>
        </table>    
     <script type="text/javascript" src="Bootstrap/js 1.12.4/jquery 1.12.4.js"></script>
     <script type="text/javascript" src="Bootstrap/js 1.12.4/datatable 1.10.4.js"></script>   
  <script type="text/javascript">

      var testdata = document.getElementById('<%=TextArea_clientes.ClientID%>').value;
          testdata = JSON.parse(testdata);
      
      var table0 = $('#tabla_clientes').DataTable({
             "pageLength":6,
              "aaData": testdata,
              "aoColumns": [{
                  "mDataProp": "ID_CLIENTE",
                  'visible' : false 
              }, {
                  "mDataProp": "RUT_CLIENTE"
              }, {
                  "mDataProp": "RZN_SOC_CLIE"
              }, {
                  "mDataProp": "CIUDAD"
              }, {
                  "mDataProp": "COMUNA"
              }, {
                  "mDataProp": "GIRO_CLIENTE"
              }, {
                  "mDataProp": "DIRECCION_CLIENTE"
              }, {
                  "mDataProp": "TELEFONO_CLIENTE"
              }, {
                  "mDataProp": "EMAIL_CLIENTE"
              }, {
                  "defaultContent": "<button  class=\"btn btn-primary\" data-dismiss=\"modal\">Seleccionar</button>"
              }]
          });

          $('#tabla_clientes tbody').on('click', 'button', function () {
              var data = table0.row($(this).parents('tr')).data();
              var algo = JSON.stringify(data);
              alert(algo);
              var data = JSON.parse(algo);

              var rut = "RUT_CLIENTE";            
              rut in data            
              document.getElementById('<%=txt_rut_recep.ClientID%>').value = data[rut];
              var rzn_soc = "RZN_SOC_CLIE";
              rzn_soc in data
              document.getElementById('<%=txt_rzn_recep.ClientID%>').value = data[rzn_soc];
              var giro_cliente = "GIRO_CLIENTE";
              giro_cliente in data
              document.getElementById('<%=txt_giro_recep.ClientID%>').value = data[giro_cliente];
              var comuna = "COMUNA";
              comuna in data
              document.getElementById('<%=txt_comuna_recep.ClientID%>').value = data[comuna];
              var direccion = "DIRECCION_CLIENTE";
              direccion in data
              document.getElementById('<%=txt_dir_recep.ClientID%>').value = data[direccion];
              var ciudad = "CIUDAD";
              ciudad in data
              document.getElementById('<%=txt_ciudad_recep.ClientID%>').value = data[ciudad];

          });
         
  </script>
</div>
		<div class="modal-footer">
			<a href="#" class="btn" data-dismiss="modal">Close</a>
			<a href="#" class="btn btn-primary">Save changes</a>
		</div>
	</div>
      
    <div class="modal hide fade width" id="ModalDetalles" style="display: none;" aria-hidden="true">
		<div class="modal-header width">
			<button type="button" class="close" data-dismiss="modal">×</button>
			<h3>Detalles</h3>
		</div>
		<div class="modal-body width">
         <script type="text/javascript" src="Bootstrap/js 1.12.4/jquery 1.12.4.js"></script>
         <script type="text/javascript" src="Bootstrap/js 1.12.4/datatable 1.10.4.js"></script>
            <textarea id="TextArea_detalles" cols="20" rows="2" runat="server"></textarea>
              <table id="tabla_detalles" class="table table-bordered table-condensed table-hover">
                  <thead>
                     <tr>
                     <th>ID_DETALLE</th>
                     <th>ID_EMPRESA</th>  
                     <th>NOMBRE</th> 
                     <th>CODIGO</th>
                     <th>UMEDIDA</th>
                     <th>STOCK</th>
                     <th>DESCRIPCION_PRODUCTO</th>
                     <th>VALOR_UNITARIO</th>
                     <th>FECHA_CREACION</th>
                     <th>DESCUENTO_PCT</th>
                     <th>ESTADO</th>
                     <th></th>
                     </tr>
                  </thead>
              </table>


            <script type="text/javascript">
          
             var detalles = document.getElementById('<%=TextArea_detalles.ClientID%>').value;
                 detalles = JSON.parse(detalles);

                var table1 = $('#tabla_detalles').DataTable({
                               "pageLength":6,
                               "aaData": detalles,
                               "aoColumns": [{
             "mDataProp": "ID_DETALLE",
              'visible' : false 
              }, {
              "mDataProp": "ID_EMPRESA",
              'visible': false
              }, {
                  "mDataProp": "NOMBRE"
              }, {
                  "mDataProp": "CODIGO"
              }, {
                  "mDataProp": "UMEDIDA"
              }, {
                  "mDataProp": "STOCK"
              }, {
                  "mDataProp": "DESCRIPCION_PRODUCTO"
              }, {
                  "mDataProp": "VALOR_UNITARIO"
              }, {
                  "mDataProp": "FECHA_CREACION"
              }, {
                  "mDataProp": "DESCUENTO_PCT"
              }, {
                  "mDataProp": "ESTADO"
              }, {
                  "defaultContent": "<button class=\"btn btn-primary\" data-dismiss=\"modal\">Seleccionar</button>"
               }]
          });

            $('#tabla_detalles tbody').on('click', 'button', function () {
              var data = table1.row($(this).parents('tr')).data();
              var algo = JSON.stringify(data);
              alert(algo);
              var data = JSON.parse(algo);


              var codigo = "CODIGO";
              codigo in data            
              document.getElementById('<%=txt_codigo.ClientID%>').value = data[codigo];

              var nombre = "NOMBRE";            
              nombre in data            
              document.getElementById('<%=txt_nombre.ClientID%>').value = data[nombre];

              var descripcion = "DESCRIPCION_PRODUCTO";            
              descripcion in data            
              document.getElementById('<%=txt_descripcion.ClientID%>').value = data[descripcion];

              var u_medida = "UMEDIDA";            
              u_medida in data
              document.getElementById('<%=list_u_medida.ClientID%>').value = data[u_medida];

              var valor = "VALOR_UNITARIO";
              valor in data
              document.getElementById('<%=txt_precio_u.ClientID%>').value = data[valor];
              
             
          });
            </script>

       </div>
		<div class="modal-footer">
			<a href="#" class="btn" data-dismiss="modal">Close</a>
			<a href="#" class="btn btn-primary">Save changes</a>
		</div>
	</div>


</asp:Content>
