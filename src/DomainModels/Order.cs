using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using order.DomainModels.Core;

namespace order.DomainModels
{
  public sealed class Order : Entity
  {
    public override Guid Id => OrderId;
    public Guid OrderId { get; private set; }
    public Guid SessionId;
    public List<OrderStatus> StatusHistory { get; private set; }

    [NotMapped]
    public DateTime DateAdded { get; private set; }
    [NotMapped]
    public DateTime DateTimeAdded { get; private set; }

    private Order()
    {
      // Leave empty to prevent construction
    }

    public static Order Create(
      Guid sessionId
    )
    {
      if(String.IsNullOrEmpty(sessionId.ToString()))
      {
        throw new Exception("Session ID is required to create an order.");
      }

      return new Order()
      {
        SessionId = sessionId,
        StatusHistory = new List<OrderStatus>()
      };
    }

    public void MarkIncomplete()
    {
      StatusHistory.Add(OrderStatus.Create(
        Id
      ));
    }

    public void MarkComplete()
    {
      StatusHistory.Add(OrderStatus.Create(
        Id,
        OrderStatusEnum.COMPLETE
      ));
    }

    public void MarkPending()
    {
      StatusHistory.Add(OrderStatus.Create(
        Id,
        OrderStatusEnum.PENDING
      ));
    }

    public void MarkShipped()
    {
      StatusHistory.Add(OrderStatus.Create(
        Id,
        OrderStatusEnum.SHIPPED
      ));
    }

    public void MarkFulfilled()
    {
      StatusHistory.Add(OrderStatus.Create(
        Id,
        OrderStatusEnum.FULFILLED
      ));
    }

    public void MarkCanceled()
    {
      StatusHistory.Add(OrderStatus.Create(
        Id,
        OrderStatusEnum.CANCELED
      ));
    }
  }
}