<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BRPL_FinalBill.aspx.cs" Inherits="BRPL_FinalBill" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Content_Display" runat="server">
            <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
            <br />
            <asp:Button ID="btnPDFShow" runat="server" Visible="false" OnClick="btnPDFShow_Click" Text="Show Final Bill" />
            <iframe runat="server"  id="PDfifram" style="width: 100%; height: 800px;"></iframe>
           <%-- <div runat="server" id="PDfifram" style="width: 100%; height: 800px; display: none"></div>--%>
        </div>
        <div id="Blank_Display" runat="server" style="text-align: center">
            <asp:Label ID="lblMessage" runat="server" Font-Bold="true"></asp:Label>
        </div>
    </form>
</body>
</html>
