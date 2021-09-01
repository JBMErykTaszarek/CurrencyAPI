using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CurrencyAPI.DataBase;
using CurrencyAPI.Models;
using CurrencyAPI.Services.Interfaces;

namespace CurrencyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;


        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        // GET: api/Authorization/GetKey
        [HttpGet]
        [Route("/GetKey")]
        public async Task<string> GetKey()
        {
            return await _authorizationService.GetKey();
        }

     
    }
}
