using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;
using DaownaMp3Library;
namespace DaownaMp3
{
    public partial class Uploader : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ID"] == null) Response.Redirect("~/Account/Login.aspx");
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtArtist.Text) || string.IsNullOrEmpty(txtTrack.Text) || !uplTrack.HasFile) return;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["AndrewBlob"].ConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container. 
            CloudBlobContainer container = blobClient.GetContainerReference("mp3s");

            // Create the container if it doesn't already exist.
            container.CreateIfNotExists();

            container.SetPermissions(  new BlobContainerPermissions { PublicAccess =  BlobContainerPublicAccessType.Blob  });

            string trackName = txtArtist.Text + "-" + txtTrack.Text+".mp3";

            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(trackName);
            blockBlob.Properties.ContentType = "audio/mpeg";
            blockBlob.UploadFromStream(uplTrack.PostedFile.InputStream);

            //DataAccess.Instance.AddNewTrack(txtTrack.Text, txtArtist.Text, blockBlob.Uri, Session["ID"].ToString());
            Track uploadTrack = new Track(txtTrack.Text, txtArtist.Text, "", Convert.ToInt32(Session["ID"].ToString()), cbxShare.Checked, blockBlob.Uri.ToString());

            Response.Redirect("~/Member/Member.aspx");
        }
    }
}