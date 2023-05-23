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
    public partial class AddressBookDtoPhones
    { 
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonPropertyName("user_id")]
        public Guid? UserId { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonPropertyName("address_book_id")]
        public Guid? AddressBookId { get; set; }

        /// <summary>
        /// Gets or Sets PhoneNumber
        /// </summary>

        [JsonPropertyName("phone_number")]
        [Required(ErrorMessage = "Phone number is required ( phone_number )")]
        [RegularExpression(@"^\+?[0-9]{8,}$", ErrorMessage = "Invalid phone number format, Use the valid phone number.The phone number must be at least of eight digit numbers optionally preceded with + sign")]
        public string PhoneNumber { get; set; }="";

        /// <summary>
        /// Gets or Sets Type
        /// </summary>

        [JsonPropertyName("type")]
        [Required(ErrorMessage = "Phone number type is required ( type )")]
        public AddressBookDtoType? Type { get; set; } 
        
    }
}
