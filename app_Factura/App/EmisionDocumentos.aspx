<%@ Page Title="" Language="C#" MasterPageFile="~/App/Site1.Master" AutoEventWireup="true" CodeBehind="EmisionDocumentos.aspx.cs" Inherits="app_Factura.App.EmisionDocumentos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script src="../Common/App/EmisionDocumentos.js"></script>


           <div class="sticky-top">
                  <div class="list-group">
                    <div class="list-group-item active"><br/></div>
                    <div class="list-group-item bg-light" style="height: 45px; padding: 10px 15px;">            
                       <div class="row">         
                        <div class="col-sm-2">
                        </div>
                        <div class="col-sm-9">
                            <div class="row">
                                &nbsp&nbsp&nbsp&nbsp &nbsp&nbsp
                                 <a class="btn btn-primary btn-xs text-white" id="btnGuardarDocumento"><i class="fa fa-floppy-o"></i> Guardar borrador</a> &nbsp&nbsp
                                 <a class="btn btn-warning btn-xs text-white" id="btnGuardarEmitir"><i class="fa fa-external-link"></i> Guardar y emitir</a> &nbsp&nbsp
                                 <a class="btn btn-danger btn-xs text-white" ><i class="fa fa-paper-plane-o"></i> Emitir</a> &nbsp&nbsp   
                                 <a class="btn btn-secondary btn-xs text-white" id="btnPrevisualizar" ><i class="fa fa-file-o"></i> Previsualizar</a> &nbsp&nbsp                            
                            </div>
                       </div>
                            
                      </div>                   
                    </div>      
                       
                  </div>
                </div>

   <asp:HiddenField  id="TipoOperacion" runat="server" ClientIDMode="Static"/>    
    
                                     
      <div class="content-wrapper">

           <div class="row">
                <div class="col-md-10">

                     <div class="container-fluid">
                   
                   
                         <!--emisor -->
                          <div class="row">
                            <div class="col-lg-10">
                               <div class="card mb-3" style="width:120%">
                                    <div class="card-header bg-success">
                                     <a class="text-white"> <i class="fa fa-user"></i> Datos del Emisor</a>
                                    </div>
                                    <div class="card-body">
                                <div class="col-md-12">  
                                  <div class="row">
                                         <div class="col-md-4">
                                          Rut emisor:<br/>
                                             <input type="text" id="txtRutEmisor" class="form-control form-control-sm" disabled="disabled"/>
                                         </div>
                                         <!-- <div class="col-md-4">
                                          Rut envia:<br/>
                                             <input type="text" id="txtRutEnvia" class="form-control"/>
                                         </div>-->
                                          <div class="col-md-4">
                                          Razon social:<br/>
                                             <input type="text" id="txtRazonSocial" class="form-control form-control-sm" disabled="disabled"/>
                                         </div>
                                         <div class="col-md-4">
                                          Fecha resolucion:<br/>
                                             <input type="date" id="txtFechaResolucion" class="form-control form-control-sm" disabled="disabled"/>
                                         </div>
                                   <div style="display:none">
                                         <input type="text" id="txtGiroEmisor" class="form-control form-control-sm" disabled="disabled"/>
                                         <input type="text" id="txtActecoEmisor" class="form-control form-control-sm" disabled="disabled"/>
                                         <input type="text" id="txtDireccionEmisor" class="form-control form-control-sm" disabled="disabled"/>
                                         <input type="text" id="txtComunaEmisor" class="form-control form-control-sm" disabled="disabled"/>
                                         <input type="text" id="txtCiudadEmisor" class="form-control form-control-sm" disabled="disabled"/>
                                    </div>

                                     </div>
                       
                                </div>
                                    </div>
                                  
                               </div>
                            </div>   
                          </div> 
                     <!--emisor -->  
                       
                         <!--receptor-->
                            <div class="row">
                               <div class="col-lg-10">
                               <div class="card mb-3" style="width:120%">
                                    <div class="card-header bg-primary">
                            
                                         <a class="text-white"> <i class="fa fa-user"></i> Datos del Receptor</a>

                                        <a id="AddReceptor" class="btn btn-sm btn-primary float-sm-right text-white"> <i class="fa fa-plus"></i> Nuevo Receptor</a>
                                    </div>
                                    <div class="card-body">
                                <div class="col-md-12">  
                               
                                  <div class="row">
                                         <div class="col-md-4">
                                             Rut Receptor:<br/>
                                            <div class="input-group mb-3">
                                              <input type="text" class="form-control form-control-sm" disabled="disabled" id="txtRutReceptor"/>
                                              <div class="input-group-append">
                                                <button id="btnModalReceptor" class="btn btn-sm btn-success" type="button">
                                                    <i class="fa fa-search" style="color:white" aria-hidden="true"></i>
                                                </button>
                                              </div>
                                            </div>

                                          
                                             <!--<input type="text" id="txtRutReceptor" class="form-control"/>-->
                                         </div>
                                          <div class="col-md-4">
                                          Razon social:<br/>
                                             <input type="text" id="txtRazonSocialReceptor" class="form-control form-control-sm" disabled="disabled"/>
                                         </div>

                                         <div class="col-md-4">
                                          Giro:<br/>
                                             <input type="text" id="txtGiroReceptor" class="form-control form-control-sm" disabled="disabled"/>
                                         </div>
                                   
                                     </div>

                                     <div class="row">
                                         <div class="col-md-4">
                                          Comuna:<br/>
                                             <input type="text" id="txtComunaReceptor" class="form-control form-control-sm" disabled="disabled"/>
                                         </div>
                                          <div class="col-md-4">
                                          Direccion:<br/>
                                             <input type="text" id="txtDireccionReceptor" class="form-control form-control-sm" disabled="disabled"/>
                                         </div>

                                         <div class="col-md-4">
                                          Ciudad:<br/>
                                             <input type="text" id="txtCiudadReceptor" class="form-control form-control-sm" disabled="disabled"/>
                                         </div>
                                         
                                     </div>
                                </div>
                                    </div>
                                
                               </div>
                            </div> 
                       </div>
                         <!--receptor-->

                          <!--detalle-->
                             <div class="row">
                              <div class="col-lg-10">
                               <div class="card mb-3" style="width:120%">
                                    <div class="card-header bg-secondary">
                            
                                         <a class="text-white"> <i class="fa fa-clipboard"></i> Detalle</a>

                                            <a id="btnAddDetalle" class="btn btn-sm btn-secondary float-sm-right text-white"> <i class="fa fa-plus"></i> Nuevo detalle</a>
                                    </div>
                                    <div class="card-body">
                                <div class="col-md-12">  

                                                                                
                                       <div class="row">       
                                            
                                       
                                             <div class="col-md-2">
                                                    <input id="btnAddListaDetalle" type="button" class="btn btn-success btn-sm" value="Agregar desde lista"/>         
                                            </div>                             
                                    </div>   
                                    <div class="row">       
                                       <div class="col-md-12">
                                        <table id="TablaDetalles" class="display compact">
                                            <thead></thead>
                                            <tbody></tbody>
                                        </table>
                                        </div>
                                    </div>    
                                    
                                           <div class="row">

                                            <div class="col-md-2">
                                                Dscto. Global:<br/>
                                            <div class="input-group mb-3">
                                              <input type="text" class="form-control form-control-sm" id="txtDctoRcrgoTotal"/>
                                              <div class="input-group-append">
                                                <select id="ListaDctoRcrgoTotal" class="form-control form-control-sm">
                                                    <option value="0">NO</option>
                                                    <option value="1">+%</option>
                                                    <option value="2">-%</option>
                                                    <option value="3">+$</option>
                                                    <option value="4">-$</option>
                                                </select>
                                              </div>
                                            </div>

                                                </div>
                                            <div class="col-md-2">
                                                     Exento:<br/>
                                                   <input type="text" id="txtExentoTot" class="form-control form-control-sm" disabled="disabled" value="0"/>
                                   
                                                </div>
                                            <div class="col-md-2">
                                                Tasa Iva:<br/>
                                                   <input type="text" id="txtTasaIvaTot" class="form-control form-control-sm" disabled="disabled" value="19.00 %"/>
                                                </div>
                                            <div class="col-md-2">
                                                Iva:<br/>
                                                  <input type="text" id="txtIvaTot" class="form-control form-control-sm" disabled="disabled" value="0"/>
                                                </div>

                                            <div class="col-md-2">
                                                Neto:<br/>
                                                 <input type="text" id="txtNetoTot" class="form-control form-control-sm" disabled="disabled" value="0"/>
                                            </div>

                                            <div class="col-md-2">
                                                Total:<br/>
                                                <input type="text" id="txtDetTotal" class="form-control form-control-sm" disabled="disabled" value="0"/>
                                                </div>
                                        </div>                             
                           

                                </div>
                                                                                       
                               </div>
                                    </div>
                                     <!--<div class="card-footer small text-muted">&nbsp</div>-->
                               </div>
                           </div> 
                          <!--detalle-->

                         <!--referencias-->
                           <div class="row">
                              <div class="col-lg-10">
                               <div class="card mb-3"  style="width:120%">
                                    <div class="card-header bg-danger">
                            
                                         <a class="text-white"> <i class="fa fa-clipboard"></i> Referencias</a>
                                    </div>
                                    <div class="card-body">
                                <div class="col-md-12">  
                                  <div class="row">
                                         <div class="col-md-3">
                                          Documento:<br/>
                                            <select id="DocReferencia" class="form-control form-control-sm"></select>
                                         </div>
                                          <div class="col-md-3">
                                          Folio:<br/>
                                             <input type="text" id="txtFolioReferencia" class="form-control form-control-sm"/>
                                         </div>

                                         <div class="col-md-3">
                                          Fecha:<br/>
                                             <input type="date" id="txtFechaReferencia" class="form-control form-control-sm"/>
                                         </div>
                                        <div class="col-md-3">
                                          Razon Referencia:<br/>
                                            <select id="RazonReferencia" class="form-control form-control-sm"></select>
                                         </div>                                 
                                  </div>     
                                  <div class="row">
                                      <!--tabla referencias-->
                                      </div>
                                   </div>                                                           
                               </div>
                                    </div>
                                  <!--   <div class="card-footer small text-muted">&nbsp</div>-->
                               </div>
                           </div>             
                         <!--referencias-->
                                            
                   

                       <!--  <div class="col-md-12">
                            <div class="row">

                                <div class="col-md-3">
                                    <input type="button" class="btn btn-default" id="BtnPrevisualizar" value="Previsualizar"/>
                                </div>

                                <div class="col-md-3">
                                    <input type="button" class="btn btn-warning" id="BtnEmitir" value="Emitir"/>
                                </div>

                                <div class="col-md-3">
                                    <input type="button" class="btn btn-success" id="BtnGuardar" value="Guardar y emitir despues"/>
                                </div>

                                <div class="col-md-3">
                                    <input type="button" class="btn btn-danger" id="BtnGuardarEmitir" value="Guardar y Emitir"/>
                                </div>

                            </div>                         
                         </div>-->

                     </div>
                 </div>                                         
              </div>
          </div>
 
 


</asp:Content>
