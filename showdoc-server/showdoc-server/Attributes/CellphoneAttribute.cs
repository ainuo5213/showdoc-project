using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace showdoc_server.Attributes
{
    public class CellphoneAttribute : ValidationAttribute
    {
        private static Regex regex = new Regex("^1\\d{10}", RegexOptions.IgnoreCase);
        public override bool IsValid(object? value)
        {
            string phone = Convert.ToString(value);
            if (string.IsNullOrEmpty(phone))
            {
                return false;
            }

            return regex.IsMatch(phone);
        }
    }
}
