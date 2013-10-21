using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using FantasyLeague.FEPLObjects;
using FantasyLeague.Resources;
namespace FantasyLeague
{
    class DataAccess
    {
        static SqlConnection MyConnection;
        private static DataAccess DA;
        private DataAccess()
        {
            if (MyConnection == null)
            {
                SqlConnectionStringBuilder Scsb = new SqlConnectionStringBuilder();
                Scsb.UserID = ServerResources.DatabaseUsername;
                Scsb.Password = ServerResources.DatabasePassword;
                Scsb.InitialCatalog = ServerResources.EPLDatabase;
                Scsb.TrustServerCertificate = true;
                Scsb.DataSource = ServerResources.DatabaseServer;
                Scsb.Encrypt = false;

                MyConnection = new SqlConnection(Scsb.ToString());
            }
        }
        public static DataAccess Instance
        {
            get
            {
                if (DA == null)
                {
                    DA = new DataAccess();
                }
                return DA;
            }
        }
        public string WhereClauseInterpreter(string[] clauses)
        {
            if (clauses == null || clauses.Count() <= 0) return string.Empty;

            StringBuilder whereClause = new StringBuilder(" WHERE ");
            for (int x = 0; x < clauses.Count(); x++)
            {
                whereClause.Append(clauses[x]);
                if (x + 1 < clauses.Count()) whereClause.Append(" AND ");
            }
            return whereClause.ToString();
        }
        public Boolean InsertPlayerGameData(PlayerGameData pgd, int playerID)
        {
            //TODO: The team information needs to be in here

            SqlCommand verifyCmd = MyConnection.CreateCommand();
            verifyCmd.CommandText = "SELECT * FROM PlayerGame WHERE Player = @PlayerID AND Game = @GameID";
            verifyCmd.Parameters.AddWithValue("@PlayerID", pgd.player);
            verifyCmd.Parameters.AddWithValue("@GameID", pgd.game);
            MyConnection.Open();
            SqlDataReader reader = verifyCmd.ExecuteReader();
            bool hasRows = reader.HasRows;
            MyConnection.Close();
            int returnVal = 0;
            if (!hasRows)
            {
                SqlCommand cmd = MyConnection.CreateCommand();

                cmd.CommandText = "INSERT INTO PlayerGame(Played, Goals, Assists, Conceded, PenSave, PenMiss, Yellow, Red, Saves, Bonus, OwnGoal, Total, Game, Player, Team, NetTransfer, EASport, Value, CleanSheets) VALUES (@Played, @Goals, @Assists, @Conceded, @PenSave, @PenMiss, @Yellow, @Red, @Saves, @Bonus, @OwnGoal, @Total, @Game, @Player, @Team, @NetTransfer, @EASport, @Value, @CleanSheets)";
                cmd.Parameters.Add(new SqlParameter("@Played", pgd.played));
                cmd.Parameters.Add(new SqlParameter("@Goals", pgd.goals));
                cmd.Parameters.Add(new SqlParameter("@Assists", pgd.assist));
                cmd.Parameters.Add(new SqlParameter("@Conceded", pgd.conceded));
                cmd.Parameters.Add(new SqlParameter("@PenSave", pgd.pensave));
                cmd.Parameters.Add(new SqlParameter("@PenMiss", pgd.penmiss));
                cmd.Parameters.Add(new SqlParameter("@Yellow", pgd.yellow));
                cmd.Parameters.Add(new SqlParameter("@Red", pgd.red));
                cmd.Parameters.Add(new SqlParameter("@Saves", pgd.saves));
                cmd.Parameters.Add(new SqlParameter("@Bonus", pgd.bonus));
                cmd.Parameters.Add(new SqlParameter("@OwnGoal", pgd.ownGoal));
                cmd.Parameters.Add(new SqlParameter("@Total", pgd.total));
                cmd.Parameters.Add(new SqlParameter("@Game", pgd.game));
                cmd.Parameters.Add(new SqlParameter("@Player", playerID));
                cmd.Parameters.Add(new SqlParameter("@Team", pgd.team)); //TODO: Fix with actual Team 
                cmd.Parameters.AddWithValue("@NetTransfer", pgd.transfer);
                cmd.Parameters.AddWithValue("@EASport", pgd.EAsports);
                cmd.Parameters.AddWithValue("@Value", pgd.value);
                cmd.Parameters.AddWithValue("@CleanSheets", pgd.cleansheets);
                MyConnection.Open();
                returnVal = cmd.ExecuteNonQuery();
                MyConnection.Close();
            }
            return returnVal != 0;
        }
        public Player GetPlayer(string playerName, string playerTeam)
        {
            List<Player> players = GetPlayers(new string[] { "LastName = '" + playerName.Replace("'", "''") + "'", "Team = " + playerTeam });

            //Player does not exist in database
            if (players.Count == 0) return null;
            //There is only one player
            else if (players.Count == 1) return players[0];

            throw new ArgumentException("There was more than one player in the array");
        }
        public Player GetPlayer(int FLID)
        {
            List<Player> players = GetPlayers(new string[] { "FLID = "+FLID });

            //Player does not exist in database
            if (players.Count == 0) return null;
            //There is only one player
            else if (players.Count == 1) return players[0];

            throw new ArgumentException("There was more than one player with that FLID");
        }
        public List<Player> GetPlayers(params string[] parameters)
        {
            SqlCommand cmd = MyConnection.CreateCommand(); 
            List<Player> players = new List<Player>();
            cmd.CommandText = "SELECT * FROM Players";
            cmd.CommandText += WhereClauseInterpreter(parameters);
            MyConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Player player = new Player()
                {
                    Price = GetDBDouble(reader, "Price"),
                    ID = GetDBInt(reader, "ID"),
                    PlayerName = GetDBString(reader, "LastName"),
                    TeamID = GetDBInt(reader, "Team"),
                    Active = GetDBBool(reader, "Active"),
                    FLID = GetDBInt(reader, "FLID"),
                    Points = GetDBDouble(reader, "TotalPredicted"),
                    FirstName = GetDBString (reader, "FirstName"),
                    PhotoURL = GetDBString(reader, "PhotoURL"),
                    Position = Player.GetPositionFromString(GetDBString(reader, "Position")),
                };
                players.Add(player);
            }
            MyConnection.Close();
            return players;
        }
        public void InsertPlayer(string playerName, string teamID)
        {
            SqlCommand cmd = MyConnection.CreateCommand();
            cmd.CommandText = "INSERT INTO [Players]([Team] ,[LastName]) VALUES(@Team, @LastName)";
            cmd.Parameters.Add(new SqlParameter("@Team", teamID));
            cmd.Parameters.Add(new SqlParameter("@LastName", playerName));
            MyConnection.Open();
            cmd.ExecuteNonQuery();
            MyConnection.Close();
        }
        public void InsertPlayer(string playerName, string teamID, int FLID)
        {
            SqlCommand cmd = MyConnection.CreateCommand();
            cmd.CommandText = "INSERT INTO [Players]([Team] ,[LastName], [FLID]) VALUES(@Team, @LastName, @FLID)";
            cmd.Parameters.Add(new SqlParameter("@Team", teamID));
            cmd.Parameters.Add(new SqlParameter("@LastName", playerName));
            cmd.Parameters.AddWithValue("@FLID", FLID);
            MyConnection.Open();
            cmd.ExecuteNonQuery();
            MyConnection.Close();
        }
      
