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
    public partial class AddressBookDto
    {   
        [JsonPropertyName("id")]
        public Guid? Id{ get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [JsonPropertyName("user_id")]
        public Guid? UserId{ get; set; }

        /// <summary>
        /// Gets or Sets FirstName
        /// </summary>

        [JsonPropertyName("first_name")]
        [Required(ErrorMessage = "First name is required ( first_name )")]
        public string FirstName { get; set; } = "";

        /// <summary>
        /// Gets or Sets LastName
        /// </summary>

        [JsonPropertyName("last_name")]
        [Required(ErrorMessage = "Last name is required ( last_name )")]
        public string LastName { get; set; } ="";

        /// <summary>
        /// Gets or Sets Emails
        /// </summary>

        [JsonPropertyName("emails")]
        [Required(ErrorMessage = "emails is required ( emails )")]
        public List<AddressBookDtoEmails>? Emails { get; set; }

        /// <summary>
        /// Gets or Sets Addresses
        /// </summary>

        [JsonPropertyName("addresses")]
        [Required(ErrorMessage = "Addresses is required ( addresses )")]
        public List<AddressBookDtoAddresses>? Addresses { get; set; }

        /// <summary>
        /// Gets or Sets Phones
        /// </summary>

        [JsonPropertyName("phones")]
        [Required(ErrorMessage = "Phones is required ( phones )")]
        public List<AddressBookDtoPhones>? Phones { get; set; }

    }
}
