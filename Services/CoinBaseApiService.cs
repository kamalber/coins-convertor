using CoinConvertor.Services.Interfaces;
using RestSharp;
using System.Collections.Generic;
namespace CoinConvertor.Services
{

    public class CoinBaseApiService:ICoinBaseApiService
    {
        private IConfiguration _configuration;
        private string apiURL="";


        public CoinBaseApiService(IConfiguration configuration)
        {
            this._configuration = configuration;
            this.apiURL = configuration.GetSection("CoinBase").GetSection("Url").Value;
        }

        public  RestResponse ExchangeRate(string currency)
        {
            var client = new RestClient(this.apiURL+ "?currency="+currency);

            var response =  client.Execute(new RestRequest());

            return response;
        }


    }
}
