using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using order.DomainModels.Core;

namespace order.DomainModels
{
  public sealed class OrderStatus : Entity
  {
    public override Guid Id => StatusId;
    public Guid StatusId { get; private set; }
    public Guid OrderId { get; private set; }
    public OrderStatusEnum StatusEnum { get; private set; }

    [NotMapped]
    public DateTime DateAdded { get; private set; }
    [NotMapped]
    public  DateTime DateTimeAdded { get; private set; }

    private OrderStatus()
    {
      // Leave empty to prevent construction
    }

    public static OrderStatus Create(Guid orderId, OrderStatusEnum status = OrderStatusEnum.INCOMPLETE)
    {
      if (String.IsNullOrEmpty(orderId.ToString()))
      {
        throw new Exception("Order ID is required on order status create.");
      }

      return new OrderStatus()
      {
        OrderId = orderId,
        StatusEnum = status
      };
    }
  }

  [JsonConverter(typeof(StringEnumConverter))]
  public enum OrderStatusEnum
  {
    INCOMPLETE = 0b000,
    COMPLETE = 0b001,
    PENDING = 0b010,
    SHIPPED = 0b011,
    FULFILLED = 0b100,
    CANCELED = 0b101
  }
}