using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Entity.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class TokenResponseDto 
    { 
        /// <summary>
        /// Gets Token type
        /// </summary>

        [JsonPropertyName("token_type")]
        public string? TokenType { get; set; }

        /// <summary>
        /// Gets or Sets Token
        /// </summary>

        [JsonPropertyName("access_token")]
        public string? Token { get; set; }

    }
}
