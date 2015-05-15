using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubMed.Explorer.Api.Models
{
    internal class PubMedResponse
    {
        public PubMedResponseHeader Header { get; set; }

        public PubMedResponseBody esearchresult { get; set; }

        
    }
}
