using CoinConvertor.Entities;

namespace CoinConvertor.Repositories.Interfaces
{
    public interface IConversionRequestRepository : IDisposable
    {

        Task<ConversionRequest> SaveAsync(ConversionRequest conversionRequest);
        Task<int> CountListByUserAndDateBetween(int userId, DateTime date);
    }
}
