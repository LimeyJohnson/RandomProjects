using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaownaMp3Library
{
    //for managing selections and shared playlists/tracks
    public class Jukebox
    {
        private int _userId;
        private List<int> _publicPlaylistIds;
        private List<int> _myPublicListIds;
        private List<int> _sharedTrackIds;
        private List<int> _mySharedTrackIds;
        private Track _selectedTrack;
        private Track _selectedSharedTrack;
        private PlayList _mySelectedPlaylist;

        public Jukebox(int id)
        {
            _userId = id;
            _publicPlaylistIds = DataAccess.Instance.GetPublicPlaylistIds(id);
            _myPublicListIds = DataAccess.Instance.GetMyPublicPlaylistIds(id);
            _sharedTrackIds = DataAccess.Instance.GetSharedTrackIds(id);
            _mySharedTrackIds = DataAccess.Instance.GetMySharedTrackIds(id);
        }

        public List<int> PublicPlayListIds
        {
            get { return _publicPlaylistIds; }
        }
        public List<int> MyPublicPlayListIds
        {
            get { return _myPublicListIds; }
        }
        public List<int> SharedTrackIds
        {
            get { return _sharedTrackIds; }
        }
        public List<int> MySharedTackIds
        {
            get { return _mySharedTrackIds; }
        }
        public Track SelectedTrack
        {
            get { return _selectedTrack; }
            set { _selectedTrack = value; }
        }
        public Track SelectedSharedTrack
        {
            get { return _selectedSharedTrack; }
            set { _selectedSharedTrack = value; }
        }
        public PlayList MySelectedPlayList
        {
            get { return _mySelectedPlaylist; }
            set { _mySelectedPlaylist = value; }
        }
        public void ResetMySharedPlaylistIds()
        {
            _myPublicListIds = DataAccess.Instance.GetMyPublicPlaylistIds(_userId);
        }
        public List<string> SharedTrackNames()
        {
            string name;
            List<string> trackNames = new List<string>(), trackArtists = new List<string>();

            for (int i = 0; i < this.SharedTrackIds.Count; i++)
            {
                name = DataAccess.Instance.GetSongName(this.SharedTrackIds[i]);
                trackNames.Add(name);
            }

            return trackNames;
        }
        public List<string> SharedTrackArtists()
        {
            string artist;
            List<string> trackArtists = new List<string>();

            for (int i = 0; i < this.SharedTrackIds.Count; i++)
            {
                artist = DataAccess.Instance.GetArtist(this.SharedTrackIds[i]);
                trackArtists.Add(artist);
            }

            return trackArtists;
        }
    }
}
