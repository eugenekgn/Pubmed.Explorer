using System;
using System.Collections.Generic;
using System.Linq;
using PubMed.Explorer.Entities.Entities;
using PubMed.Explorer.Entities.Enums;
using PubMed.Explorer.QueryBuilder;

namespace Pubmed.Explorer.Tests.Helps
{
    public static class QueryGenerator
    {
        public static string GetSimpleQuery(string doctorName = "Leffell")
        {
            var queryBase = new QueryBuilder();

            var operand1 = new PubMedQeuryBlock
            {
                QueryType = QueryBlockTypes.Operand,
                Term = PubMedTerms.Author,
                SearchTerm = doctorName,
            };

            var operand2 = new PubMedQueryRangeBlock
            {
                QueryType = QueryBlockTypes.Operand,
                Term = PubMedTerms.DatePublication,
                StartDate = new DateTime(2000, 1, 1),
                EndDate = new DateTime(2015, 1, 1)
            };

            var operatorAnd = new PubMedQueryOperatorBlock
            {
                QueryType = QueryBlockTypes.Operator,
                Operator = PubMedQueryOperator.AND
            };

            queryBase.AddToQuery(operand1);
            queryBase.AddToQuery(operatorAnd);
            queryBase.AddToQuery(operand2);

            return queryBase.ToString();
        }


        public static string GetSimpleQueryRandomDoctrNames()
        {
            var count = GetDoactorNames().Count() - 1;
            var random = new Random();
            var randomNumber = random.Next(0, count);

            var queryBase = new QueryBuilder();

            var operand1 = new PubMedQeuryBlock
            {
                QueryType = QueryBlockTypes.Operand,
                Term = PubMedTerms.Author,
                SearchTerm = GetDoactorNames().ElementAt(randomNumber),
            };

            var operand2 = new PubMedQueryRangeBlock
            {
                QueryType = QueryBlockTypes.Operand,
                Term = PubMedTerms.DatePublication,
                StartDate = new DateTime(2000, 1, 1),
                EndDate = new DateTime(2015, 1, 1)
            };

            var operatorAnd = new PubMedQueryOperatorBlock
            {
                QueryType = QueryBlockTypes.Operator,
                Operator = PubMedQueryOperator.AND
            };

            queryBase.AddToQuery(operand1);
            queryBase.AddToQuery(operatorAnd);
            queryBase.AddToQuery(operand2);

            return queryBase.ToString();

        }


        public static List<string> GetDoactorNames()
        {
            return new List<string>
            {
                "Ackerman",
                "Torres",
                "Beard",
                "Bian",
                "Booth",
                "Brown",
                "Cerrone",
                "Cheng",
                "Cohen",
                "Esterlis",
                "Desir",
                "Fertig",
                "Ford",
                "Gan",
                "Giovanelli",
                "Grilo",
                "Halene",
                "Heinz",
                "Kadan-Lottick",
                "Kluger",
                "Krumholz",
                "Landrette",
                "Lengyel",
                "Liptak",
                "Maccarelli",
                "Mosaheb",
                "Gandotra",
                "Ohene-Adjei",
                "Patel",
                "Pirruccello",
                "Prudic",
                "Safriel",
                "Saunders",
                "Selemon",
                "Silva",
                "Steele",
                "Svejda",
                "Tufro",
                 "Vega",
                "Wang",
                "Whang",
                "Xiang",
                "Zaha",
                "Whang",
                "Wheeler",
                "Wheeler",
                "Whitaker"
 
                

            };
        }
    }
}