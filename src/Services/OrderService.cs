using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using order.DAL;
using order.DomainModels;

namespace order.Services {
  public class OrderService
  {
    private readonly ILogger<OrderService> _logger;
    private readonly IOrderRepository _orderRepository;
    public OrderService(
      ILogger<OrderService> logger,
      IOrderRepository orderRepository
    )
    {
      _logger = logger;
      _orderRepository = orderRepository;
    }

    public Order Find(Guid id)
    {
      return _orderRepository.Find(id);
    }

    public Order Create(
      Guid sessionId
    )
    {
      var order = Order.Create(
        sessionId
      );

      var entity = _orderRepository.Insert(order);

      return entity;
    }

    public Order Status(
      Guid orderId
    )
    {
      return _orderRepository.Find(orderId);
    }

    public Order Submit(
      Guid sessionId,
      Guid orderId
    )
    {
      var order = _orderRepository.Find(orderId);
      order.MarkComplete();
      
      return order;
    }
  }
}