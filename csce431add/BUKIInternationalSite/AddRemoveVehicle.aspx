<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="AddRemoveVehicle.aspx.cs" Inherits="AddRemoveVehicle" Title="BUKI Add/Remove Vehicle" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<script runat="server">
    void btnAddVehicle_Click(Object Source, EventArgs E)
    {
        this.AddNewVehicle(Source, E);
    }
    void btnRemoveVehicle_Click(Object Source, EventArgs E)
    {
        this.RemoveVehicle(Source, E);
    }
    void makeModelDL_Click(Object Source, EventArgs E)
    {
        this.MakeModelDL_SelectedIndexChanged(Source, E);
    }
</script>

<asp:Content ID="AdvancedSearch" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="Contents">
        <h1>
            <u>Vehicle Management: </u>
        </h1>
        <br />
        <div class="AddRemoveLeft">
            <h3>
                <i><u>Add Vehicle to Inventory:</u></i></h3>
            <br />
            <table border="0">
                <tr>
                    <td>
                        <b>Vehicle: </b>
                    </td>
                    <td>
                        <asp:DropDownList ID="MakeModelDL" ToolTip="Vehicle's Make/Model" Width="100%" runat="server"
                            OnSelectedIndexChanged="makeModelDL_Click" AutoPostBack="true" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Location: </b>
                    </td>
                    <td>
                        <asp:DropDownList ID="LocationDL" runat="server" Width="100%">
                            <asp:ListItem Selected="True" Value="" Text="" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>VIN: </b>
                    </td>
                    <td>
                        <asp:TextBox ID="VINBox" Width="100%" Rows="10" MaxLength="17" Enabled="true" ReadOnly="false"
                            ToolTip="Vehicle's VIN" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Price: </b>
                    </td>
                    <td>
                        <asp:TextBox ID="PriceBox" Width="50%" Rows="10" MaxLength="100" Enabled="true" ReadOnly="false"
                            ToolTip="Vehicle's Price" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Color: </b>
                    </td>
                    <td>
                        <asp:DropDownList ID="ColorDL" runat="server" Width="50%">
                            <asp:ListItem Selected="True" Value="" Text="" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <%--                <tr>
                <td><b>Year: </b></td>
                 <td><asp:TextBox
                                id = "YearBox"
                                columns = "4"
                                maxlength = "4"
                                enabled = "true"
                                readonly = "false"
                                toolTip = "Vehicle's Year"
                                runat = "server"
                      /> </td>
                </tr>--%>
                <tr>
                    <td>
                        <b>Mileage: </b>
                    </td>
                    <td>
                        <asp:TextBox ID="MileageBox" Width="50%" Rows="10" MaxLength="15" Enabled="true"
                            ReadOnly="false" ToolTip="Vehicle's Mileage" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Condition: </b>
                    </td>
                    <td>
                        <asp:DropDownList ID="ConditionDL" runat="server" Width="50%">
                            <asp:ListItem Selected="True" Value="" Text="" />
                            <asp:ListItem Value="TestData2">Great</asp:ListItem>
                            <asp:ListItem Value="TestData3">Poor</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div class="AddRemoveRight">
            <br />
            <br />
            <br />
            <br />
            <table border="0">
                <tr>
                    <td>
                        <b>Type: </b>
                    </td>
                    <td>
                        <asp:TextBox ID="TypeBox" runat="server" Width="50%" Enabled="true" ReadOnly="true"
                            toolTipe="Vehicle's Type">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>MPG: </b>
                    </td>
                    <td>
                        <asp:TextBox ID="MPGBox" Rows="10" Columns="2" MaxLength="15" Enabled="true" ReadOnly="true"
                            ToolTip="Vehicle's MPG" runat="server" />
                    </td>
                </tr>
                <%--            <tr>
            <td><b>Fuel: </b></td>
             <td><asp:TextBox
                                id = "FuelBox"
                                width = "50%"
                                rows = "10"
                                maxlength = "15"
                                enabled = "true"
                                readonly = "false"
                                toolTip = "Vehicle's Fuel Type"
                                runat = "server"
                      /></td>
            </tr>--%>
                <tr>
                    <td>
                        <b>Doors: </b>
                    </td>
                    <td>
                        <asp:TextBox ID="DoorsBox" Columns="2" MaxLength="2" Enabled="true" ReadOnly="true"
                            ToolTip="Number of Doors" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Image: </b>
                    </td>
                    <td>
                        <input id="ImageBox" width="50%" type="file" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style='vertical-align: top'>
                        <b>Description: </b>
                    </td>
                    <td>
                        <asp:TextBox ID="DescriptionBox" TextMode="MultiLine" Width="50%" Rows="5" Columns="50"
                            MaxLength="15" Enabled="true" ReadOnly="false" ToolTip="Vehicle Description"
                            runat="server" />
                    </td>
                </tr>
            </table>
            <br />
            <asp:Button ID="AddVehicle" Text="Add Vehicle" OnClick="btnAddVehicle_Click" Enabled="true"
                Visible="true" ToolTip="Adds the Vehicle to the Database!" runat="server" AutoPostBack="true" />
        </div>
        <br />
        <div class="DBOperationStatus">
            <asp:Label ID="AddVehicleStatus" Width="100%" runat="server" Text="" AutoPostBack="true" />
        </div>
        <br />
        <br />
        <div class="AddRemoveLeft">
            <h3>
                <i><u>Remove Vehicle from Inventory:</u></i></h3>
            <br />
            <table border="0">
                <tr>
                    <td>
                        <asp:DropDownList ID="RemoveDL" runat="server">
                            <asp:ListItem Selected="True" Value="" Text="" />
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="RemoveVehicleButton" Text="Remove Vehicle" Enabled="true" Visible="true"
                            ToolTip="Removes a Vehicle from the Database!" runat="server" OnClick="btnRemoveVehicle_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div class="DBOperationStatus">
            <asp:Label ID="RemoveVehicleStatus" Width="100%" runat="server" Text="" AutoPostBack="true" />
        </div>
    </div>
    <div id="AddVehicleModelDiv" runat="server" visible="false">
        <h3>
            <i><u>Add Vehicle Model to Inventory:</u></i></h3>
        <br />
        <table border="0">
            <tr>
                <td>
                    <b>Vehicle Model: </b>
                </td>
                <td>
                    <asp:TextBox ID="VehicleModelModel" ToolTip="Vehicle's Make/Model" Width="100%" runat="server"
                        OnSelectedIndexChanged="makeModelDL_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <b>Vehicle Make: </b>
                </td>
                <td>
                    <asp:TextBox ID="VehicleMakeModel" ToolTip="Vehicle's Make/Model" Width="100%" runat="server"
                        OnSelectedIndexChanged="makeModelDL_Click" />
                </td>
            </tr>
            </tr>
            <tr>
                <td>
                    <b>Year: </b>
                </td>
                <td>
                    <asp:TextBox ID="YearBoxModel" Columns="4" MaxLength="4" Enabled="true" ReadOnly="false"
                        ToolTip="Vehicle's Year" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <b>Mileage: </b>
                </td>
                <td>
                    <asp:TextBox ID="MilageInputModel" Width="50%" Rows="10" MaxLength="15" Enabled="true"
                        ReadOnly="false" ToolTip="Vehicle's Mileage" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <b>Type: </b>
                </td>
                <td>
                    <asp:DropDownList ID="TypeInputModel" runat="server" Width="50%" Enabled="true">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <b>MPG: </b>
                </td>
                <td>
                    <asp:TextBox ID="MPGBoxModel" Rows="10" Columns="2" MaxLength="15" Enabled="true"
                        ReadOnly="false" ToolTip="Vehicle's MPG" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <b>Doors: </b>
                </td>
                <td>
                    <asp:TextBox ID="DoorModel" Columns="2" MaxLength="2" Enabled="true" ReadOnly="false"
                        ToolTip="Number of Doors" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <b>Market Price: </b>
                </td>
                <td>
                    <asp:TextBox ID="PriceModel" Enabled="true" ReadOnly="false" ToolTip="Vehicles Expected Price"
                        runat="server" />
                </td>
            </tr>
        </table>
        <asp:Button ID="PostModelButton" Text="Add Model" Enabled="true" Visible="true" ToolTip="Adds the Vehicle to the Database!"
            runat="server" AutoPostBack="true" OnClick="PostModelButton_Click" />
    </div>
</asp:Content>
