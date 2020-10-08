﻿using AuthService.Domain.Entities;
using System.Threading.Tasks;

namespace AuthService.Application.UseCases
{
    public interface IUserInfoUseCase
    {
        Task<User> GetUserInfo(string userId);
    }
}
