<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CreatePending.aspx.cs" Inherits="CreatePending" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="Contents">
        <div class="FormTitle">
            Create a sale:
            <asp:Label ID="m_InfoTitle" runat="server" />
        </div>
        <div>
            <table border="0">
                <tr>
                    <td>
                        <asp:Image ID="CarImage" runat="server" ImageUrl="PlaceHolderLogo.jpg" Height="250px"
                            Width="325px" Style="margin-right: 10px" border="1" hspace="20" />
                    </td>
                    <td>
                        <table border="0">
                            <tr>
                                <td>
                                    <asp:Label ID="m_CreatePendingCarVIN" runat="server" Text="VIN: " Font-Underline="True"
                                        CssClass="CarInfoItem"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="m_txtCrtPendingCarVINVal" runat="server" Text="" CssClass="CarInfoTextInsert"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="m_CreatePendingCarMake" runat="server" Text="Make: " CssClass="CarInfoItem"
                                        Font-Underline="True"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="m_txtCrtPendingCarMakeVal" runat="server" CssClass="CarInfoTextReadOnly" ReadOnly="true">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="m_CreatePendingCarModel" runat="server" Text="Model: " Font-Underline="True"
                                        CssClass="CarInfoItem"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="m_txtCrtPendingCarModelVal" runat="server" CssClass="CarInfoTextReadOnly" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="m_CreatePendingCarColor" runat="server" Text="Color: " Font-Underline="True"
                                        CssClass="CarInfoItem"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="m_cmbCrtPendingCarColorVal" runat="server" CssClass="CarInfoItemValue">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="m_CreatePendingCarMSRP" runat="server" Text="MSRP: " Font-Underline="True"
                                        CssClass="CarInfoItem"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="m_txtCrtPendingCarMSRPVal" runat="server" CssClass="CarInfoTextInsert"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 25px;">
                    </td>
                    <td style="margin-left: 25px;">
                        <asp:Button ID="m_btnCreate" runat="server" Text="Create" CssClass="BUKIButton" Width="100px" OnClick="m_btnCreateClicked" OnClientClick="ValidateForm" />
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
                                    <asp:Label ID="m_CreatePendingCustName" runat="server" Text="Customer Name: " CssClass="CustInfoItem"
                                        Font-Underline="True"></asp:Label>
                                </td>
                                <td style="padding-left: 5px;">
                                    <asp:TextBox ID="m_txtCrtPendingCustNameVal" runat="server" Text="" CssClass="CarInfoTextInsert"></asp:TextBox>
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
                        </table>
                    </td>
                    <td style="padding-left: 20px">
                        Additional Information:
                        <br />
                        <asp:TextBox runat="server" ID="m_txtApprPendingAddtlInfo" TextMode="MultiLine" Rows="7"
                            Width="350px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
