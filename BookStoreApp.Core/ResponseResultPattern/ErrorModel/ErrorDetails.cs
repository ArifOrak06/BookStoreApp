using System.Text.Json;

namespace BookStoreApp.Core.ResponseResultPattern.ErrorModel
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public override string ToString() // nesneyi serialize etmek için...
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
