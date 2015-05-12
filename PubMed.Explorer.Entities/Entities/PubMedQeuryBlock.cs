using PubMed.Explorer.Entities.Entities.interfaces;
using PubMed.Explorer.Entities.Enums;

namespace PubMed.Explorer.Entities.Entities
{
    public class PubMedQeuryBlock : IQueryBlock
    {
        public PubMedTerms Term { get; set; }
        public string SearchTerm { get; set; }
        public QueryBlockTypes QueryType { set; get; }
    }
}
