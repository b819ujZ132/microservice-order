using System;
using order.DomainModels;

namespace order.DAL
{
  public interface IOrderRepository
  {
    Order Find(Guid id);
    Order Insert(Order order);
    Order Update(Order order);
  }
}