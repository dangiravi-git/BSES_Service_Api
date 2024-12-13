<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BillPDF_Explain.aspx.cs" Inherits="BillPDF_Explain" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btnPDFShow" runat="server" OnClick="btnPDFShow_Click" Text="Show Bill PDF Explain" />

            <%--<iframe runat="server" visible="false"  id="PDfifram" style="width:100%; height:500px;">

            </iframe>--%>

            <div runat="server" id="PDfifram" visible="false" style="width:100%; height:500px;"></div>

        </div>
    </form>
</body>
</html>
