using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace order.DomainModels.Core
{
  public abstract class Entity
  {
    /// <summary>
    /// Gets the ID associated with the domain object, needed for comparing
    /// </summary>
    [NotMapped]
    public abstract Guid Id { get; }
    [NotMapped]
    public List<IDomainEvent> Events { get; }

    /// <summary>
    /// Empty constructor for EF
    /// </summary>
    protected Entity() { }

    public override bool Equals(object obj)
    {
      if (obj == null || !(obj is Entity))
        return false;

      if (Object.ReferenceEquals(this, obj))
        return true;

      if (this.GetType() != obj.GetType())
        return false;

      Entity item = (Entity)obj;

      return item.Id == this.Id;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}