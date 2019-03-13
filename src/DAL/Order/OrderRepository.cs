using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;
using order.DomainModels;

namespace order.DAL
{
  public class OrderRepository : IOrderRepository
  {
    private readonly OrderContext _dbContext;
    public OrderRepository(OrderContext dbContext)
    {
      if (dbContext?.Order == null)
      {
        throw new Exception("Order database context cannot be null");
      }

      _dbContext = dbContext;
    }

    public Order Find(Guid key)
    {
      return _dbContext.Order.FirstOrDefault(o => o.Id == key);
    }

    public Order Insert(Order order)
    {
      // Guard if _dbContext.Order does not exist on exchangable context class

      var change = _dbContext.Order.Add(order);
      return change.Entity;
    }

    public Order Update(Order order)
    {
      var change = _dbContext.Order.Update(order);
      return change.Entity;
    }
  }
}