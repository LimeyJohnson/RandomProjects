using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DaownaMp3Library;

namespace DaownaMp3.Member
{
    public partial class Member : System.Web.UI.Page
    {
        DaownaMp3Library.Member currentUser;
        Jukebox hero;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Retrieve login's user ID value
            if (Session["ID"] == null)
                Response.Redirect(@"~/Default.aspx");
            string readId = Session["ID"].ToString();
            int memberId = Convert.ToInt32(readId);


            //Redirect unwelcome guests
            if (memberId < 0)
                Response.Redirect(@"~/Default.aspx");

            //Create member class for user's info
            currentUser = new DaownaMp3Library.Member(memberId);
            string memberName = currentUser.UserName;
            string email = currentUser.Email;
            DateTime dateRegistered = currentUser.Date;

            //Should probably be done else where (jukebox)
            List<int> playlistIds = currentUser.MyPlaylistIds;
            string playlistString = string.Join(", ", playlistIds);
            List<int> trackIds = currentUser.MyTrackIds;
            string trackString = string.Join(", ", trackIds);

            //Display user's info
            txtGreet.Text = "Welcome " + memberName + ", to your playlist library!";
            txtEmail.Text = "Your registered e-mail address is:      " + email;
            txtDate.Text = "You started using this app on      " + dateRegistered;
            txtPlaylists.Text = "Your playlist Ids:      " + playlistString;
            txtTracks.Text = "Your track Ids:      " + trackString;

            //Fill user's playlist selection
            if (ddlPlaylistSelection.Items.Count == 0 || ddlPlaylistSelection.Items.Count <= currentUser.MyPlaylistIds.Count)
                ReloadLocalDropDownLists();
            if (lbxMyTracks.Items.Count != currentUser.MyTrackIds.Count)
                ReloadLocalListBoxTracks();

            //Fill in public playlists and shared tracks
            hero = new Jukebox(currentUser.ID);
            if (ddlPublicPlayList.Items.Count == 0 || ddlPublicPlayList.Items.Count <= (hero.PublicPlayListIds.Count + hero.MyPublicPlayListIds.Count + 1))
                ReloadPublicDropDownLists();
            if (lbxSharedTracks.Items.Count != hero.SharedTrackIds.Count)
                ReloadSharedListBoxTracks();

            //Session data retrieval for Jukebox
            if (Session["TrackId"] != null)
                hero.SelectedTrack = new Track(Convert.ToInt32(Session["TrackId"].ToString()));
            else
                hero.SelectedTrack = null;
            
            if (Session["SharedTrackId"] != null)
                hero.SelectedSharedTrack = new Track(Convert.ToInt32(Session["SharedTrackId"].ToString()));
            else
                hero.SelectedSharedTrack = null;

            if (Session["PlaylistId"] != null)
                hero.MySelectedPlayList = new PlayList(Convert.ToInt32(Session["PlaylistId"].ToString()));
            else
                hero.MySelectedPlayList = null;
        }

