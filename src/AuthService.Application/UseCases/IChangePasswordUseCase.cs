using AuthService.Domain.ValueObjects;
using System.Threading.Tasks;

namespace AuthService.Application.UseCases
{
    public interface IChangePasswordUseCase
    {
        Task ChangePassword(string userId, Password oldPassword, Password newPassword);
    }
}
