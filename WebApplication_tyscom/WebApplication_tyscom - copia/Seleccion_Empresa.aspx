<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Seleccion_Empresa.aspx.cs" Inherits="WebApplication_tyscom.Seleccion_Empresa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Seleccion Empresa</h1>
    <p>&nbsp;</p>
         <div class="table-options">
                <h4 class="m0bottom m0top">Empresa : 
                    <asp:DropDownList ID="List_Emp" runat="server" onchange="get_value()" Width="300" data-style="btn-primary" >
                    <asp:ListItem Text="?" Value="33" />          
                    </asp:DropDownList>
                  </h4>
                <p class="m0bottom m0top">&nbsp;</p>
                <p class="m0bottom m0top">&nbsp;</p>
             <script type="text/javascript">
                 
                 function get_value() {
                     var id_emp = document.getElementById('<%=List_Emp.ClientID%>');
                     var algo = id_emp.options[id_emp.selectedIndex].value;
                     alert(algo);
                     document.getElementById('<%=Text1.ClientID%>').value = algo;

                    
                     var selectedText = id_emp.options[id_emp.selectedIndex].text;
                     document.getElementById('<%=Text2.ClientID%>').value = selectedText;
                 }
             </script>
              <input id="Text1" type="text" runat="server"/> 
              <input id="Text2" type="text" runat="server"/> 
             <asp:LinkButton ID="link_emp" runat="server" CssClass="btn btn-success" Text="Siguiente" OnClick="link_emp_Click" ></asp:LinkButton>
            
               </div>
</asp:Content>
