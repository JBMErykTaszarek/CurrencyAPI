using CurrencyAPI.DTOs;
using CurrencyAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Text.Json;
using System.Net.Http;
using FluentAssertions;
using System.Net;
using System.Linq;

namespace CurrencyApiTests
{
    public class CurrencyTests : TestBase
    {
        [Fact]
        public async Task GetCurrencyFromOneDayShouldReturnOneRate()
        {
            var serializedQueryObject = JsonConvert.SerializeObject(createContent("PLN", "EUR", "2020-09-01", "2020-09-01"));
            var response = await _client.PostAsync("/GetCurrencyRates", new StringContent(serializedQueryObject, Encoding.UTF8, "application/json"));
            var SerializedResponseBody = await response.Content.ReadAsStringAsync();
            var responseBody = JsonConvert.DeserializeObject<List<SingleCurrencyDTO>>(SerializedResponseBody);

            responseBody.Count.Should().Be(1);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

        }

        [Fact]
        public async Task GetCurrencyFromOneWeekShouldReturnFiveRate()
        {
            var serializedQueryObject = JsonConvert.SerializeObject(createContent("PLN", "EUR", "2020-08-30", "2020-09-05"));
            var response = await _client.PostAsync("/GetCurrencyRates", new StringContent(serializedQueryObject, Encoding.UTF8, "application/json"));
            var SerializedResponseBody = await response.Content.ReadAsStringAsync();
            var responseBody = JsonConvert.DeserializeObject<List<SingleCurrencyDTO>>(SerializedResponseBody);

            responseBody.Count.Should().Be(5);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

        }

        [Fact]
        public async Task GetCurrencyFromFutureShouldReturn404()
        {
            var serializedQueryObject = JsonConvert.SerializeObject(createContent("PLN", "EUR", "2020-08-30", "2100-09-05"));
            var response = await _client.PostAsync("/GetCurrencyRates", new StringContent(serializedQueryObject, Encoding.UTF8, "application/json"));

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        }

        [Fact]
        public async Task GetCurrencyWithBadDateOrderShouldReturn404()
        {
            var serializedQueryObject = JsonConvert.SerializeObject(createContent("PLN", "EUR", "2020-08-30", "2020-08-05"));
            var response = await _client.PostAsync("/GetCurrencyRates", new StringContent(serializedQueryObject, Encoding.UTF8, "application/json"));

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        }

        [Fact]
        public async Task GetCurrencySaturdayShouldReturnDayBeforeRate()
        {
            var serializedQueryObject = JsonConvert.SerializeObject(createContent("PLN", "EUR", "2021-09-04", "2021-09-04"));
            var response = await _client.PostAsync("/GetCurrencyRates", new StringContent(serializedQueryObject, Encoding.UTF8, "application/json"));
            var SerializedResponseBody = await response.Content.ReadAsStringAsync();
            var responseBody = JsonConvert.DeserializeObject<List<SingleCurrencyDTO>>(SerializedResponseBody);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseBody.Count.Should().Be(1);
            responseBody.First().date.Should().Be(DateTime.Parse("2021-09-03"));


        }
    }
}
