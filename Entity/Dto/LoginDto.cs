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
    public partial class LoginDto
    { 
        /// <summary>
        /// Gets or Sets UserName
        /// </summary>

        [JsonPropertyName("user_name")]
        [Required(ErrorMessage = "user name is required ( user_name )")]
        public string UserName { get; set; }="";

        /// <summary>
        /// Gets or Sets Password
        /// </summary>

        [JsonPropertyName("password")]
        [Required(ErrorMessage = "Password is required ( password )")]
        public string Password { get; set; }="";

    }
}
