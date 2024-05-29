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
    public class AddEventTypeDTOValidation:AbstractValidator<AddEventTypeDTO>
    {
        public AddEventTypeDTOValidation(string culture)
        {
            RuleFor(dto => dto.LangCode)
          .Must(langCodeList => langCodeList == null || !langCodeList.Any(x => x == null)).WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LangCodeIsRequired", new CultureInfo(culture)))
           .NotNull().NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LangCodeIsRequired", new CultureInfo(culture)));
            RuleFor(dto => dto.Content)
                 .Must(langCodeList => langCodeList == null || !langCodeList.Any(x => x == null)).WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ContentIsRequired", new CultureInfo(culture)))
            .NotNull()
            .NotEmpty()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ContentIsRequired", new CultureInfo(culture)));

            RuleFor(dto => dto.LangCode.Count)
            .Equal(dto => dto.Content.Count)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LangCodeCountTest", new CultureInfo(culture)));
        }
    }
}
