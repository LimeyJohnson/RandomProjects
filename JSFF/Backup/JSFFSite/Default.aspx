<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="JSFFSite.Default" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Friends Graph</title>
</head>
<body>
   
    
    <script type="text/javascript" src="//connect.facebook.net/en_US/all.js"></script>
     <script type="text/javascript" src="Scripts/jquery-1.4.1.js"></script>
    <script type="text/javascript" src="Scripts/mscorlib.js"></script>
    <script type="text/javascript" src="Scripts/JSFFScript.debug.js"></script>
    <img id="image" alt="Profile Pic" width="100", height="100" />
    <div class="fb-login-button">
    </div>
    <div id="fb-root">
    </div>
    <button id="MyButton" title="Clickme">
        LogIn</button>
    <button id="PostButton" title="Clickme">
       GraphIt</button>
       <button id="Iterate" title="Clickme">
        Iterate</button>
    <button id="LogoutButton" title="Clickme">
        Logout</button>
        <label id="friendName"></label>
    <div id="resultsDiv">
    </div>
    <canvas id="tutorial" width="800" height="800">
    </canvas>
    <div id="images">
    </div>
    <form id="form1" runat="server">
    </form>
</body>
</html>
