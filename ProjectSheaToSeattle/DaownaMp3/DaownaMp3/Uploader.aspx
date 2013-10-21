<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Uploader.aspx.cs" Inherits="DaownaMp3.Uploader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>Artist</td>
                    <td>
                        <asp:TextBox ID="txtArtist" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Track Name</td>
                    <td>
                        <asp:TextBox ID="txtTrack" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>File</td>
                    <td>
                        <asp:FileUpload ID="uplTrack" runat="server" /></td>
                </tr>
                <tr>
                    <td>Share</td>
                    <td>
                        <asp:CheckBox ID="cbxShare" runat="server" Checked="true" />
                    </td>
                </tr>
                <tr><td></td>
                    <td align="right"><asp:Button ID="btnUpload" runat="server" Text="UploadSong" OnClick="btnUpload_Click" /></td></tr>
            </table>
        </div>
    </form>
</body>
</html>
