<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Base.aspx.cs" Inherits="Aurum.Web.Base" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" src="Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.validate.min.js"></script>

    <script type="text/javascript" src="Scripts/Base.js?0.0.1"></script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Services>
                <asp:ServiceReference Path="~/Pagina.asmx" />
            </Services>
        </asp:ScriptManager>

        <div>

            Base

            <div id="conteudo" style="padding: 20px; border: solid 1px blue">
            </div>

        </div>
    </form>
</body>
</html>
