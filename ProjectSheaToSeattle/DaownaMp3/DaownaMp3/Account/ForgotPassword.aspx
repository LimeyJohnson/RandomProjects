<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="DaownaMp3.Account.ForgotPassword" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Forgotten Password?</h2>
    <table cellpadding="1" cellspacing="0" style="border-collapse:collapse; background-color: #6699FF;">
        <tr>
            <td class="auto-style3">
                <table cellpadding="0">
                    <tr>
                        <td align="center" colspan="2" class="auto-style6"><h4>Enter your account name or e-mail address to recover password.</h4>
                            </td>
                    </tr>
                    <tr>
                        <td align="right" class="auto-style7">
                            <asp:Label ID="NameEmailLabel" runat="server" AssociatedControlID="txtNameEmail">User Name:</asp:Label>
                        </td>
                        <td class="auto-style5">
                            <asp:TextBox ID="txtNameEmail" runat="server"></asp:TextBox>                            
                        </td>
                    </tr>                   
                    <tr>
                        <td align="right" colspan="2">
                            <br />
                            <asp:Button ID="EmailPasswordButton" runat="server" Text="E-mail my password" Width="152px" OnClick="EmailPassword_Click" />
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
            width: 534px;
        }
        .auto-style5 {
            width: 319px;
        }
        .auto-style6 {
            height: 62px;
        }
        .auto-style7 {
            width: 271px;
        }
        </style>
</asp:Content>
