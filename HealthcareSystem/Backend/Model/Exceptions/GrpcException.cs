
namespace Backend.Model.Exceptions
{
    public class GrpcException : HealthClinicException
    {
        public GrpcException() : base() { }
        public GrpcException(string message) : base(message) { }
    }
}
