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
    public partial class MetaResponseDto
    { 
        /// <summary>
        /// Gets or Sets Id
        /// </summary>

        [JsonPropertyName("id")]
        public Guid? Id { get; set; }

        /// <summary>
        /// Gets or Sets Key
        /// </summary>

        [JsonPropertyName("key")]
        public string? Key { get; set; }

        /// <summary>
        /// Gets or Sets Description
        /// </summary>

        [JsonPropertyName("description")]
        public string? Description { get; set; }

    }
}
