using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MainProject.ValidationRules
{
    class RangeNumberValidationRule : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public String ErrorMsg { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null)
            {
                if (value.GetType() == typeof(int))
                {
                    int input = (int)value;
                    if (input <= Min && input >= Max) { return new ValidationResult(true, null); }
                    else{ return new ValidationResult(false, ErrorMsg); }
                }
            }
            return new ValidationResult(false, "ERROR: Object is NULL or invalid type");
        }
    }
}
