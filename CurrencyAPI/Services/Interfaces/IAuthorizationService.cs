using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyAPI.Services.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<string> GetKey();
    }
}
