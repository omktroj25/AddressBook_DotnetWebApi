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
    public partial class AddressBookDtoAddresses
    { 
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonPropertyName("user_id")]
        public Guid? UserId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [JsonPropertyName("address_book_id")]
        public Guid? AddressBookId { get; set; }
        
        /// <summary>
        /// Gets or Sets Line1
        /// </summary>

        [JsonPropertyName("line1")]
        [Required(ErrorMessage = "Address line1 is required ( line1 )")]
        public string Line1 { get; set; } = "";

        /// <summary>
        /// Gets or Sets Line2
        /// </summary>

        [JsonPropertyName("line2")]
        [Required(ErrorMessage = "Address line2 is required ( line2 )")]
        public string Line2 { get; set; } = "";

        /// <summary>
        /// Gets or Sets City
        /// </summary>

        [JsonPropertyName("city")]
        [Required(ErrorMessage = "Address city is required ( city )")]
        public string City { get; set; } = "";

        /// <summary>
        /// Gets or Sets Zipcode
        /// </summary>

        [JsonPropertyName("zipcode")]
        [Required(ErrorMessage = "Address zipcode is required ( zipcode )")]
        [RegularExpression(@"^[0-9]{5,}$", ErrorMessage = "Invalid zipcode format, The zipcode must be at least of five digit numbers")]
        public string Zipcode { get; set; } = "";

        /// <summary>
        /// Gets or Sets StateName
        /// </summary>

        [JsonPropertyName("state_name")]
        [Required(ErrorMessage = "Address state name is required ( state_name )")]
        public string StateName { get; set; } = "";

        /// <summary>
        /// Gets or Sets Type
        /// </summary>

        [JsonPropertyName("type")]
        [Required(ErrorMessage = "Address type is required ( type )")]
        public AddressBookDtoType? Type { get; set; }

        /// <summary>
        /// Gets or Sets Country
        /// </summary>

        [JsonPropertyName("country")]
        [Required(ErrorMessage = "Country type is required ( type )")]
        public AddressBookDtoType? Country { get; set; }

    }
}
