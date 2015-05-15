using System;
using System.Threading;
using NUnit.Framework;
using PubMed.Explorer.Api;

namespace Pubmed.Explorer.Tests
{
    public class ThrottlerTest
    {
        [Test]
        public void ThrottlerBasicTest()
        {

            var numOfRequests = 200;

            for (var i = 0; i < numOfRequests; i++)
            {

                Console.WriteLine("unthrottled request");

                var waitTime = PubMedThrottler.GetWaitTime();

                if (waitTime == TimeSpan.Zero)
                {
                    Console.WriteLine("throttled request, {0}", waitTime);
                }
                else
                {
                    Thread.Sleep(waitTime);
                    Console.WriteLine("throttled request, {0}", waitTime);

                }
            }



        }
    }
}
