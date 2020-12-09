using Shopping.Models.Enums;

namespace Shopping.Models.Models
{
    public class ProductModel: BaseModel
    {
        public string Name { get; set; }

        public float Price { get; set; }

        public string Thumbnail { get; set; }

        public ProductsEnum Category { get; set; }
    }
}
