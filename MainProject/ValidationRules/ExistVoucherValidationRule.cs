using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using MainProject.Model;

namespace MainProject.ValidationRules
{
    class ExistVoucherValidationRule : ValidationRule
    {
        public String ErrorMsg { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            String code = value.ToString();
            if (!VoucherWorkSpace.VoucherViewModel.HasExisted(code))
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, this.ErrorMsg);
            }
        }
    }
}
