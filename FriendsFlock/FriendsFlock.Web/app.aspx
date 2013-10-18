<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="app.aspx.cs" Inherits="FriendsFlock.Web.app" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" xmlns:fb="https://www.facebook.com/2008/fbml">
<head runat="server" title="Friends Flock">
    <title>Friends Flock - App</title>
    <!--STYLES-->
    <style type="text/css">  html, body {  height: 100%;  overflow: auto;  }  body {  padding: 0;  margin: 0;  }   #silverlightControlHost {    height: 100%;  }</style>

    <!--SCRIPTS-->
    <script type="text/javascript" src="Silverlight.js"></script>
    <script type="text/javascript" src="Scripts/Silverlight.supportedUserAgent.js"></script>
    <script type="text/javascript">
        var PromptFinishInstall = "<div><p>You are now installing Silverlight, refresh your browser when done.</p></div>";
        var PromptUpgrade = "<div><p onclick='UpgradeClicked'>This application needs you to upgrade the Silverlight plugin that runs it. An older version is installed. Click here to upgrade it.</p></div>";
        var PromptFinishUpgrade = "<div><p>You are now upgrading Silverlight. When this is done, please restart your browser.</p></div>";
        var PromptRestart = "<div><p>Please restart your browser.</p></div>";
        var PromptNotSupported = "<div><p>This browser doesn't support Silverlight, sorry!</p></div>";

        function onSilverlightError(sender, args) {

            var appSource = "";
            if (sender != null && sender != 0) {
                appSource = sender.getHost().Source;
            }

            var errorType = args.ErrorType;
            var iErrorCode = args.ErrorCode;
            // ADD THIS BLOCK!
            if (iErrorCode == 8001) {
                window.location = "http://www.friendsflock.com/Silverlight/upgrade.aspx";

                return;
            }

            if (Silverlight.IsVersionAvailableOnerror(sender, args)) {

                var errMsg = "Unhandled Error in Silverlight 2 Application " + appSource + "\n";
                errMsg += "Code: " + iErrorCode + " \n";
                errMsg += "Category: " + errorType + " \n";
                errMsg += "Message: " + args.ErrorMessage + " \n";

                if (errorType == "ParserError") {
                    errMsg += "File: " + args.xamlFile + " \n";
                    errMsg += "Line: " + args.lineNumber + " \n";
                    errMsg += "Position: " + args.charPosition + " \n";
                } else if (errorType == "RuntimeError") {
                    if (args.lineNumber != 0) {
                        errMsg += "Line: " + args.lineNumber + " \n";
                        errMsg += "Position: " + args.charPosition + " \n";
                    }

                    errMsg += "MethodName: " + args.methodName + " \n";
                } throw new Error(errMsg);
            } 
        }

        function onSilverlightLoad(sender) {
            Silverlight.IsVersionAvailableOnLoad(sender);
        }

        Silverlight.onRequiredVersionAvailable = function () {
        };
        
        Silverlight.onRestartRequired = function () {
            alert("Restart Required");
            document.getElementById("silverlightControlHost").innerHTML = PromptRestart;
        };

        //UPGRADE REQUIRED
        Silverlight.onUpgradeRequired = function () {
            window.location = "http://www.friendsflock.com/Silverlight/upgrade.aspx";
        };

        //INSTALL REQUIRED
        Silverlight.onInstallRequired = function () {
            window.location = "http://www.friendsflock.com/Silverlight/install.aspx";
        };

        //UPGRADE CLICKED, NEED RESTART
        function UpgradeClicked() {
            window.location = "http://www.friendsflock.com/Silverlight/upgrade.aspx";
        }

        //INSTALL CLICKED, NEED REFRESH
        function InstallClicked() {
            window.location = "http://www.friendsflock.com/Silverlight/install.aspx";
        }

        //NOT SUPPORTED
        function CheckSupported() {
            var tst = Silverlight.supportedUserAgent();
            if (tst) {      // Do nothing    
            }
            else {
//                window.location = "http://www.friendsflock.com/Silverlight/unsupported.aspx";
            }
        }  
        </script>
        <script type="text/javascript">

            var _gaq = _gaq || [];
            _gaq.push(['_setAccount', 'UA-25437372-1']);
            _gaq.push(['_trackPageview']);

            (function () {
                var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
                ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
                var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
            })();
        </script>

</head>
<body onload="CheckSupported()">
    <form id="form1" runat="server" >
       <div id='errorLocation' style="font-size: small;color: Gray;"></div>
        <div style="position: fixed; height: 100%; width: 100%">
            <object
                type="application/x-silverlight-2" 
                data="data:application/x-silverlight-2," 
                style="height: 100%; width: 100%; margin-left: 0%; margin-right: 0%;">
                <param name="source" value="ClientBin/FriendsFlockClient.xap"/>
                <param name="background" value="white" />
                <param name="minRuntimeVersion" value="4.0.50401.0" />
                <param name="autoUpgrade" value="false" />
                <param name="onerror" value="onSilverlightError" />  
                <param name="onload" value="onSilverlightLoad" />  
                <param name="allowHtmlPopupWindow" value="true" />
                <param name="enableHtmlAccess" value="true" />
                <asp:Literal ID="ParamInitParams" runat="server"></asp:Literal>
           
            <div id="SLInstallFallback">
                   <div><p onclick='UpgradeClicked'>This application needs you to use the Silverlight plugin to use it. Click here to install it.</p></div>
            </div>
        </object>
    
        <iframe style='visibility:hidden;height:0;width:0;border:0px'>
        </iframe>
    </div> <div id="silverlightExperienceHost" style="visibility:hidden;"></div>
    </form>

    <!--Facebook Logout-->
    <script src="http://connect.facebook.net/en_US/all.js" type="text/javascript"></script>
    <script type="text/javascript">
        function logout() {
            FB.logout(function (response) {
            });
            window.location = "default.aspx";
        }
    </script>
    </body>
</html>