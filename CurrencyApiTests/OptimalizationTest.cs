using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CurrencyApiTests
{
    public class OptimalizationTest : TestBase
    {
        [Fact]
        public async Task OneRateSpeedTest()
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"..\..\..\SpeedTestResaults\", DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + "-SpeedTest.txt");
            var serializedQueryObject = JsonConvert.SerializeObject(createContent("PLN", "EUR", "2018-09-01", "2018-09-01"));
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var response = await _client.PostAsync("/GetCurrencyRates", new StringContent(serializedQueryObject, Encoding.UTF8, "application/json"));
            sw.Stop();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            if (!File.Exists(path))
            {
                using (FileStream fs = File.Create(path)) ;
            }
            using (StreamWriter writer = File.AppendText(path))
            {
                writer.WriteLine($"Getting One rate last: {sw.Elapsed}");

            }
        }

    }
}
