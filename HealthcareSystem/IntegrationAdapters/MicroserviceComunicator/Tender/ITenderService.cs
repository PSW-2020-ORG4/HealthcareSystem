using Backend.Model.DTO;
using Backend.Model.Pharmacies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationAdapters.MicroserviceComunicator
{
    public interface ITenderService
    {
        public Task<Tender> GetTender(int tenderId);
        public Task<Tender> GetTenderByMessage(int tenderId);
        public Task<List<Tender>> GetAllTenders();
        public Task<bool> CreateTender(Tender tender);
        public Task<bool> CloseTender(int tenderId);
        public Task<List<TenderDrugDTO>> GetDrugsByTender(int tenderId);
        public Task<TenderMessage> GetMessage(int messageId);
        public Task<List<TenderMessage>> GetAllMessages(int tenderId);
        public Task<TenderMessage> GetAcceptedMessage(int tenderId);
        public Task<bool> AcceptMessage(int messageId);

    }
}
