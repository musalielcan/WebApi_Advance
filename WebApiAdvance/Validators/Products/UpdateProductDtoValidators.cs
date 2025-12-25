using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WebApiAdvance.DAL.EFCore;
using WebApiAdvance.Entities.DTOs.Products;
using WebApiAdvance.Helpers;

namespace WebApiAdvance.Validators.Products
{
    public class UpdateProductDtoValidators:AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidators(IProductUniqueChecker checker)
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Ad boş ola bilməz.")
                .NotNull().WithMessage("Ad daxil edin.")
                .MaximumLength(200).WithMessage("Məhsulun adı 200 simvoldan çox olmamalıdır.");

            RuleFor(c => c.Description)
                .MaximumLength(1000).WithMessage("Təsvir 1000 simvoldan çox olmamalıdır.");

            RuleFor(c => c.Price)
                .NotEmpty().WithMessage("Qiymət boş ola bilməz.")
                .NotNull().WithMessage("Qiyməti daxil edin.")
                .GreaterThanOrEqualTo(0).WithMessage("Qiymər mənfi ola bilməz.")
                .PrecisionScale(18, 2, false).WithMessage("Qiymət maksimum 18 rəqəmdən, maksimum 2 onluq yerdən ibarət olmalıdır.");

            RuleFor(c => c.DiscountPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Endirimli Qiymər mənfi ola bilməz.")
                .LessThan(c => c.Price)
                .When(c => c.DiscountPrice.HasValue).WithMessage("Endirimli Qiymət Qiymətdən az olmalıdır.")
                .PrecisionScale(18, 2, false).WithMessage("Endirimli Qiymət maksimum 18 rəqəmdən, maksimum 2 onluq yerdən ibarət olmalıdır.");

            RuleFor(c => c.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Stock sıfırdan böyük və ya bərabər olmalıdır.");

            RuleFor(c => c.SKU)
                .NotEmpty().WithMessage("SKU boş ola bilməz.")
                .MinimumLength(3).WithMessage("SKU ən azı 3 simvoldan ibarət olmalıdır.")
                .MaximumLength(20).WithMessage("SKU 20 simvoldan çox olmamalıdır.")
                .Matches(@"^[A-Z0-9\-]+$").WithMessage("SKU-da yalnız böyük hərflər, rəqəmlər və defis olmalıdır.")
                .Must(checker.BeUniqueSKUSync).WithMessage("Bu SKU artıq mövcuddur");

            RuleFor(c => c.Barcode)
                .NotEmpty().WithMessage("Barkod boş ola bilməz.")
                .Matches(@"^\d+$").WithMessage("Barkod yalnız rəqəm simvollarından ibarət olmalıdır.")
                .Must(b => b.Length == 8 || b.Length == 12 || b.Length == 13).WithMessage("Barkod 8, 12 və ya 13 rəqəmdən ibarət olmalıdır.")
                .Must(checker.BeUniqueBarcodeSync).WithMessage("Bu barkod artıq mövcuddur.");
        }
    }
}
