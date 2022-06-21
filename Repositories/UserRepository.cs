using CoinConvertor.Entities;
using CoinConvertor.Context;
using Microsoft.EntityFrameworkCore;
using CoinConvertor.Repositories.Interfaces;

namespace CoinConvertor.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;

        public UserRepository(ApplicationDBContext context)
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

       
        public async Task<User> FindByEmailAsync(string Email)
        {
           return await _context.Users.SingleOrDefaultAsync(user => user.Email == Email);
        }

        public async Task<User> SaveAsync(User user)
        {

            if (user != null)
            {
                await _context.Users.AddAsync(user);
                var saveResponse = await _context.SaveChangesAsync();
                if (saveResponse >= 0)
                    return user;
            }
            return null;
            
            
        }
        public async Task<User> FindByIdAsync(int Id)
        {
            return await _context.Users.FindAsync(Id);

        }
    }
}
