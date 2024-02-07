using System.Text.Json;

namespace BookStoreApp.Core.ResponseResultPattern.LogModel
{
    public class LogDetails
    {
        public Object? ModelName { get; set; }
        public Object? ControllerName { get; set; }
        public Object? ActionName { get; set; }
        public Object? Id { get; set; }
        public Object? CreatedAt { get; set; }

        public LogDetails()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
