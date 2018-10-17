<%@ Page Title="" Language="C#" MasterPageFile="~/App/Site1.Master" AutoEventWireup="true" CodeBehind="AdministracionFolios.aspx.cs" Inherits="app_Factura.App.AdministracionFolios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../Common/App/AdministracionFolios.js"></script>
        <script src="../Common/App/Utilities/dte.js"></script>
     <br/><br/><br/>
     <div class="content-wrapper">

           <div class="row">
                <div class="col-md-10">
                     <div class="container-fluid">
                         
                           <div class="row">
                            <div class="col-lg-10">
                               <div class="card mb-3" style="width:120%">
                                    <div class="card-header bg-info">
                                     <a class="text-white"> <i class="fa fa-user"></i> Administracion de folios</a>
                                    </div>
                                    <div class="card-body">
                                <div class="col-md-12">  

                                     <div class="row">
                                         <div class="col-md-4">
                                          Rut Empresa:<br/>
                                             <input type="text" id="txtRutEmpresa" class="form-control form-control-sm" disabled="disabled"/>
                                         </div>

                                         <div class="col-md-4">
                                          Fecha Autorizacion:<br/>
                                             <input type="text" id="txtFechaAut" class="form-control form-control-sm" disabled="disabled"/>
                                         </div>

                                        <div class="col-md-4">
                                          Tipo Documento:<br/>
                                             <input type="text" id="txtTipoDoc" class="form-control form-control-sm" disabled="disabled"/>
                                         </div>

                                     </div>
                                    
                                     <div class="row">
                                         <div class="col-md-4">
                                          Folio desde:<br/>
                                             <input type="text" id="txtFolioD" class="form-control form-control-sm" disabled="disabled"/>
                                         </div>

                                         <div class="col-md-4">
                                          Folio hasta:<br/>
                                             <input type="text" id="txtFolioH" class="form-control form-control-sm" disabled="disabled"/>
                                         </div>

                                 

                                     </div>

                                  <div class="row">
                                         <div class="col-md-4">
                                          Ruta Folio:<br/>
                                             <input type="file" id="txtRutaFolio" class="form-control form-control-sm" accept=".xml"/>
                                         </div> 

                                     </div>
                                    <br/>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <input type="button" id="btnVerificar" class="btn btn-default" value="Verificar Folio"/>
                                            </div>

                                              <div class="col-md-2">
                                              <input type="button" id="btnGuardar" class="btn btn-info" value="Guardar Folio"/>
                                              </div>

                                        </div>
                                </div>
                                     
                                    </div>
                                  
                               </div>
                            </div>   
                          </div> 


                     </div>
                </div>
           </div>
     </div>

</asp:Content>
