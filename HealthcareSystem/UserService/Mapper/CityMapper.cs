using UserService.DTO;
using UserService.Model;

namespace UserService.Mapper
{
    public static class CityMapper
    {
        public static CityDTO ToCityDTO(this City city)
        {
            var memento = city.GetMemento();
            return new CityDTO()
            {
                Id = memento.Id,
                Name = memento.Name,
                CountryId = memento.Country.Id
            };
        }
    }
}
