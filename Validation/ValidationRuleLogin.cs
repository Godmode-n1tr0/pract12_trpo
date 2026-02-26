using pract12_trpo.Data;
using pract12_trpo.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace pract12_trpo.Validation
{
    public class ValidationRuleLogin :ValidationRule
    {
        public int CurrentUserId { get; set; }
        public bool IsEditMode { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string login = (value as string ?? "").Trim();

            if (string.IsNullOrWhiteSpace(login))
            {
                return new ValidationResult(false, "Логин не может быть пустым");
            }

            if (login.Length < 5)
            {
                return new ValidationResult(false, "Логин должен содержать минимум 5 символов");
            }

            if (login.Length > 50)
            {
                return new ValidationResult(false, "Логин не может быть длиннее 50 символов");
            }

            try
            {
                using (var context = new AppDbContext())
                {
                    bool exists;

                    if (IsEditMode && CurrentUserId > 0)
                    {
                        exists = context.Users.Any(u => u.Login.ToLower() == login.ToLower()
                                                       && u.Id != CurrentUserId);
                    }
                    else
                    {
                        exists = context.Users.Any(u => u.Login.ToLower() == login.ToLower());
                    }

                    if (exists)
                    {
                        return new ValidationResult(false, "Пользователь с таким логином уже существует");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка при проверке логина: {ex.Message}");
            }

            return ValidationResult.ValidResult;
        }
    }
}