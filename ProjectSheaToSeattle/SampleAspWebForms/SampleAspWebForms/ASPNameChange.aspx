<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ASPNameChange.aspx.cs" Inherits="SampleAspWebForms.ASPNameChange" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Please Enter your name:
           <br />
            <asp:TextBox ID="input" runat="server" Width="265px"></asp:TextBox>
            <br />
            <asp:Button ID="submit" runat="server" Text="Button" OnClick="submit_Click" />
            <br />
            <asp:TextBox ID="result" runat="server" Width="359px"></asp:TextBox>
        </div>
    </form>
</body>
</html>
