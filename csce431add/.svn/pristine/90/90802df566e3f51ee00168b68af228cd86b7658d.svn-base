<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CarIsPending.aspx.cs" Inherits="CarIsPending" Title="BUKI Pending Sale Success" %>
    
<script runat="server">
    void btnRedirectToHome_Click(Object Source, EventArgs E)
    {
        String homeLink = "default.aspx";
        Response.Redirect(homeLink);
    }
</script>

<asp:Content ID="CarIsPending" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <center>
            <asp:Image ID="m_pendingSale" runat="server" ImageUrl="pendingSale.jpg" 
                             Style="margin-right: 10px" border="1" hspace="20" />
            <br />
            <br />
            <br />
            <asp:Button ID ="m_RedirectToPage" Text="Back to Home Page!" runat="server" 
                        onClick = btnRedirectToHome_Click />
        </center>
    </div>
</asp:Content>
