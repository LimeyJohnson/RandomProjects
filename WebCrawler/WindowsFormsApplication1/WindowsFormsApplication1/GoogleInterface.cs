using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.API.Search; 
namespace WindowsFormsApplication1
{
    class GoogleInterface
    {
        
       public IList<IWebResult> getWebQueryResults(String query)
       {
           GwebSearchClient client = new GwebSearchClient("http://hopepointe.org");
           IList<IWebResult> results = client.Search(query, 32);  
           return results;
       
       }
       public IList<INewsResult> getNewsQueryResults(String query)
       {
           GnewsSearchClient client = new GnewsSearchClient("http://hopepointe.org");
           IList<INewsResult> result = client.Search(query, 32);
           return result;
       }

       public IList<INewsResult> getTopicQueryResults(String query)
       {
           GnewsSearchClient client = new GnewsSearchClient("http://hopepointe.org");

           IList<INewsResult> result = client.Search(query, 32, null, SortType.Relevance, null, null, NewsEdition.UnitedStates);
           //keyword, resultCount, geo, sortBy, quoteId, topic, edition
           //SortType.Relevance
           //NewsTopic.World
           
           return result;
       }
    }
   

}
