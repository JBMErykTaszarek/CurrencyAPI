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

namespace CurrencyAPI.Services
{
    public class CurrencyService : ICurrencyService
    {
        static readonly HttpClient client = new HttpClient();

        public CurrencyService ()
        {

        }

        public async Task<List<SingleCurrencyDTO>> GetCurrency()
        {
            HttpClient _client = new HttpClient();
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://sdw-wsrest.ecb.europa.eu/service/data/EXR/"),
                Timeout = new TimeSpan(0, 0, 0, 0, -1)
            };

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                                 new MediaTypeWithQualityHeaderValue("text/csv"));
            var kk= await _client.GetAsync("D.PLN.EUR.SP00.A?startPeriod=2009-05-12&endPeriod=2009-05-12&detail=dataonly&dimensionAtObservation=AllDimensions");
            var d = kk.ToString();
            ResponseParser.ParseCSVToObjectList(d);

            return new List<SingleCurrencyDTO> { };
        }


    }
}
