using FluentValidation;
using WebApiAdvance.Entities.DTOs.Categories;

namespace WebApiAdvance.Validators.Categories
{
    public class UpdateCategoryDtoValidators:AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidators()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Ad boş ola bilməz.")
                .NotNull().WithMessage("Ad daxil edin.")
                .MaximumLength(100).WithMessage("Kateqoriya adı 100 simvoldan çox olmamalıdır.");

            RuleFor(c => c.Description)
                .MaximumLength(1000).WithMessage("Təsvir 1000 simvoldan çox olmamalıdır.");
        }
    }
}
