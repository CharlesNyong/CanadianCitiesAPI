using System.Text.Json.Serialization;

namespace CanadianCitiesAPI.Model
{
    public class Province
    {
        public string Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public List<City> Cities { get; set; }
    }
}
