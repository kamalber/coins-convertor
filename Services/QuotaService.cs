using CoinConvertor.Repositories.Interfaces;
using CoinConvertor.Services.Interfaces;

namespace CoinConvertor.Services
{
    public class QuotaService : IQuotaService
    {
        private readonly IConversionRequestRepository conversionRequestRepository;
        private IConfiguration _configuration;
        private int workDayLimit;
        private int weekEndLimit ;


        public QuotaService(IConversionRequestRepository conversionRequestRepository,IConfiguration configuration)
        {
            this._configuration = configuration;
            this.conversionRequestRepository = conversionRequestRepository;

            this.workDayLimit = int.Parse(this._configuration.GetSection("ApiRateLimit").GetSection("WorkDayLimit").Value);
            this.weekEndLimit = int.Parse(this._configuration.GetSection("ApiRateLimit").GetSection("WeekEndLimit").Value);
        }
        

        public async Task<bool> IsQuotaLimitExceeded(int userId)
        {
            DateTime todayDate = DateTime.Today;
          
            // Count user's conversion requets made today
           int count= await conversionRequestRepository.CountListByUserAndDateBetween(userId, todayDate);
           
            if ((todayDate.DayOfWeek == DayOfWeek.Saturday) || (todayDate.DayOfWeek == DayOfWeek.Sunday))
            {
                if (count >= weekEndLimit) return true;
                else return false; 
            }
            else
            {
                if (count >= workDayLimit) return true;
                else return false;

            }

            
        }
    }
}
