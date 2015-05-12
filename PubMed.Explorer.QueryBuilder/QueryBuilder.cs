using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using PubMed.Explorer.Entities.Entities;
using PubMed.Explorer.Entities.Entities.interfaces;
using PubMed.Explorer.Entities.Enums;
using PubMed.Explorer.QueryBuilder.Maps;

namespace PubMed.Explorer.QueryBuilder
{
    /// <summary>
    /// This Object builds PubMed Query
    /// </summary>
    public class QueryBuilder
    {
        public const string DateFroamt = "yyyy/MM/dd";
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
                else if (term is PubMedQeuryRangeBlock)
                {
                    var queryBlock = buildQeuryRangeBlock((term as PubMedQeuryRangeBlock));
                    buildQueryResult = buildQueryResult.AppendFormat(" {0} ", queryBlock);
                }
                else if (term is PubMedQeuryOperatorBlock)
                {
                    currentOperator = (term as PubMedQeuryOperatorBlock).Operator;
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

        private string buildQeuryRangeBlock(PubMedQeuryRangeBlock queryBlock)
        {
            var rangeBuilder = new StringBuilder();
            rangeBuilder.Append("(");
            var queryTerm = TermToQeuryTermMap.GetQueryTerm(queryBlock.Term);
            rangeBuilder.AppendFormat("\"{0}\"[{1}]", queryBlock.StartDate.ToString(DateFroamt), queryTerm);
            rangeBuilder.Append(" : ");


            if (queryBlock.EndDate != DateTime.MinValue)
            {
                rangeBuilder.AppendFormat("\"{0}\"[{1}]", queryBlock.EndDate.ToString(DateFroamt), queryTerm);
            }
            else
            {
                rangeBuilder.AppendFormat("\"{0}\"[{1}]", queryBlock.EndDate.ToString(DateFroamt), queryTerm);
            }

            return Wrap(rangeBuilder.ToString());

        }

        private string Wrap(string expression)
        {
            return string.Format("({0})", expression);
        }
    }
}
