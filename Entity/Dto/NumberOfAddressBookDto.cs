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
    public partial class NumberOfAddressBookDto
    { 
        /// <summary>
        /// Gets or Sets Count
        /// </summary>

        [JsonPropertyName("count")]
        public int? Count { get; set; }

    }
}
