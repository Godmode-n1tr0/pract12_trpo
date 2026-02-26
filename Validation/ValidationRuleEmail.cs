using pract12_trpo.Data;
using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace pract12_trpo.Validation
{
    public class ValidationRuleEmail :ValidationRule
    {
        public int CurrentUserId { get; set; }
        public bool IsEditMode { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string email = (value as string ?? "").Trim();

            if (string.IsNullOrWhiteSpace(email))
            {
                return new ValidationResult(false, "Email не может быть пустым");
            }

            if (!IsValidEmail(email))
            {
                return new ValidationResult(false, "Введите корректный email адрес");
            }

            try
            {
                using (var context = new AppDbContext())
                {
                    bool exists;

                    if (IsEditMode && CurrentUserId > 0)
                    {
                        exists = context.Users.Any(u => u.Email.ToLower() == email.ToLower()
                                                       && u.Id != CurrentUserId);
                    }
                    else
                    {
                        exists = context.Users.Any(u => u.Email.ToLower() == email.ToLower());
                    }

                    if (exists)
                    {
                        return new ValidationResult(false, "Пользователь с таким email уже существует");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка при проверке email: {ex.Message}");
            }

            return ValidationResult.ValidResult;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}