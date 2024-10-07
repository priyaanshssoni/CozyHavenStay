using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

public class LoggingActionFilter : IActionFilter
{
    private readonly ILogger _logger;

    public LoggingActionFilter(ILogger<LoggingActionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Log before the action is executed
        _logger.LogInformation($"Action {context.ActionDescriptor.DisplayName} is executing.");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Log after the action is executed
        _logger.LogInformation($"Action {context.ActionDescriptor.DisplayName} has executed.");
    }
}