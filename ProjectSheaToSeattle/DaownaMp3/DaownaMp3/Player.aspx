<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Player.aspx.cs" Inherits="DaownaMp3.Player" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.8.2.js"></script>
    <script src="Scripts/Speakker/speakker-big-1.2.32r284.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var player;
            player = projekktor('.projekktor', { enableFlashFallback: false, autoplay:true });
            //Set the starting track if it is specified
            //var songIndex = parseInt(document.URL.split('#')[1]);
            //if (songIndex && songIndex > 0) {
            //    player.setActiveItem(songIndex - 1);
            //    player.setPlay();
            //}
            ////Listen to the start event which means that a new track is loaded. Update the URL to reflect the new track
            //player.addListener('start', function (a) {
            //    var songID = player.getItemIdx() + 1;
            //    var newURL = location.href.split("#")[0] + "#" + songID;
            //    if (history.pushState) history.pushState({}, document.title, newURL);
            //});
            setPlayList('PlayListService.svc/GetActiveTracks');
            function setPlayList(url)
            {
                $.getJSON(url, function (data) {
                    var playlist = [];
                    $.each(data.d, function (i, song) {
                        var songObject = { 0: { "src": song.BlobURL, "type": "audio/mp3" }, "config": { "title": song.Artist + " - " + song.SongName } }
                        playlist.push(songObject);
                    });
                    player.setFile(playlist);
                }).fail(function (jqxhr, textStatus, error) {
                    var err = textStatus + ', ' + error;
                    console.log("Request Failed: " + err);
                });
            }
            $('#everyone').click(function (a) { setPlayList("PlayListService.svc/GetActiveTracks") });
            $('#useronly').click(function (a) { setPlayList("PlayListService.svc/GetActiveTracksByUser") });
            $.getJSON('PlayListService.svc/GetAllPlaylists',function (data)
            {
                $.each(data.d, function (i, pl) {
                    $('#playlists').append("<input type='button' id='pl"+ pl.ID+"' value='"+pl.Name+"'>");
                });
                $("input[id^='pl']").click(handlePLClick);
            }).fail(function (jqxhr, textStatus, error) {
                var err = textStatus + ', ' + error;
                console.log("Request Failed: " + err);
            });
            function handlePLClick(eventargs)
            {
                var id = eventargs.target.id.slice(2);
                setPlayList('PlayListService.svc/GetPlaylistTracks?ID=' + id);
            }
        });
    </script>
</head>
<body>
    <input  type="button" id="everyone" value="Everyones tracks"/>
    <input  type="button" id="useronly" value="Just Yours"/>
    <div id="playlists">

    </div>
    <audio class="projekktor speakker dark">
    </audio>
</body>
</html>

