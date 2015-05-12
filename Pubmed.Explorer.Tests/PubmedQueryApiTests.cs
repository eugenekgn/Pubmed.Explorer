using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PubMed.Explorer.Api;
using PubMed.Explorer.Entities.Entities;
using PubMed.Explorer.Entities.Enums;
using PubMed.Explorer.QueryBuilder;

namespace Pubmed.Explorer.Tests
{
    [TestClass]
    public class PubmedQueryApiTests
    {
        [TestMethod]
        public async void BulickQueryTest()
        {

            var filter = new PubMedQueryFilter();;



            var pubmedRequest = new PubMedRequest();
            
            var searchResults = await pubmedRequest.Submit(filter);



        }
    }
}
