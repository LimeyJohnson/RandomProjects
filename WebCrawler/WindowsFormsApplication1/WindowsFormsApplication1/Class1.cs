using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
namespace WindowsFormsApplication1
{
    interface DataAccess
    {

        int InsertArticle(String searchString, String catagory, String clusterURL, int related, String title, String source, String author, String pubDate, String URL, String HTML, String ArticleText);
        int DoSomething();


    }

    class AccessDataAccess : DataAccess
    {
        SqlConnection connection;
        SqlCommand cmd;
        AccessDataAccess()
        {
            connection = new SqlConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + System.IO.Directory.GetCurrentDirectory() + "\\CrawlDB.accdb");
            cmd.Connection = new SqlCommand("", connection);
        }

        int InsertArticle(String searchString, String catagory, String clusterURL, int related, String title, String source, String author, String pubDate, String URL, String HTML, String ArticleText)
        {
            cmd.Parameters.Add(new SqlParameter("@searchString", searchString));
            cmd.Parameters.Add(new SqlParameter("@catagory", catagory));
            cmd.Parameters.Add(new SqlParameter("@clusterURL", clusterURL));
            cmd.Parameters.Add(new SqlParameter("@related", related));
            cmd.Parameters.Add(new SqlParameter("@title", title));
            cmd.Parameters.Add(new SqlParameter("@source", source));
            cmd.Parameters.Add(new SqlParameter("@author", author));
            cmd.Parameters.Add(new SqlParameter("@pubDate", pubDate));
            cmd.Parameters.Add(new SqlParameter("@URL", URL));
            cmd.Parameters.Add(new SqlParameter("@HTML", HTML));
            cmd.Parameters.Add(new SqlParameter("@ArticleText", ArticleText));

            cmd.CommandText= "INSERT INTO Articles(SearchString, TimeStamp, Catagory, ClusterURL, Related, Title, Source, Author, PubDate, URL, HTML, ArticleText) VALUES(@SearchString, NOW(), @Catagory, @ClusterURL, @Related, @Title, @Source, @Author, @PubDate, @URL, @HTML, @ArticleText)"
            try
            {
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch(Exception e)
            {
                return 0;
            }
            return 1;

        }



    }
    class SqlDataAccess : DataAccess
    {


    }
}
