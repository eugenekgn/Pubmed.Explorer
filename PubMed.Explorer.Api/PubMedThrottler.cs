using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace PubMed.Explorer.Api
{

    public static class PubMedThrottler
    {
        private static readonly List<DateTime> JobQueue = new List<DateTime>();
        private const int maxRequestsPerSecond = 3;

        public static TimeSpan GetWaitTime()
        {
            var currTime = DateTime.UtcNow;
            var waitTime = new TimeSpan(0);

            lock (JobQueue)
            {
                // Get Rid of everything at least one second before
                while (JobQueue.Count > 0 && (currTime.AddSeconds(-1) > JobQueue.First()))
                {
                    JobQueue.RemoveAt(0);
                }

                if (JobQueue.Count > maxRequestsPerSecond)
                {
                    waitTime = (JobQueue[JobQueue.Count() - maxRequestsPerSecond].AddSeconds(1)).Subtract(currTime);
                }

                waitTime = new TimeSpan(waitTime.Ticks);


                JobQueue.Add(currTime + waitTime);
            }


            return waitTime;
        }
    }
}
