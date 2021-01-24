using System.Threading.Tasks;

namespace IntegrationAdaptersPharmacySystemService.MicroserviceComunicator
{
    public interface IActionBenefitService
    {
        public Task<bool> SubscriptionEdit(string exOld, bool subOld, string exNew, bool subNew);
        public Task<bool> Subscribe(string exchangeName);
    }
}
