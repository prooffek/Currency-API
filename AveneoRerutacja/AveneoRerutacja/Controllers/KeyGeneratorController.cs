using AveneoRerutacja.KeyGenerator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AveneoRerutacja.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class KeyGeneratorController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> GenerateNewKey()
        {
            AuthenticationKey newKey = new();

            if (newKey == null) throw new NullReferenceException();
            
            if (string.IsNullOrEmpty(newKey.KeyValue)) throw new NullReferenceException();

            return Ok(new AuthenticationKey().KeyValue);
        }
    }
}
