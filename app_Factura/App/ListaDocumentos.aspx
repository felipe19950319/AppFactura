<%@ Page Title="" Language="C#" MasterPageFile="~/App/Site1.Master" AutoEventWireup="true" CodeBehind="ListaDocumentos.aspx.cs" Inherits="app_Factura.App.ListaDocumentos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../Common/App/ListaDocumentos.js"></script>
    <br/>
    <br/>
    <br/>  
      <div class="content-wrapper">
           <div class="row">
                <div class="col-md-12">
                     <div class="container-fluid">
                          <div class="col-lg-12">
                             <div class="row">
                                     <div class="col-md-8">
                                     <h4>Lista Documentos</h4>
                             
                                    </div>
                             </div>
                              <br/>
                              <div class="row">
                   
                                      <div class="col-md-12">
                                        <table id="tblDocumentos" class="display compact">
                                            <thead></thead>
                                            <tbody></tbody>
                                        </table>
                                        </div>
                                 
                              </div>
                          </div>
                         </div>
                    </div>
               </div>
          </div>

</asp:Content>
