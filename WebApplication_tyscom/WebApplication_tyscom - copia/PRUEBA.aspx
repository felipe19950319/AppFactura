<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PRUEBA.aspx.cs" Inherits="WebApplication_tyscom.PRUEBA" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   
     <script type="text/javascript" src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.13/js/jquery.dataTables.min.js"></script>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <textarea id="TextArea1" cols="20" rows="2" runat="server" ></textarea>
         <textarea id="TextArea2" cols="20" rows="2" runat="server" ></textarea>


        <table id="test" border="1">
        </table>

  <script type="text/javascript">

      testdata = [{
          "id": "58",
          "country_code": "UK",
          "title": "Legal Director",
          "pubdate": "2012-03-08 00:00:00",
          "url": "http://..."
      }, {
          "id": "59",
          "country_code": "UK",
          "title": "Solutions Architect,",
          "pubdate": "2012-02-23 00:00:00",
          "url": "http://..."
      }];
      $(document).ready(function () {
          var table = $('#test').DataTable({
              "aaData": testdata,
              "aoColumns": [{
                  "mDataProp": "id"
              }, {
                  "mDataProp": "country_code"
              }, {
                  "mDataProp": "title"
              }, {
                  "mDataProp": "pubdate"
              }, {
                  "mDataProp": "url"
              }, {
                  "defaultContent": "<button>Click!</button>"
              }]
          });

          $('#test tbody').on('click', 'button', function () {
              var data = table.row($(this).parents('tr')).data();
         
              var algo = JSON.stringify(data);
            
              var data = JSON.parse(algo);

              var valor = "id";
             valor in data             
            alert(valor + ' --> ' + data[valor]);
              

              document.getElementById('TextArea2').value = algo;
          });
      });
  </script>

        </form>
</body>
</html>
