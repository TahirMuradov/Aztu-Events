using Aztu_Events.Entities.DTOs.AuthDTOs;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Business.FluentValidation.AuthDTOValidator
{
    public class RegisterDTOValidator:AbstractValidator<RegisterDTO>
    {
        public RegisterDTOValidator(string currentCulture)
        {
            RuleFor(x => x.Firstname)
    .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("FirstnameRequired", new CultureInfo(currentCulture)));

            RuleFor(x => x.Lastname)
                .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LastnameRequired", new CultureInfo(currentCulture)));

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("EmailRequired", new CultureInfo(currentCulture)))
                .EmailAddress().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("InvalidEmailFormat", new CultureInfo(currentCulture)));

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("PhoneNumberRequired", new CultureInfo(currentCulture)))
                .Matches("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$").WithMessage(ValidatorOptions.Global.LanguageManager.GetString("InvalidPhoneNumberFormat", new CultureInfo(currentCulture)));

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("PasswordRequired", new CultureInfo(currentCulture)));

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ConfirmPasswordRequired", new CultureInfo(currentCulture)))
                .Equal(x => x.Password).WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ConfirmPasswordMismatch", new CultureInfo(currentCulture)));

        }
    }
}
