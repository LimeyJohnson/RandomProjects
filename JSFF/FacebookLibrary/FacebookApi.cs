// Class1.cs
//

using System;
using System.Html;
using System.Runtime.CompilerServices;

namespace FacebookLibrary
{

   
        [Imported]
        [IgnoreNamespace]
        [ScriptName("FB")]
        public static class Facebook
        {
            public static void init(InitOptions options)
            {
            }
        }
        [Imported, IgnoreNamespace, ScriptName("Object")]
        public sealed class InitOptions
        {
            public string appId;
            public string channelUrl;
            public bool status;
            public bool cookie;
            public bool xfbml;
        }
    
        //[Imported]
        //[IgnoreNamespace]
        //internal sealed class PhotoCollection : Record {

        //    public Photo[] photo = null;
        //}

        //[Imported]
        //[IgnoreNamespace]
        //internal sealed class PhotoSearchResponse : Record {

        //    public PhotoCollection photos = null;
        //}

        //public interface IPhotoService {

        //    void SearchPhotos(string tags, int count, FlickrSearchCallback callback);
        //}

        //public sealed class FlickrService : IPhotoService {

        //    private const string FlickrSearchURLFormat =
        //        "http://api.flickr.com/services/rest/?method=flickr.photos.search&api_key=be9b6f66bd7a1c0c0f1465a1b7e8a764&tags={0}&per_page={1}&sort=interestingness-desc&safe_search=1&content_type=1&in_gallery=true&extras=o_dims%2Curl_sq%2Curl_m&format=json&jsoncallback={2}";

        //    public void SearchPhotos(string tags, int count, FlickrSearchCallback callback) {
        //        FlickrCallback requestCallback = delegate(PhotoSearchResponse response) {
        //            callback(response.photos.photo);
        //        };
        //        string callbackName = Delegate.CreateExport(requestCallback);

        //        string url = String.Format(FlickrSearchURLFormat, tags.EncodeUriComponent(), count, callbackName);
        //        ScriptElement script = (ScriptElement)Document.CreateElement("script");
        //        script.Type = "text/javascript";
        //        script.Src = url;
        //        Document.GetElementsByTagName("head")[0].AppendChild(script);
        //    }
        //}

        //internal delegate void FlickrCallback(PhotoSearchResponse response);

        
    }

