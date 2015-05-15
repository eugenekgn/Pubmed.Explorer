using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pubmed.Explorer.Tests.Helps;
using PubMed.Explorer.Api;
using PubMed.Explorer.Api.Enums;

namespace Pubmed.Explorer.Tests
{
    [TestClass]
    [SuppressMessage("ReSharper", "LoopCanBePartlyConvertedToQuery")]
    public class PubmedQueryApiTests
    {
        [TestMethod]
        public void QueryApiTest()
        {

            var filter = new PubMedQueryFilter
            {
                Skip = 0,
                Take = 50,
                Query = QueryGenerator.GetSimpleQueryRandomDoctrNames()
            };

            // Get Query Ids 
            var pubmedRequest = new PubMedRequest(EntrezDatabaseTypes.pubmed);

            // Task1
            var pubMedQueryResult = pubmedRequest.GetPublicationsIds(filter);

            // Task2
            var result = pubmedRequest.GetPublicationSummaries(pubMedQueryResult.PubMedIdCollection);

            var Ids = string.Join(", ", pubMedQueryResult.PubMedIdCollection);
            Console.WriteLine(Ids);
            Console.WriteLine(result);
        }
    }
}




/*
 *           PubMed.Api               
 *  [web] => [(getIds),(summaries)]
 *  
 * 
 * 
 */




//const int max = 50;
//ThreadPool.SetMaxThreads(max, max);
//ThreadPool.SetMinThreads(max, max);
//var currentJobs = 0;
//var processingStarted = 0;
//for (var idx = 0; idx < 10; idx++)
//{
//    ThreadPool.QueueUserWorkItem(state =>
//    {
//        Interlocked.Increment(ref processingStarted);
//        Interlocked.Increment(ref currentJobs);
//        try
//        {

//            var filter = new PubMedQueryFilter
//            {
//                Skip = 0,
//                Take = 50,
//                Query = QueryGenerator.GetSimpleQueryRandomDoctrNames()
//            };

//            // Get Query Ids 
//            var pubmedRequest = new PubMedRequest(EntrezDatabaseTypes.pubmed);

//            // Task1
//            var pubMedQueryResult = pubmedRequest.GetPublicationsIds(filter);

//            // Task2
//            var result = pubmedRequest.GetPublicationSummaries(pubMedQueryResult.PubMedIdCollection);

//            var Ids = string.Join(", ", pubMedQueryResult.PubMedIdCollection);
//            Console.WriteLine(Ids);

//            var resSummery = string.Join(Environment.NewLine, Regex.Split(result, "<Title>.*?</Title>", RegexOptions.Multiline));
//            Console.WriteLine();

//        } 
//        catch (Exception ex)
//        {

//        }
//        Interlocked.Decrement(ref currentJobs);
//    });
//}
//while (currentJobs != 0 || processingStarted == 0)
//{
//    Thread.Sleep(100);
//}