﻿using PubMed.Explorer.Entities.Entities.interfaces;
using PubMed.Explorer.Entities.Enums;

namespace PubMed.Explorer.Entities.Entities 
{
    public class PubMedQueryOperatorBlock : IQueryBlock
    {
        public QueryBlockTypes QueryType { get; set; }
        public PubMedQueryOperator Operator { get; set; }
    }
}
