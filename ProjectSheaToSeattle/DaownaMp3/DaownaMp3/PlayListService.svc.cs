using DaownaMp3Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace DaownaMp3
{
    [ServiceContract(Namespace = "", SessionMode=SessionMode.Allowed)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service1
    {
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]
        [WebGet]
        public Track[] GetActiveTracks()
        {
           List<Track> activeTracks = DataAccess.Instance.GetActiveTracks();
           return activeTracks.ToArray<Track>();
        }

        [OperationContract]
        [WebGet]
        public Track[] GetActiveTracksByUser()
        {
            string userID = HttpContext.Current.Session["ID"].ToString();
            List<Track> activeTracks = DataAccess.Instance.GetActiveTracks(userID);
            return activeTracks.ToArray<Track>();
        }
        [OperationContract]
        [WebGet]
        public PlaylistJSON[] GetAllPlaylists()
        {
            return DataAccess.Instance.GetAllPlaylists();
        }
        [OperationContract]
        [WebGet]
        public Track[] GetPlaylistTracks(int ID)
        {
            //Sorting mess
            PlayList organizer = new PlayList(ID);
            organizer.PlayListSyncOrder();
            List<Track> orderedList = new List<Track>();
            List<Track> unorderedList = DataAccess.Instance.GetTracksInPlaylist(ID);
            Track hold;

            for (int i = 0; i < organizer.PlayListTrackIds.Count; i++)
            {
                hold = unorderedList.Find(x => x.TrackId == organizer.PlayListTrackIds[i]);
                orderedList.Add(hold);
            }

            return orderedList.ToArray<Track>();
            //return DataAccess.Instance.GetTracksInPlaylist(ID).ToArray<Track>();
        }
        // Add more operations here and mark them with [OperationContract]
    }
}
