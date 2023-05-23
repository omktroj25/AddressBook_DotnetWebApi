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
    public partial class AddressBookDtoType
    { 
        /// <summary>
        /// Gets or Sets Key
        /// </summary>

        [JsonPropertyName("key")]
        [Required(ErrorMessage = "key type is required ( key )")]
        public string Key { get; set; } = "";

        [System.Text.Json.Serialization.JsonIgnore]
        [JsonPropertyName("id")]
        public Guid? Id { get; set; }

    }
}
