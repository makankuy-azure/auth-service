using System.ComponentModel.DataAnnotations;

namespace AuthService.API.IntegrationTests.Settings
{
    public class AuthServiceAPIUrl
    {
        [Required]
        public string BaseUrl { get; private set; }

        [Required]
        public string ChangePassword { get; private set; }

        [Required]
        public string SignInByEmail { get; private set; }

        [Required]
        public string SignInByPhone { get; private set; }

        [Required]
        public string SignUpByEmail { get; private set; }

        [Required]
        public string SignUpByPhone { get; private set; }

        /// <summary>
        /// Validate model attributes
        /// </summary>
        /// <exception cref="System.ComponentModel.DataAnnotations.ValidationException">
        /// Thrown when attribute is invalid
        /// </exception>
        public void ValidateAttributes()
        {
            Validator.ValidateObject(this, new ValidationContext(this), validateAllProperties: true);
        }
    }
}
