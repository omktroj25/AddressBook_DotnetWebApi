using System.Diagnostics.CodeAnalysis;

namespace Entity.Dto
{
    [ExcludeFromCodeCoverage]
    public class ResponseErrorDto
    {
    public int StatusCode { get; set; }
    public string Message { get; set; }="";
    public string Description { get; set; }="";
    public object Error { get; set; }="";
    }
}