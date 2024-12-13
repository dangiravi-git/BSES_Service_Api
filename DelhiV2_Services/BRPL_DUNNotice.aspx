<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BRPL_DUNNotice.aspx.cs" Inherits="BRPL_DUNNotice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Content_Display" runat="server">
            <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="TextBox2" runat="server" Visible="false"></asp:TextBox>
            <br />
            <asp:Button ID="btnPDFShow" runat="server" OnClick="btnPDFShow_Click" Visible="false" Text="Show BRPL Notice" />
            <%--<iframe runat="server" visible="false" id="PDfifram" style="width: 100%; height: 800px;"></iframe>--%>
            <div runat="server" id="PDfifram" visible="false" style="width: 100%; height: 800px;"></div>
        </div>
        <div id="Blank_Display" runat="server" style="text-align: center">
            <asp:Label ID="lblMessage" runat="server" Font-Bold="true"></asp:Label>
        </div>
    </form>
</body>
</html>
