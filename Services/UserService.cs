using CoinConvertor.Context;
using CoinConvertor.Helpers;
using Microsoft.EntityFrameworkCore;
using CoinConvertor.Entities;
using System.Security.Cryptography;
using CoinConvertor.DTO.Requests;
using CoinConvertor.DTO.Responses;
using CoinConvertor.Services.Interfaces;
using CoinConvertor.Repositories.Interfaces;

namespace CoinConvertor.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService( IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<ResponseData> Login(LoginRequest loginRequest)
        {
            var user = await userRepository.FindByEmailAsync(loginRequest.Email);

            if (user == null)
            {
                return new ResponseData
                {
                    Success = false,
                    ErrorMessage = "Email not found",
                    ErrorCode = "101"
                };
            }
            var passwordHash = HashingHelper.HashUsingPbkdf2(loginRequest.Password, Convert.FromBase64String(user.PasswordSalt));

            if (user.Password != passwordHash)
            {
                return new ResponseData
                {
                    Success = false,
                    ErrorMessage = "Password Invalid",
                    ErrorCode = "102"
                };
            }

            var token = await Task.Run(() => TokenHelper.GenerateToken(user));
            var loginData = new LoginData
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = token
            };

            return new ResponseData
            {
                Success = true,
                Data=loginData
                
            };  
        }

        public async Task<ResponseData> SignupAsync(SignupRequest signupRequest)
        {
            var existingUser = await userRepository.FindByEmailAsync(signupRequest.Email);

            if (existingUser != null)
            {
                return new ResponseData
                {
                    Success = false,
                    ErrorMessage = "Email already exists",
                    ErrorCode = "103"
                };
            }

            if (signupRequest.Password != signupRequest.ConfirmPassword)
            {
                return new ResponseData
                {
                    Success = false,
                    ErrorMessage = "Password & confirmPassword do not match",
                    ErrorCode = "104"
                };
            }

            if (signupRequest.Password.Length <= 7) 
            {
                return new ResponseData
                {
                    Success = false,
                    ErrorMessage = "Password is weak",
                    ErrorCode = "105"
                };
            }
            var salt = RandomNumberGenerator.GetBytes(32);
            var passwordHash = HashingHelper.HashUsingPbkdf2(signupRequest.Password, salt);

            var user = new User
            {
                Email = signupRequest.Email,
                Password = passwordHash,
                PasswordSalt = Convert.ToBase64String(salt),
                FirstName = signupRequest.FirstName,
                LastName = signupRequest.LastName,
            };

            

            var savedUser = await userRepository.SaveAsync(user);
            var signupData = new SignupData
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            if (savedUser != null)
            {
                return new ResponseData { Success = true,Data=signupData };
            }

            return new ResponseData
            {
                Success = false,
                ErrorMessage = "Unable to register user",
                ErrorCode = "106"
            };

        }

    }
}
