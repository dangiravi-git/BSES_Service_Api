<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BRPL_ELNotice.aspx.cs" Inherits="BRPL_ELNotice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
              <asp:TextBox ID="TextBox2" runat="server" Visible="false"></asp:TextBox>
            <br />
            <asp:Button ID="btnPDFShow" runat="server" Visible="false" OnClick="btnPDFShow_Click" Text="Show BRPL Notice" />
            <asp:Label ID="lblMessage" runat="server" Font-Bold="true"></asp:Label>
            <%--<iframe runat="server" visible="false"  id="PDfifram" style="width:100%; height:800px;">

            </iframe>--%>
            <div runat="server" id="PDfifram" visible="false" style="width:100%; height:800px;"></div>
        </div>
    </form>
</body>
</html>
