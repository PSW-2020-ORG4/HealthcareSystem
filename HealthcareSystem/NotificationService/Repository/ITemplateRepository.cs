using System;

namespace NotificationService
{
    interface ITemplateRepository
    {
        Template Get(String id);
    }
}
