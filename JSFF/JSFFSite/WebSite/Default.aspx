<%@ Page Title="Home Page" Language="C#"  AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" ValidateRequest="false" %>
<html>
<body>
<form runat="server" style="height: 361px"> 
<asp:TextBox runat="server" ID="inputbox" ontextchanged="inputbox_TextChanged"></asp:TextBox>
<div id="tablediv" runat="server"></div>
</form>

</body>
</html>