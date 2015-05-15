using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using PubMed.Explorer.Entities.Entities;
using PubMed.Explorer.Entities.Entities.interfaces;
using PubMed.Explorer.Entities.Enums;
using PubMed.Explorer.QueryBuilder.Maps;
using PubMed.Explorer.Utils;

namespace PubMed.Explorer.QueryBuilder
{
    /// <summary>
    /// This Object builds PubMed Query
    /// </summary>
    public class QueryBuilder
    {
        private PubMedQueryOperator currentOperator;

        private readonly List<IQueryBlock> queryBlocks;

        public QueryBuilder()
        {
            queryBlocks = new List<IQueryBlock>();
        }

        public void AddToQuery(IQueryBlock queryBlock)
        {
            queryBlocks.Add(queryBlock);
        }


        public override string ToString()
        {
            var buildQueryResult = new StringBuilder();

            foreach (var term in queryBlocks)
            {
                if (term is PubMedQeuryBlock)
                {
                    var queryBlock = buildQeuryBlock((term as PubMedQeuryBlock));
                    buildQueryResult = buildQueryResult.AppendFormat(" {0} ", queryBlock);
                }
                else if (term is PubMedQueryRangeBlock)
                {
                    var queryBlock = buildQeuryRangeBlock((term as PubMedQueryRangeBlock));
                    buildQueryResult = buildQueryResult.AppendFormat(" {0} ", queryBlock);
                }
                else if (term is PubMedQueryOperatorBlock)
                {
                    currentOperator = (term as PubMedQueryOperatorBlock).Operator;
                    buildQueryResult = buildQueryResult.AppendFormat(" {0} ", currentOperator);
                }
                else
                {
                    throw new Exception();
                }

            }
            return Wrap(buildQueryResult.ToString().Trim());
        }

        private string buildQeuryBlock(PubMedQeuryBlock queryBlock)
        {
            var queryTerm = TermToQeuryTermMap.GetQueryTerm(queryBlock.Term);
            return string.Format("({0}[{1}])", queryBlock.SearchTerm, queryTerm);
        }

        private string buildQeuryRangeBlock(PubMedQueryRangeBlock queryBlock)
        {
            var rangeBuilder = new StringBuilder();
  
            var queryTerm = TermToQeuryTermMap.GetQueryTerm(queryBlock.Term);
            rangeBuilder.AppendFormat("\"{0}\"[{1}]", PubMedDateOperations.DatetimeToPubMedDate(queryBlock.StartDate), queryTerm);
            rangeBuilder.Append(" : ");


            if (queryBlock.EndDate != DateTime.MinValue)
            {
                rangeBuilder.AppendFormat("\"{0}\"[{1}]", PubMedDateOperations.DatetimeToPubMedDate(queryBlock.StartDate), queryTerm);
            }
            else
            {
                rangeBuilder.AppendFormat("\"{0}\"[{1}]", PubMedDateOperations.DatetimeToPubMedDate(queryBlock.StartDate), queryTerm);
            }

            return Wrap(rangeBuilder.ToString());

        }

        private string Wrap(string expression)
        {
            return string.Format("({0})", expression);
        }
    }
}
