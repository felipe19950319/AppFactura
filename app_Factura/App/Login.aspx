<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="app_Factura.App.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
<script src="../Common/jquery-3.3.1.js"></script>
<script src="../Common/bootstrap%204.1.0/js/bootstrap.min.js"></script>
<link href="../Common/bootstrap%204.1.0/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
  
        <br/>

        <div class="row">
       
            <div class="col-md-5">
                </div>
        <div class="col-md-2">
       
      <center><h1 class="h3 mb-3 font-weight-normal">Inicio de sesion</h1></center>
      <label for="rut" class="sr-only">Rut</label>
      <input type="text" id="rut" class="form-control" placeholder="Rut" required="" autofocus="" runat="server"/>
      <label for="inputPassword" class="sr-only">Password</label>
      <input type="password" id="inputPassword" class="form-control" placeholder="Password" required="" runat="server"/>

            <br/>
         
            <asp:Button  CssClass="btn btn-lg btn-primary btn-block" ID="btnLogin" runat="server" Text="Iniciar Sesion" OnClick="Login_Click"  />
     
      <p class="mt-5 mb-3 text-muted">© 2017-2018</p>

           </div>
             <div class="col-md-5">

                </div>
       
      </div>


    </form>
</body>
</html>
