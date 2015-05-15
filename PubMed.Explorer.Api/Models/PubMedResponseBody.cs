using System.Collections.Generic;

namespace PubMed.Explorer.Api.Models
{
    public class PubMedResponseBody
    {
        public string count { get; set; }
        public string retmax { get; set; }
        public string retstart { get; set; }
        public List<string> idlist { get; set; }
        public List<Translationset> translationset { get; set; }
        public string querytranslation { get; set; }
    }
}
