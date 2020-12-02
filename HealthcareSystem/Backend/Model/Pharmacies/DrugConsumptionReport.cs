namespace Backend.Model.Pharmacies
{
    public class DrugConsumptionReport
    {
        public int DrugId { get; set; }
        public string DrugName { get; set; }
        public int Quantity { get; set; }

        public DrugConsumptionReport(int drugId, string drugName, int quantity)
        {
            DrugId = drugId;
            DrugName = drugName;
            Quantity = quantity;
        }
    }
}