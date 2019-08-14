<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Createuser_test.aspx.cs" Inherits="App_Web.Createuser_test" %>

<!doctype html>
<html lang="en">
<head runat="server">
  <title>Bestil IT-Bruger</title>
  <!-- Required meta tags -->
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <!-- Bootstrap CSS -->
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
  <link href="https://cdn.jsdelivr.net/gh/gitbrent/bootstrap4-toggle@3.4.0/css/bootstrap4-toggle.min.css" rel="stylesheet">
  <telerik:RadStyleSheetManager id="RadStyleSheetManager1" runat="server" />
  <style type="text/css">
    body {
      background: #e6ece5;
      padding: 15px;
    }
    .requiredfield{
      color: red;
      padding-left: 5px;
      font-weight: 800;
    }
  </style>
</head>
<body>
  <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
      <Scripts>
        <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
        <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
        <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
        <telerik:RadScriptReference Path="bootstrap4-toggle.min.js"></telerik:RadScriptReference>
      </Scripts>
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <div class="container">
      <h3>Formular til oprettelse af IT bruger til ny medarbejder.</h3>
      <h4>Oplysninger om medarbejderen</h4>
      <input type="checkbox" runat="server" id="cb_toggletest" data-toggle="toggle" data-onstyle="success" data-offstyle="secondary" />
      <div class='<%= state_cb %>' style="overflow-x: auto;" id="collapseExample">
        hej
      </div>
      <telerik:RadButton runat="server" AutoPostBack="true" ID="Button_submit" ButtonType="StandardButton" Text="Opret bruger" OnClick="Button_submit_Click" />
    </div>
  </form>
  <!-- Optional JavaScript -->
  <!-- jQuery first, then Popper.js, then Bootstrap JS -->
  <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" type="text/javascript"></script>
  <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" type="text/javascript"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" type="text/javascript"></script>
  <script src="https://cdn.jsdelivr.net/gh/gitbrent/bootstrap4-toggle@3.4.0/js/bootstrap4-toggle.min.js"></script>
  <script>
    $(function () {
      $('#cb_toggletest').bootstrapToggle({
        on: 'Ja',
        off: 'Nej'
      });
      $('#cb_toggletest').change(function () {
        //console.log($(this).prop('checked'))
        $('#collapseExample').collapse('toggle');
      });
    });
  </script>
</body>
</html>
