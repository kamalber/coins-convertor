using CoinConvertor.DTO.Requests;
using CoinConvertor.DTO.Responses;

namespace CoinConvertor.Services.Interfaces
{
    public interface IConversionService
    {
        Task<ResponseData> Convert(ConversionRequest conversionRequest, int UserId);
    }
}
