using System;

namespace UserService.Model.Memento
{
    public class MaliciousActionMemento : IMemento
    {
        public int Id { get; set; }
        public MaliciousActionType Type { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
