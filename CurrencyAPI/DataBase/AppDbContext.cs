using CurrencyAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyAPI.Models;

namespace CurrencyAPI.DataBase
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<SingleCurrencyDTO> CurrencyRates {get; set;}

        public DbSet<AuthorizationKey> AuthorizationKey { get; set; }
    }
}
