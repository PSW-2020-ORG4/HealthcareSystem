using Backend.Model.Enums;
using Model.Manager;

namespace PatientWebAppTests.CreateObjectsForTests
{
    public class CreateRoom
    {
        public Room CreateValidTestObject()
        {
            return new Room(number: 1, typeOfUsage: TypeOfUsage.CONSULTING_ROOM, capacity: 1, occupation: 1, renovation: false);
        }
    }
}
