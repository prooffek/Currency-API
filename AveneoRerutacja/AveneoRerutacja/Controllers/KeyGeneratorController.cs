using AveneoRerutacja.KeyGenerator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AveneoRerutacja.Data;
using AveneoRerutacja.Infrastructure;

namespace AveneoRerutacja.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class KeyGeneratorController : ControllerBase
    {
        private readonly IUnitOfWork<AuthenticationKeyDbContext> _uow;

        public KeyGeneratorController(IUnitOfWork<AuthenticationKeyDbContext> uow)
        {
            _uow = uow;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> GenerateNewKey()
        {
            AuthenticationKey newKey = new();

            if (string.IsNullOrEmpty(newKey.KeyValue)) throw new NullReferenceException();

            try
            {
                await _uow.AuthenticationKeys.Add(newKey);
                await _uow.Save();
                
                return Ok(newKey.KeyValue);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Problem($"Internal server error: {e}");
            }
        }
    }
}
