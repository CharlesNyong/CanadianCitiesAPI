using System.Text.Json.Serialization;

namespace CanadianCitiesAPI.Model
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public string ProvinceId { get; set; } = null;

        [JsonIgnore]
        public virtual Province? Province { get; set; }
    }
}
