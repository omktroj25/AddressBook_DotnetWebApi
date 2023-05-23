using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace Entity.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [ExcludeFromCodeCoverage]
    public partial class DownloadFileDto
    { 
        /// <summary>
        /// Gets or Sets Id
        /// </summary>

        [JsonPropertyName("id")]
        public Guid? Id { get; set; }

        /// <summary>
        /// Gets or Sets FileName
        /// </summary>

        [JsonPropertyName("file_name")]
        public string? FileName { get; set; }

        /// <summary>
        /// Gets or Sets FileType
        /// </summary>

        [JsonPropertyName("file_type")]
        public string? FileType { get; set; }

        /// <summary>
        /// Gets or Sets FileContent
        /// </summary>

        [JsonPropertyName("file_content")]
        public IFormFile? FileContent { get; set; }

    }
}
