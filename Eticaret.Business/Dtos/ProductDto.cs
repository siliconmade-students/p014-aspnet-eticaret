using System.Text.Json.Serialization;

namespace Eticaret.Business.Dtos
{
    public class ProductDto
    {
        // API Response içerisinde gizlemeyi sağlar
        //[JsonIgnore]
        public int Id { get; set; }

        public string ProductName { get; set; }
        public string Description { get; set; }

        // API Response içerisinde Kolon ismini değiştirmeyi sağlar.
        [JsonPropertyName("productPrice")]
        public decimal Price { get; set; }

        public List<string> ImagePaths { get; set; }
    }
}
