using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pubmed.Explorer.Tests.Helps;
using PubMed.Explorer.Entities.Entities;
using PubMed.Explorer.Entities.Enums;
using PubMed.Explorer.QueryBuilder;

namespace Pubmed.Explorer.Tests
{
    [TestClass]
    public class QueryBuilderTests
    {
        [TestMethod]
        public void BuildQueryTest()
        {
            var query = QueryGenerator.GetSimpleQuery();

            Assert.IsNotNull(query);
        }

    }
}
