using Aztu_Events.Entities.DTOs.UserDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Business.FluentValidation.UserDTOValidator
{
    public class UserUpdateDTOValidator:AbstractValidator<UserUpdateDTO>
    {
        public UserUpdateDTOValidator(string culture)
        {
            RuleFor(dto => dto.UserId)
                .NotNull().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("UserIdIsRequird", new CultureInfo(culture)))
                .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("UserIdIsRequird",new CultureInfo(culture)));
            RuleFor(dto => dto.Firstname)
                .NotNull().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("FirstNameIsRequird", new CultureInfo(culture)))
                .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("FirstNameIsRequird", new CultureInfo(culture)));
            RuleFor(dto => dto.UserName)
                .NotNull().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("UserNameIsRequird", new CultureInfo(culture)))
                .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("UserNameIsRequird", new CultureInfo(culture)));
            RuleFor(dto => dto.Lastname)
                .NotNull().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LastNameIsRequird", new CultureInfo(culture)))
                .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LastNameIsRequird", new CultureInfo(culture)));
            RuleFor(dto => dto.PhoneNumber)
                .NotNull().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("PhoneNumberIsRequird", new CultureInfo(culture)))
                .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("PhoneNumberIsRequird", new CultureInfo(culture)))
                .Matches("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$").WithMessage(ValidatorOptions.Global.LanguageManager.GetString("PhoneNumberIsFormat", new CultureInfo(culture)));
            RuleFor(dto => dto.Password)
                .NotNull().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("PasswordIsRequird", new CultureInfo(culture)))
                .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("PasswordIsRequird", new CultureInfo(culture)));
            RuleFor(dto => dto.ConfirmPassword)
                .NotNull().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ConfirmPasswordIsRequird", new CultureInfo(culture)))
                .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ConfirmPasswordIsRequird", new CultureInfo(culture)))
               .Equal(dto => dto.Password).WithMessage(ValidatorOptions.Global.LanguageManager.GetString("PasswordChecked", new CultureInfo(culture)));
            RuleFor(dto => dto.NewPassword).Equal(dto => dto.NewConfirmPassword).When(dto => !string.IsNullOrEmpty(dto.NewPassword))
                                            .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("NewPasswordChecked", new CultureInfo(culture)));

        }
    }
}
