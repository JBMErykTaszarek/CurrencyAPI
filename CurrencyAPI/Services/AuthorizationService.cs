using CurrencyAPI.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CurrencyAPI.Services.Interfaces
{
    public class AuthorizationService : IAuthorizationService
    {

        
        private readonly AppDbContext _context;

        public AuthorizationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> GetKey()
        {
            var key = new byte[32];
            using (var generator = RandomNumberGenerator.Create())
                generator.GetBytes(key);
            string apiKey = Convert.ToBase64String(key);
            _context.AuthorizationKey.Add(new Models.AuthorizationKey() { Hash = apiKey });
            await _context.SaveChangesAsync();

            return apiKey;
        }
    }
}
