using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService
{
    interface ITemplateRepository
    {
        Template Get(String id);
    }
}
