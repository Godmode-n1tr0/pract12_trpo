using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using pract12_trpo.Data;

namespace pract12_trpo.Validation
{
    public class ValidationRuleName :ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = (value ?? "").ToString().Trim();

            if (string.IsNullOrEmpty(input))
            {
                return new ValidationResult(false, "Ввод имени обязателен");
            }

            if (input.Length < 5)
            {
                return new ValidationResult(false, "Имя должно содержать минимум 5 символов");
            }

            return ValidationResult.ValidResult;
        }
    }
}