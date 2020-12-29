using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Model.Memento;

namespace UserService.Model
{
    public class MaliciousAction : IOriginator<MaliciousActionMemento>
    {
        private int Id { get; set; }
        private MaliciousActionType Type { get; set; }
        private DateTime TimeStamp { get; set; }

        public MaliciousAction(int id, MaliciousActionType type, DateTime timeStamp)
        {
            Id = id;
            Type = type;
            TimeStamp = timeStamp;
            Validate();
        }

        public MaliciousAction(MaliciousActionMemento memento)
        {
            Id = memento.Id;
            Type = memento.Type;
            TimeStamp = memento.TimeStamp;
            Validate();
        }

        public MaliciousActionMemento GetMemento()
        {
            return new MaliciousActionMemento()
            {
                Id = Id,
                TimeStamp = TimeStamp,
                Type = Type
            };
        }

        private void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
