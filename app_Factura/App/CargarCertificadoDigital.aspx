<%@ Page Title="" Language="C#" MasterPageFile="~/App/Site1.Master" AutoEventWireup="true" CodeBehind="CargarCertificadoDigital.aspx.cs" Inherits="app_Factura.App.CargarCertificadoDigital" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../Common/App/Utilities/Utilities.js"></script>
    <script src="../Common/App/CargarCertificadoDigital.js"></script>
    <script src="../Common/App/Utilities/dte.js"></script>
    <br/><br/><br/>
     <div class="content-wrapper">
         <script type="text/javascript">
             function ShowTextPass() {
                 var x = document.getElementById("txtPassCertificado");
                 if (x.type === "password") {
                     x.type = "text";
                 } else {
                     x.type = "password";
                 }
             }
         </script>

           <div class="row">
                <div class="col-md-10">
                     <div class="container-fluid">
                         
                           <div class="row">
                            <div class="col-lg-10">
                               <div class="card mb-3" style="width:120%">
                                    <div class="card-header bg-success">
                                     <a class="text-white"> <i class="fa fa-user"></i>Cargar certificado digital</a>
                                    </div>
                                    <div class="card-body">
                                <div class="col-md-12">  
                                  <div class="row">
                                         <div class="col-md-4">
                                          Ruta certificado:<br/>
                                             <input type="file" id="txtRutaCertificado" class="form-control form-control-sm" accept=".pfx"/>
                                         </div> 
                                         <div class="col-md-4">
                                          Password:<br/>
                                             <input type="password" id="txtPassCertificado" class="form-control form-control-sm"/>
                                             <input type="checkbox" onclick="ShowTextPass()"/>Mostrar Contraseña
                                         </div> 
                                     </div>
                                        <div class="row">
                                            <div class="col-md-2">

                                                <input type="button" id="btnGetFile" class="btn btn-default" value="Cargar Certificado"/>
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
