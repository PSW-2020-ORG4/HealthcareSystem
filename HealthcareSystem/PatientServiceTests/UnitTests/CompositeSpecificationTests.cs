using Moq;
using PatientService.Model.Specification;
using Xunit;

namespace PatientServiceTests.UnitTests
{
    public class CompositeSpecificationTests
    {
        [Theory]
        [InlineData(true, true, true, LogicalOperator.And)]
        [InlineData(true, false, false, LogicalOperator.And)]
        [InlineData(false, true, false, LogicalOperator.And)]
        [InlineData(false, false, false, LogicalOperator.And)]
        [InlineData(true, true, true, LogicalOperator.Or)]
        [InlineData(true, false, true, LogicalOperator.Or)]
        [InlineData(false, true, true, LogicalOperator.Or)]
        [InlineData(false, false, false, LogicalOperator.Or)]
        public void LogicalOperationTests(bool left, bool right, bool expected, LogicalOperator logOp)
        {
            var leftMock = new Mock<CompositeSpecification<bool>>();
            leftMock.Setup(m => m.IsSatisfiedBy(true)).Returns(left);

            var rightMock = new Mock<CompositeSpecification<bool>>();
            rightMock.Setup(m => m.IsSatisfiedBy(true)).Returns(right);

            ISpecification<bool> uut = leftMock.Object.BinaryOperation(logOp, rightMock.Object);
            bool result = uut.IsSatisfiedBy(true);

            Assert.Equal(result, expected);
        }
    }
}
