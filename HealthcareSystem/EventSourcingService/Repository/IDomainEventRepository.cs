using EventSourcingService.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EventSourcingService.Repository
{
    public interface IDomainEventRepository<T> where T : DomainEvent
    {
        IEnumerable<T> GetAll();

        IEnumerable<T> GetAll(Expression<Func<T, bool>> condition);

        T Add(T domainEvent);
    }
}
