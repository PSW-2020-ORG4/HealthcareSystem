using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Model.Users;

namespace Backend.Service.SendingMail
{
    public interface IMailService
    {
        Task SendWelcomeEmailAsync(WelcomeRequest request);
    }
}
