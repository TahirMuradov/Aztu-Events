using Aztu_Events.Entities.DTOs.Conferences;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Business.FluentValidation.ConferanceValidator
{
    public class ConferenceUpdateDTOValidator:AbstractValidator<ConferenceUpdateDto>
    {
        public ConferenceUpdateDTOValidator(string culture)
        {
            RuleFor(dto => dto.ConferenceName)
          .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ConferenceNameIsRequird", new CultureInfo(culture)))
          .Must((dto, conferenceNames) => conferenceNames.Count == dto.LangCode.Count)
              .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ConferenceNameCount", new CultureInfo(culture)));

            RuleFor(dto => dto.ConferenceContent)
                .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ConferenceContentIsRequird", new CultureInfo(culture)))
                .Must((dto, conferenceContents) => conferenceContents.Count == dto.LangCode.Count)
                    .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ConferenceContentCount", new CultureInfo(culture)));

            RuleFor(dto => dto.specialGuestsEmail)
                     .Must((dto, emails) => dto.specialGuestsName == null || emails?.Count == dto.specialGuestsName.Count)
                    .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("SepecialGuestEmailCount", new CultureInfo(culture)))
                ;

            RuleFor(dto => dto.specialGuestsName)
                .Must((dto, names) => dto.specialGuestsEmail == null || names?.Count == dto.specialGuestsEmail.Count)
                    .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("SepecialGuestNameCount", new CultureInfo(culture)))
                ;

            RuleFor(dto => dto.Day)
                .Must(day => day != default).WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ConferenceDayIsDefault", new CultureInfo(culture)))
                .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ConferenceDayIsRequird", new CultureInfo(culture)));

            RuleFor(dto => dto.StartedDate)
                 .Must(startTime => startTime != default).WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ConferenceStartedDateIsDefault", new CultureInfo(ValidatorOptions.Global.LanguageManager.GetString("ConferenceDayIsrequird", new CultureInfo(culture)))))
                .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ConferenceStartedDateIsrequird", new CultureInfo(culture)));

            RuleFor(dto => dto.EndDate)
                .Must(endTime => endTime != default).WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ConferenceEndDateIsDefault", new CultureInfo(culture)))
                .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ConferenceEndDateIsRequird", new CultureInfo(culture)))
                .GreaterThan(dto => dto.StartedDate).WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ConferenceEndDateIsTest", new CultureInfo(culture)));



            

            RuleFor(dto => dto.AudutoriumId)
               .NotNull().NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("AuditoriumIdIsRequird", new CultureInfo(culture)));
        }
    }
}
