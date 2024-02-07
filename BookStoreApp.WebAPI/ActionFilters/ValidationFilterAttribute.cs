using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookStoreApp.WebAPI.ActionFilters
{
    public class ValidationFilterAttribute : ActionFilterAttribute
    {

        // Method Çalışmadan önce
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Öncelikle hangi controller da olduğumuzu anlayalım.?

            var currentController = context.RouteData.Values["action"];

            // Daha sonra hangi methodda tanımlandığını anlayalım ? 

            var currentAction = context.RouteData.Values["action"];


            // Parametre olarak Dto gönderilmiş mi ? sonuçta tüm Dto'lar Dto olarak tanımlandı ve Dto kelimesini ek olarak aldı.
            var param = context.ActionArguments.SingleOrDefault(p => p.Value.ToString().Contains("Dto")).Value;

            if(param is null)
            {
                // Dto yoksa
                context.Result = new BadRequestObjectResult($"Object is null. " + $"Controller : {currentController} " + $"Action : {currentAction}");
                return; // 400 status code

            }
            // parametre olarak gönderilen objenin geçerliliğini kontrol edelim.

            if(!context.ModelState.IsValid) 
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);




        }
    }
}
