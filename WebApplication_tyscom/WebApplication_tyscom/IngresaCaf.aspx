<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="IngresaCaf.aspx.cs" Inherits="WebApplication_tyscom.IngresaCaf" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 749px;
            height: 13px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row form-horizontal">
       <div class="col-xs-10">
              <h2>&nbsp;&nbsp; Subir Timbre Electronico</h2>
             <h4> &nbsp;&nbsp; Seleccione Archivo de Timbre Electronico</h4>
            </div>
    
    
                  
                       
                         
                                    
                      
                <table class="row-fluid">
                    <tr>
                        <td>
                           
                        </td>
                    </tr>
                            <tr>
                                <td class="auto-style4"></td>
                                <td class="auto-style4">&nbsp;&nbsp;&nbsp; &nbsp;</td>
                                <td class="auto-style3">
                                    <script type="text/javascript">

                                            
                                        function UploadFile(fileUpload) {
                                                if (fileUpload.value != '') {
                                                    document.getElementById("<%=btnUploadDoc.ClientID %>").click();
                                                }
                                            }
                                    </script>
                              <asp:FileUpload ID="fuDocument" runat="server"  OnChange="UploadFile(this);" Width="432px" />
    <br />
                                Archivo:  <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                    <br />
    <asp:Label ID="lblMsg" runat="server" Text="Documento Valido Como CAF(Timbre Electronico)." Visible="false" ForeColor="Green" />
                                      <asp:Label ID="Label1" runat="server" Text="Documento No Valido Como CAF(Timbre Electronico)." Visible="false" ForeColor="red" />
    <asp:Button ID="btnUploadDoc" Text="Upload" runat="server" OnClick="UploadDocument" Style="display: none;" />
                                    &nbsp;</td>
                            </tr>
              
                       </table>
           <br>
                     
                        <div class="title">
                          <h4 class="auto-style1" >&nbsp;&nbsp;&nbsp; Información del Timbre Electronico</h4>
                            </div>
              <div class="doc-content f12">            
                  <div>            
                      <div class="form-horizontal">
                             
                        <div class="form-group">
                          <label class="col-xs-4 control-label p0right align-left">RUT Empresa:</label>
                          <div class="col-xs-4">            
                              <asp:TextBox ID="txt_rut_emp" runat="server" CssClass="form-control input-sm" ReadOnly="true"></asp:TextBox>
                          </div>
                        </div>
                                 <div class="form-group">
                          <label class="col-xs-4 control-label p0right align-left">Nombre Empresa:</label>
                          <div class="col-xs-4">            
                              <asp:TextBox ID="txt_nom_emp" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                          </div>
                        </div>

                            <div class="form-group">
                          <label class="col-xs-4 control-label p0right align-left">Tipo Documento:</label>
                          <div class="col-xs-4">            
                              <asp:TextBox ID="txt_tipo_doc" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                          </div>
                        </div>

                               <div class="form-group">
                          <label class="col-xs-4 control-label p0right align-left">Folio Desde:</label>
                          <div class="col-xs-4">            
                              <asp:TextBox ID="txt_folio_desde" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                          </div>
                        </div>

                               <div class="form-group">
                          <label class="col-xs-4 control-label p0right align-left">Folio Hasta:</label>
                          <div class="col-xs-4">            
                              <asp:TextBox ID="txt_folio_hasta" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                          </div>
                        </div>

                               <div class="form-group">
                          <label class="col-xs-4 control-label p0right align-left">Fecha Autorizacion:</label>
                          <div class="col-xs-4">            
                              <asp:TextBox ID="txt_fecha_aut" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                          </div>
                        </div>

                          <div class="form-group">
                         
                               <div > 
                                   <br />
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 <asp:LinkButton ID="LinkButton5" runat="server" CssClass="btn btn-success" Text="Agregar Nuevo Timbre" OnClick="LinkButton5_Click"  >Agregar Nuevo Timbre</asp:LinkButton>
                                 </div> 

                          </div>  

                      </div>
                    </div>        
                </div>

         </div>

      
      

   
      
</asp:Content>
