using System;
/// <summary>
/// To add your server into the mix simply add in a case statement for the last 7 characters of the base url of your server and add in your app ID.
/// For instance LimeySrv1 would be imeysrv1 (they need to be lower case) 
/// </summary>
public static class ServerVariables
{
    private static string appID, appSecret, channelURL;


    public static void Init(string ServerURL)
    {
#if DEBUG
        string search = ServerURL.Substring(ServerURL.Length - 8).ToLower();
        switch (search)
        {
            case "edns.org":
                appID = "165603903461908";
                channelURL = "http://invictus.homedns.org/channel.html";
                appSecret = "14ca18db4bdc49907b1e82a58ad24f68";
                break;
            case "ndns.org":
            case "imeysrv1":
                appID = "240082229369859";
                channelURL = "http://LimeyHouse.dyndns.org/channel.html";
                appSecret = "240082229369859";
                break;
            case "lock.com":
            default:
                channelURL = "http://www.friendsflock.com/channel.html";
                appID = "157050984313106";
                appSecret = "0042dcdecbe8fc7193073c492d0a885e";
                break;
        }
#else
       channelURL = "http://www.friendsflock.com/channel.html";
                appID = "157050984313106";
                appSecret = "0042dcdecbe8fc7193073c492d0a885e";
#endif
    }
    public static string AppID
    {
        get
        {
            if (!string.IsNullOrEmpty(appID)) return appID;
            throw new ArgumentNullException("The APP ID has not been initialized yet");
        }
    }
    public static string ChannelURL
    {
        get
        {
            if (!string.IsNullOrEmpty(appID)) return channelURL;
            throw new ArgumentNullException("The ChannelURL has not been initialized yet");
        }
    }
    public static string AppSecret
    {
        get
        {
            if (!string.IsNullOrEmpty(appID)) return appSecret;
            throw new ArgumentNullException("The AppSecret has not been initialized yet");
        }
    }
}