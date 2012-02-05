<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddRemoveDealership.aspx.cs" 
Inherits="AddRemoveDealership" Title="Add/Remove Dealership" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>

<script runat= "server">
    void btnAddDealership_Click(Object Source, EventArgs E)
    {
        this.AddNewDealership(Source, E);
    }

    void btnRemoveDealership_Click(Object Source, EventArgs E)
    {
        this.RemoveDealership(Source, E);
    }
</script>

<asp:Content ID="AddRemoveDealership" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="Contents">
        <h1><u>Dealership Management: </u></h1>
        <br />
        <div class="AddRemoveLeft">
            <h3><i><u>Add Dealership:</u></i></h3>
            <br />
            <table border="0">
                <tr>
                <td><b>Name: </b></td>
                     <td> <asp:TextBox
                               id = "NameBox"
                               toolTip = "Dealership Name"
                               width = "100%"
                               runat = "server"
                     /> </td>
                </tr>
                <tr>
                <td><b>City: </b></td>
                    <td> <asp:TextBox
                              id = "CityBox"
                              toolTip = "City"
                              width = "100%"
                              runat = "server"
                         />                        
                    </td>
                </tr>
                <tr>
                <td><b>State: </b></td>
                    <td> <asp:TextBox
                              id = "StateBox"
                              toolTip = "State"
                              width = "50%"
                              runat = "server"
                              maxlength = "2"
                         />
                    </td>
                </tr>
                <tr>
                <td><b>ZIP: </b></td>
                    <td> <asp:TextBox
                              id = "ZipBox"
                              toolTip = "ZIP"
                              width = "50%"
                              runat = "server"
                              maxlength = "5"
                         />
                    </td>
                </tr>
            </table>
            <br />
            <asp:Button
                 id = "AddDealership"
                 text = "Add Dealership"
                 onClick = "btnAddDealership_Click"
                 enabled = "true"
                 visible = "true"
                 toolTip = "Adds the dealership to the Database!"
                 runat = "server"
                 AutoPostBack = "true"
            />
            <br />
            <br />
            </div>
            <div class="DBOperationStatus">
                <asp:Label
                     id = "AddDealershipStatus"
                     width = "100%"
                     runat = "server"
                     text = ""
                     AutoPostBack = "true"
                />
            </div>
            <br />
            <br />
            <div class ="AddRemoveLeft">
            <h3><i><u>Remove Dealership:</u></i></h3>
            <br />
            <table border="0">
            <tr>
               <td><asp:DropDownList 
                                 id="RemoveDL"
                                 runat = "server"   
                     >
                         <asp:ListItem Selected="True" Value="" Text=""/>
                    </asp:DropDownList> </td>
                    
                <td><asp:Button
                                id = "RemoveDealershipButton"
                                text = "Remove Dealership"
                                enabled = "true"
                                visible = "true"
                                toolTip = "Removes a Dealership from the Database!"
                                runat = "server"
                                onclick = "btnRemoveDealership_Click"
                    /></td>
                     </tr>
               </table>
           </div>
           <br />
           <br />
           <div class="DBOperationStatus">
                <asp:Label
                     id = "RemoveDealershipStatus"
                     width = "100%"
                     runat = "server"
                     text = ""
                     AutoPostBack = "true"
                />
           </div>
        </div>        
    </div>
</asp:Content>