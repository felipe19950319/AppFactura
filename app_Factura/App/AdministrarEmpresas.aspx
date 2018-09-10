<%@ Page Title="" Language="C#" MasterPageFile="~/App/Site1.Master" AutoEventWireup="true" CodeBehind="AdministrarEmpresas.aspx.cs" Inherits="app_Factura.App.AdministrarEmpresas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
                                     <asp:DropDownList ID="DropEmpresa" runat="server" CssClass="form-control" >

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
