using PubMed.Explorer.Entities.Enums;

namespace PubMed.Explorer.Entities.Entities.interfaces
{
    public interface IQueryBlock
    {
        QueryBlockTypes QueryType { get; set; }
    }
}
