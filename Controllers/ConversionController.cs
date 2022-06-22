using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoinConvertor.DTO.Requests;
using CoinConvertor.DTO.Responses;
using Microsoft.AspNetCore.Authorization;
using CoinConvertor.Services.Interfaces;
using CoinConvertor.Decorators;

namespace CoinConvertor.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ConversionController : BaseController
    {
        private readonly IConversionService conversionService;
        private readonly IQuotaService quotaService;
        public ConversionController(IConversionService conversionService, IQuotaService quotaService)
        {
            this.conversionService = conversionService;
            this.quotaService = quotaService;
        }
        [HttpGet]
        [Route("convert")]
        [LimitRequests(MaxRequests = 5, TimeWindow = 2)]
        public async Task<IActionResult> Convert([FromQuery] string from, [FromQuery] string to, [FromQuery] double amount)
        {
            if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to) || amount==0)
            {
                return BadRequest(new ResponseData
                {
                    ErrorMessage = "Missing Parameters",
                    ErrorCode = "110"
                });
            }

            var isuotaLimitExceeded= await quotaService.IsQuotaLimitExceeded(UserID);
            if (isuotaLimitExceeded==true)
            {
                return BadRequest(new ResponseData
                {
                    ErrorMessage = "Quota Exceeded",
                    ErrorCode = "115"
                });
            }
            var response = await conversionService.Convert
                (new ConversionRequest { From = from, To = to, Amount = amount},UserID);

            if (!response.Success)
            {
                return UnprocessableEntity(response);
            }

            return Ok(response);
        }


    }
}
