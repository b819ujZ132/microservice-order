using Microsoft.Extensions.Logging;
using System;
using order.DAL;
using order.DomainModels;

// TODO: Switch to consumption of real session microservice client instead of a direct database query implementation
namespace order.Services
{
  public class SessionService
  {
    private readonly ILogger<SessionService> _logger;
    private readonly ISessionRepository _sessionRepository;
    public Session Session { get; private set; }

    public SessionService(
      ILogger<SessionService> logger,
      ISessionRepository sessionRepository
    )
    {
      _logger = logger;
      _sessionRepository = sessionRepository;
    }

    public Session Find(Guid id)
    {
      return Session = _sessionRepository.Find(id);
    }
  }
}