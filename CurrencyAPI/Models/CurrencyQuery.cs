using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyAPI.Models
{
    public class CurrencyQuery
    {
        public Dictionary<string,string> currencyCodes { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string apiKey { get; set; }
    }
}
