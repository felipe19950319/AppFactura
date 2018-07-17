﻿<%@ Page Title="" Language="C#" MasterPageFile="~/App/Site1.Master" AutoEventWireup="true" CodeBehind="EmisionDocumentos.aspx.cs" Inherits="app_Factura.App.EmisionDocumentos" %>
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
                                             <input type="text" id="txtRutEmisor" class="form-control" disabled="disabled"/>
                                         </div>
                                         <!-- <div class="col-md-4">
                                          Rut envia:<br/>
                                             <input type="text" id="txtRutEnvia" class="form-control"/>
                                         </div>-->
                                          <div class="col-md-4">
                                          Razon social:<br/>
                                             <input type="text" id="txtRazonSocial" class="form-control" disabled="disabled"/>
                                         </div>
                                         <div class="col-md-4">
                                          Fecha resolucion:<br/>
                                             <input type="date" id="txtFechaResolucion" class="form-control" disabled="disabled"/>
                                         </div>
                                   
                                     </div>

                                     <div class="row">
                                     
                                         <!-- <div class="col-md-4">
                                          Giro:<br/>
                                             <input type="text" id="txtGiro" class="form-control"/>
                                         </div>-->

                                       <!--  <div class="col-md-4">
                                          Direccion:<br/>
                                             <input type="text" id="txtDireccion" class="form-control"/>
                                         </div>-->
                                         
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

                                        <a id="AddReceptor" class="btn btn-sm btn-primary float-sm-right text-white"> <i class="fa fa-plus"></i> Nuevo Receptor</a>
                                    </div>
                                    <div class="card-body">
                                <div class="col-md-12">  
                               
                                  <div class="row">
                                         <div class="col-md-4">
                                             Rut Receptor:<br/>
                                            <div class="input-group mb-3">
                                              <input type="text" class="form-control" disabled="disabled" id="txtRutReceptor"/>
                                              <div class="input-group-append">
                                                <button id="btnModalReceptor" class="btn btn-success" type="button">
                                                    <i class="fa fa-search" style="color:white" aria-hidden="true"></i>
                                                </button>
                                              </div>
                                            </div>

                                          
                                             <!--<input type="text" id="txtRutReceptor" class="form-control"/>-->
                                         </div>
                                          <div class="col-md-4">
                                          Razon social:<br/>
                                             <input type="text" id="txtRazonSocialReceptor" class="form-control" disabled="disabled"/>
                                         </div>

                                         <div class="col-md-4">
                                          Giro:<br/>
                                             <input type="text" id="txtGiroReceptor" class="form-control" disabled="disabled"/>
                                         </div>
                                   
                                     </div>

                                     <div class="row">
                                         <div class="col-md-4">
                                          Comuna:<br/>
                                             <input type="text" id="txtComunaReceptor" class="form-control" disabled="disabled"/>
                                         </div>
                                          <div class="col-md-4">
                                          Direccion:<br/>
                                             <input type="text" id="txtDireccionReceptor" class="form-control" disabled="disabled"/>
                                         </div>

                                         <div class="col-md-4">
                                          Ciudad:<br/>
                                             <input type="text" id="txtCiudadReceptor" class="form-control" disabled="disabled"/>
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
                              <div class="col-lg-10">
                               <div class="card mb-3" style="width:120%">
                                    <div class="card-header bg-secondary">
                            
                                         <a class="text-white"> <i class="fa fa-clipboard"></i> Detalle</a>
                                    </div>
                                    <div class="card-body">
                                <div class="col-md-12">  

                                                                                
                                       <div class="row">       
                                            
                                            <div class="col-md-2">
                                                    <input id="btnAddDetalle" type="button" class="btn btn-danger btn-sm" value="Agregar nuevo detalle"/>         
                                            </div>
                                             <div class="col-md-2">
                                                    <input id="btnAddListaDetalle" type="button" class="btn btn-success btn-sm" value="Agregar desde lista"/>         
                                            </div>  
                                            <div class="col-md-8">
                                                  <input id="PruebaTotal" type="button" class="btn btn-success btn-sm" value="Prueba total"/>         
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
                                  <!--   <div class="card-footer small text-muted">&nbsp</div>-->
                               </div>
                           </div>             
                         <!--referencias-->

                    

                     </div>
                 </div>                                         
              </div>
          </div>
 
 


</asp:Content>
