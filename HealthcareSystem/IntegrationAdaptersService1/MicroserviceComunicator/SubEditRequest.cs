namespace IntegrationAdaptersService1.MicroserviceComunicator
{
    internal class SubEditRequest
    {
        public string ExOld { get; set; }
        public bool SubOld { get; set; }
        public string ExNew { get; set; }
        public bool SubNew { get; set; }

        public SubEditRequest(string exOld, bool subOld, string exNew, bool subNew)
        {
            ExOld = exOld;
            SubOld = subOld;
            ExNew = exNew;
            SubNew = subNew;
        }
    }
}