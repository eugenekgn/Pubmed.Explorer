using System.Threading.Tasks;
using RestSharp;

namespace PubMed.Explorer.Api
{
    public class PubMedRequest
    {
        public async Task<PubMedSearchResults> Submit(PubMedQueryFilter filter)
        {
            var restClient = new RestClient(ServiceURLs.ESearchBaseURL);

            var restRequest = new RestRequest();
            //restRequest.AddParameter("db", searchProperties.Database.ValidEntrezName.ToLower(), ParameterType.QueryString);
            //restRequest.AddParameter("retmode", "json", ParameterType.QueryString);
            //restRequest.AddParameter("retstart", searchProperties.StartIndex, ParameterType.QueryString);
            //restRequest.AddParameter("retmax", searchProperties.MaximumResults, ParameterType.QueryString);
            //if (searchProperties.RelDate != null)
            //{
            //    restRequest.AddParameter("reldate", searchProperties.RelDate, ParameterType.QueryString);
            //}

            return null;
        }
    }
}
