namespace IntegrationAdapters.MicroserviceComunicator
{
    internal class SetPublicRequest
    {
        public int Id { get; set; }
        public bool IsPublic { get; set; }

        public SetPublicRequest(int id, bool isPublic)
        {
            Id = id;
            IsPublic = isPublic;
        }
    }
}