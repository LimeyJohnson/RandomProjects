using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DaownaMp3Library
{
    public class DataAccess
    {
        SqlConnection MyConnection;
        private static DataAccess DA;
        private DataAccess()
        {
            if (MyConnection == null)
            {
                SqlConnectionStringBuilder Scsb = new SqlConnectionStringBuilder();
                Scsb.UserID = ServerInfo.DatabaseUsername;
                Scsb.Password = ServerInfo.DatabasePassword;
                Scsb.InitialCatalog = ServerInfo.DatabaseDaownaMp3;
                Scsb.TrustServerCertificate = false;
                Scsb.DataSource = ServerInfo.DatabaseServer;
                Scsb.Encrypt = true;

                MyConnection = new SqlConnection(Scsb.ToString());
            }
        }
        public static DataAccess Instance
        {
            get
            {
                return new DataAccess();
            }
        }
        public string WhereClauseInterpreter(params string[] clauses)
        {
            if (clauses == null || clauses.Count() <= 0) return string.Empty;
            clauses = clauses.Where(s => !string.IsNullOrEmpty(s)).ToArray();
            string whereClause = "WHERE "+string.Join(" AND ", clauses);
            return whereClause;
        }
        public void AddNewTrack(string trackName, string artist, Uri URL, string userID)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "INSERT INTO Track(SongName, Artist, UploadedById, BlobURL, Share) VALUES (@SongName, @Artist, @UploadedById, @BlobURL,1)";
            cmd.Parameters.AddWithValue("@SongName", trackName);
            cmd.Parameters.AddWithValue("@Artist", artist);
            cmd.Parameters.AddWithValue("@UploadedById", userID);
            cmd.Parameters.AddWithValue("@BlobURL", URL.ToString());

            MyConnection.Open();
            int rowsReturned = cmd.ExecuteNonQuery();
            MyConnection.Close();
            if (rowsReturned == 0) throw new Exception("There were no rows affected, something went wrong with sql statment");

        }
        public int AddTrack(string trackName, string artist, string album, int userId, bool share, string url)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "INSERT INTO Track(SongName, Artist, Album, UploadedById, Share, BlobURL) VALUES (@SongName, @Artist, @Album, @UploadedById, @Share, @BlobURL)";
            cmd.Parameters.AddWithValue("@SongName", trackName);
            cmd.Parameters.AddWithValue("@Artist", artist);
            cmd.Parameters.AddWithValue("@Album", album);
            cmd.Parameters.AddWithValue("@UploadedById", userId);
            cmd.Parameters.AddWithValue("@Share", share);
            cmd.Parameters.AddWithValue("@BlobURL", url);

            MyConnection.Open();
            int rowsReturned = cmd.ExecuteNonQuery();
            MyConnection.Close();
            if (rowsReturned == 0) throw new Exception("There were no rows affected, something went wrong with sql statment");

            return GetTrackId(url);
        }
        public bool Authenticate(LoginCredentials creds)
        {
            int userId = -1;
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [ID] FROM [User] WHERE ([UserName] = @Name OR [Email] = @Name) AND [Password] = @Password";            
            cmd.Parameters.Add(new SqlParameter("@Name", (string)creds.Username));
            cmd.Parameters.Add(new SqlParameter("@Password", (string)creds.Password));

            MyConnection.Open();
            object returnVal = cmd.ExecuteScalar();
            MyConnection.Close();

            if (returnVal != null)
                userId = (int)returnVal;

            if (userId >= 0)
            {
                creds.ID = userId;
                return true;
            }
            return false;
        }
        public bool CheckRegistrationName(string name)
        {
            bool exists;
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [ID] FROM [USER] WHERE [UserName] = @Name OR [Email] = @Name";
            cmd.Parameters.Add(new SqlParameter("@Name", name));

            MyConnection.Open();
            SqlDataReader returnReader = cmd.ExecuteReader();

            if (returnReader.Read() != true)
                exists = false;
            else
                exists = true;
            MyConnection.Close();

            return exists;
        }
        public bool AddAccount(string name, string password, string email, DateTime now)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "INSERT INTO [USER] ([UserName] ,[Password] ,[Email] ,[DateRegistered] ,[Active] ,[Admin]) VALUES (@Name, @Pass, @Email, @Date, @Active, @Admin)";
            cmd.Parameters.Add(new SqlParameter("@Name", name));
            cmd.Parameters.Add(new SqlParameter("@Pass", password));
            cmd.Parameters.Add(new SqlParameter("@Email", email));
            cmd.Parameters.Add(new SqlParameter("@Date", now));
            cmd.Parameters.Add(new SqlParameter("@Active", false));
            cmd.Parameters.Add(new SqlParameter("@Admin", false));

            MyConnection.Open();
            int result = cmd.ExecuteNonQuery();
            MyConnection.Close();
            return result == 1;
        }
        public string GetAccountName(int id)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [UserName] FROM [USER] WHERE [ID] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", id));

            MyConnection.Open();
            SqlDataReader returnReader = cmd.ExecuteReader();
            returnReader.Read();
            string userName = GetDBString(returnReader, "UserName");
            MyConnection.Close();

            return userName;
        }
        public DateTime GetDateRegistered(int id)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [DateRegistered] FROM [USER] WHERE [ID] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", id));

            MyConnection.Open();
            SqlDataReader returnReader = cmd.ExecuteReader();
            returnReader.Read();
            DateTime date = GetDBDate(returnReader, "DateRegistered");
            MyConnection.Close();

            return date;
        }
        public bool GetActive(int id)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [Active] FROM [USER] WHERE [ID] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", id));

            MyConnection.Open();
            SqlDataReader returnReader = cmd.ExecuteReader();
            returnReader.Read();
            bool active = GetDBBool(returnReader, "Active");
            MyConnection.Close();

            return active;
        }
        public string GetEmail(int id)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [Email] FROM [USER] WHERE [ID] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", id));

            MyConnection.Open();
            SqlDataReader returnReader = cmd.ExecuteReader();
            returnReader.Read();
            string email = GetDBString(returnReader, "Email");
            MyConnection.Close();

            return email;
        }
        public string GetEmail(string name)
        {
            string email;
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [Email] FROM [USER] WHERE [UserName] = @Name OR [Email] = @Name";
            cmd.Parameters.Add(new SqlParameter("@Name", name));

            MyConnection.Open();
            SqlDataReader returnReader = cmd.ExecuteReader();

            if (returnReader.Read() != true)
               email = "dne";
            else
                email = GetDBString(returnReader, "Email");
            MyConnection.Close();

            return email;
        }        
        public bool SetEmail(int id, string email)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "UPDATE [User] SET [Email] = @Email WHERE [ID] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", id));
            cmd.Parameters.Add(new SqlParameter("@Email", email));

            MyConnection.Open();
            int result = cmd.ExecuteNonQuery();
            MyConnection.Close();
            return result == 1;
        }
        public string GetLostPassword(string email)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [Password] FROM [User] WHERE [Email] = @Name";
            cmd.Parameters.Add(new SqlParameter("@Name", email));

            MyConnection.Open();
            SqlDataReader returnReader = cmd.ExecuteReader();
            returnReader.Read();
            string password = GetDBString(returnReader, "Password");
            MyConnection.Close();

            return password;
        }
        public string GetPassword(int id)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [Password] FROM [USER] WHERE [ID] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", id));

            MyConnection.Open();
            SqlDataReader returnReader = cmd.ExecuteReader();
            returnReader.Read();
            string password = GetDBString(returnReader, "Password");
            MyConnection.Close();

            return password;
        }
        public bool SetPassword(int id, string password)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "UPDATE [User] SET [Password] = @Password WHERE [ID] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", id));
            cmd.Parameters.Add(new SqlParameter("@Password", password));

            MyConnection.Open();
            int result = cmd.ExecuteNonQuery();
            MyConnection.Close();
            return result == 1;
        }
        public List<int> GetUserPlaylistIds(int userId)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [ID] FROM [PlayList] WHERE [UserId] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", userId));

            MyConnection.Open();
            SqlDataReader returnReader = cmd.ExecuteReader();
            List<int> playlistIds = new List<int>();
            int read = returnReader.GetOrdinal("ID");

            while (returnReader.Read())
            {
                playlistIds.Add((int)returnReader.GetValue(read));
            }
            MyConnection.Close();

            return playlistIds;
        }
        public List<int> GetUserTrackIds(int userId)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [ID] FROM [Track] WHERE [UploadedById] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", userId));

            MyConnection.Open();
            SqlDataReader returnReader = cmd.ExecuteReader();
            List<int> trackIds = new List<int>();
            int read = returnReader.GetOrdinal("ID");

            while (returnReader.Read())
            {
                trackIds.Add((int)returnReader.GetValue(read));
            }
            MyConnection.Close();

            return trackIds;
        }
        public List<int> GetPublicPlaylistIds(int userId)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [ID] FROM [PlayList] WHERE [Public] = @True AND [UserId] <> @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", userId));
            cmd.Parameters.Add(new SqlParameter("@True", true));

            MyConnection.Open();
            SqlDataReader returnReader = cmd.ExecuteReader();
            List<int> playlistIds = new List<int>();
            int read = returnReader.GetOrdinal("ID");

            while (returnReader.Read())
            {
                playlistIds.Add((int)returnReader.GetValue(read));
            }
            MyConnection.Close();

            return playlistIds;
        }
        public List<int> GetMyPublicPlaylistIds(int userId)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [ID] FROM [PlayList] WHERE [Public] = @True AND [UserId] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", userId));
            cmd.Parameters.Add(new SqlParameter("@True", true));

            MyConnection.Open();
            SqlDataReader returnReader = cmd.ExecuteReader();
            List<int> playlistIds = new List<int>();
            int read = returnReader.GetOrdinal("ID");

            while (returnReader.Read())
            {
                playlistIds.Add((int)returnReader.GetValue(read));
            }
            MyConnection.Close();

            return playlistIds;
        }
        public List<int> GetSharedTrackIds(int userId)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [ID] FROM [Track] WHERE [Share] = @True AND [UploadedById] <> @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", userId));
            cmd.Parameters.Add(new SqlParameter("@True", true));

            MyConnection.Open();
            SqlDataReader returnReader = cmd.ExecuteReader();
            List<int> trackIds = new List<int>();
            int read = returnReader.GetOrdinal("ID");

            while (returnReader.Read())
            {
                trackIds.Add((int)returnReader.GetValue(read));
            }
            MyConnection.Close();

            return trackIds;
        }
        public List<int> GetMySharedTrackIds(int userId)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [ID] FROM [Track] WHERE [Share] = @True AND [UploadedById] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", userId));
            cmd.Parameters.Add(new SqlParameter("@True", true));

            MyConnection.Open();
            SqlDataReader returnReader = cmd.ExecuteReader();
            List<int> trackIds = new List<int>();
            int read = returnReader.GetOrdinal("ID");

            while (returnReader.Read())
            {
                trackIds.Add((int)returnReader.GetValue(read));
            }
            MyConnection.Close();

            return trackIds;
        }
        public int GetPlayListId(string playlistName, int userId)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [ID] FROM [PlayList] WHERE [PlayListName] = @Name AND [UserId] = @UserId";
            cmd.Parameters.Add(new SqlParameter("@Name", playlistName));
            cmd.Parameters.Add(new SqlParameter("@UserId", userId));

            MyConnection.Open();
            object returnVal = cmd.ExecuteScalar();
            MyConnection.Close();

            return (int)returnVal; 
        }
        public string GetPlayListName(int playlistId)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [PlayListName] FROM [PlayList] WHERE [ID] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", playlistId));

            MyConnection.Open();
            SqlDataReader returnReader = cmd.ExecuteReader();
            returnReader.Read();
            string playlistname = GetDBString(returnReader, "PlayListName");
            MyConnection.Close();

            return playlistname;
        }
        public bool SetPlayListName(int id, string name)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "UPDATE [PlayList] SET [PlayListName] = @Name WHERE [ID] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", id));
            cmd.Parameters.Add(new SqlParameter("@Name", name));

            MyConnection.Open();
            int result = cmd.ExecuteNonQuery();
            MyConnection.Close();
            return result == 1;
        }
        public int GetPlayListUserId(int playlistId)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [PlayListUserId] FROM [PlayList] WHERE [ID] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", playlistId));

            MyConnection.Open();
            object returnVal = cmd.ExecuteScalar();
            MyConnection.Close();

            return (int)returnVal;            
        }
        public bool SetPlayListUserId(int id, int userId)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "UPDATE [PlayList] SET [PlayListUserId] = @UserId WHERE [ID] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", id));
            cmd.Parameters.Add(new SqlParameter("@UserId", userId));

            MyConnection.Open();
            int result = cmd.ExecuteNonQuery();
            MyConnection.Close();
            return result == 1;
        }
        public PlaylistJSON[] GetAllPlaylists()
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [ID],[PlayListName] FROM [PlayList]";
            MyConnection.Open();
            SqlDataReader returnReader = cmd.ExecuteReader();
            List<PlaylistJSON> playlists = new List<PlaylistJSON>();
            while (returnReader.Read())
            {
                playlists.Add(new PlaylistJSON() { ID = GetDBInt(returnReader, "ID"), Name = GetDBString(returnReader, "PlayListName") });
            }
            return playlists.ToArray<PlaylistJSON>();
        }
        public bool GetPlayListIsPublic(int id)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [Public] FROM [PlayList] WHERE [ID] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", id));

            MyConnection.Open();
            SqlDataReader returnReader = cmd.ExecuteReader();
            returnReader.Read();
            bool isPublic = GetDBBool(returnReader, "Public");
            MyConnection.Close();

            return isPublic;
        }
        public bool SetPlayListPublic(int id, bool isPublic)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "UPDATE [PlayList] SET [Public] = @Public WHERE [ID] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", id));
            cmd.Parameters.Add(new SqlParameter("@Public", isPublic));

            MyConnection.Open();
            int result = cmd.ExecuteNonQuery();
            MyConnection.Close();
            return result == 1;
        }
        public List<int> GetPlayListTrackIds(int playlistId)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [TrackId] FROM [PlayListTracks] WHERE [PlayListId] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", playlistId));

            MyConnection.Open();
            SqlDataReader returnReader = cmd.ExecuteReader();
            List<int> trackIds = new List<int>();
            int read = returnReader.GetOrdinal("TrackId");

            while (returnReader.Read())
            {
                trackIds.Add((int)returnReader.GetValue(read));
            }
            MyConnection.Close();

            return trackIds;
        }
        public List<int> GetPlayListTrackOrder(int playlistId)
        {
            List<int> order = new List<int>();

            try
            {
                SqlCommand cmd = MyConnection.CreateCommand();

                cmd.CommandText = "SELECT [ID], [TrackId], [Order] FROM [PlayListTracks] WHERE [PlayListId] = @PlaylistId";
                cmd.Parameters.Add(new SqlParameter("@PlaylistId", playlistId));

                MyConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    order.Add(GetDBInt(reader, "ID"));
                    order.Add(GetDBInt(reader, "TrackId"));
                    order.Add(GetDBInt(reader, "Order"));
                }
                MyConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
            }

            return order;                
        }
        public int GetPlayListTrackOrderPoint(int playlistTrackId)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [Order] FROM [PlayListTracks] WHERE [ID] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", playlistTrackId));

            MyConnection.Open();
            object returnVal = cmd.ExecuteScalar();
            MyConnection.Close();

            return (int)returnVal;
        }
        public bool FindPlayListTrackId(int playlistId, int trackId)
        {
            bool found;
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [ID] FROM [PlayListTracks] WHERE [PlayListId] = @PlayListId AND [TrackId] = @TrackId";
            cmd.Parameters.Add(new SqlParameter("@PlayListId", playlistId));
            cmd.Parameters.Add(new SqlParameter("@TrackId", trackId));
            
            MyConnection.Open();
            SqlDataReader returnReader = cmd.ExecuteReader();

            if (returnReader.Read() != true)
                found = false;
            else
                found = true;

            returnReader.Dispose();
            MyConnection.Close();

            return found;
        }
        public int AddPlayList(string playlistName, int userId, bool isPublic)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "INSERT INTO [PlayList]([PlayListName] ,[UserId] ,[Public]) VALUES(@PlayListName, @UserId, @Public)";
            cmd.Parameters.Add(new SqlParameter("@PlayListName", playlistName));
            cmd.Parameters.Add(new SqlParameter("@UserId", userId));
            cmd.Parameters.Add(new SqlParameter("@Public", isPublic));

            MyConnection.Open();
            int result = cmd.ExecuteNonQuery();
            MyConnection.Close();

            return result = GetPlayListId(playlistName, userId);
        }
        public bool RemovePlayList(int playlistId)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "DELETE FROM [PlayList] WHERE [ID] = @PlayListId";
            cmd.Parameters.Add(new SqlParameter("@PlayListId", playlistId));

            MyConnection.Open();
            int result = cmd.ExecuteNonQuery();
            MyConnection.Close();
            return result == 1;
        }
        public bool RemovePlayListTracks(int playlistId, int trackId)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "DELETE FROM [PlayListTracks] WHERE [PlayListId] = @PlayListId AND [TrackId] = @TrackID";
            cmd.Parameters.Add(new SqlParameter("@PlayListId", playlistId));
            cmd.Parameters.Add(new SqlParameter("@TrackId", trackId));

            MyConnection.Open();
            int result = cmd.ExecuteNonQuery();
            MyConnection.Close();
            return result == 1;
        }
        public bool AddPlayListTrack(int playlistId, int trackId, int order)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "INSERT INTO [PlayListTracks]([PlayListId] ,[TrackId], [Order]) VALUES(@PlayListId, @TrackId, @Order)";
            cmd.Parameters.Add(new SqlParameter("@PlayListId", playlistId));
            cmd.Parameters.Add(new SqlParameter("@TrackId", trackId));
            cmd.Parameters.Add(new SqlParameter("@Order", order));

            MyConnection.Open();
            int result = cmd.ExecuteNonQuery();
            MyConnection.Close();
            return result == 1;
        }
        public int GetTrackId(string url)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [ID] FROM [Track] WHERE [BlobURL] = @URL";
            cmd.Parameters.Add(new SqlParameter("@URL", url));

            MyConnection.Open();
            object returnVal = cmd.ExecuteScalar();
            MyConnection.Close();

            return (int)returnVal;
        }
        public string GetSongName(int trackId)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [SongName] FROM [Track] WHERE [ID] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", trackId));

            MyConnection.Open();
            SqlDataReader returnReader = cmd.ExecuteReader();
            returnReader.Read();
            string songName = GetDBString(returnReader, "SongName");
            MyConnection.Close();

            return songName;
        }
        public List<Track> GetTracksInPlaylist(int PLID)
        {
           
            List<Track> tracks = new List<Track>();
            try
            {
                SqlCommand cmd = MyConnection.CreateCommand();                
                cmd.CommandText = "SELECT TrackId, SongName, Artist, BlobURL FROM PlayListTracks INNER JOIN Track ON PlayListTracks.TrackId = Track.ID WHERE PlayListTracks.PlayListId = @PLID";
                cmd.Parameters.AddWithValue("@PLID", PLID);
                MyConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tracks.Add(new Track(GetDBInt(reader, "TrackId"), GetDBString(reader, "Artist"), GetDBString(reader, "BlobURL"), GetDBString(reader, "SongName")));
                }
                MyConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
            }

            return tracks;
        }
        public List<Track> GetActiveTracks(string uploadedByID = null)
        {
            List<Track> tracks = new List<Track>();
            SqlCommand cmd = MyConnection.CreateCommand();
            string whereClause = WhereClauseInterpreter("Share = 1", " BlobURL != 'null'", uploadedByID==null? "": "UploadedById = "+uploadedByID);
            cmd.CommandText = "SELECT * FROM Track " + whereClause;
            MyConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                tracks.Add(new Track(GetDBInt(reader, "ID"), GetDBString(reader, "Artist"), GetDBString(reader, "BlobURL"), GetDBString(reader, "SongName")));
            }
            MyConnection.Close();
            return tracks;
        }
        public bool SetSongName(int id, string name)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "UPDATE [Track] SET [SongName] = @Name WHERE [ID] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", id));
            cmd.Parameters.Add(new SqlParameter("@Name", name));

            MyConnection.Open();
            int result = cmd.ExecuteNonQuery();
            MyConnection.Close();
            return result == 1;
        }
        public string GetArtist(int trackId)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [Artist] FROM [Track] WHERE [ID] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", trackId));

            MyConnection.Open();
            SqlDataReader returnReader = cmd.ExecuteReader();
            returnReader.Read();
            string artist = GetDBString(returnReader, "Artist");
            MyConnection.Close();

            return artist;
        }
        public bool SetArtist(int id, string name)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "UPDATE [Track] SET [Artist] = @Name WHERE [ID] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", id));
            cmd.Parameters.Add(new SqlParameter("@Name", name));

            MyConnection.Open();
            int result = cmd.ExecuteNonQuery();
            MyConnection.Close();
            return result == 1;
        }
        public string GetAlbum(int trackId)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [Album] FROM [Track] WHERE [ID] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", trackId));

            MyConnection.Open();
            SqlDataReader returnReader = cmd.ExecuteReader();
            returnReader.Read();
            string artist = GetDBString(returnReader, "Album");
            MyConnection.Close();

            return artist;
        }
        public bool SetAlbum(int id, string name)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "UPDATE [Track] SET [Album] = @Name WHERE [ID] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", id));
            cmd.Parameters.Add(new SqlParameter("@Name", name));

            MyConnection.Open();
            int result = cmd.ExecuteNonQuery();
            MyConnection.Close();
            return result == 1;
        }
        public int GetTrackUserId(int trackId)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [UploadedById] FROM [Track] WHERE [ID] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", trackId));

            MyConnection.Open();
            object returnVal = cmd.ExecuteScalar();
            MyConnection.Close();

            return (int)returnVal;
        }
        public bool SetTrackUserId(int id, int userId)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "UPDATE [Track] SET [UploadedById] = @UserId WHERE [ID] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", id));
            cmd.Parameters.Add(new SqlParameter("@UserId", userId));

            MyConnection.Open();
            int result = cmd.ExecuteNonQuery();
            MyConnection.Close();
            return result == 1;
        }
        public bool GetTrackIsShare(int id)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [Share] FROM [Track] WHERE [ID] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", id));

            MyConnection.Open();
            SqlDataReader returnReader = cmd.ExecuteReader();
            returnReader.Read();
            bool isShare = GetDBBool(returnReader, "Share");
            MyConnection.Close();

            return isShare;
        }
        public bool SetTrackShare(int id, bool isShare)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "UPDATE [Track] SET [Share] = @Share WHERE [ID] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", id));
            cmd.Parameters.Add(new SqlParameter("@Share", isShare));

            MyConnection.Open();
            int result = cmd.ExecuteNonQuery();
            MyConnection.Close();
            return result == 1;
        }
        public string GetTrackBlobURL(int trackId)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [BlobURL] FROM [Track] WHERE [ID] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", trackId));

            MyConnection.Open();
            SqlDataReader returnReader = cmd.ExecuteReader();
            returnReader.Read();
            string blobURL = GetDBString(returnReader, "BlobURL");
            MyConnection.Close();

            return blobURL;
        }
        public bool SetTrackBlobURL(int id, string url)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "UPDATE [Track] SET [BlobURL] = @Name WHERE [ID] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", id));
            cmd.Parameters.Add(new SqlParameter("@Name", url));

            MyConnection.Open();
            int result = cmd.ExecuteNonQuery();
            MyConnection.Close();
            return result == 1;
        }
        public int GetPlayListTrackIdPointer(int playlistTrackId)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT [ID] FROM [PlayListTracks] WHERE [Order] = @PlaylistTrackId";
            cmd.Parameters.Add(new SqlParameter("@PlaylistTrackId", playlistTrackId));

            MyConnection.Open();
            object returnVal = cmd.ExecuteScalar();
            MyConnection.Close();

            if (returnVal != null)
                return (int)returnVal;
            else
                return -1;
        }
        public bool PlayListTrackOrderChange(int playlistTrackId, int order)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "UPDATE [PlayListTracks] SET [Order] = @Order WHERE [ID] = @ID";
            cmd.Parameters.Add(new SqlParameter("@ID", playlistTrackId));
            cmd.Parameters.Add(new SqlParameter("@Order", order));

            MyConnection.Open();
            int result = cmd.ExecuteNonQuery();
            MyConnection.Close();
            return result == 1;
        }

        public bool HasConnection
        {
            get
            {
                return MyConnection != null;
            }
        }

        private int GetDBInt(SqlDataReader reader, string columnName)
        {
            try
            {
                int ordinal = reader.GetOrdinal(columnName);
                return reader.IsDBNull(ordinal) ? -1 : reader.GetInt32(ordinal);
            }
            catch (IndexOutOfRangeException)
            {
                return -1;
            }
        }
        private double GetDBDouble(SqlDataReader reader, string columnName)
        {
            try
            {
                int ordinal = reader.GetOrdinal(columnName);
                return reader.IsDBNull(ordinal) ? -1 : reader.GetDouble(ordinal);
            }
            catch (IndexOutOfRangeException)
            {
                return -1;
            }
        }
        private string GetDBString(SqlDataReader reader, string columnName)
        {
            try
            {
                int ordinal = reader.GetOrdinal(columnName);
                return reader.IsDBNull(ordinal) ? string.Empty : reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException)
            {
                return string.Empty;
            }
        }
        private bool GetDBBool(SqlDataReader reader, string columnName)
        {
            try
            {
                int ordinal = reader.GetOrdinal(columnName);
                return reader.IsDBNull(ordinal) ? false : reader.GetBoolean(ordinal);
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }
        private DateTime GetDBDate(SqlDataReader reader, string columnName)
        {
            try
            {
                int ordinal = reader.GetOrdinal(columnName);
                return reader.IsDBNull(ordinal) ? DateTime.MinValue : reader.GetDateTime(ordinal);
            }
            catch (IndexOutOfRangeException)
            {
                return DateTime.MinValue;
            }
        }

    }
}
