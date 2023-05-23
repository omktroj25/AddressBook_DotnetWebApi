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
    public partial class AddressBookDtoEmails
    { 
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonPropertyName("user_id")]
        public Guid? UserId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [JsonPropertyName("address_book_id")]
        public Guid? AddressBookId { get; set; }
        
        /// <summary>
        /// Gets or Sets EmailAddress
        /// </summary>

        [JsonPropertyName("email_address")]
        [Required(ErrorMessage = "Email address is required ( email_address )")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format, Use valid email id. example: username@domain.com")]
        public string EmailAddress { get; set; }="";

        /// <summary>
        /// Gets or Sets Type
        /// </summary>

        [JsonPropertyName("type")]
        [Required(ErrorMessage = "Email type is required ( type )")]
        public AddressBookDtoType? Type { get; set; }

    }
}
