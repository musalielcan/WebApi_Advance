using Microsoft.EntityFrameworkCore;
using System;
using WebApiAdvance.DAL.EFCore;

namespace WebApiAdvance.Helpers
{
    public class ProductUniqueChecker : IProductUniqueChecker
    {
        private readonly ApiDbContext _context;

        public ProductUniqueChecker(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsSkuUniqueAsync(string sku, Guid? productId = null)
        {
            return !await _context.Products.AnyAsync(p =>
                p.SKU == sku && (!productId.HasValue || p.Id != productId));
        }

        public async Task<bool> IsBarcodeUniqueAsync(string barcode, Guid? productId = null)
        {
            return !await _context.Products.AnyAsync(p =>
                p.Barcode == barcode && (!productId.HasValue || p.Id != productId));
        }
    }
}
