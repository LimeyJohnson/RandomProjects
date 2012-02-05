<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddRemoveEmployee.aspx.cs" 
Inherits="AddRemoveEmployee" Title="BUKI Human Resources System" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="AddRemoveEmployee" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
    <center>
        <table border=0>
        <tr>
            <td>
            <asp:Image ID="Image2" runat="server" ImageUrl="blueEmployees.jpg"
                                    Height="200px" Width="250px" Style="margin-right: 10px" border="1" hspace="20" 
                                    /></td>
            <td><h1>BUKI Human Resources </h1></td>
            <td><asp:Image ID="Image1" runat="server" ImageUrl="blueEmployees.jpg"
                                    Height="200px" Width="250px" Style="margin-right: 10px" border="1" hspace="20" 
                                    /></td>
        </tr>
        </table>
    </center>
    </div>
    <div>
        <br />
        <center>
        <table border=0, cellpadding = 10, cellspacing = 10>
            <tr>
                <td><h3>Add an Employee: </h3></td>
                <td>
                        <asp:Button
                            id = "btnAddEmployee"
                            text = "Add Employee"
                            enabled = "true"
                            visible = "true"
                            toolTip = "Click here to add the employee!"
                            runat = "server"
                            align = "center"
                            OnClick = "btnAddEmployee_Click"
                         />
                  </td>
            </tr>
            <tr>
                   <td>First Name: <asp:TextBox
                        id = "addEmpFirstName"
                        visible = "true"
                        runat = server
                        />
                   </td>
    
                <td>Last Name: <asp:TextBox
                        id = "addEmpLastName"
                        visible = "true"
                        runat = server
                        />
                   </td>
          
                <td>City: <asp:TextBox
                        id = "addEmpCity"
                        visible = "true"
                        runat = server
                        />
                   </td>
            </tr>
            <tr>
                <td>State: <asp:TextBox
                        id = "addEmpState"
                        visible = "true"
                        runat = server
                        />
                   </td>
           
                <td>Zip Code: <asp:TextBox
                        id = "addEmpZip"
                        visible = "true"
                        runat = server
                        />
                   </td>
      
               <td>Employee Grade: <asp:DropDownList 
                                     id="addEmpGradeDL"
                                     runat = "server"   
                       >
                       </asp:DropDownList>
                </td>
               
            </tr>
            <tr>
                 <td>Phone: <asp:TextBox
                        id = "addEmpPhone"
                        visible = "true"
                        runat = server
                        />
                   </td>
            
                <td>Online Username: <asp:TextBox
                        id = "addEmpUsername"
                        visible = "true"
                        runat = server
                        />
                   </td>
            </tr>
            <tr>
              <td>Password: <asp:TextBox
                        id = "addEmpPassword"
                        visible = "true"
                        runat = "server"
                        TextMode="Password"                    
                        />
                   </td>
             <td>Email: <asp:TextBox
                        id = "addEmpEmail"
                        visible = "true"
                        runat = server
                        />
                   </td>
            </tr>
            <tr></tr>
            <tr>
                <td><h3>Remove an Employee: </h3></td>
                  <td>
                  <asp:Button
                            id = "btnRemoveEmployee"
                            text = "Remove Employee"
                            enabled = "true"
                            visible = "true"
                            toolTip = "Click here to remove the employee!"
                            onClick = "btnRemoveEmployee_Click"
                            runat = "server"
                            align = "center"
                         />
                  </td>
            </tr>
            <tr>
              <td>List of Employees: <asp:DropDownList 
                                     id="RemoveEmployeeDL"
                                     runat = "server"   
                       >
                       </asp:DropDownList>
                  </td>
            </tr>
        </table>
        </center>
    </div>
</asp:Content>

