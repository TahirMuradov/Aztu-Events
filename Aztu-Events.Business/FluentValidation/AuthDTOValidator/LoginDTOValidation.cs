using Aztu_Events.Entities.DTOs.AuthDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Aztu_Events.Business.FluentValidation.AuthDTOValidator
{
    public class LoginDTOValidation:AbstractValidator<LoginDTO>
    {
     
        public LoginDTOValidation(string culture)
        {
            RuleFor(x => x.Email)
              .NotEmpty().NotNull().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("EmailIsRequird", new CultureInfo(culture)));
            

            RuleFor(x => x.Password)
               .NotEmpty().NotNull().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("PasswordIsRequird", new CultureInfo(culture)));
    
        }
    }
}
