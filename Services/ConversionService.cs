using CoinConvertor.DTO.Requests;
using CoinConvertor.DTO.Responses;
using CoinConvertor.Repositories.Interfaces;
using CoinConvertor.Services.Interfaces;
using Newtonsoft.Json;
using System.Net;

namespace CoinConvertor.Services
{
    public class ConversionService : IConversionService
    {
        private readonly ICoinBaseApiService coinBaseApiService;
        private readonly IConversionRequestRepository conversionRequestRepository;
        private readonly IUserRepository userRepository;


       public ConversionService(ICoinBaseApiService coinBaseApiService, IConversionRequestRepository conversionRequestRepository,IUserRepository userRepository)
        {
            this.coinBaseApiService = coinBaseApiService;
            this.conversionRequestRepository = conversionRequestRepository;
            this.userRepository = userRepository;     
        }


        public async Task<ResponseData> Convert(ConversionRequest conversionRequest,int UserId)
        {
            // Getting the exchange rate based on requsted currency : From
            var exchangeRateResponse =await Task.Run(()=> coinBaseApiService.ExchangeRate(conversionRequest.From));
            
            if (exchangeRateResponse.StatusCode!= HttpStatusCode.OK)
            {
                string errorMessage = "Internal Server Error";

                if (exchangeRateResponse.StatusCode == HttpStatusCode.BadRequest)
                    errorMessage = "Currency is invalid";

                return new ResponseData
                {
                    Success = false,
                    ErrorMessage = errorMessage,
                    ErrorCode = "109"
                };
            }
           
            dynamic jsonResult = JsonConvert.DeserializeObject(exchangeRateResponse.Content);

            // Getting rate of the desired currency 
            var rate = jsonResult.data["rates"][conversionRequest.To.ToUpper()];
            if(rate==null) return new ResponseData
            {
                Success = false,
                ErrorMessage = "Currency is invalid",
                ErrorCode = "116"
            };

            var convertedAmount = rate * conversionRequest.Amount;

            // Generate conversion response data
            var conversionData = new ConversionResponseData

            {
                Source = new CurrencyData { Currency = conversionRequest.From.ToUpper(), Amount = conversionRequest.Amount },
                Conversion = new CurrencyData { Currency = conversionRequest.To.ToUpper(), Amount = convertedAmount },

            };
            // Get user 'connected user' and assing int to the request
            var user = await userRepository.FindByIdAsync(UserId);

            // Save the request to the DB
            await conversionRequestRepository.SaveAsync(new Entities.ConversionRequest
            {
                From = conversionRequest.From.ToUpper(),
                To = conversionRequest.From.ToUpper(),
                Amount = conversionRequest.Amount,
                ResponseBody = conversionData.ToString(),
                User=user

            });

            return new ResponseData
            {
                Success = true,
                Data= conversionData
            };



        }
      
    }
}
