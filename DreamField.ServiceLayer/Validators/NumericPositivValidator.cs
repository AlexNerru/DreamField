using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Validators;
using FluentValidation.TestHelper;
using DreamField.BusinessLogic;
using System.Text.RegularExpressions;

namespace DreamField.ServiceLayer.Validators
{
    /// <summary>
    /// Validates if all properties are numeric and equal or greater than 0
    /// </summary>
    class NumericPositivValidator : AbstractValidator<RationStatsDto>
    {
        private const string validationString = "Please enter a positiv numeric value";

        public NumericPositivValidator()
        {
            RuleFor(RationData => RationData)
                .Must((RationData) => (IsNumericPositiv(RationData)))
                .WithMessage(validationString);
        }

        //TODO:Optimise this
        private static bool IsNumericPositiv(RationStatsDto model)
        {
            foreach (var item in model.GetType().GetProperties().Where(prop => prop.PropertyType == typeof(string)))
            {
                string value = (string)item.GetValue(model);
                if (!string.IsNullOrEmpty(value))
                {
                    if (Regex.IsMatch(value, @"\-?\d+(\,\d{0,})?"))
                    {
                        if (Convert.ToDouble(value) < 0)
                            return false;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            return true;
        }



    }
}
