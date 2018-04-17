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
using DreamField.WPFInterface.ViewModel;

namespace DreamField.WPFInterface.Helpers
{
    class AddRationValidator:AbstractValidator<AddRationViewModel>
    {
        private const string validationString = "Please enter a numeric value";

        public AddRationValidator()
        {
            RuleFor(RationData => RationData)
                .Must((RationData)=>(IsNumericPositiv(RationData)))
                .WithMessage(validationString);
        }

        private static bool IsNumericPositiv(AddRationViewModel model)
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
