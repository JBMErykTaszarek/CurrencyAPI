using CurrencyAPI;
using CurrencyAPI.Models;
using CurrencyAPI.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyApiTests
{
    public class TestBase
    {
        protected private HttpClient _client { get; set; }
        protected readonly ICurrencyService _currencyService;

        public TestBase()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44399/"),
                Timeout = new TimeSpan(0, 0, 0, 0, -1)
            };
        }

        public CurrencyQuery createContent(string firstCurrency, string secondCurrency, string startDate, string endDate)
        {
            var queryFromData = new CurrencyQuery()
            {
                currencyCodes = new Dictionary<string, string>
            {
                { firstCurrency, secondCurrency} },
                startDate = Convert.ToDateTime(startDate),
                endDate = Convert.ToDateTime(endDate),
                apiKey = "1dNE12qt2M9RSGzIWgaeqOYXoladM3GbVjXychLnR9w="
            };


            return queryFromData;

        }


    }
}
