namespace UserService.ActionsBenefits
{
    public class ActionBenefit
    {
        public int Id { get; }
        public string PharmacyName { get; }
        public string Subject { get; }
        public string Message { get; }

        public ActionBenefit(int id, string pharmacyName, string subject, string message)
        {
            Id = id;
            PharmacyName = pharmacyName;
            Subject = subject;
            Message = message;
        }
    }
}