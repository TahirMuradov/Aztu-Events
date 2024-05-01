using Aztu_Events.Entities.DTOs.AudutoriumDTOs;
using FluentValidation;
using System.Globalization;

namespace Aztu_Events.Business.FluentValidation.AuditoriumValidator
{
    public class AddAuditoriumDTOValidator:AbstractValidator<AddAuditoriumDTO>
    {
        public AddAuditoriumDTOValidator(string culture)
        {
            RuleFor(x => x.AudutoriyaNumber)
            .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("AuditoriumNumberIsRequird", new CultureInfo(culture)))
           .MinimumLength(3) .MaximumLength(10).WithMessage(ValidatorOptions.Global.LanguageManager.GetString("AuditoriumNumberLength", new CultureInfo(culture)));

            RuleFor(x => x.AuditoryCapacity)
                .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("AuditoriumCapacityIsRequird", new CultureInfo(culture)))
                .GreaterThan(0).WithMessage(ValidatorOptions.Global.LanguageManager.GetString("AuditoriumCapacityIsNull", new CultureInfo(culture)));
                
        }
    }
}
