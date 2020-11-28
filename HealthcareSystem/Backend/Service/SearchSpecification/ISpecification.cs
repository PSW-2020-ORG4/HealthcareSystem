using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.SearchSpecification
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T o);
        ISpecification<T> BinaryOperation(LogicalOperator op, ISpecification<T> specification);
    }
}
