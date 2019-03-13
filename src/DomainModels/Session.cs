using System;
using order.DomainModels.Core;

namespace order.DomainModels
{
  public class Session : Entity
  {
    public override Guid Id => SessionId;
    public Guid SessionId { get; private set; }

    public static Session Create(
      Guid sessionId
    )
    {
      var s = new Session();
      s.SessionId = sessionId;

      return s;
    }
  }
}