using CurrencyAPI.DTOs;
using CurrencyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyAPI.Services.Interfaces
{
    interface ICurrencyService
    {
        public Task<List<SingleCurrencyDTO>> GetCurrency ();
    }
}
