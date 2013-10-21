using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace DaownaMp3Library
{
    [DataContract]
    public class Track
    {
        private int _trackId;
        private int _playlistTrackId;
        private int _orderPoint;
        private int _pointingTo, _pointedBy;
        private string _songName;
        private string _artist;
        private string _album;
        private int _uploadUserId;
        private bool _share;
        private string _blobURL;

        //Track Constructor for Player
        public Track(int id, string artist, string blobURL, string songName)
        {
            _trackId = id;
            _artist = artist;
            _blobURL = blobURL;
            _songName = songName;
        }
        public Track(int trackId)
        {
            _trackId = trackId;
        }
        //Track Uploader Constructor
        public Track(string songName, string artist, string album, int userID, bool share, string blobURL)
        {
            _songName = songName;
            _artist = artist;
            _album = album;
            _uploadUserId = userID;
            _share = share;
            _blobURL = blobURL;

            _trackId = DataAccess.Instance.AddTrack(_songName, _artist, _album, _uploadUserId, _share, _blobURL);
        }
        //PlayListTrack Constructor
        public Track(int playlistTrackId, int trackId, int order)
        {
            _trackId = trackId;
            _playlistTrackId = playlistTrackId;
            _orderPoint = order;
        }
        public int TrackId
        {
            get
            {
                if (_trackId != null)
                    return _trackId;
                else
                    return -1;
            }            
        }
        public int PlaylistTrackId
        {
            get { return _playlistTrackId; }
            set { _playlistTrackId = value; }
        }
        public int OrderPoint
        {
            get
            {
                _orderPoint = DataAccess.Instance.GetPlayListTrackOrderPoint(_playlistTrackId);
                
                return _orderPoint;
            }
        }
        public int PointingTo
        {
            get { return _orderPoint; }
            set { _orderPoint = value; }
        }
        public int PointedBy
        {
            get 
            { 
                int point = DataAccess.Instance.GetPlayListTrackIdPointer(_playlistTrackId);

                if (point >= 0)
                    _pointedBy = point;

                return point;
            }
        }
        [DataMember]
        public string SongName
        {
            get
            {
                if (_songName == null)
                    _songName = DataAccess.Instance.GetSongName(_trackId);

                return _songName;
            }
            set
            {
                if (DataAccess.Instance.SetSongName(_trackId, value))
                    _songName = value;
                else
                {
                    //error setting track name
                }
            }
        }
        [DataMember]
        public string Artist
        {
            get
            {
                if (_artist == null)
                    _artist = DataAccess.Instance.GetArtist(_trackId);

                return _artist;
            }
            set
            {
                if (DataAccess.Instance.SetArtist(_trackId, value))
                    _artist = value;
                else
                {
                    //error setting artist name
                }
            }
        }
        public string Album
        {
            get
            {
                if (_album == null)
                    _album = DataAccess.Instance.GetAlbum(_trackId);

                return _album;
            }
            set
            {
                if (DataAccess.Instance.SetAlbum(_trackId, value))
                    _album = value;
                else
                {
                    //error setting album name
                }
            }
        }
        public int UserId
        {
            get
            {
                if (_uploadUserId == null)
                    DataAccess.Instance.GetTrackUserId(_trackId);

                return _uploadUserId;
            }
            set
            {
                if (DataAccess.Instance.SetTrackUserId(_trackId, value))
                    _uploadUserId = value;
                else
                {
                    //error setting user id
                }
            }
        }
        public bool Share
        {
            get
            {
                return DataAccess.Instance.GetTrackIsShare(_trackId);
            }
            set
            {
                if (DataAccess.Instance.SetTrackShare(_trackId, value))
                    _share = value;
                else
                {
                    //error setting share
                }
            }
        }
        [DataMember]
        public string BlobURL
        {
            get
            {
                if (_blobURL == null)
                    _blobURL = DataAccess.Instance.GetTrackBlobURL(_trackId);

                return _blobURL;
            }
            set
            {
                if (DataAccess.Instance.SetTrackBlobURL(_trackId, value))
                    _blobURL = value;
                else
                {
                    //error setting album name
                }
            }
        }
    }
}