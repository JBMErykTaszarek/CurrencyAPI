using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyAPI.DTOs
{
    public class SingleCurrencyDTO
    {
        public int Id { get; set; }
        public DateTime date { get; set; }
        public string rate { get; set; }
        
    }
}
