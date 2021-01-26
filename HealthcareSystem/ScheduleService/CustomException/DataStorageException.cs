﻿namespace ScheduleService.CustomException
{
    public class DataStorageException : ScheduleServiceException
    {
        public DataStorageException() : base() { }
        public DataStorageException(string message) : base(message) { }
    }
}
