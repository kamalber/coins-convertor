namespace CoinConvertor.Services.Interfaces
{
    public interface IQuotaService
    {
        Task<bool> IsQuotaLimitExceeded(int userId);
    }
}
