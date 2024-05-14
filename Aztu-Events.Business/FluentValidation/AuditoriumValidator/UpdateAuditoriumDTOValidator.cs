using Aztu_Events.Entities.DTOs.AudutoriumDTOs;
using FluentValidation;
using System.Globalization;

namespace Aztu_Events.Business.FluentValidation.AuditoriumValidator
{
    public class UpdateAuditoriumDTOValidator : AbstractValidator<UpdateAuditoriumDTO>
    {
        public UpdateAuditoriumDTOValidator(string culture)
        {
            RuleFor(x => x.AuditoriumId)
                .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("AuditoriumIdIsRequird", new CultureInfo(culture)));

            RuleFor(x => x.AudutoriyaNumber)
          .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("AuditoriumNumberIsRequird", new CultureInfo(culture)))
         //.NotNull().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("AuditoriumNumberIsRequird", new CultureInfo(culture)))
         .MinimumLength(3).MaximumLength(10).WithMessage(ValidatorOptions.Global.LanguageManager.GetString("AuditoriumNumberLength", new CultureInfo(culture)));

            RuleFor(x => x.AuditoriumCapacity)
               // .Must(day => day != default)
               //  .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("AuditoriumCapacityIsRequird", new CultureInfo(culture)))
               .Must(day => day >= 0)
                    .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("AuditoriumCapacityIsNull", new CultureInfo(culture)));

        }
    }
}
