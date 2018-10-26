<%@ Page Title="" Language="C#" MasterPageFile="~/App/Site1.Master" AutoEventWireup="true" CodeBehind="SeleccionEmpresa.aspx.cs" Inherits="app_Factura.App.SeleccionEmpresa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../Common/App/SeleccionEmpresa.js"></script>
      <br/>
    <br/>
    <br/>  
      <div class="content-wrapper">
           <div class="row">
                <div class="col-md-12">
                     <div class="container-fluid">
                          <div class="col-lg-8">
                             <div class="row">
                                     <div class="col-md-8">
                                     <h4>Seleccionar empresa</h4>
                                         <br/>
                                         Empresa:
                                     <asp:DropDownList ID="DropEmpresa" runat="server" CssClass="form-control" >

                                     </asp:DropDownList>
                                         <br/>
                                         Ambiente
                                     <asp:DropDownList ID="DropAmbiente" runat="server" CssClass="form-control" >
                                         <asp:ListItem Value="CERT">Certificacion</asp:ListItem>
                                         <asp:ListItem Value="PROD">Produccion</asp:ListItem>
                                     </asp:DropDownList>    
                                    </div>
                             </div>
                              <br/>
                              <div class="row">
                                  <div class="col-md-2">
                                      <input type="button" id="btnSelectEmpresa" class="btn btn-danger" value="Seleccionar empresa"/>
                               
                                  </div>
                              </div>
                          </div>
                         </div>
                    </div>
               </div>
          </div>
</asp:Content>
