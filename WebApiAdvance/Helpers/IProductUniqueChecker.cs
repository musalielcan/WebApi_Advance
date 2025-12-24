namespace WebApiAdvance.Helpers
{
    public interface IProductUniqueChecker
    {
        Task<bool> IsSkuUniqueAsync(string sku, Guid? productId = null);
        Task<bool> IsBarcodeUniqueAsync(string barcode, Guid? productId = null);
    }
}
