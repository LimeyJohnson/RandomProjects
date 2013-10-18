<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="AdTreeMapSite._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Johnson Family Age - Tree Graph</title>
    <script src="Scripts/jquery-1.8.2.min.js"></script>
    <script src="Scripts/AddScript.js"></script>
    <script src="http://d3js.org/d3.v3.min.js"></script>
</head>
   
<body>
    <div id="breadcrumbs"></div>
    <style>
        body {
  font-family: "Helvetica Neue", Helvetica, Arial, sans-serif;
  margin: auto;
  position: relative;
  width: 960px;
}

form {
  position: absolute;
  right: 10px;
  top: 10px;
}

.node {
  border: solid 1px white;
  font: 10px sans-serif;
  line-height: 12px;
  overflow: hidden;
  position: absolute;
  text-indent: 2px;
}
        </style>
</body>
</html>
