<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Seleccion_Docs.aspx.cs" Inherits="WebApplication_tyscom.Seleccion_Docs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row-fluid">
				
				<div class="statbox purple span6" ontablet="span6" ondesktop="span3">
					<div class="boxchart"><canvas width="4" height="60" style="display: inline-block; width: 4px; height: 60px; vertical-align: top;"></canvas></div>
					<div class="number">Tipo 33<i class="icon-arrow-up"></i></div>
					<div class="title">Factura Afecta</div>
					<div class="footer">
                        <asp:LinkButton ID="link_33" runat="server" OnClick="link_33_Click">Emitir Documento</asp:LinkButton>
					
					</div>	
				</div>
				<div class="statbox green span6" ontablet="span6" ondesktop="span3">
					<div class="boxchart"><canvas width="4" height="60" style="display: inline-block; width: 4px; height: 60px; vertical-align: top;"></canvas></div>
					<div class="number">Tipo 34<i class="icon-arrow-up"></i></div>
					<div class="title">Factura Exenta de IVA</div>
					<div class="footer">
						 <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Emitir Documento</asp:LinkButton>
					</div>
				</div>
				<div class="statbox blue noMargin span6" ontablet="span6" ondesktop="span3">
					<div class="boxchart"><canvas width="4" height="60" style="display: inline-block; width: 4px; height: 60px; vertical-align: top;"></canvas></div>
					<div class="number">Tipo 56<i class="icon-arrow-up"></i></div>
					<div class="title">Nota de Debito Electronica</div>
					<div class="footer">
						 <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">Emitir Documento</asp:LinkButton>
					</div>
				</div>
				<div class="statbox yellow span6" ontablet="span6" ondesktop="span3">
					<div class="boxchart"><canvas width="4" height="60" style="display: inline-block; width: 4px; height: 60px; vertical-align: top;"></canvas></div>
					<div class="number">Tipo 61<i class="icon-arrow-down"></i></div>
					<div class="title">Nota de Credito Electronica</div>
					<div class="footer">
						 <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">Emitir Documento</asp:LinkButton>
					</div>
				</div>	
				
			</div>


</asp:Content>
