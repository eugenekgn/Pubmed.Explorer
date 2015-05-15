using System.Collections.Generic;

namespace PubMed.Explorer.Api
{
    public class PubMedPublicationEntry
    {
        List<PubMedPublicationEntry> Publications { get; set; }

        public PubMedPublicationEntry()
        {
            Publications = new List<PubMedPublicationEntry>();
        }
    }
}
