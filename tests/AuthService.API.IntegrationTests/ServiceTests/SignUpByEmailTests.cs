using AuthService.API.IntegrationTests.Util;
using AuthService.API.Services;
using AuthService.API.Services.Response;
using AuthService.Domain.UnitTests.Util;
using AuthService.Persistence.IntegrationTests.Util;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AuthService.API.IntegrationTests.User
{
    [Collection(nameof(APITestCollection))]
    public class SignUpByEmailTests
    {
        private readonly APITestFixture _apiFixture;

        private readonly PersistenceTestFixture _persistenceFixture;

        public SignUpByEmailTests(APITestFixture apiFixture, PersistenceTestFixture persistenceFixture)
        {
            _apiFixture = apiFixture;
            _persistenceFixture = persistenceFixture;
        }

        [Fact]
        public async Task SignUpByEmailShouldBeSucceed()
        {
            var dateTimeBeforeSignUp = DateTime.UtcNow;

            /* sign up using service */
            var expectedUser = UserFactory.GenerateUserUsingEmail();
            var requestBodyObject = new SignUpService.SignUpByEmailRequest
            {
                Email = expectedUser.Email,
                Password = UserFactory.DefaultPassword,
                FirstName = expectedUser.FirstName,
                LastName = expectedUser.LastName
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBodyObject), Encoding.UTF8, "application/json");
            
            var httpResponse = await _apiFixture.Client.PostAsync("api/sign-up-by-email", content);

            httpResponse.EnsureSuccessStatusCode();

            var actualUserReponse = JsonConvert.DeserializeObject<UserResponse>(
                await httpResponse.Content.ReadAsStringAsync());

            /* check if the created user response is match with expected user */

            Assert.NotNull(actualUserReponse);
            Assert.NotNull(actualUserReponse.Id);
            Assert.Equal(expectedUser.Email, actualUserReponse.Email);
            Assert.Equal(expectedUser.FirstName, actualUserReponse.FirstName);
            Assert.Equal(expectedUser.LastName, actualUserReponse.LastName);
            Assert.True(expectedUser.SignUpDate > dateTimeBeforeSignUp);

            /* check again if the user stored in database is match with expected user */
            var actualUser = _persistenceFixture.GetUserByEmail(expectedUser.Email);

            Assert.NotNull(actualUser);
            Assert.NotNull(actualUser.Id);
            Assert.Equal(expectedUser.Email, actualUser.Email);
            Assert.Equal(expectedUser.FirstName, actualUser.FirstName);
            Assert.Equal(expectedUser.LastName, actualUser.LastName);
            Assert.True(expectedUser.SignUpDate > dateTimeBeforeSignUp);
        }
    }
}
 