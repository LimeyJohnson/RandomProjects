<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="News.aspx.cs" Inherits="News" Title="BUKI News Page" %>

<asp:Content ID="NewsContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="Contents">
        <div class="FormTitle">
            The Latest at BUKI International!
        </div>
         <div>
            <div class="NewsItemHeader">
                Presentation Day - December 9, 2009
            </div>
            <div class="NewsItemBody">
                <table border="0">
                    <tr>
                        <td>
                            <asp:Image ID="Image2" runat="server" ImageUrl="NewspaperImage.gif" Height="75px"
                                Width="100px" Style="margin-right: 10px" border="1" hspace="" />
                        </td>
                        <td>
                            The website is completed and we are presenting at 2:00PM today!
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div>
            <div class="NewsItemHeader">
                News Page Created! - November 20, 2009
            </div>
            <div class="NewsItemBody">
                <table border="0">
                    <tr>
                        <td>
                            <asp:Image ID="NewsHeader1Img" runat="server" ImageUrl="NewspaperImage.gif" Height="75px"
                                Width="100px" Style="margin-right: 10px" border="1" hspace="" />
                        </td>
                        <td>
                            The news page was created, and we are currently working hard on the rest of the site!
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div>
            <div class="NewsItemHeader">
                Initial Website Uploaded! - November 16, 2009
            </div>
            <div class="NewsItemBody">
                <table border="0">
                    <tr>
                        <td>
                            Today the Initial website was uploaded to source control, including the default
                            page and master page with links formatted.
                        </td>
                        <td>
                            <asp:Image ID="NewsHeader0Img" runat="server" ImageUrl="under_construction.jpg" Height="75px"
                                Width="100px" Style="margin-left: 10px" border="1" hspace="" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
