using AuthService.AccessTokenHandler.Settings;
using System.ComponentModel.DataAnnotations;

namespace AuthService.API.IntegrationTests.Util
{
    public class Settings
    {
        [Required]
        public string DotNetExecutablePath { get; set; }

        [Required]
        public string FunctionHostPath { get; set; }

        [Required]
        public string FunctionApplicationPath { get; set; }

        [Required]
        public AccessTokenSettings AccessTokenSettings { get; set; }

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
