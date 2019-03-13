using System;

namespace order.APIModels
{
  public sealed class OrderStatusAPIModel
  {
    public Guid OrderId;
    public string Status;
  }
}