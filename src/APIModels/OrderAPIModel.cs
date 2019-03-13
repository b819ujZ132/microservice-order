using System;
using System.Collections.Generic;

namespace order.APIModels
{
  public sealed class OrderAPIModel
  {
    public Guid OrderId;
    public Guid SessionId;
    public List<OrderStatusAPIModel> StatusHistory;
  }
}