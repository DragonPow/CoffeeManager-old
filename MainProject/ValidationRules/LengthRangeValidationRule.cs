using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MainProject.ValidationRules
{
    class LengthRangeValidationRule:ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public String ErrorMessage { get; set; }
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            bool rs = false;
            if (value != null)
            {
                if (value.GetType() == typeof(String))
                {
                    String input = value.ToString();
                    rs = (input.Length >= Min && input.Length < Max);
                }
                else { ErrorMessage = "This type cannot be check : " + value.GetType().ToString(); }
            }

            if (rs)
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, ErrorMessage);
            }
        }
    }
}
