using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace pract12_trpo.Validation
{
    public class ValidationRulePassword :ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = (value ?? "").ToString().Trim();

            if (string.IsNullOrEmpty(input))
            {
                return new ValidationResult(false, "Ввод пароля обязателен");
            }

            if (input.Length < 8)
            {
                return new ValidationResult(false, "Длина пароля должна быть не менее 8 символов");
            }

            if (!input.Any(char.IsDigit))
            {
                return new ValidationResult(false, "Пароль должен содержать хотя бы одну цифру");
            }

            if (!input.Any(char.IsUpper))
            {
                return new ValidationResult(false, "Пароль должен содержать хотя бы одну заглавную букву");
            }

            if (!input.Any(char.IsLower))
            {
                return new ValidationResult(false, "Пароль должен содержать хотя бы одну строчную букву");
            }

            if (!input.Any(c => !char.IsLetterOrDigit(c)))
            {
                return new ValidationResult(false, "Пароль должен содержать хотя бы один специальный символ");
            }

            return ValidationResult.ValidResult;
        }
    }
}