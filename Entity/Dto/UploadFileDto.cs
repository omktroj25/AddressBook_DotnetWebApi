using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Entity.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class UploadFileDto
    { 
        /// <summary>
        /// Gets or Sets FileName
        /// </summary>

        // [JsonPropertyName("file_name")]
        // public string FileName { get; set; }="";

        /// <summary>
        /// Gets or Sets File
        /// </summary>

        [JsonPropertyName("file")]
        [Required(ErrorMessage = "Please upload the file ( file )")]
        public IFormFile? File { get; set; }

    }
}
