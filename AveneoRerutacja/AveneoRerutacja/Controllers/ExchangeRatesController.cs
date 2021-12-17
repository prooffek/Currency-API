using AveneoRerutacja.ApiHandler;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AveneoRerutacja.Data;
using AveneoRerutacja.DbHandler;
using AveneoRerutacja.Dimension;
using AveneoRerutacja.Domain;
using AveneoRerutacja.Infrastructure;
using AveneoRerutacja.KeyGenerator;
using AveneoRerutacja.Models;
using Microsoft.AspNetCore.Http;

namespace AveneoRerutacja.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExchangeRatesController : ControllerBase
    {
        private readonly IUnitOfWork<ExchangeRatesDbContext> _erUnitOfWork;
        private readonly IUnitOfWork<AuthenticationKeyDbContext> _keyUnitOfWork;
        private readonly IMapper _mapper;

        public ExchangeRatesController(
            IUnitOfWork<ExchangeRatesDbContext> eruow, 
            IUnitOfWork<AuthenticationKeyDbContext> keyuow,
            IMapper mapper)
        {
            _erUnitOfWork = eruow;
            _keyUnitOfWork = keyuow;
            _mapper = mapper;
        }
        
        //HttpPost allows retrieving dictionary values from the url parameters
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetRates([FromQuery] Dictionary<string, string> currencyCodes, 
            string startsOn, string endsOn, string apiKey)
        {
            if (await AuthenticationKey.IsValid(_keyUnitOfWork, apiKey))
            {
                try
                {
                    var (startDate, endDate) = DateClass.ValidateDates(startsOn, endsOn);

                    var dbHandler = new DbRequestsHandler(startDate.Copy(), endDate.Copy());
                    dbHandler.DailyRates = await dbHandler.SetDailyRates(_erUnitOfWork);

                    if (dbHandler.AllDailyRatesInDb())
                        return Ok(_mapper.Map<IList<DailyRateDto>>(dbHandler.DailyRates));

                    string responseString = await ApiHelper.GetResponseString(currencyCodes,
                        startDate.Copy(),
                        endDate.Copy(), apiKey);

                    IDataGetter dataGetter = new JDataGetter(responseString);
                    IList<DailyRate> result = new ApiResponseHandler<IDataGetter>(dataGetter).GetDailyRates();
                    await dbHandler.AddDailyRatesToDb(result, _erUnitOfWork);

                    return Ok(_mapper.Map<IList<DailyRateDto>>(result));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return NotFound("Page not found");
                }
            }
            
            return NotFound("Page not found");
            
            //Status "NotFound" is returned independently of the exception, to hide data from the client/receiver.
            //More detailed info is printed in the console and logged in txt files
        }
    }
}
