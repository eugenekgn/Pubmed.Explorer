using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PubMed.Explorer.Entities.Entities.interfaces;
using PubMed.Explorer.Entities.Enums;

namespace PubMed.Explorer.Entities.Entities 
{
    public class PubMedQueryRangeBlock : IQueryBlock
    {
        public QueryBlockTypes QueryType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public PubMedTerms Term { get; set; }
    }
}
