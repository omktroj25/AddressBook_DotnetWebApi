using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Entity.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class ResponseDto
    { 
        /// <summary>
        /// Gets or Sets StatusCode
        /// </summary>

        [DataMember(Name ="Status_code")]
        public int? StatusCode { get; set; }

        /// <summary>
        /// Gets or Sets Message
        /// </summary>

        [DataMember(Name ="message")]
        public string? Message { get; set; }

        /// <summary>
        /// Gets or Sets Message
        /// </summary>

        [DataMember(Name ="description")]
        public string? Description { get; set; }

    }
}
