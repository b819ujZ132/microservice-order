using System;
using order.DomainModels;

namespace order.DAL
{
  public interface ISessionRepository
  {
    Session Find(Guid id);
  }
}