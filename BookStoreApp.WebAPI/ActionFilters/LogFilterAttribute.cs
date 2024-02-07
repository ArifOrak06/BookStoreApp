using BookStoreApp.Core.ResponseResultPattern.LogModel;
using BookStoreApp.Core.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookStoreApp.WebAPI.ActionFilters
{
    public class LogFilterAttribute : ActionFilterAttribute
    {
        private readonly ILoggerService _logger;

        public LogFilterAttribute(ILoggerService logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInfo(Log("OneActionExecuting", context.RouteData));
        }

        private string Log(string modelName, RouteData routeData)
        {
            var logDetails = new LogDetails()
            {
                ModelName = modelName,
                ActionName = routeData.Values["Action"],
                ControllerName = routeData.Values["Controller"]
            };

            if (routeData.Values.Count >= 3)
                logDetails.Id = routeData.Values["Id"];

            return logDetails.ToString();
        }
    }
}
