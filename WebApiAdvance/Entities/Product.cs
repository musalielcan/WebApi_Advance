using WebApiAdvance.Entities.Common;

namespace WebApiAdvance.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public int Stock { get; set; }
        public string SKU { get; set; }
        public string Barcode { get; set; }


        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
