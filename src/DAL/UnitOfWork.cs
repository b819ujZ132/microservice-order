using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace order.DAL
{
  class UnitOfWork : IUnitOfWork
  {
    private readonly DbContext _dbContext;

    public UnitOfWork(DbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public void Commit()
    {
      _dbContext.SaveChanges();
    }
  }
}