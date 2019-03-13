using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using order.DomainModels;

namespace order.DAL
{
  public class SessionRepository : ISessionRepository
  {
    private readonly SessionContext _dbContext;
    public SessionRepository(SessionContext dbContext)
    {
      if (dbContext?.Session == null)
      {
        throw new Exception("Order database context cannot be null");
      }

      _dbContext = dbContext;
    }

    public Session Find(Guid id)
    {
      return _dbContext.Session.Find(id);
    }
  }
}