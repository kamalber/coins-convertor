using CoinConvertor.Entities;

namespace CoinConvertor.Repositories.Interfaces
{
    public interface IUserRepository : IDisposable
    {
        Task<User> FindByEmailAsync(string Email);
        Task<User> SaveAsync(User user);
        Task<User> FindByIdAsync(int Id);


    }
}
