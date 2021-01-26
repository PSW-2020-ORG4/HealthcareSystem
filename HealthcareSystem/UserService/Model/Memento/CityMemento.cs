namespace UserService.Model.Memento
{
    public class CityMemento : IMemento
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CountryMemento Country { get; set; }
    }
}
