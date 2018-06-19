<%@ Page Title="" Language="C#" MasterPageFile="~/App/Site1.Master" AutoEventWireup="true" CodeBehind="EmisionDocumentos.aspx.cs" Inherits="app_Factura.App.EmisionDocumentos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script src="../Common/App/EmisionDocumentos.js"></script>
    <br/>
    <br/>
    <br/>  
      <div class="content-wrapper">
           <div class="row">
                <div class="col-md-12">

                     <div class="container-fluid">
                         
                   
                         <!--emisor -->
                          <div class="row">
                            <div class="col-lg-8">
                               <div class="card mb-3">
                                    <div class="card-header bg-success">
                                     <a class="text-white"> <i class="fa fa-user"></i> Datos del Emisor</a>
                                    </div>
                                    <div class="card-body">
                                <div class="col-md-12">  
                                  <div class="row">
                                         <div class="col-md-4">
                                          Rut emisor:<br/>
                                             <input type="text" id="txtRutEmisor" class="form-control"/>
                                         </div>
                                          <div class="col-md-4">
                                          Rut envia:<br/>
                                             <input type="text" id="txtRutEnvia" class="form-control"/>
                                         </div>

                                         <div class="col-md-4">
                                          Fecha resolucion:<br/>
                                             <input type="date" id="txtFechaResolucion" class="form-control"/>
                                         </div>
                                   
                                     </div>

                                     <div class="row">
                                         <div class="col-md-4">
                                          Razon social:<br/>
                                             <input type="text" id="txtRazonSocial" class="form-control"/>
                                         </div>
                                          <div class="col-md-4">
                                          Giro:<br/>
                                             <input type="text" id="txtGiro" class="form-control"/>
                                         </div>

                                         <div class="col-md-4">
                                          Direccion:<br/>
                                             <input type="text" id="txtDireccion" class="form-control"/>
                                         </div>
                                         
                                     </div>
                                </div>
                                    </div>
                                     <div class="card-footer small text-muted">&nbsp</div>
                               </div>
                            </div>   
                          </div> 
                     <!--emisor -->  
                       
                         <!--receptor-->
                            <div class="row">
                               <div class="col-lg-8">
                               <div class="card mb-3">
                                    <div class="card-header bg-primary">
                            
                                         <a class="text-white"> <i class="fa fa-user"></i> Datos del Receptor</a>
                                    </div>
                                    <div class="card-body">
                                <div class="col-md-12">  
                                  <div class="row">
                                         <div class="col-md-4">
                                          Rut Receptor:<br/>
                                             <input type="text" id="txtRutReceptor" class="form-control"/>
                                         </div>
                                          <div class="col-md-4">
                                          Razon social:<br/>
                                             <input type="text" id="txtRazonSocialReceptor" class="form-control"/>
                                         </div>

                                         <div class="col-md-4">
                                          Giro:<br/>
                                             <input type="text" id="txtGiroReceptor" class="form-control"/>
                                         </div>
                                   
                                     </div>

                                     <div class="row">
                                         <div class="col-md-4">
                                          Comuna:<br/>
                                             <input type="text" id="txtComunaReceptor" class="form-control"/>
                                         </div>
                                          <div class="col-md-4">
                                          Direccion:<br/>
                                             <input type="text" id="txtDireccionReceptor" class="form-control"/>
                                         </div>

                                         <div class="col-md-4">
                                          Ciudad:<br/>
                                             <input type="text" id="txtCiudadReceptor" class="form-control"/>
                                         </div>
                                         
                                     </div>
                                </div>
                                    </div>
                                     <div class="card-footer small text-muted">&nbsp</div>
                               </div>
                            </div> 
                       </div>
                         <!--receptor-->

                          <!--detalle-->
                             <div class="row">
                              <div class="col-lg-12">
                               <div class="card mb-3">
                                    <div class="card-header bg-secondary">
                            
                                         <a class="text-white"> <i class="fa fa-clipboard"></i> Detalle</a>
                                    </div>
                                    <div class="card-body">
                                <div class="col-md-12">  

                                    <div class="row">                                      

                                     <div class="col-md-2">
                                        <input id="btnAddDetalle" type="button" class="btn btn-danger" value="Agregar nuevo detalle"/>         
                                      </div> 
                                          <div class="col-md-2">
                                        <input id="btnAddListaDetalle" type="button" class="btn btn-success" value="Agregar desde lista"/>         
                                      </div>                        
                                    </div>
                                    <br/>
                                      
                                  <div class="row">
                                       <div class="col-md-12">
                                        <table id="TablaDetalles">
                                            <thead></thead>
                                            <tbody></tbody>
                                        </table>
                                        </div>
                                 </div>

                                </div>
                                                                                       
                               </div>
                                    </div>
                                     <div class="card-footer small text-muted">&nbsp</div>
                               </div>
                           </div> 
                          <!--detalle-->

                         <!--referencias-->
                           <div class="row">
                              <div class="col-lg-8">
                               <div class="card mb-3">
                                    <div class="card-header bg-danger">
                            
                                         <a class="text-white"> <i class="fa fa-clipboard"></i> Referencias</a>
                                    </div>
                                    <div class="card-body">
                                <div class="col-md-12">  
                                  <div class="row">
                                         <div class="col-md-3">
                                          Documento:<br/>
                                            <select id="DocReferencia" class="form-control"></select>
                                         </div>
                                          <div class="col-md-3">
                                          Folio:<br/>
                                             <input type="text" id="txtFolioReferencia" class="form-control"/>
                                         </div>

                                         <div class="col-md-3">
                                          Fecha:<br/>
                                             <input type="date" id="txtFechaReferencia" class="form-control"/>
                                         </div>
                                        <div class="col-md-3">
                                          Razon Referencia:<br/>
                                            <select id="RazonReferencia" class="form-control"></select>
                                         </div>                                 
                                  </div>     
                                  <div class="row">
                                      <!--tabla referencias-->
                                      </div>
                                   </div>                                                           
                               </div>
                                    </div>
                                     <div class="card-footer small text-muted">&nbsp</div>
                               </div>
                           </div>             
                         <!--referencias-->

                    

                     </div>
                 </div>                                         
              </div>
          </div>
 
 


</asp:Content>
