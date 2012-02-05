<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ApprovePendingSale.aspx.cs" Inherits="ApprovePendingSale" Title="Untitled Page" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="Contents">
        <div class="FormTitle">
            The following sale is pending from salesman:
            <asp:Label ID="m_SalesPerson" runat="server" />
        </div>
        <div>
            <table border="0">
                <tr>
                    <td>
                        <asp:Image ID="CarImage" runat="server" ImageUrl="1Lamborghini_Diablo_VT_test.jpg"
                            Height="250px" Width="325px" Style="margin-right: 10px" border="1" hspace="20" />
                    </td>
                    <td>
                        <table border="0">
                            <tr>
                                <td>
                                    <asp:Label ID="m_ApprPendingCarMake" runat="server" Text="Make: " CssClass="CarInfoItem"
                                        Font-Underline="True"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="m_ApprPendingCarMakeVal" runat="server" Text="Lamborghini Test" CssClass="CarInfoItemValue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="m_ApprPendingCarModel" runat="server" Text="Model: " Font-Underline="True"
                                        CssClass="CarInfoItem"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="m_ApprPendingCarModelVal" runat="server" Text="Diablo VT Test" CssClass="CarInfoItemValue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="m_ApprPendingCarColor" runat="server" Text="Color: " Font-Underline="True"
                                        CssClass="CarInfoItem"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="m_ApprPendingCarColorVal" runat="server" Text="Silver Test" CssClass="CarInfoItemValue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="m_ApprPendingCarMSRP" runat="server" Text="MSRP: " Font-Underline="True"
                                        CssClass="CarInfoItem"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="m_ApprPendingCarMSRPVal" runat="server" Text="$250,000 Test" CssClass="CarInfoItemValue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="m_ApprPendingCarVIN" runat="server" Text="VIN: " Font-Underline="True"
                                        CssClass="CarInfoItem"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="m_ApprPendingCarVINVal" runat="server" Text="1N654IIDJJGHIS" CssClass="CarInfoItemValue"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 25px;">
                    </td>
                    <td style="margin-left: 25px;">
                        <asp:Button ID="m_btnApprove" runat="server" Text="Approve" CssClass="BUKIButton"
                            Width="100px" onClick = "approveBtn_Click" />
                        <br />
                        <asp:Button ID="m_btnDeny" runat="server" Text="Deny" CssClass="BUKIButton" Width="100px" 
                        onClick = "denyBtn_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div style="font-size: 24px; margin-top: 20px; margin-bottom: 20px;">
                <u>Customer Information:</u>
            </div>
            <table border="0">
                <tr>
                    <td>
                        <table border="0">
                            <tr>
                                <td>
                                    <asp:Label ID="m_ApprPendingCustName" runat="server" Text="Customer Name: " CssClass="CustInfoItem"
                                        Font-Underline="True"></asp:Label>
                                </td>
                                <td style="padding-left: 5px;">
                                    <asp:Label ID="m_ApprPendingCustNameVal" runat="server" Text="John Doe" CssClass="CustInfoItemValue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="m_ApprPendingCustAge" runat="server" Text="Customer Age: " Font-Underline="True"
                                        CssClass="CustInfoItem"></asp:Label>
                                </td>
                                <td style="padding-left: 5px;">
                                    <asp:Label ID="m_ApprPendingCustAgeVal" runat="server" Text="156 Test" CssClass="CustInfoItemValue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="m_ApprPendingCustAddress" runat="server" Text="Customer Address: "
                                        Font-Underline="True" CssClass="CustInfoItem"></asp:Label>
                                </td>
                                <td style="padding-left: 5px;">
                                    <asp:Label ID="m_ApprPendingCustAddressVal" runat="server" Text="353 Nowhere St."
                                        CssClass="CustInfoItemValue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="m_ApprPendingCustCity" runat="server" Text="Customer City: " Font-Underline="True"
                                        CssClass="CustInfoItem"></asp:Label>
                                </td>
                                <td style="padding-left: 5px;">
                                    <asp:Label ID="m_ApprPendingCustCityVal" runat="server" Text="Madeupsville" CssClass="CustInfoItemValue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="m_ApprPendingCustState" runat="server" Text="Customer State: " Font-Underline="True"
                                        CssClass="CustInfoItem"></asp:Label>
                                </td>
                                <td style="padding-left: 5px;">
                                    <asp:Label ID="m_ApprPendingCustStateVal" runat="server" Text="NW" CssClass="CustInfoItemValue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="m_ApprPendingCustZip" runat="server" Text="Customer Zip: " Font-Underline="True"
                                        CssClass="CustInfoItem"></asp:Label>
                                </td>
                                <td style="padding-left: 5px;">
                                    <asp:Label ID="m_ApprPendingCustZipVal" runat="server" Text="55555" CssClass="CustInfoItemValue"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="padding-left:20px">
                    Additional Information: <br />
                    <asp:TextBox runat="server" ID="m_txtApprPendingAddtlInfo" TextMode="MultiLine" Rows="7" Width="350px"></asp:TextBox>
                    </td>                    
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
