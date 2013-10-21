<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="DaownaMp3.Account.Register" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Register.</h2>
    <table cellpadding="1" cellspacing="0" style="border-collapse:collapse; background-color: #6699FF;">
        <tr>
            <td class="auto-style3">
                <table cellpadding="0">
                    <tr>
                        <td align="center" colspan="2" class="auto-style6"><h4>Complete the fields below.</h4>
                            <p>&nbsp;</p>

                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="auto-style7">
                            <asp:Label ID="lblAccountName" runat="server" AssociatedControlID="AccountName">Account Name:</asp:Label>
                        </td>
                        <td class="auto-style5">
                            <asp:TextBox ID="AccountName" runat="server"></asp:TextBox>                            
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="auto-style7">
                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                        </td>
                        <td class="auto-style5">
                            <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                     </tr>
                    <tr>
                        <td align="right" class="auto-style7">
                            <asp:Label ID="lblEmail" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                        </td>
                        <td class="auto-style5">
                            <asp:TextBox ID="Email" runat="server" ></asp:TextBox>
                        </td>
                     </tr>
                     <tr>                         
                         <td align="right" colspan ="2">
                            <br />
                            <asp:Button ID="RegisterButton" runat="server" Text="Register" Width="82px" OnClick="RegisterButton_Click" />
                         </td>                 
                     </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .auto-style3 {
            width: 501px;
        }
        .auto-style5 {
            width: 322px;
        }
        .auto-style6 {
            height: 62px;
        }
        .auto-style7 {
            width: 199px;
        }
        </style>
</asp:Content>