namespace AuthService.API.IntegrationTests.Settings
{
    public sealed class ApplicationSettings
    {
        public AuthServiceAPIUrl AuthServiceAPIUrl { get; set; }

        public void ValidateAttributes()
        {
            AuthServiceAPIUrl.ValidateAttributes();
        }
    }
}
