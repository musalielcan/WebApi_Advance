namespace WebApiAdvance.Helpers
{
    public interface IProductUniqueChecker
    {
        public bool BeUniqueSKUSync(string sku);
        public bool BeUniqueBarcodeSync(string barcode);
    }
}
