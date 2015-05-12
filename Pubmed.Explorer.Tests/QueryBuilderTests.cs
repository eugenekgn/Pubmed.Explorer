using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PubMed.Explorer.Entities.Entities;
using PubMed.Explorer.Entities.Enums;
using PubMed.Explorer.QueryBuilder;

namespace Pubmed.Explorer.Tests
{
    [TestClass]
    public class QueryBuilderTests
    {
        [TestMethod]
        public void BulickQueryTest()
        {
            var queryBase = new QueryBuilder();

            var operand1 = new PubMedQeuryBlock
            {
                QueryType = QueryBlockTypes.Operand,
                Term = PubMedTerms.Author,
                SearchTerm = "Leffell",
            };

            var operand2 = new PubMedQeuryRangeBlock
            {
                QueryType = QueryBlockTypes.Operand,
                Term = PubMedTerms.DatePublication,
                StartDate = new DateTime(2014, 1, 1),
                EndDate = new DateTime(2015, 1, 1)
            };

            var operatorAnd = new PubMedQeuryOperatorBlock
            {
                QueryType = QueryBlockTypes.Operator,
                Operator = PubMedQueryOperator.AND
            };

            queryBase.AddToQuery(operand1);
            queryBase.AddToQuery(operatorAnd);
            queryBase.AddToQuery(operand2);


            var query = queryBase.ToString();

            Assert.IsNotNull(query);

        }
    }
}
