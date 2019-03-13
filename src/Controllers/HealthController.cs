using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;
using order.DomainModels;
using order.Filters;

namespace order.Controllers
{
  [SkipSessionHeadersFilter]
  public class HealthController : Controller
  {
    private readonly ILogger<HealthController> _logger;

    public HealthController(
      ILogger<HealthController> logger
    )
    {
      _logger = logger;
    }

    /// <summary>
    /// Check health
    /// </summary>
    /// <returns>Health status</returns>
    /// <response code="200">If application is healthy</response>
    /// <response code="500">If application is unhealthy</response>
    [HttpGet]
    [HttpHead]
    [Route("health")]
    [ProducesResponseType(typeof(HealthCheck), 200)]
    [ProducesResponseType(typeof(HealthCheck), 500)]
    public IActionResult Health()
    {
      // TODO: Open database, cache, other clients, and logger connection. If failure, return 500.
      return Ok(new HealthCheck());
    }
  }
}
