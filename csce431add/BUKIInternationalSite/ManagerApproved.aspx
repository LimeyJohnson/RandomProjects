<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ManagerApproved.aspx.cs" Inherits="ManagerApproved" Title="BUKI Manager Approves Sale" %>
    
<script runat="server">
    void btnRedirectToHome_Click(Object Source, EventArgs E)
    {
        String homeLink = "default.aspx";
        Response.Redirect(homeLink);
    }
</script>

<asp:Content ID="ManagerApproves" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <center>
            <asp:Image ID="m_managerApprove" runat="server" ImageUrl="approved.jpg" width=350px
                       height=350px Style="margin-right: 10px" border="1" hspace="20" />
            <br />
            <br />
            <br />
            <asp:Button ID ="m_RedirectToPage" Text="Back to Home Page!" runat="server" 
                        onClick = btnRedirectToHome_Click />
        </center>
    </div>
</asp:Content>

