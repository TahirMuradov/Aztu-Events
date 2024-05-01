using Aztu_Events.Entities.DTOs.AudutoriumDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Business.FluentValidation.AuditoriumValidator
{
    public class UpdateAuditoriumDTOValidator:AbstractValidator<UpdateAuditoriumDTO>
    {
        public UpdateAuditoriumDTOValidator(string culture)
        {
            RuleFor(x => x.AuditoriumId)
                .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("AuditoriumIdIsRequird", new CultureInfo(culture)));

            RuleFor(x => x.AudutoriyaNumber)
                .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("AuditoriumNumberIsRequird", new CultureInfo(culture)))
               .MinimumLength(3) .MaximumLength(10).WithMessage(ValidatorOptions.Global.LanguageManager.GetString("AuditoriumNumberLength", new CultureInfo(culture)))          ;

            RuleFor(x => x.AuditoriumCapacity)
                .GreaterThan(0).WithMessage(ValidatorOptions.Global.LanguageManager.GetString("AuditoriumCapacityIsNull", new CultureInfo(culture)))
            ;
        }
    }
}
