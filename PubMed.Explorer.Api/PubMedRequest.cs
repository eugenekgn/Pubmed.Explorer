using System;
using System.Threading.Tasks;
using PubMed.Explorer.Api.Enums;
using PubMed.Explorer.Api.Models;
using PubMed.Explorer.Utils;
using RestSharp;

namespace PubMed.Explorer.Api
{
    public class PubMedRequest
    {
        private readonly string databaseName;

        public PubMedRequest(EntrezDatabaseTypes dbType)
        {
            databaseName = dbType.ToString();
        }
        public async Task<PubMedResponse> Submit(PubMedQueryFilter filter)
        {
            var restClient = new RestClient(ServiceURLs.ESearchBaseURL);

            var restRequest = new RestRequest();
            restRequest.AddParameter("db", databaseName, ParameterType.QueryString);
            restRequest.AddParameter("retmode", "json", ParameterType.QueryString);
            restRequest.AddParameter("retstart", (filter.Skip * filter.Take), ParameterType.QueryString);
            restRequest.AddParameter("term", filter.Query, ParameterType.QueryString);
            restRequest.AddParameter("retmax", filter.Take, ParameterType.QueryString);
            if (filter.RelDate != DateTime.MinValue)
            {
                var pmDate = PubMedDateOperations.DatetimeToPubMedDate(filter.RelDate);
                restRequest.AddParameter("reldate", pmDate, ParameterType.QueryString);
            }


            var response = new PubMedResponse*


            return null;
        }
    }
}
