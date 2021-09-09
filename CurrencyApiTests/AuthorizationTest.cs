using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Net;

using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CurrencyApiTests
{
    public class AuthorizationTest : TestBase
    {
        [Fact]
        public async Task GetKeyShouldReturnNewAPIKey()
        {
            var response = await _client.GetAsync("/GetKey");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Should().NotBeNull();
        }
    }
}
