using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Entity.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class SignupDto
    { 
    
        /// <summary>
        /// Gets or Sets UserName
        /// </summary>

        [JsonPropertyName("user_name")]
        [Required(ErrorMessage = "user name is required ( Type your email id for username )")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format, Type the valid email id for user name. example: username@domain.com")]
        public string UserName { get; set; }="";

        /// <summary>
        /// Gets or Sets Password
        /// </summary>

        [JsonPropertyName("password")]
        [Required(ErrorMessage = "Password is required ( password )")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Invalid password format, Password must contain at least 1 lowercase letter, 1 uppercase letter, 1 number, and 1 special character, and be at least 8 characters long")]
        public string Password { get; set; }="";

        /// <summary>
        /// Gets or Sets ConfirmPassword
        /// </summary>

        [JsonPropertyName("confirm_password")]
        [Required(ErrorMessage = "Confirm Password is required ( confirm_password )")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password doesn't match. Please try again")]
        public string ConfirmPassword { get; set; }="";

    }
}
