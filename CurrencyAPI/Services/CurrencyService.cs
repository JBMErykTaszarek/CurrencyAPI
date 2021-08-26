using CurrencyAPI.Models;
using CurrencyAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyAPI.Services
{
    public class CurrencyService : ICurrencyService
    {
        public CurrencyService ()
        {

        }

        public async Task<List<<SingleCurrencyDTO>> GetCurrency (CurrencyQuerry)
        {
            return //TODO
        }
        
    }
}
