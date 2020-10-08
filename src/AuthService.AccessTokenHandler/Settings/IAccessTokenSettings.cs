using Microsoft.IdentityModel.Tokens;

namespace AuthService.AccessTokenHandler.Settings
{
    public interface IAccessTokenSettings
    {
        string PublicKey { get; }

        string Audience { get; }

        string Issuer { get; }

        SecurityKey RsaKey { get; }

        /// <summary>
        /// Validate model attributes
        /// </summary>
        /// <exception cref="System.ComponentModel.DataAnnotations.ValidationException">
        /// Thrown when attribute is invalid
        /// </exception>
        void ValidateAttributes();
    }
}
