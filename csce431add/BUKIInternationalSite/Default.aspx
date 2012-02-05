<%@ Page Language="C#" MasterPageFile="MasterPage.master" Title="Content Page"  CodeFile="~/Default.aspx.cs" Inherits="Default"%>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
    <table>
        <tr>
            <td colspan="2">
                <div style="position: relative">
                    <asp:Image ID="PromotionImg" runat="server" ImageUrl="bukiLogo.jpg" 
                         Style="padding-top: 10px;" ToolTip="Test Tooltip" border="0" hspace="20" />
                </div>
            </td>
        </tr>
    </table>
    
    <h1>WELCOME TO BUKI INTERNATIONAL</h1>
    BUKI International is the world's leader in used car sales. Now, that great customer service you
    enjoy in your dealerships is now online! Thanks to our friends at Active Data Dynamics (ADD), our
    web-based system is designed to provide you with the best experience when buying your used
    vehicle!
    <br />
    <h2>THANKS FOR VISITING!</h2>
    </center>

</asp:Content>
