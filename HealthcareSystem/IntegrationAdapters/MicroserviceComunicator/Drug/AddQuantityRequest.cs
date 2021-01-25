namespace IntegrationAdapters.MicroserviceComunicator
{
    internal class AddQuantityRequest
    {
        public string Code { get; set; }
        public int Quantity { get; set; }

        public AddQuantityRequest(string code, int quantity)
        {
            Code = code;
            Quantity = quantity;
        }
    }
}