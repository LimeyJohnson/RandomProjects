<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateMessage.aspx.cs" Inherits="CreateMessage" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            font-size: small;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table style="text-align:center">
<tr><td style="text-align:right" colspan="2">
    <asp:Button ID="btn_send" runat="server" Text="Send" onclick="btn_send_Click" /></td></tr>
<tr>

<td ><b>TO</b><br />
    <span class="style1">(Multiple Allowed)</span>
<br/>
    <asp:ListBox ID="lbx_employee" runat="server" SelectionMode="Multiple" Height="450px"></asp:ListBox>
    </td>
    <td><b>Message</b><br />
        <asp:TextBox ID="txt_message" Height="450px" Width="450px" runat="server" TextMode="MultiLine"></asp:TextBox>
    </td>
</tr>

</table>
</asp:Content>

