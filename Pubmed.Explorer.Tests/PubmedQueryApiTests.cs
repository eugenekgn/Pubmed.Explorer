using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using NUnit.Framework;
using Pubmed.Explorer.Tests.Helps;
using PubMed.Explorer.Api;
using PubMed.Explorer.Api.Enums;

namespace Pubmed.Explorer.Tests
{
    [SuppressMessage("ReSharper", "LoopCanBePartlyConvertedToQuery")]
    public class PubmedQueryApiTests
    {
        [Test]
        public void SingleQueryTest()
        {
            var filter = new PubMedQueryFilter
             {
                 Skip = 0,
                 Take = 50,
                 Query = QueryGenerator.GetSimpleQuery()
             };

            // Get Query Ids 
            var pubmedRequest = new PubMedRequest(EntrezDatabaseTypes.pubmed);

            // Task1
            var pubMedQueryResult = pubmedRequest.GetPublicationsIds(filter);

            // Task2
            var result = pubmedRequest.GetPublicationSummaries(pubMedQueryResult.PubMedIdCollection);
        }

        [Test]
        public void MultipleParallelQueriesApiTest()
        {
            const int simultaneousUsers = 3;

            ThreadPool.SetMaxThreads(simultaneousUsers, simultaneousUsers);
            ThreadPool.SetMinThreads(simultaneousUsers, simultaneousUsers);

            var currentJobs = 0;
            var processingStarted = 0;
            for (var idx = 0; idx < 5; idx++)
            {
                ThreadPool.QueueUserWorkItem(state =>
                {
                    Interlocked.Increment(ref processingStarted);
                    Interlocked.Increment(ref currentJobs);
                    try
                    {
                        UserMakesCalls(5);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                    }
                    Interlocked.Decrement(ref currentJobs);
                });
            }
            while (currentJobs != 0 || processingStarted == 0)
            {
                Thread.Sleep(100);
            }
        }


        public void UserMakesCalls(int times)
        {
            for (var i = 0; i < times; i++)
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
                Debug.WriteLine(Ids);
                Debug.WriteLine(result);
            }
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