using Microsoft.EntityFrameworkCore;
using WebApiAdvance.DAL.EFCore;

namespace WebApiAdvance.Helpers
{
    public class ProductUniqueChecker: IProductUniqueChecker
    {
        private readonly ApiDbContext _context;

        public ProductUniqueChecker(ApiDbContext context)
        {
            _context = context;
        }

        public bool BeUniqueSKUSync(string sku)
        {
            return !_context.Products.AnyAsync(p => p.SKU == sku).GetAwaiter().GetResult();
        }

        public bool BeUniqueBarcodeSync(string barcode)
        {
            return !_context.Products.AnyAsync(p => p.Barcode == barcode).GetAwaiter().GetResult();
        }
    }
}
