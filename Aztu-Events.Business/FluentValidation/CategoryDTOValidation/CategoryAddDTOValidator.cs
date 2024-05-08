﻿using Aztu_Events.Entities.DTOs.CategoryDTOs;
using FluentValidation;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Aztu_Events.Business.FluentValidation.CategoryDTOValidation
{
    public class CategoryAddDTOValidator:AbstractValidator<CategoryAddDTO>
    {
        public CategoryAddDTOValidator(string culture)
        {
            RuleFor(dto => dto.LangCode).NotNull().NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LangCodeIsRequired", new CultureInfo(culture)));
            RuleFor(dto => dto.Content).NotNull().NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ContentIsRequired", new CultureInfo(culture)));
        
            RuleFor(dto => dto.LangCode.Count).Equal(dto => dto.Content.Count)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LangCodeCountTest", new CultureInfo(culture)));

        }
    }
}