        protected void LogOut_Click(object sender, EventArgs e)
        {
            currentUser = new DaownaMp3Library.Member(-1);
            Session["ID"] = null;
            Response.Redirect(@"~/Default.aspx");
        }
        protected void ChangeEmail_Click(object sender, EventArgs e)
        {
            if (currentUser.ValidateEmail(txtChangeEmail.Text))
            {
                currentUser.Email = txtChangeEmail.Text;
                Response.Redirect(Request.RawUrl);
            }
            else
                Response.Write("<script>alert('Invalid e-mail address format.');</script>");
        }
        protected void ChangePassword_Click(object sender, EventArgs e)
        {
            if (txtNewPassword.Text == "" || txtOldPassword.Text == "")
            {
                Response.Write("<script>alert('Please give an input for both the new and old password');</script>");
            }
            else
            {
                if (currentUser.ChangePassword(txtOldPassword.Text, txtNewPassword.Text))
                    Response.Write("<script>alert('Your password has been changed.');</script>");
                else
                    Response.Write("<script>alert('Unable to change password.  Wrong password given.');</script>");

                txtOldPassword.Text = "";
                txtNewPassword.Text = "";
            }
        }
        protected void DeleteActivePlaylist_Click(object sender, EventArgs e)
        {
            int value = -1;
            string activePlaylistValue = ddlPlaylistSelection.SelectedValue;

            if (activePlaylistValue != "default")
            {
                value = Convert.ToInt32(activePlaylistValue);

                if (currentUser.RemoveFromPlaylistIds(value))
                {
                    ddlPlaylistSelection.Items.Clear();
                    ReloadLocalDropDownLists();
                    Response.Redirect(Request.RawUrl);
                }
                else
                {
                    //unable to delete value obtained
                }
            }
            else
            {
                //attempted to delete default value (not allowed)
            }
        }
        protected void CreatePlaylist_Click(object sender, EventArgs e)
        {
            //displays playlist creation controls
            lblNewPlaylistName.Visible = txtNewPlaylistName.Visible = lblMakeAsPublic.Visible = cbxMakeAsPublic.Visible = CreatePlaylistSubmitButton.Visible = true;
            NewPlaylistButton.Visible = false;
        }
        protected void CreatePlaylistSubmit_Click(object sender, EventArgs e)
        {
            if (currentUser.CreateUserPlaylist(txtNewPlaylistName.Text, cbxMakeAsPublic.Checked))
            {
                //hide playlist creation controls and reload
                lblNewPlaylistName.Visible = txtNewPlaylistName.Visible = lblMakeAsPublic.Visible = cbxMakeAsPublic.Visible = CreatePlaylistSubmitButton.Visible = false;
                NewPlaylistButton.Visible = true;
                ReloadLocalDropDownLists();
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                //duplicate playlist name error
            }
        }
        protected void ddlPlaylistSelection_Change(object sender, EventArgs e)
        {
            ReloadPlaylistTracksListBox();
        }
        protected void ddlPublicPlaylist_Change(object sender, EventArgs e)
        {
            PlayList selectedPublicList;
            string song, artist;

            //determines which list to read from for selected playlist
            if (ddlPublicPlayList.SelectedIndex > 0 && ddlPublicPlayList.SelectedValue != "default2")
            {
                if (ddlPublicPlayList.SelectedIndex <= hero.PublicPlayListIds.Count)
                    selectedPublicList = new PlayList((hero.PublicPlayListIds)[ddlPublicPlayList.SelectedIndex - 1]);
                else
                    selectedPublicList = new PlayList((hero.MyPublicPlayListIds)[ddlPublicPlayList.SelectedIndex - 2 - hero.PublicPlayListIds.Count]);

                selectedPublicList.PlayListSyncOrder();

                //populates listbox lbxPublicPlaylistTracks with tracks from the actively selected playlist                
                lbxPublicPlaylistTracks.Items.Clear();
                for (int i = 0; i < selectedPublicList.PlayListTrackIds.Count; i++)
                {
                    song = DataAccess.Instance.GetSongName(selectedPublicList.PlayListTrackIds[i]);
                    artist = DataAccess.Instance.GetArtist(selectedPublicList.PlayListTrackIds[i]);
                    lbxPublicPlaylistTracks.Items.Add(new ListItem(song + "          -  " + artist, i.ToString()));
                }
            }

        }
        public void ReloadPlaylistTracksListBox()
        {
            PlayList selectedPlaylist;
            string song, artist;

            if (ddlPlaylistSelection.SelectedIndex > 0)
            {
                selectedPlaylist = new PlayList((currentUser.MyPlaylistIds)[ddlPlaylistSelection.SelectedIndex - 1]);
                selectedPlaylist.PlayListSyncOrder();
                Session["PlaylistId"] = selectedPlaylist.PlaylistID;
                cbxEditPlaylistPublic.Checked = selectedPlaylist.IsPublic;

                //populates listbox lbPlaylistTracks with tracks from the actively selected playlist
                lbPlaylistTracks.Items.Clear();
                for (int i = 0; i < selectedPlaylist.PlayListTrackIds.Count; i++)
                {
                    song = DataAccess.Instance.GetSongName(selectedPlaylist.PlayListTrackIds[i]);
                    artist = DataAccess.Instance.GetArtist(selectedPlaylist.PlayListTrackIds[i]);
                    lbPlaylistTracks.Items.Add(new ListItem(song + "          -  " + artist, i.ToString()));
                }
            }
            else
                Session["PlaylistId"] = null;
        }
        protected void lbxMyTracks_SelectedChange(object sender, EventArgs e)
        {
            int trackId = currentUser.MyTrackIds[lbxMyTracks.SelectedIndex];
            hero.SelectedTrack = new Track(trackId);

            txtEditSongName.Text = hero.SelectedTrack.SongName;
            txtEditArtist.Text = hero.SelectedTrack.Artist;
            txtEditAlbum.Text = hero.SelectedTrack.Album;
            cbxEditShare.Checked = hero.SelectedTrack.Share;

            Session["TrackId"] = hero.SelectedTrack.TrackId;
        }
        protected void lbxSharedTracks_SelectedChange(object sender, EventArgs e)
        {
            int trackId = hero.SharedTrackIds[lbxSharedTracks.SelectedIndex];
            hero.SelectedSharedTrack = new Track(trackId);
            Session["SharedTrackId"] = hero.SelectedSharedTrack.TrackId;
        }
        protected void ChangeIsPlaylistPublic_Click(object sender, EventArgs e)
        {
            if (Session["PlaylistId"] != null)
            {
                hero.MySelectedPlayList.IsPublic = cbxEditPlaylistPublic.Checked;
                hero.ResetMySharedPlaylistIds();
                ReloadPublicDropDownLists();
            }
        }
        protected void RemoveTrackLocalList_Click(object sender, EventArgs e)
        {
            if (hero.MySelectedPlayList != null && lbPlaylistTracks.SelectedIndex >= 0)
            {
                int removeTrack = hero.MySelectedPlayList.PlayListTrackIds[lbPlaylistTracks.SelectedIndex];
                hero.MySelectedPlayList.RemoveTrack(removeTrack);
                ReloadPlaylistTracksListBox();
            }
            else
                Response.Write("<script>alert('No playlist or track selected.');</script>");
        }
        protected void ChangeOrderUp_Click(object sender, EventArgs e)
        {
            if (hero.MySelectedPlayList != null)
            {
                hero.MySelectedPlayList.PlayListSyncOrder();
                int selected = lbPlaylistTracks.SelectedIndex;
                int up, down;

                if (selected != 0)
                {
                    up = hero.MySelectedPlayList.PlayListTrackIds[selected];
                    down = hero.MySelectedPlayList.PlayListTrackIds[selected - 1];
                    hero.MySelectedPlayList.SwapTrackOrder(up, down);
                    ReloadPlaylistTracksListBox();
                    lbPlaylistTracks.SelectedIndex = selected - 1;
                }
            }
        }
        protected void ChangeOrderDown_Click(object sender, EventArgs e)
        {
            if (hero.MySelectedPlayList != null)
            {
                hero.MySelectedPlayList.PlayListSyncOrder();
                int selected = lbPlaylistTracks.SelectedIndex;
                int up, down;

                if (selected < hero.MySelectedPlayList.PlayListTrackIds.Count - 1)
                {
                    down = hero.MySelectedPlayList.PlayListTrackIds[selected];
                    up = hero.MySelectedPlayList.PlayListTrackIds[selected + 1];
                    hero.MySelectedPlayList.SwapTrackOrder(up, down);
                    ReloadPlaylistTracksListBox();
                    lbPlaylistTracks.SelectedIndex = selected + 1;
                }
            }

        }
        protected void AddTrackLocalList_Click(object sender, EventArgs e)
        {
            if (hero.MySelectedPlayList != null && hero.SelectedTrack != null)
            {
                int addTrack = currentUser.MyTrackIds[lbxMyTracks.SelectedIndex];
                if (hero.MySelectedPlayList.AddTrack(addTrack) == false)
                    Response.Write("<script>alert('Identical playlist track connection found.');</script>");
                else
                {
                    ReloadPlaylistTracksListBox();
                }
            }
            else
                Response.Write("<script>alert('No playlist or track selected.');</script>");
        }

