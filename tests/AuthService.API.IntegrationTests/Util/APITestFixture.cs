using AuthService.AccessTokenHandler;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;

namespace AuthService.API.IntegrationTests.Util
{
    public class APITestFixture : IDisposable
    {
        private readonly Process _funcHostProcess;

        public readonly HttpClient Client = new HttpClient();

        public AccessTokenProvider AccessTokenProvider;

        public APITestFixture()
        {
            var dotnetExePath = Environment.ExpandEnvironmentVariables(ConfigurationHelper.Settings.DotNetExecutablePath);
            var functionHostPath = Environment.ExpandEnvironmentVariables(ConfigurationHelper.Settings.FunctionHostPath);
            var functionAppFolder = Path.GetRelativePath(Directory.GetCurrentDirectory(), ConfigurationHelper.Settings.FunctionApplicationPath);

            _funcHostProcess = new Process
            {
                StartInfo =
                {
                    FileName = dotnetExePath,
                    Arguments = $"\"{functionHostPath}\" start -p {Port}",
                    WorkingDirectory = functionAppFolder
                }
            };
            var success = _funcHostProcess.Start();
            if (!success)
            {
                throw new InvalidOperationException("Could not start Azure Functions host.");
            }

            Client.BaseAddress = new Uri($"http://localhost:{Port}");

            AccessTokenProvider = new AccessTokenProvider(ConfigurationHelper.Settings.AccessTokenSettings);
        }

        public AccessTokenResult ValidateToken(string accessToken)
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = $"Bearer {accessToken}"; //Set header

            return AccessTokenProvider.ValidateToken(httpContext.Request);
        }

        public int Port { get; } = 7071;

        public virtual void Dispose()
        {
            if (!_funcHostProcess.HasExited)
            {
                _funcHostProcess.Kill();
            }

            _funcHostProcess.Dispose();
        }
    }
}
