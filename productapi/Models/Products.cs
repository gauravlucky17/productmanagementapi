using System.Text.Json.Serialization;

namespace productapi.Models
{
    public class Products
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
        public DateTime DateTime { get; set; }
    }
}
