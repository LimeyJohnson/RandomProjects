<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SalesPromotions.aspx.cs" Inherits="SalesPromotions" 
Title="BUKI Sales Events" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="FormTitle">
        Sales and Promotions
    </div>
    <div>
        <div class="NewsItemHeader">
            Christmas Car Deals
        </div>
        <div class="NewsItemBody">
            <table border="0">
                <tr>
                    <td>
                        <asp:Image ID="SalesHeader1Img" runat="server" ImageUrl="NewspaperImage.gif" Height="75px"
                            Width="100px" Style="margin-right: 10px" border="1" hspace="" />
                    </td>
                    <td>
                        Come into one of our fine BUKI Dealerships today, and chat with one of our salespeople who will merrily direct your car buying appetite
                        to the car of your dreams.  Also bring your children in for free candy and christmas movies in the lobby.
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div>
        <div class="NewsItemHeader">
            BLACK FRIDAY SALE - THE DAY AFTER THANKSGIVING
        </div>
        <div class="NewsItemBody">
            <table border="0">
                <tr>
                    <td>
                        COME IN to your nearest BUKI Dealership and experience some of the great deals going
                        on for Black Friday! All approved customers will be able to select $500 cash back
                        or 2.9% APR on any used vehicle purchased during this promotion!
                    </td>
                    <td>
                        <asp:Image ID="SalesHeader0Img" runat="server" ImageUrl="beatercar.jpg" Height="75px"
                            Width="100px" Style="margin-left: 10px" border="1" hspace="" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
