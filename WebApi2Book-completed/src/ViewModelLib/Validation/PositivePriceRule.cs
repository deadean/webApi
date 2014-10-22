using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ViewModelLib.Validation
{
    public class PositivePriceRule : ValidationRule
    {
        private decimal min = 0;
        private decimal max = Decimal.MaxValue;
        public decimal Min
        {
            get { return min; }
            set { min = value; }
        }
        public decimal Max
        {
            get { return max; }
            set { max = value; }
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            decimal price = 0;
            try
            {
                if (((string)value).Length > 0)
                    price = Decimal.Parse((string)value, NumberStyles.Any, cultureInfo);
            }
            catch
            {
                return new ValidationResult(false, "Illegal characters.");
            }
            if ((price < Min) || (price > Max))
            {
                return new ValidationResult(false,
                "Not in the range " + Min + " to " + Max + ".");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
