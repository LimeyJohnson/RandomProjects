<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MessageBoardOLD.aspx.cs" 
Inherits="MessageBoard" Title="BUKI Message Board System" %>

<asp:Content ID="MessageBoard" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div class ="Contents">
        <div class="MessageBoardLeft">     
        <asp:Image ID="CrashDummyImage" runat="server" ImageUrl="CrashTestDummy.jpg"
                            Height="250px" Width="330px"
         />    
         <br />
         <br />
            <div class="MessageBoardTitle">
                <center>Welcome to John Doe's Message Board!</center>    
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
                <br />       
            </div>
          </div>
          <div class="MessageBoardRight">
            <center>
            <b>Posted by Angry Admin: </b>
            <br />
            <br />
            <asp:TextBox
                            id = "TextBox2"
                            width = "50%"
                            TextMode = "MultiLine"
                            rows = "10"
                            columns = "100"
                            maxlength = "1000"
                            text = "Message Gets Posted Here!"
                            enabled = "true"
                            readonly = "false"
                            toolTip = "Message by: (input name)"
                            runat = "server"
                  />
             <br />
             <asp:Button
                            id = "ReplyButton"
                            text = "Reply"
                            onClick = "btnReplyTo_Click"
                            enabled = "true"
                            visible = "true"
                            toolTip = "Reply to this message!"
                            runat = "server"
                />
                <asp:Button
                            id = "DeleteButton"
                            text = "Delete"
                            onClick = "btnDeleteMsg_Click"
                            enabled = "true"
                            visible = "true"
                            toolTip = "Click here to post the message!"
                            runat = "server"
                />
            
            <br />
            <br />
                
            <b>Posted by: Jane Doe</b>
            <br />
            <br />
            <asp:TextBox
                            id = "TextBox3"
                            width = "50%"
                            TextMode = "MultiLine"
                            rows = "10"
                            columns = "100"
                            maxlength = "1000"
                            text = "Need to auto-generate with loop when classes are made!"
                            enabled = "true"
                            readonly = "false"
                            toolTip = "Message by: (input name)"
                            runat = "server"
                  />
             <br />
             <asp:Button
                            id = "replyButton2"
                            text = "Reply"
                            onClick = "btnReplyTo_Click"
                            enabled = "true"
                            visible = "true"
                            toolTip = "Reply to this message!"
                            runat = "server"
                />
                <asp:Button
                            id = "deleteButton2"
                            text = "Delete"
                            onClick = "btnDeleteMsg_Click"
                            enabled = "true"
                            visible = "true"
                            toolTip = "Click here to post the message!"
                            runat = "server"
                />
                
              </center>
          </div>
    </div>
</asp:Content>