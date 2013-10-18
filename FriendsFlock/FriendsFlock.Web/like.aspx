<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="like.aspx.cs" Inherits="FriendsFlock.Web.like" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" 
    xmlns:fb="https://www.facebook.com/2008/fbml"
    xmlns:og="http://ogp.me/ns#">
<head runat="server">
    <title>Friends Flock - Like</title>
    <meta property="og:title" content="Friends Flock" />
    <meta property="og:type" content="website" />
    <meta property="og:url" content="http://www.friendsflock.com" />
    <meta property="og:image" content="http://www.friendsflock.com/images/friendsflock_demo.jpg" />
    <meta property="og:site_name" content="Friends Flock" />
    <meta property="fb:app_id" content="157050984313106" />
    <meta property="og:description" content="This website builds a map of your friends and clusters them into flocks. See how your friends inter-connect!" />
    <link rel="stylesheet" href="css/reset.css" type="text/css" media="screen"/>
    <link rel="stylesheet" href="css/style.css" type="text/css" media="screen"/>
    <link rel="stylesheet" href="css/layout.css" type="text/css" media="screen"/>
    <script type="text/javascript" src="js/html5.js"></script>
    <script type="text/javascript" src="js/jquery-1.4.2.min.js" ></script>
    <script type="text/javascript" src="js/jquery.faded.js"></script>
    <script type="text/javascript" src="js/custom.js"></script>
    <script type="text/javascript" src="js/maxheight.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
	    <div class="wrapper">
		    <h1><a href="default.aspx">
		    <strong>friends flock<em>beta</em></strong></a></h1>
        </div>
        <div id="fb-root"></div>
        <div style="margin-top:8px; margin-left:12px;">
            <script src="http://connect.facebook.net/en_US/all.js#xfbml=1" type="text/javascript"></script>
            <fb:like href="http://www.friendsflock.com/" send="false" width="450" show_faces="true" colorscheme="dark" font="verdana"></fb:like>
        </div>
    </div>
    </form>
</body>
</html>
