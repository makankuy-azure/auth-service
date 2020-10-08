using AuthService.AccessTokenHandler;
using AuthService.API.Services.Response;
using AuthService.Application.UseCases;
using AuthService.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AuthService.API.Services
{
    public class UserInfoService
    {
        private readonly IUserInfoUseCase _userInfoUseCase;
        private readonly IAccessTokenProvider _accessTokenProvider;
        private readonly ILogger<UserInfoService> _logger;

        public UserInfoService(IUserInfoUseCase userInfoUseCase, IAccessTokenProvider accessTokenProvider, ILogger<UserInfoService> logger)
        {
            _userInfoUseCase = userInfoUseCase;
            _accessTokenProvider = accessTokenProvider;
            _logger = logger;
        }

        [FunctionName("GetUserInfo")]
        public async Task<IActionResult> GetUserInfoAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "user-info")] HttpRequest req,
            ILogger log)
        {
            var accessToken = _accessTokenProvider.ValidateToken(req);

            if (accessToken.Status != AccessTokenStatus.Valid)
            {
                return new UnauthorizedResult();
            }

            var userId = accessToken.Principal.GetUserId();

            if (string.IsNullOrWhiteSpace(userId))
            {
                /*
                 * Because we're using access token to authenticate, user id should be not empty.
                 * If empty, there something must be wrong, so we create the log for further inspect.
                 * TODO: provide more log info
                 * */
                _logger.LogError($"User with id was empty.");

                return new NotFoundObjectResult(
                    ErrorResponse.NotFound(
                        message: "User Not found."
                    ));
            }

            try
            {
                var user = await _userInfoUseCase.GetUserInfo(
                    userId);

                return new OkObjectResult(
                    UserResponse.Load(user));
            }
            catch (NotFoundException e)
            {
                /*
                 * Because we're using access token to authenticate, user id should be exist in database.
                 * If not exist, there something must be wrong, so we create the log for further inspect.
                 * TODO: provide more log info
                 * */
                _logger.LogError($"User with id {accessToken.Principal.GetUserId()} was not found.");

                return new NotFoundObjectResult(
                    ErrorResponse.NotFound(
                        message: "User Not found."
                    ));
            }
            catch (DomainException e)
            {
                return new BadRequestObjectResult(
                    ErrorResponse.BadRequest(
                        message: e.Message
                    ));
            }
        }
    }
}
