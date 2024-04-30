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

        }

    }
}
