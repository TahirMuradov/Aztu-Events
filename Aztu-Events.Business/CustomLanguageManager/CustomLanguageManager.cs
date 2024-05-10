using FluentValidation.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Business.CustomLanguageManager
{
    public class CustomLanguageManager : LanguageManager
    {
        public CustomLanguageManager()
        {
            #region UserValidationMessage
            AddTranslation("az", "UserIdIsRequird", "Istifadəçi İd-si Boş Ola Bilməz!.");
            AddTranslation("az", "FirstNameIsRequird", "Ad Boş Ola Bilməz!.");
            AddTranslation("az", "UserNameIsRequird", "Istifadəçi Adı Boş Ola Bilməz!.");
            AddTranslation("az", "LastNameIsRequird", "Soy Ad Boş Ola Bilməz!.");
            AddTranslation("az", "PhoneNumberIsRequird", "Əlaqə Nömrəsi Boş Ola Bilməz!.");
            AddTranslation("az", "PhoneNumberIsFormat", "Əlaqə Nömrəsi Formatı Düzgün Yazın!.");
            AddTranslation("az", "PasswordIsRequird", "Şifrə boş ola bilməz!.");
            AddTranslation("az", "ConfirmPasswordIsRequird", "Şifrənin təkrarı boş ola bilməz!.");
            AddTranslation("az", "PasswordChecked", "Şifrə ilə Şifrənin təkrarı eyni deyil!");
            AddTranslation("az", "NewPasswordChecked", "Yeni Şifrə ilə Yeni Şifrənin təkrarı eyni deyil!");

            AddTranslation("ru", "UserIdIsRequird", "Идентификатор пользователя не может быть пустым!");
            AddTranslation("ru", "FirstNameIsRequird", "Имя не может быть пустым!");
            AddTranslation("ru", "UserNameIsRequird", "Имя пользователя не может быть пустым!");
            AddTranslation("ru", "LastNameIsRequird", "Фамилия не может быть пустой!");
            AddTranslation("ru", "PhoneNumberIsRequird", "Номер телефона не может быть пустым!");
            AddTranslation("ru", "PhoneNumberIsFormat", "Неверный формат номера телефона!");
            AddTranslation("ru", "PasswordIsRequird", "Пароль не может быть пустым!");
            AddTranslation("ru", "ConfirmPasswordIsRequird", "Подтверждение пароля не может быть пустым!");
            AddTranslation("ru", "PasswordChecked", "Пароли не совпадают!");
            AddTranslation("ru", "NewPasswordChecked", "Новый пароль и его подтверждение не совпадают!");

            AddTranslation("en", "UserIdIsRequird", "User ID cannot be empty!");
            AddTranslation("en", "FirstNameIsRequird", "First name cannot be empty!");
            AddTranslation("en", "UserNameIsRequird", "Username cannot be empty!");
            AddTranslation("en", "LastNameIsRequird", "Last name cannot be empty!");
            AddTranslation("en", "PhoneNumberIsRequird", "Phone number cannot be empty!");
            AddTranslation("en", "PhoneNumberIsFormat", "Incorrect phone number format!");
            AddTranslation("en", "PasswordIsRequird", "Password cannot be empty!");
            AddTranslation("en", "ConfirmPasswordIsRequird", "Confirm password cannot be empty!");
            AddTranslation("en", "PasswordChecked", "Passwords do not match!");
            AddTranslation("en", "NewPasswordChecked", "New password and confirmation do not match!");

            #endregion
            #region LoginValidationMessage

            AddTranslation("az", "PasswordIsRequird", "Şifrə bos olmamalidir!.");
            AddTranslation("ru", "PasswordIsRequird", "Поле пароля не должно быть пустым!");
            AddTranslation("en", "PasswordIsRequird", "Password field cannot be empty!");
            AddTranslation("az", "EmailIsRequird", "Elektron Poçt boş ola bilməz!");
            AddTranslation("ru", "EmailIsRequird", "Поле электронной почты не должно быть пустым!");
            AddTranslation("en", "EmailIsRequird", "Email field cannot be empty!");
            #endregion
            #region RegisterValidationMessage
            AddTranslation("az", "FirstnameRequired", "Ad boş olmamalidir!.");
            AddTranslation("az", "LastnameRequired", "Soyad boş olmamalidir!.");
            AddTranslation("az", "EmailRequired", "Elektron Poçt boş olmamalidir!.");
            AddTranslation("az", "InvalidEmailFormat", "Elektron Poçt formatı düzgün olmalıdır!.");
            AddTranslation("az", "PhoneNumberRequired", "Telefon nömrəsi boş olmamalidir!.");
            AddTranslation("az", "InvalidPhoneNumberFormat", "Telefon Nömrəsi formatı +994-xx-xxx-xx-xx formatında olmalıdır.");
            AddTranslation("az", "PasswordRequired", "Şifrə boş ola bilməz!.");
            AddTranslation("az", "ConfirmPasswordRequired", "Təkrar şifrə boş olmamlıdır!.");
            AddTranslation("az", "ConfirmPasswordMismatch", "Təkrar şifrəni düzgün yazın!.");
            AddTranslation("ru", "FirstnameRequired", "Имя не может быть пустым!");
            AddTranslation("en", "FirstnameRequired", "First name field cannot be empty!");
            AddTranslation("ru", "LastnameRequired", "Фамилия не может быть пустой!");
            AddTranslation("en", "LastnameRequired", "Last name field cannot be empty!");
            AddTranslation("ru", "EmailRequired", "Поле электронной почты не может быть пустым!");
            AddTranslation("en", "EmailRequired", "Email field cannot be empty!");
            AddTranslation("ru", "InvalidEmailFormat", "Неправильный формат электронной почты!");
            AddTranslation("en", "InvalidEmailFormat", "Invalid email format!");
            AddTranslation("ru", "PhoneNumberRequired", "Поле номера телефона не может быть пустым!");
            AddTranslation("en", "PhoneNumberRequired", "Phone number field cannot be empty!");
            AddTranslation("ru", "InvalidPhoneNumberFormat", "Неправильный формат номера телефона (формат: +994-xx-xxx-xx-xx)!");
            AddTranslation("en", "InvalidPhoneNumberFormat", "Invalid phone number format (format: +994-xx-xxx-xx-xx)!");
            AddTranslation("ru", "PasswordRequired", "Пароль не может быть пустым!");
            AddTranslation("en", "PasswordRequired", "Password field cannot be empty!");
            AddTranslation("ru", "ConfirmPasswordRequired", "Подтверждение пароля не может быть пустым!");
            AddTranslation("en", "ConfirmPasswordRequired", "Confirm password field cannot be empty!");
            AddTranslation("ru", "ConfirmPasswordMismatch", "Пароли не совпадают!");
            AddTranslation("en", "ConfirmPasswordMismatch", "Passwords do not match!");




            #endregion
            #region AuditoriumValidationMessage
            #region AuditoriumAddDTO

            AddTranslation("az", "AuditoriumNumberIsRequird", "Auditoriya nömrəsi boş olmamalidir!.");
            AddTranslation("az", "AuditoriumNumberLength", "Auditoriya nömrəsi 3-10 simvoldan ibarət olmalidir!");
            AddTranslation("az", "AuditoriumCapacityIsRequird", "Auditoriya tutumu boş ola bilməz!");
            AddTranslation("az", "AuditoriumCapacityIsNull", "Auditoriya tutumu boş ola bilməz!");

            AddTranslation("en", "AuditoriumNumberIsRequird", "Auditorium number cannot be empty!");
            AddTranslation("en", "AuditoriumNumberLength", "Auditorium number must be between 3 and 10 characters!");
            AddTranslation("en", "AuditoriumCapacityIsRequird", "Capacity cannot be empty!");
            AddTranslation("en", "AuditoriumCapacityIsNull", "Capacity cannot be empty!");

            AddTranslation("ru", "AuditoriumNumberIsRequird", "Номер аудитории не может быть пустым!");
            AddTranslation("ru", "AuditoriumNumberLength", "Номер аудитории должен содержать от 3 до 10 символов!");
            AddTranslation("ru", "AuditoriumCapacityIsRequird", "Вместимость не может быть пустой!");
            AddTranslation("ru", "AuditoriumCapacityIsNull", "Вместимость не может быть пустой!");
            #endregion
            #region AuditoriumUpdateDTO
            AddTranslation("az", "AuditoriumIdIsRequird", "Auditoriya Id boş ola bilməz!");
            AddTranslation("en", "AuditoriumIdIsRequird", "Auditorium Id cannot be empty!");
            AddTranslation("ru", "AuditoriumIdIsRequird", "Идентификатор аудитории не может быть пустым!");


            #endregion
            #endregion
            #region ConfreanceValidationMessage
            #region ConferenceAddDTOVAlidationMessage
            AddTranslation("az", "ConferenceNameIsRequird", "Konfrans adı boş ola bilməz.");
            AddTranslation("az", "CategoryIdIsRequird", "Kateqoriya boş ola bilməz!");

            AddTranslation("az", "ConferenceNameCount", "Konfrans adlarının sayı dil kodlarının sayı ilə eyni olmalıdır.");
            AddTranslation("az", "ConferenceContentIsRequird", "Konfrans məzmunu boş ola bilməz.");
            AddTranslation("az", "ConferenceContentCount", "Konfrans məzmunun sayı dil kodlarının sayı ilə eyni olmalıdır.");
            AddTranslation("az", "SepecialGuestEmailCount", "Xüsusi qonaqların e-poçt ünvanlarının sayı xüsusi qonaq adlarının sayı ilə eyni olmalıdır.");
            AddTranslation("az", "SepecialGuestNameCount", "Xüsusi qonaq adlarının sayı xüsusi qonaqların e-poçt ünvanlarının sayı ilə eyni olmalıdır.");
            AddTranslation("az", "ConferenceStartedDateIsrequird", "Konfransın başlama vaxtı müəyyənləşdirilməlidir.");
            AddTranslation("az", "ConferenceStartedDateIsDefault", "Konfrans başlama vaxtı boş olmamalıdır.");
            AddTranslation("az", "ConferenceDayIsDefault", "Konfrans günü vaxtı boş olmamalıdır.");
            AddTranslation("az", "ConferenceDayIsRequird", "Konfrans günü müəyyənləşdirilməlidir.");
            AddTranslation("az", "ConferenceEndDateIsRequird", "Konfransın bitmə vaxtı müəyyənləşdirilməlidir.");
            AddTranslation("az", "ConferenceEndDateIsDefault", "Konfrans bitmə vaxtı boş olmamalıdır.");
            AddTranslation("az", "ConferenceEndDateIsTest", "Konfransın bitmə vaxtı başlama vaxtından sonra olmalıdır.");
            AddTranslation("az", "UserIdIsRequird", "İstifadəçi İD-si müəyyənləşdirilməlidir.");
            AddTranslation("az", "AuditoriumIdIsRequird", "Auditoriya İD-si müəyyənləşdirilməlidir.");

            AddTranslation("en", "ConferenceNameIsRequird", "Conference name cannot be empty.");
            AddTranslation("en", "CategoryIdIsRequird", "Category cannot be empty!");
            AddTranslation("en", "ConferenceNameCount", "Number of conference names must match the number of language codes.");
            AddTranslation("en", "ConferenceContentIsRequird", "Conference content cannot be empty.");
            AddTranslation("en", "ConferenceContentCount", "Number of conference contents must match the number of language codes.");
            AddTranslation("en", "SepecialGuestEmailCount", "Number of special guest emails must match the number of special guest names.");
            AddTranslation("en", "SepecialGuestNameCount", "Number of special guest names must match the number of special guest emails.");
            AddTranslation("en", "ConferenceStartedDateIsrequird", "Conference start time must be specified.");
            AddTranslation("en", "ConferenceStartedDateIsDefault", "Conference start time cannot be empty.");
            AddTranslation("en", "ConferenceDayIsDefault", "Conference day cannot be empty.");
            AddTranslation("en", "ConferenceDayIsRequird", "Conference day must be specified.");
            AddTranslation("en", "ConferenceEndDateIsRequird", "Conference end time must be specified.");
            AddTranslation("en", "ConferenceEndDateIsDefault", "Conference end time cannot be empty.");
            AddTranslation("en", "ConferenceEndDateIsTest", "Conference end time must be after start time.");
            AddTranslation("en", "UserIdIsRequird", "User ID must be specified.");
            AddTranslation("en", "AuditoriumIdIsRequird", "Auditorium ID must be specified.");

            AddTranslation("ru", "ConferenceNameIsRequird", "Название конференции не может быть пустым.");
            AddTranslation("ru", "CategoryIdIsRequird", "Категория не может быть пустой!");
            AddTranslation("ru", "ConferenceNameCount", "Количество названий конференций должно соответствовать количеству языковых кодов.");
            AddTranslation("ru", "ConferenceContentIsRequird", "Содержание конференции не может быть пустым.");
            AddTranslation("ru", "ConferenceContentCount", "Количество содержания конференции должно соответствовать количеству языковых кодов.");
            AddTranslation("ru", "SepecialGuestEmailCount", "Количество электронных адресов специальных гостей должно соответствовать количеству имен специальных гостей.");
            AddTranslation("ru", "SepecialGuestNameCount", "Количество имен специальных гостей должно соответствовать количеству электронных адресов специальных гостей.");
            AddTranslation("ru", "ConferenceStartedDateIsrequird", "Время начала конференции должно быть указано.");
            AddTranslation("ru", "ConferenceStartedDateIsDefault", "Время начала конференции не может быть пустым.");
            AddTranslation("ru", "ConferenceDayIsDefault", "День конференции не может быть пустым.");
            AddTranslation("ru", "ConferenceDayIsRequird", "День конференции должен быть указан.");
            AddTranslation("ru", "ConferenceEndDateIsRequird", "Время окончания конференции должно быть указано.");
            AddTranslation("ru", "ConferenceEndDateIsDefault", "Время окончания конференции не может быть пустым.");
            AddTranslation("ru", "ConferenceEndDateIsTest", "Время окончания конференции должно быть после времени начала.");
            AddTranslation("ru", "UserIdIsRequird", "Идентификатор пользователя должен быть указан.");
            AddTranslation("ru", "AuditoriumIdIsRequird", "Идентификатор аудитории должен быть указан.");


            #endregion
            #endregion

            #region CategoryValidatorErrorMessage
            #region CategoryAddDtoErrorMessage
            AddTranslation("az", "LangCodeIsRequired", "Dil Kodu boş olmamalidir!.");
            AddTranslation("az", "ContentIsRequired", "Məzmun boş olmamalidir!.");
            AddTranslation("az", "LangCodeCountTest", "Dil Kodu ilə Məzmun sayı eyni olmalıdır!.");
            AddTranslation("ru", "LangCodeIsRequired", "Необходим код языка!");
            AddTranslation("ru", "ContentIsRequired", "Содержание не должно быть пустым!");
            AddTranslation("ru", "LangCodeCountTest", "Количество языкового кода должно быть таким же, как количество содержания!");
            AddTranslation("en", "LangCodeIsRequired", "Language code is required!");
            AddTranslation("en", "ContentIsRequired", "Content is required");
            AddTranslation("en", "LangCodeCountTest", "Language code count must be equal to content count!");

            #endregion
            AddTranslation("az", "IdIsRequired", "Id boş olmamalidir!.");
            AddTranslation("ru", "IdIsRequired", "Id должен быть заполнен!");
            AddTranslation("en", "IdIsRequired", "Id is required!");


            #endregion

        }

    }
}
