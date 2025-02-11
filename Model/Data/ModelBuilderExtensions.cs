using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

namespace CanadianCitiesAPI.Model.Data
{
    // extends the functionality of modelBuilder
    public static class ModelBuilderExtensions
    {
        // want flexibility with the type, so using dynamic
        private static dynamic jsonContentArray;

        public static void LoadJsonData() 
        {
            using (StreamReader r = new StreamReader("data.json"))
            {
                string json = r.ReadToEnd();
                jsonContentArray = JsonConvert.DeserializeObject<dynamic>(json);
            }
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            int id = 1;
            LoadJsonData();
            
            if (jsonContentArray == null || jsonContentArray.Count == 0) 
            {
                throw new Exception("Failed to seed data due to inability to get data from source.");
            }


            foreach (JObject data in jsonContentArray) 
            {
                if (data != null) 
                {
                    modelBuilder.Entity<Province>().HasData(
                        new Province { Id = (string)data["Id"], Name = data["Province"].ToString() }
                    );
                }
            }

            foreach (JObject data in jsonContentArray)
            {
                if (data != null)
                {
                    foreach (var city in data["Cities"])
                    {
                        modelBuilder.Entity<City>().HasData(
                            new City { Id = id, Name = (string)city, ProvinceId = (string)data["Id"] }
                        );
                        id++;
                    }
                }
            }
        }
    }
}
