using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.SearchSpecification
{
    public abstract class CompositeSpecification<T> : ISpecification<T>
    {
        public ISpecification<T> BinaryOperation(LogicalOperator op, ISpecification<T> specification)
        {
            switch (op)
            {
                case LogicalOperator.And:
                    return new AndSpecification<T>(this, specification);
                case LogicalOperator.Or:
                    return new OrSpecification<T>(this, specification);
                default:
                    return this;
            }
        }

        public abstract bool IsSatisfiedBy(T o);
    }
}
