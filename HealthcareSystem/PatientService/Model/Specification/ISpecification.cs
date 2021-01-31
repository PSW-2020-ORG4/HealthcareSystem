namespace PatientService.Model.Specification
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T o);
        ISpecification<T> BinaryOperation(LogicalOperator op, ISpecification<T> specification);
    }
}
