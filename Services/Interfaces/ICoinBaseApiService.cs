using RestSharp;

namespace CoinConvertor.Services.Interfaces
{
    public interface ICoinBaseApiService
    {
        RestResponse ExchangeRate(string currency);

    }
}
