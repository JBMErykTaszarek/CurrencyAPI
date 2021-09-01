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

        




        // GET: api/Currency/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SingleCurrencyDTO>> GetSingleCurrencyDTO(int id)
        {
            var singleCurrencyDTO = await _context.CurrencyRates.FindAsync(id);

            if (singleCurrencyDTO == null)
            {
                return NotFound();
            }

            return singleCurrencyDTO;
        }

        // PUT: api/Currency/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSingleCurrencyDTO(int id, SingleCurrencyDTO singleCurrencyDTO)
        {
            if (id != singleCurrencyDTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(singleCurrencyDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SingleCurrencyDTOExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Currency
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SingleCurrencyDTO>> PostSingleCurrencyDTO(SingleCurrencyDTO singleCurrencyDTO)
        {
            _context.CurrencyRates.Add(singleCurrencyDTO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSingleCurrencyDTO", new { id = singleCurrencyDTO.Id }, singleCurrencyDTO);
        }

        // DELETE: api/Currency/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SingleCurrencyDTO>> DeleteSingleCurrencyDTO(int id)
        {
            var singleCurrencyDTO = await _context.CurrencyRates.FindAsync(id);
            if (singleCurrencyDTO == null)
            {
                return NotFound();
            }

            _context.CurrencyRates.Remove(singleCurrencyDTO);
            await _context.SaveChangesAsync();

            return singleCurrencyDTO;
        }

        private bool SingleCurrencyDTOExists(int id)
        {
            return _context.CurrencyRates.Any(e => e.Id == id);
        }
    }
}
