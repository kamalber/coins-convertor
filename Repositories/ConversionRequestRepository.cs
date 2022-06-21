using CoinConvertor.Entities;
using CoinConvertor.Context;
using CoinConvertor.Repositories.Interfaces;

namespace CoinConvertor.Repositories
{
    public class ConversionRequestRepository : IConversionRequestRepository
    {
        private readonly ApplicationDBContext _context;

        public ConversionRequestRepository(ApplicationDBContext context)
        {
                this._context = context;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        public async Task<ConversionRequest> SaveAsync(ConversionRequest conversionRequest)
        {

            if (conversionRequest != null)
            {
                await _context.ConversionRequests.AddAsync(conversionRequest);
                var saveReponse= await _context.SaveChangesAsync();
                if (saveReponse >= 0)
                    return conversionRequest;
            }
            return null;
        }

        public  async Task<int> CountListByUserAndDateBetween(int userId, DateTime date)
        {
            return await Task.Run(() =>  _context.ConversionRequests.
            Where(c => c.User.Id == userId)
            .Where(c => c.PerfprmedAt.Day==date.Day)
            .ToList()
            .Count());


        }
    }
}
