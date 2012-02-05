<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MessageBoard.aspx.cs" 
Inherits="MessageBoard" Title="BUKI Message Board System" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<script runat= "server">
    void btnPostNew_Click(Object Source, EventArgs E)
    {
        if (m_currEmployee != null)
        {
            addNewMessage(m_currEmployee.ID, mbLocationID, txtMessageContent.Text);
        }
    }
</script> 


<asp:Content ID="MessageBoard" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div class ="Contents">
        <div class="MessageBoardLeft">     
        <asp:Image ID="CrashDummyImage" runat="server" ImageUrl="CrashTestDummy.jpg"
                            Height="250px" Width="330px"
         />    
         <br />
         <br />
            <div class="MessageBoardTitle">
                <center>Welcome to 
                <asp:Label 
                                id= "currentMsgBrdLocale"
                                enabled = true
                                runat = "server"
                />
                
                Message Board! </center>
            </div>
            <br />
            <div>
                <asp:TextBox
                                id = "txtMessageContent"
                                width = "100%"
                                TextMode = "MultiLine"
                                rows = "10"
                                columns = "100"
                                maxlength = "1000"
                                text = "New Message!"
                                enabled = "true"
                                readonly = "false"
                                toolTip = "New Message!"
                                runat = "server"
                />
                <br />
                <center>
                <asp:Button
                            id = "btnPostNew"
                            text = "Post!"
                            onClick = "btnPostNew_Click"
                            enabled = "true"
                            visible = "true"
                            toolTip = "Click here to post the message!"
                            runat = "server"
                            align = "center"
                />
                </center>
            </div>
          </div>
          <div class="MessageBoardRight">
                <asp:Table BorderWidth="0" ID="m_tblListofMsgs" runat="server" style="margin-left:20%">
                </asp:Table>
          </div>
    </div>
</asp:Content>