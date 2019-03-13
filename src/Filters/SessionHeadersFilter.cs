using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using order.Services;

namespace order.Middleware
{
  public class SessionHeadersFilter : IActionFilter
  {
    private readonly SessionService _sessionService;

    public SessionHeadersFilter(SessionService sessionService)
    {
      _sessionService = sessionService;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
      // Skip if SkipSessionHeadersFilter attribute exists
      var descriptor = (ControllerActionDescriptor)context.ActionDescriptor;
      if (
          descriptor.ControllerTypeInfo.CustomAttributes.Any(a => a.AttributeType == typeof(SkipSessionHeadersFilter))
          || descriptor.MethodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(SkipSessionHeadersFilter))
      )
      {
          return;
      }

      context.HttpContext.Request.Headers.TryGetValue("X-SessionID", out var sessionId);

      if (sessionId.Count == 0)
      {
        // TODO: Swap with custom lib exception that supports custom markers for various service consumption
        throw new Exception("Session ID is missing");
      }

      if (!Guid.TryParse(sessionId, out var s))
      {
        throw new Exception("Session ID could not be parsed");
      }

      // TODO: Swap for client call
      try {
        if (_sessionService.Find(s) == null)
        {
          throw new Exception();
        }
      }
      catch (Exception)
      {
        throw new Exception($"Session not found for Session ID, {s}");
      };
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
      // Do nothing
    }
  }

  public class SkipSessionHeadersFilter : Attribute { }
}