using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.CustomException;

namespace UserService.Model
{
    public class MaliciousAction
    {
        private MaliciousActionType Type { get; }
        private DateTime TimeStamp { get; }

        public MaliciousAction(MaliciousActionType type, DateTime timeStamp)
        {
            Type = type;
            TimeStamp = timeStamp;
            Validate();
        }

        private void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
