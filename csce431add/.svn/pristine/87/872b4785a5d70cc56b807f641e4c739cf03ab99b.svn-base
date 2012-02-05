<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CarIsDeniedPending.aspx.cs" Inherits="CarIsDeniedPending" Title="BUKI Pending Sale Denied" %>
    
<script runat="server">
    void btnRedirectToHome_Click(Object Source, EventArgs E)
    {
        String homeLink = "default.aspx";
        Response.Redirect(homeLink);
    }
</script>

<asp:Content ID="CarIsDeniedPending" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <center>
            <asp:Image ID="m_deniedSale" runat="server" ImageUrl="car_already_pending.jpg" width= 350px
                             height = 350px Style="margin-right: 10px" border="1" hspace="20" />
            <br />
            <br />
            <br />
            <asp:Button ID ="m_RedirectToPage" Text="Back to Home Page!" runat="server" 
                        onClick = btnRedirectToHome_Click />
            <br />
            <br />
            <asp:Label ID ="Reason" Text ="This car is currently pending for another customer!" Font-Bold = true
             runat="server" />
        </center>
    </div>
</asp:Content>

