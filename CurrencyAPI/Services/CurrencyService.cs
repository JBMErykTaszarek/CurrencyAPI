using CurrencyAPI.DTOs;
using CurrencyAPI.Models;
using CurrencyAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CurrencyAPI;
using System.Globalization;

namespace CurrencyAPI.Services
{
    public class CurrencyService : ICurrencyService
    {
        static readonly HttpClient client = new HttpClient();

        public CurrencyService ()
        {

        }

        public async Task<List<SingleCurrencyDTO>> GetCurrency(CurrencyQuery query)
        {
            HttpClient _client = new HttpClient();
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://sdw-wsrest.ecb.europa.eu/service/data/EXR/"),
                Timeout = new TimeSpan(0, 0, 0, 0, -1)
            };

            _client.DefaultRequestHeaders.Accept.Clear();
            var d = query.startDate.ToString("yyyy-mm-dd");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/csv"));
            var urlString = ($"D.{query.currencyCodes.FirstOrDefault().Key}.{query.currencyCodes.FirstOrDefault().Value}.SP00.A?startPeriod={query.startDate.ToString("yyyy-MM-dd")}&endPeriod={query.endDate.ToString("yyyy-MM-dd")}&detail=dataonly&dimensionAtObservation=AllDimensions");
            var response = await _client.GetAsync(urlString);
            //D.PLN.EUR.SP00.A?startPeriod=2009-05-12&endPeriod=2009-05-12&detail=dataonly&dimensionAtObservation=AllDimensions
            var responseContent = await response.Content.ReadAsStringAsync();

            return ResponseParser.ParseCSVToObjectList(responseContent); 
        }


    }
}
