using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ApiKeyAuthAttribute : ActionFilterAttribute
{
    private const string ApiKeyHeaderName = "x-api-key";

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKey) || string.IsNullOrEmpty(apiKey))
        {
            context.Result = new JsonResult(new { error = "x-api-key header missing" }) { StatusCode = 403 };
            return;
        }        

        base.OnActionExecuting(context);
    }
}
