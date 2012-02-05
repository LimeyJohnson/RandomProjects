<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="BuyNowOLD.aspx.cs" Inherits="BuyNow" Title="BUKI: Buy This Car Now" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="Contents">
        <div class="BuyNowTitle">
            <asp:Label ID="m_lblPageTitle" Text="Place the name of the vehicle here" runat="server" />
        </div>
        <br />
        <div class="FormTitle">
            <br />
            <table border="0">
                <tr>
                    <td>
                        <asp:Image ID="m_imgCarImage" runat="server" ImageUrl="PlaceHolderLogo.jpg" Height="250px"
                            Width="325px" Style="margin-right: 10px" border="1" hspace="20" />
                    </td>
                    <td>
                        <div>
                            <table border="2">
                                <tr>
                                    <td style="text-align: right">
                                        <u><b>VIN:</b></u>
                                    </td>
                                    <td>
                                        <asp:Label ID="m_lblVinValue" Text="The vehicles VIN Number" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <u><b>Make:</b></u>
                                    </td>
                                    <td>
                                        <asp:Label ID="m_lblMakeValue" Text="The vehicles Make" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <u><b>Model:</b></u>
                                    </td>
                                    <td>
                                        <asp:Label ID="m_lblModelValue" Text="The vehicles Model" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <u><b>Color:</b></u>
                                    </td>
                                    <td>
                                        <asp:Label ID="m_lblColorValue" Text="The vehicles Color" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <u><b>MSRP:</b></u>
                                    </td>
                                    <td>
                                        <asp:Label ID="m_lblMSRPValue" Text="The vehicles MSRP" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="div_customer" runat="server" visible="false">
            <div style="font-size: 24px; margin-top: 20px; margin-bottom: 20px;">
                <b><u>Customer Information:</u></b>
            </div>
            <table border="0">
                <tr>
                    <td>
                        <table border="0">
                            <tr>
                                <td>
                                    <asp:Label ID="m_CreatePendingCustFirstName" runat="server" Text="Customer First Name: "
                                        CssClass="CustInfoItem" Font-Underline="True"></asp:Label>
                                </td>
                                <td style="padding-left: 5px;">
                                    <asp:TextBox ID="m_txtCrtPendingCustFirstNameVal" runat="server" Text="" CssClass="CarInfoTextInsert"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="m_CreatePendingCustLastName" runat="server" Text="Customer Last Name: "
                                        CssClass="CustInfoItem" Font-Underline="True"></asp:Label>
                                </td>
                                <td style="padding-left: 5px;">
                                    <asp:TextBox ID="m_txtCrtPendingCustLastNameVal" runat="server" Text="" CssClass="CarInfoTextInsert"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="m_CreatePendingCustAge" runat="server" Text="Customer Age: " Font-Underline="True"
                                        CssClass="CustInfoItem"></asp:Label>
                                </td>
                                <td style="padding-left: 5px;">
                                    <asp:TextBox ID="m_txtCrtPendingCustAgeVal" runat="server" Text="" CssClass="CarInfoTextInsert"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="m_CreatePendingCustAddress" runat="server" Text="Customer Address: "
                                        Font-Underline="True" CssClass="CustInfoItem"></asp:Label>
                                </td>
                                <td style="padding-left: 5px;">
                                    <asp:TextBox ID="m_txtCrtPendingCustAddressVal" runat="server" Text="" CssClass="CarInfoTextInsert"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="m_CreatePendingCustCity" runat="server" Text="Customer City: " Font-Underline="True"
                                        CssClass="CustInfoItem"></asp:Label>
                                </td>
                                <td style="padding-left: 5px;">
                                    <asp:TextBox ID="m_txtCrtPendingCustCityVal" runat="server" Text="" CssClass="CarInfoTextInsert"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="m_CreatePendingCustState" runat="server" Text="Customer State: " Font-Underline="True"
                                        CssClass="CustInfoItem"></asp:Label>
                                </td>
                                <td style="padding-left: 5px;">
                                    <asp:TextBox ID="m_txtCrtPendingCustStateVal" runat="server" Text="" CssClass="CarInfoTextInsert"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="m_CreatePendingCustZip" runat="server" Text="Customer Zip: " Font-Underline="True"
                                        CssClass="CustInfoItem"></asp:Label>
                                </td>
                                <td style="padding-left: 5px;">
                                    <asp:TextBox ID="m_txtCrtPendingCustZipVal" runat="server" Text="" CssClass="CarInfoTextInsert"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="m_CustomerPhoneNumber" runat="server" Text="Customer Phone Number: "
                                        CssClass="CustInfoItem" Font-Underline="True"></asp:Label>
                                </td>
                                <td style="padding-left: 5px;">
                                    <asp:TextBox ID="m_txtCustomerPhoneNumber" runat="server" Text="" CssClass="CarInfoTextInsert"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="m_CustomerEmail" runat="server" Text="Customer Email: " CssClass="CustInfoItem"
                                        Font-Underline="True"></asp:Label>
                                </td>
                                <td style="padding-left: 5px;">
                                    <asp:TextBox ID="m_txtCustomerEmail" runat="server" Text="" CssClass="CarInfoTextInsert"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <asp:Image ID="ManPointing" runat="server" ImageUrl="woman-pointing.jpg" Height="300px" />
                    </td>
                    <td>
                        <asp:Button ID="BuyNowBtn" Text="BUY THIS CAR NOW!!!!!" OnClick="btnBuyNow_Click"
                            Enabled="true" Visible="true" ToolTip="Buy the Vehicle Selected!" runat="server"
                            Style="background-color: Navy; text-decoration: blink; font-weight: bolder; font-size: x-large;
                            color: Yellow;" Width="350px" Height="150px" />
                    </td>
                </tr>
            </table>
        </div>
      <div id="div_salesman" runat="server" visible="false">
            <div style="font-size: 24px; margin-top: 20px; margin-bottom: 20px;">
                <b><u>
                    <asp:Label ID="lbl_salesman_title" runat="server" Text=""></asp:Label></u></b>
            </div>
            <table border="0">
                <tr>
                    <td>
                        <table border="0" class="CarInfoItem" >
                            <tr>
                                <td class = "CarInfoItem">
                                    Customer First Name: 
                                    
                                </td>
                                <td >
                                    <asp:TextBox ID="cust_firstname" runat="server" Text="" CssClass="CarInfoTextInsert"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                   Customer Last Name:
                                        
                                </td>
                                <td >
                                    <asp:TextBox ID="cust_lastname" runat="server" Text="" CssClass="CarInfoTextInsert"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                   Customer Age: 
                                </td>
                                <td >
                                    <asp:TextBox ID="cust_age" runat="server" Text="" CssClass="CarInfoTextInsert"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Customer Address:
                                       </td>
                                <td >
                                    <asp:TextBox ID="cust_address" runat="server" Text="" CssClass="CarInfoTextInsert"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                   Customer City: 
                                </td>
                                <td >
                                    <asp:TextBox ID="cust_city" runat="server" Text="" CssClass="CarInfoTextInsert"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                   Customer State: 
                                                                   </td>
                                <td >
                                    <asp:TextBox ID="cust_state" runat="server" Text="" CssClass="CarInfoTextInsert"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Customer Zip:
                                </td>
                                <td >
                                    <asp:TextBox ID="TextBox7" runat="server" Text="" CssClass="CarInfoTextInsert"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Customer Phone Number:                                      
                                </td>
                                <td style="padding-left: 5px;">
                                    <asp:TextBox ID="TextBox8" runat="server" Text="" CssClass="CarInfoTextInsert"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label9" runat="server" Text="Customer Email: " CssClass="CustInfoItem"
                                        Font-Underline="True"></asp:Label>
                                </td>
                                <td style="padding-left: 5px;">
                                    <asp:TextBox ID="TextBox9" runat="server" Text="" CssClass="CarInfoTextInsert"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
