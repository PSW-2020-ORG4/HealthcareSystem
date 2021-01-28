namespace PatientService.CustomException
{
    public class DataStorageException : PatientServiceException
    {
        public DataStorageException() : base() { }

        public DataStorageException(string message) : base(message) { }
    }
}