        protected void AddSharedTrackLocalList_Click(object sender, EventArgs e)
        {
            if (hero.MySelectedPlayList != null && hero.SelectedSharedTrack != null)
            {
                int addTrack = hero.SharedTrackIds[lbxSharedTracks.SelectedIndex];
                if (hero.MySelectedPlayList.AddTrack(addTrack) == false)
                    Response.Write("<script>alert('Identical playlist track connection found.');</script>");
                else
                {
                    ReloadPlaylistTracksListBox();
                }
            }
            else
                Response.Write("<script>alert('No playlist or track selected.');</script>");
        }
        protected void SubmitChangesToTrack_Click(object sender, EventArgs e)
        {
            if (txtEditSongName.Text != "" && Session["TrackId"] != null)
            {
                hero.SelectedTrack.SongName = txtEditSongName.Text;
                hero.SelectedTrack.Artist = txtEditArtist.Text;
                hero.SelectedTrack.Album = txtEditAlbum.Text;
                hero.SelectedTrack.Share = cbxEditShare.Checked;

                txtEditSongName.Text = "";
                txtEditArtist.Text = "";
                txtEditAlbum.Text = "";
                cbxEditShare.Checked = false;

                Session["TrackId"] = null;
                ReloadLocalListBoxTracks();
            }
            else
                Response.Write("<script>alert('No track selected for editing or missing name.');</script>");
            
        }
        public void ReloadLocalDropDownLists()
        {
            ddlPlaylistSelection.Items.Clear();
            ddlPlaylistSelection.Items.Insert(0, new ListItem("--Select Playlist--", "default"));

            for (int i = 1; i <= currentUser.MyPlaylistIds.Count; i++)
                ddlPlaylistSelection.Items.Insert(i, new ListItem(DataAccess.Instance.GetPlayListName(currentUser.MyPlaylistIds[i - 1]),
                                                                    currentUser.MyPlaylistIds[i - 1].ToString()));
        }
        public void ReloadLocalListBoxTracks()
        {
            string artist, song;

            lbxMyTracks.Items.Clear();
            for (int i = 0; i < currentUser.MyTrackIds.Count; i++)
            {
                song = DataAccess.Instance.GetSongName(currentUser.MyTrackIds[i]);
                artist = DataAccess.Instance.GetArtist(currentUser.MyTrackIds[i]);
                lbxMyTracks.Items.Add(new ListItem(song + "          -  " + artist, i.ToString()));
            }
        }
        public void ReloadSharedListBoxTracks()
        {
            List<string> names = hero.SharedTrackNames(), artists = hero.SharedTrackArtists();

            lbxSharedTracks.Items.Clear();
            for (int i = 0; i < hero.SharedTrackIds.Count; i++)
            {
                lbxSharedTracks.Items.Add(new ListItem(names[i] + "          -  " + artists[i], i.ToString()));
            }

        }
        public void ReloadPublicDropDownLists()
        {
            ddlPublicPlayList.Items.Clear();
            ddlPublicPlayList.Items.Insert(0, new ListItem("--Public Playlists--", "default1"));

            for (int i = 1; i <= hero.PublicPlayListIds.Count; i++)
                ddlPublicPlayList.Items.Insert(i, new ListItem(DataAccess.Instance.GetPlayListName(hero.PublicPlayListIds[i - 1]),
                                                                    hero.PublicPlayListIds[i - 1].ToString()));

            ddlPublicPlayList.Items.Insert(hero.PublicPlayListIds.Count + 1, new ListItem("--Your Public Playlists--", "default2"));
            for (int i = 0; i < hero.MyPublicPlayListIds.Count; i++)
                ddlPublicPlayList.Items.Insert(i + hero.PublicPlayListIds.Count + 2, new ListItem(DataAccess.Instance.GetPlayListName(hero.MyPublicPlayListIds[i]),
                                                                    hero.MyPublicPlayListIds[i].ToString()));
        }
    }
}