        public bool UpdatePlayer(Player P)
        {
            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "UPDATE Players SET Team = @Team, LastName = @LastName, Position = @Position, FLID = @FLID, Active = @Active, Price = @Price, FirstName = @FirstName, PhotoURL = @PhotoURL WHERE ID = @ID";

            cmd.Parameters.Add(new SqlParameter("@Team", P.TeamID));
            cmd.Parameters.Add(new SqlParameter("@LastName", P.PlayerName));
            cmd.Parameters.Add(new SqlParameter("@Position", System.Enum.GetName(typeof(Player.Positions), P.Position)));
            cmd.Parameters.Add(new SqlParameter("@FLID", P.FLID));
            cmd.Parameters.Add(new SqlParameter("@Active", P.Active ? 1 : 0));
            cmd.Parameters.Add(new SqlParameter("@Price", P.Price));
            cmd.Parameters.Add(new SqlParameter("@ID", P.ID));
            cmd.Parameters.AddWithValue("@FirstName", P.FirstName);
            cmd.Parameters.AddWithValue("@PhotoURL", P.PhotoURL);

            MyConnection.Open();
            int result = cmd.ExecuteNonQuery();
            MyConnection.Close();
            return result == 1;
        }



        public int GetTeamIDByName(String name)
        {

            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT Teams.ID FROM Teams WHERE Teams.DisplayName = @Name";
            cmd.Parameters.Add(new SqlParameter("@Name", name));

            MyConnection.Open();
            object returnVal = cmd.ExecuteScalar();
            MyConnection.Close();

            if (returnVal != null) return (int)returnVal;

            throw new Exception("Team not found");
        }
        public int GetTeamIDByInitials(String initials)
        {

            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT Teams.ID FROM Teams WHERE Teams.Initials = @Initials";
            cmd.Parameters.Add(new SqlParameter("@Initials", initials));

            MyConnection.Open();
            object returnVal = cmd.ExecuteScalar();
            MyConnection.Close();

            if (returnVal != null) return (int)returnVal;

            throw new Exception("Team not found");
        }
        public int GetGameIDByTeams(int Team, int season, int gameWeek, out int TeamID)
        {

            SqlCommand cmd = MyConnection.CreateCommand();

            cmd.CommandText = "SELECT Games.ID, AwayTeam, HomeTeam FROM Games WHERE (HomeTeam=@TeamID  OR AwayTeam = @TeamID) AND Season = @Season AND Games.GameWeek = @GameWeek";

            cmd.Parameters.Add(new SqlParameter("@TeamID", Team));
            cmd.Parameters.Add(new SqlParameter("@Season", season));
            cmd.Parameters.Add(new SqlParameter("@GameWeek", gameWeek));

            MyConnection.Open();
            SqlDataReader returnReader = cmd.ExecuteReader();
            returnReader.Read();
            int gameID = GetDBInt(returnReader, "ID");
            int AwayTeamID = GetDBInt(returnReader, "Awayteam");
            int HomeTeamID = GetDBInt(returnReader, "HomeTeam");

            MyConnection.Close();

            TeamID = (AwayTeamID == Team) ? HomeTeamID : AwayTeamID;
            if (gameID != null) return gameID;

            return 0;
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
    }
}
