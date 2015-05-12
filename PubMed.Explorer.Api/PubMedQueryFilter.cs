using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PubMed.Explorer.Api
{
    public class PubMedQueryFilter
    {
        public int Take { get; set; }
        public string Query { get; set; }

        public int Skip { get; set; }

        /// <summary>
        /// http://www.ncbi.nlm.nih.gov/books/NBK25499/
        /// When reldate is set to an integer n, the search returns only those items that have a date specified by datetype within the last n days.
        /// </summary>
        public DateTime RelDate { get; set; }
    }
}
