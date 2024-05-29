using Aztu_Events.Entities.DTOs.EventTypeDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aztu_Events.Business.FluentValidation.EventTypeDTOValidation
{
    public class UpdateEventTypeDTOValidation:AbstractValidator<UpdateEventTypeDTO>
    {
        public UpdateEventTypeDTOValidation(string culture)
        {
            RuleFor(entry => entry.Id).NotNull().NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("IdIsRequired", new CultureInfo(culture)));
            RuleFor(dto => dto.LangCode).NotNull().NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LangCodeIsRequired", new CultureInfo(culture)));
            RuleFor(dto => dto.Content).NotNull().NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ContentIsRequired", new CultureInfo(culture)));

            RuleFor(dto => dto.LangCode.Count).Equal(dto => dto.Content.Count)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LangCodeCountTest", new CultureInfo(culture)));
        }
    }
}
