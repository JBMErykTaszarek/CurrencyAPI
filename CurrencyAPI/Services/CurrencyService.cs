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
using CurrencyAPI.DataBase;
using CurrencyAPI.Helpers;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using CurrencyAPI.Logging;

namespace CurrencyAPI.Services
{
    public class CurrencyService : ICurrencyService
    {
        static readonly HttpClient client = new HttpClient();
        private readonly AppDbContext _context;

        public CurrencyService (AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SingleCurrencyDTO>> GetCurrency(CurrencyQuery query)

        {
            var validator = new RequestValidator();
            var validarotResault = validator.Validate(query);
            if (validarotResault.IsValid)
            {
                if (_context.CurrencyRates.Any(c => c.date == query.startDate) && _context.CurrencyRates.Any(c => c.date == query.endDate))
                {
                    var date = query.startDate;
                    var daysLenght = 1;
                    for (int i = 0; i < (query.endDate - query.startDate).TotalDays; i++)
                    {
                        if (date.DayOfWeek.ToString() != DayOfWeek.Saturday.ToString() && date.DayOfWeek.ToString() != DayOfWeek.Sunday.ToString())
                        {
                            daysLenght++;
                        }
                        date = date.AddDays(1);
                    }

                    if (daysLenght == _context.CurrencyRates.Where(cr => cr.date >= query.startDate && cr.date <= query.endDate).Count())
                    {
                        List<SingleCurrencyDTO> fromCache = new List<SingleCurrencyDTO>();
                        var ratesFromCache = _context.CurrencyRates.ToList().Where(cr => cr.date >= query.startDate && cr.date <= query.endDate).OrderBy(cr => cr.date);
                        foreach (var item in ratesFromCache)
                        {
                            fromCache.Add(item);
                        }

                        Logger.LogAction("Action GetCurrency was successfully called and gets data from cache");
                        return fromCache;
                    }


                }

                HttpClient _client = new HttpClient();
                _client = new HttpClient
                {
                    BaseAddress = new Uri("https://sdw-wsrest.ecb.europa.eu/service/data/EXR/"),
                    Timeout = new TimeSpan(0, 0, 0, 0, -1)
                };

                if (!(_context.AuthorizationKey.Any(x => x.Hash == query.apiKey)))
                {
                    throw new Exception("Bad api key");
                }

                

                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/csv"));

                var response = await _client.GetAsync(GetURLString(query));
                var responseContent = await response.Content.ReadAsStringAsync();
                while (responseContent == "")
                {
                    var newQuery = query;
                    newQuery.startDate = query.startDate.AddDays(-1);
                    GetURLString(newQuery);
                    response = await _client.GetAsync(GetURLString(query));
                    responseContent = await response.Content.ReadAsStringAsync();
                }
                var parsedResponse = ResponseParser.ParseCSVToObjectList(responseContent);
                var currentRates = _context.CurrencyRates.AsNoTracking();
                foreach (var item in parsedResponse)
                {
                    if (!(currentRates.Any(c => c.date == item.date)))
                    {
                        _context.CurrencyRates.Add(new SingleCurrencyDTO() { date = item.date, rate = item.rate });
                    }
                }
               
                _context.SaveChanges();

                Logger.LogAction("Action GetCurrency was successfully called and gets data from ECB API");
                return parsedResponse;
                
            }
            else
            {
                Logger.LogAction($"Action GetCurrency throwed error. Reason: {validarotResault.ToString()}");
                throw new Exception("to to");
            }
            
        



        }

        public string GetURLString(CurrencyQuery query)
        {
            var urlString = ($"D.{query.currencyCodes.FirstOrDefault().Key}.{query.currencyCodes.FirstOrDefault().Value}.SP00.A?startPeriod={query.startDate.ToString("yyyy-MM-dd")}&endPeriod={query.endDate.ToString("yyyy-MM-dd")}&detail=dataonly&dimensionAtObservation=AllDimensions");

            return urlString;

        }


    }
}
