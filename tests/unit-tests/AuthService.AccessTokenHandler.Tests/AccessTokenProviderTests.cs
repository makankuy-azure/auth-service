using AuthService.AccessTokenHandler.Settings;
using AuthService.AccessTokenHandler.Tests.Util;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace AuthService.AccessTokenHandler.Tests
{
    public class AccessTokenProviderTests
    {
        [Theory]
        [MemberData(nameof(AccessTokenFactory.Data), MemberType = typeof(AccessTokenFactory))]
        public void DecodedJwtAccessTokenShouldBeValid(
            IAccessTokenSettings accessTokenSettings,
            string accessTokenToDecoded,
            AccessTokenFactory.PrincipalResult principalResult
            )
        {
            var AccessTokenProvider = new AccessTokenProvider(accessTokenSettings);

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = $"Bearer {accessTokenToDecoded}";

            var accessTokenResult = AccessTokenProvider.ValidateToken(httpContext.Request);

            Assert.Equal(AccessTokenStatus.Valid, accessTokenResult.Status);
            Assert.Equal(principalResult.Id, accessTokenResult.Principal.GetUserId());
            Assert.Equal(principalResult.Name, accessTokenResult.Principal.Identity.Name);
        }
    }
}
