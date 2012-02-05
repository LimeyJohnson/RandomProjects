<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CarInfo.aspx.cs" Inherits="CarInfo" Title="BUKI: Car Information Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="Contents">
        <div class="FormTitle">
            Car Info For:
            <asp:Label ID="m_InfoTitle" runat="server" />
        </div>
        <div>
            <table border="0">
                <tr>
                    <td style="width: 10%">
                    </td>
                    <td style="">
                        <br />
                        <br />
                                    <asp:TextBox Wrap = "true" Rows = "10" ReadOnly  = "true" TextMode = "MultiLine" 
                                    BorderStyle = "None" BorderWidth = "0" runat ="server" 
                            Width="334px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Image ID="CarImage" runat="server" ImageUrl="1Lamborghini_Diablo_VT_test.jpg"
                            Height="250px" Width="325px" Style="margin-right: 10px" border="1" hspace="20" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="font-size: 24px;">
                        <u>Vehicle Details:</u> <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="style2">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="m_InfoCarYear" runat="server" Text="Year: "  CssClass="CarInfoItem" Font-Underline="True"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="m_InfoCarYearVal" runat="server" Text="2010 Test" CssClass="CarInfoItemValue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="m_InfoCarMileage" runat="server" Text="Mileage: " Font-Underline="True" CssClass="CarInfoItem"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="m_InfoCarMileageVal" runat="server" Text="250,000 Test" CssClass="CarInfoItemValue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="m_InfoCarCondition" runat="server" Text="Condition: " Font-Underline="True" CssClass="CarInfoItem"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="m_InfoCarConditionVal" runat="server" Text="Run-Down Test" CssClass="CarInfoItemValue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="m_InfoCarMilesPerGal" runat="server" Text="MPG: " Font-Underline="True" CssClass="CarInfoItem"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="m_InfoCarMilesPerGalVal" runat="server" Text="20 MPG / 26 MPG Test" CssClass="CarInfoItemValue"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="style2">
                        <table border="0">
                            <tr>
                                <td>
                                    <asp:Label ID="m_InfoCarStyle" runat="server" Text="Car Style: " Font-Underline="True" CssClass="CarInfoItem"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="m_InfoCarStyleVal" runat="server" Text="Coupe" CssClass="CarInfoItemValue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="m_InfoCarFuel" runat="server" Text="Fuel: " Font-Underline="True" CssClass="CarInfoItem"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="m_InfoCarFuelVal" runat="server" Text="Gasoline Test" CssClass="CarInfoItemValue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="m_InfoCarType" runat="server" Text="Car Type: " Font-Underline="True" CssClass="CarInfoItem"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="m_InfoCarTypeVal" runat="server" Text="Sports Car Test" CssClass="CarInfoItemValue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="m_InfoCarVIN" runat="server" Text="VIN: " Font-Underline="True" CssClass="CarInfoItem"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="m_InfoCarVINVal" runat="server" Text="1N654IIDJJGHIS" CssClass="CarInfoItemValue"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    <style type="text/css">
        .style2
        {
            height: 99px;
        }
    </style>

</asp:Content>
