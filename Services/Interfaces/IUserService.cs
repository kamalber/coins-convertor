using CoinConvertor.DTO.Requests;
using CoinConvertor.DTO.Responses;

namespace CoinConvertor.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseData> Login(LoginRequest loginRequest);
        Task<ResponseData> SignupAsync(SignupRequest signupRequest);


    }
}
