namespace PatientService.CustomException
{
    public class NotFoundException : PatientServiceException
    {
        public NotFoundException() : base() { }

        public NotFoundException(string message) : base(message) { }
    }
}
