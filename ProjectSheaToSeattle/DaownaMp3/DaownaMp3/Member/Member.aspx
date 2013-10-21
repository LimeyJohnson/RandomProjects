<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Member.aspx.cs" Inherits="DaownaMp3.Member.Member" 
    MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>


    </title>
    <style type="text/css">
        .auto-style1 {
            width: 308px;
        }
        .auto-style2 {
            width: 191px;
        }
        .auto-style3 {
            width: 309px;
        }
        .auto-style4 {
            width: 1500px;
        }
        .auto-style6 {
            width: 203px;
        }
        .auto-style8 {
            width: 276px;
        }
        .auto-style11 {
            width: 296px;
            height: 312px;
        }
        .auto-style12 {
            width: 200px;
            height: 312px;
        }
        .auto-style14 {
            width: 296px;
            height: 316px;
        }
        .auto-style15 {
            width: 200px;
            height: 316px;
        }
        .auto-style22 {
            width: 189px;
        }
        .auto-style23 {
            width: 189px;
            height: 312px;
        }
        .auto-style24 {
            width: 189px;
            height: 316px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <table id="Table1" runat="server" CellPadding="1" cellspacing="1" style="border-collapse:collapse;" >
            <tr>
                <td class="auto-style4"></td>
            </tr>
            <tr>
                <td align="right" class="auto-style4">
                    <asp:Button ID="LogOut" runat="server" align="right" Text="Log out" OnClick="LogOut_Click" Width="61px" />
                </td>
            </tr>
        </table>
        <asp:TextBox ID="txtGreet" runat="server" Width="600" BorderStyle="None" ></asp:TextBox>
        <br />
        <asp:TextBox ID="txtEmail" runat="server" Width="600" BorderStyle="None" ></asp:TextBox>
        <br />
        <asp:TextBox ID="txtDate" runat="server" Width="600" BorderStyle="None" ></asp:TextBox>
        <br /><br />
        <asp:TextBox ID="txtPlaylists" runat="server" Width="600" BorderStyle="None" ></asp:TextBox>
        <br /><br />
        <asp:TextBox ID="txtTracks" runat="server" Width="600" BorderStyle="None" ></asp:TextBox>
        <br /><br />
        <asp:Label ID="lblChangeEmail" runat="server" Width ="218px" Text="Change your listed e-mail address:" ForeColor="Red"></asp:Label>
        <asp:TextBox ID="txtChangeEmail" runat="server" Width ="280px" ></asp:TextBox>
        <asp:Button ID="ChangeEmailButton" runat="server" Text="Submit" Width="82px" OnClick="ChangeEmail_Click" />
        <br /><br />
        <table cellpadding="1" cellspacing="0" style="border-collapse:collapse; background-color: #CC6666;">
        <tr>
            <td class="auto-style3">
                <table cellpadding="0" style="width: 609px">
                    <tr>
                        <td align="center" colspan="2" class="auto-style6"><h4 style="width: 603px">Change your account's password.</h4>
                            <p>&nbsp;</p>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="auto-style2">
                            <asp:Label ID="lblOldPassword" runat="server" AssociatedControlID="txtOldPassword">Old Password:</asp:Label>
                        </td>
                        <td class="auto-style1">
                            <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" Width="269px"></asp:TextBox>                       
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="auto-style2">
                            <asp:Label ID="lblNewPassword" runat="server" AssociatedControlID="txtNewPassword">New Password:</asp:Label>
                        </td>
                        <td class="auto-style1">
                            <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" Width="269px"></asp:TextBox>
                        </td>
                     </tr>
                     <tr>                         
                         <td align="center" colspan ="2">
                            <br />
                            <asp:Button ID="ChangePasswordButton" runat="server" Text="Submit" Width="143px" OnClick="ChangePassword_Click" />                            
                         </td>                 
                     </tr>
                </table>
            </td>
        </tr>
        </table>
        <br /><br />
        <h2>User's playlist and track inteface:</h2>
        <br />
        <table align="center" cellpadding="1" cellspacing="0" style="border-collapse:collapse; background-color: #7a80b1;">
            <tr>
                <td align="center" colspan="3" class="auto-style8"><h2 style="width: 1053px">Data Display</h2></td>
            </tr>
            <tr>
                <td align="center" colspan="3" class="auto-style8">
                    Song Name:
                    <asp:TextBox ID="txtEditSongName" runat="server" Text="" Width="150px"></asp:TextBox>
                    Artist:
                    <asp:TextBox ID="txtEditArtist" runat="server" Text="" Width="150px"></asp:TextBox>
                    Album:
                    <asp:TextBox ID="txtEditAlbum" runat="server" Text="" Width="150px"></asp:TextBox>
                    Shared:
                    <asp:CheckBox ID="cbxEditShare" runat="server" />
                    <asp:Button ID="SubmitChangesToTrack" runat="server" Text="Submit Changes" OnClick="SubmitChangesToTrack_Click" />
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td align="center"><h3>Playlist Selection</h3></td>
                <td align="center"><h3>Tracks in Playlist</h3></td>
                <td align="center" class="auto-style22"><h3>Track Selection</h3></td>
            </tr>
            <tr>
                <td align="center" class="auto-style11">
                    <asp:Label ID="lblPlayListSelection" runat="server" AssociatedControlID="ddlPlayListSelection" >Playlist Selection:</asp:Label>                        
                    <asp:DropDownList ID="ddlPlaylistSelection" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPlaylistSelection_Change" >
                    </asp:DropDownList>
                    <asp:Button ID="DeleteActivePlaylist" runat="server" Text="<-Delete" OnClick="DeleteActivePlaylist_Click" />
                    <br />
                    <asp:Button ID="NewPlaylistButton" runat="server" Text="Create Playlist" Width="143px" OnClick="CreatePlaylist_Click" />
                    <br />
                    <asp:Label ID="lblNewPlaylistName" runat="server" AssociatedControlID="txtNewPlaylistName" Text="New Playlist's Name:" Visible="False" ></asp:Label>
                    <asp:TextBox ID="txtNewPlaylistName" runat="server" Width="150px" Visible="False" />
                    <br />
                    <asp:Label ID="lblMakeAsPublic" runat="server" AssociatedControlID="cbxMakeAsPublic" Text="Public Playlist:" Visible="False" ></asp:Label>
                    <asp:CheckBox ID="cbxMakeAsPublic" runat="server" Text="      "  Visible="False" Checked="true" />
                    &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="CreatePlaylistSubmitButton" runat="server" Text="Create" OnClick="CreatePlaylistSubmit_Click" Visible="False" />
                </td>
                <td align="center" class="auto-style12">
                    <asp:ListBox ID="lbPlaylistTracks" runat="server" style="margin-left: 0px" Width="247px" Height="186px"></asp:ListBox>
                    <p></p>
                    <asp:Button ID="ChangeOrderUp" runat="server" Text="Up" OnClick="ChangeOrderUp_Click"  Width="48px" />
                    <asp:Button ID="ChangeOrderDown" runat="server" Text="Down" OnClick="ChangeOrderDown_Click" />
                </td>
                <td align="center" class="auto-style23">
                    <asp:ListBox ID="lbxMyTracks" runat="server" style="margin-left: 0px" Width="247px" Height="186px" OnSelectedIndexChanged="lbxMyTracks_SelectedChange" AutoPostBack="True">
                    </asp:ListBox>
                    <p></p>
                    <p></p>
                </td>
            </tr>
            <tr style="background-color: #9966FF">
                <td align="center">
                    <asp:CheckBox ID="cbxEditPlaylistPublic" runat="server" Text="Public Playlist" />
                    <asp:Button ID="ChangeIsPlaylistPublic" runat="server" Text="Submit Change" OnClick="ChangeIsPlaylistPublic_Click" />
                </td>
                <td align="center">
                    <asp:Button ID="RemoveTrackLocalList" runat="server" Text="Remove Track from Playlist" OnClick="RemoveTrackLocalList_Click" />
                </td>
                <td align="center" class="auto-style22">
                    <asp:Button ID="AddTrackLocalList" runat="server" Text="Add Track to Current Playlist" OnClick="AddTrackLocalList_Click" Width="263px" />
                </td>
            </tr>  
            <tr style="background-color: darkgreen">
                <td></td>
                <td></td>
                <td align="center" class="auto-style22">
                    <asp:Button ID="AddSharedTrackLocalList" runat="server" Text="Add Shared Track to Your List" OnClick="AddSharedTrackLocalList_Click" Width="263px" />
                </td>
            </tr>    
            <tr style="background-color: #00CCFF">
                <td align="center" colspan="3" class="auto-style8"></td>  
            </tr>    
            <tr style="background-color: #00CCFF">                
                <td align="center" class="auto-style14">
                    <asp:Label ID="lblPublicPlaylist" runat="server" AssociatedControlID="ddlPublicPlayList" >Public List Selection:</asp:Label>                        
                    <asp:DropDownList ID="ddlPublicPlayList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPublicPlaylist_Change" >
                    </asp:DropDownList>
                </td>
                <td align="center" class="auto-style15">
                    <asp:ListBox ID="lbxPublicPlaylistTracks" runat="server" style="margin-left: 0px" Width="247px" Height="186px" ></asp:ListBox>
                </td>
                <td align="center" class="auto-style24">
                    <asp:ListBox ID="lbxSharedTracks" runat="server" style="margin-left: 0px" Width="247px" Height="186px" OnSelectedIndexChanged="lbxSharedTracks_SelectedChange" AutoPostBack="True">
                    </asp:ListBox>
                </td>
            </tr>
            <tr style="background-color: #00CCFF">
                <td align="center" colspan="3" class="auto-style8"></td>  
            </tr>
        </table>
        <asp:Label ID="lblDropDownValue" runat="server" BackColor="#99FF99" ></asp:Label>
        <br />
        <br />
        <asp:HyperLink ID="hplUploadSongs" runat="server" NavigateUrl="~/Uploader.aspx">Upload Songs</asp:HyperLink>
        <asp:HyperLink ID="hplPlayer" runat="server" NavigateUrl="~/Player.aspx">Player</asp:HyperLink>
        <br /><br />
        </div>
    </form>
</body>
</html>
