using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CurrencyAPI.DTOs;
using CurrencyAPI.DataBase;
using CurrencyAPI.Services.Interfaces;
using CurrencyAPI.Services;
using CurrencyAPI.Models;

namespace CurrencyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ICurrencyService _currencyService;

        public CurrencyController(AppDbContext context, ICurrencyService currencyService)
        {
            _context = context;
            _currencyService = currencyService;
        }

        // GET: api/Currency
        [HttpPost]
        [Route("/GetCurrencyRates")]
        public async Task<ActionResult<IEnumerable<SingleCurrencyDTO>>> GetCurrencyRates([FromBody]CurrencyQuery query)
        {
            try
            {
                return await _currencyService.GetCurrency(query);
            }
            catch (Exception ex)
            {

                return new NotFoundResult();
            }
            
        }

  
    }
}
