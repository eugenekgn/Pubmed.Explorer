using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using Newtonsoft.Json;
using PubMed.Explorer.Api.Constants;
using PubMed.Explorer.Api.Enums;
using PubMed.Explorer.Api.Models;
using PubMed.Explorer.Utils;
using RestSharp.Portable;

namespace PubMed.Explorer.Api
{
    public class PubMedRequest
    {
        private readonly string databaseName;

        public PubMedRequest(EntrezDatabaseTypes dbType)
        {
            databaseName = dbType.ToString();
        }

        public PubMedPublicationIdsResult GetPublicationsIds(PubMedQueryFilter filter)
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

            var waitTime = PubMedThrottler.GetWaitTime();
            Thread.Sleep(waitTime);
            var response = restClient.Execute<PubMedResponse>(restRequest).Result;

            if (response.Data == null)
                throw new Exception("No Response From The Server");

            var result = new PubMedPublicationIdsResult();
            result.PubMedIdCollection = new List<string>();
            response.Data.esearchresult.idlist.ForEach(r => result.PubMedIdCollection.Add(r));

            return result;
        }

        public string GetPublicationSummaries(List<string> publicationIds)
        {
            var restClient = new RestClient(ServiceURLs.ESummaryBaseURL);

            var restRequest = new RestRequest();
            restRequest.AddParameter("db", databaseName, ParameterType.QueryString);
            restRequest.AddParameter("retmode", "json", ParameterType.QueryString);
            restRequest.AddParameter("rettype", "abstract", ParameterType.QueryString);
            restRequest.AddParameter("id", string.Join(", ", publicationIds.ToArray()), ParameterType.QueryString);
            restRequest.AddParameter("version", "2.0", ParameterType.QueryString);

            // Get the response.
            var waitTime = PubMedThrottler.GetWaitTime();
            Thread.Sleep(waitTime);

            //TODO: Fix mapping from json or xml;
            var response = restClient.Execute(restRequest).Result;


            return response.ToString();
        }
    }

    public class PubMedSummeryReponses
    {
        public PubMedResponseHeader Header { get; set; }
        public PubMedSummaryBody Result { get; set; }
    }


    public class PubMedSummaryBody
    {
        public List<int> Uids { get; set; }

        [JsonProperty("PublicationsSummeryInfo")]
        public Dictionary<int, PublicationsSummeryInfo> PublicationsSummeryInfo { get; set; }
    }


    public class PublicationsSummeryInfo
    {
        public int uid { get; set; }
        public int pubdate { get; set; }
        public string epubdate { get; set; }
        public string source { get; set; }
    }
}




