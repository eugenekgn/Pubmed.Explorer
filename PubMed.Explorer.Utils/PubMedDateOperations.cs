using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubMed.Explorer.Utils
{
    public static class PubMedDateOperations
    {
        public const string DateFroamt = "yyyy/MM/dd";

        public static string DatetimeToPubMedDate(DateTime date)
        {
            return date.ToString(DateFroamt);
        }

    }
}
