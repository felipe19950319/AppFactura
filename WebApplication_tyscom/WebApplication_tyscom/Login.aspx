<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication_tyscom.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    	<link  href="Bootstrap/css/bootstrap.min.css" rel="stylesheet"/>
	<link href="Bootstrap/css/bootstrap-responsive.min.css" rel="stylesheet"/>
	<link  href="Bootstrap/css/style.css" rel="stylesheet"/>
	<link  href="Bootstrap/css/style-responsive.css" rel="stylesheet"/>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"/>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
    <table width="650">
        <tr>
            <td>
    <div class="row-fluid" >
		
			<div class="lr-page span6 offset3">

				<div id="login-box">
					<div class="row-fluid">
						<div id="login-form" class="span12">
							<div class="page-title-small">
								<h2>Ingresar al Sistema de Facturacion</h2>
						</div>
							<div class="page-title-small">
								<h3>Login</h3>
							</div>
								<div class="row-fluid">

                   <asp:TextBox ID="txt_rut" CssClass="span12" runat="server"></asp:TextBox>
								
<asp:TextBox ID="txt_pass" CssClass="span12" runat="server" TextMode="Password"></asp:TextBox>
								</div>

								<div class="row-fluid">

									<div class="remember">
										<input id="remember" name="remember" type="checkbox" value="1"> Recordarme!
									</div>

									<div class="forgot">
										<a href="#">Contraseña Perdida?</a>
									</div>
								</div>	
								<div class="actions">
<asp:Button ID="Button1"  class="btn btn-primary span12" runat="server" Text="Ingresar" OnClick="Button1_Click"></asp:Button>
									

								</div>

							
						</div>

					</div>
					<!-- end: Row -->	

				</div>
				<!-- end: Login Box  -->
				
			</div>	
			
      	</div>
                </td>
            </tr>
        </table>
            </center>
    </div>
    </form>
</body>
</html>
