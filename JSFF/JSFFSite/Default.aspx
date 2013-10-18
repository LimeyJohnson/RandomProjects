<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="JSFFSite.Default" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Friends Graph</title>
</head>
<body>
   <style>

.node {
  stroke: #fff;
  stroke-width: 1.5px;
}

.link {
  stroke: #999;
  stroke-opacity: .6;
}

</style>
    
    <script type="text/javascript" src="//connect.facebook.net/en_US/all.js"></script>
     <script type="text/javascript" src="Scripts/jquery-1.4.1.js"></script>
    <script type="text/javascript" src="Scripts/mscorlib.js"></script>
    <script type="text/javascript" src="Scripts/JSFFScript.debug.js"></script>
    <script src="http://d3js.org/d3.v3.min.js" charset="utf-8"></script>
    <img id="image" alt="Profile Pic" width="100", height="100" />
    <div class="fb-login-button">
    </div>
    <div id="fb-root">
    </div>
    <button id="login" title="Clickme">
        LogIn</button>
    <button id="graph" title="Clickme">
       GraphIt</button>
       <button id="Iterate" title="Clickme">
        Iterate</button>
    <button id="LogoutButton" title="Clickme">
        Logout</button>
        <label id="friendName"></label>
    <div id="resultsDiv">
    </div>
    <div id="canvas"></div>
    </body>
</html>
