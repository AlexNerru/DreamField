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
using GalaSoft.MvvmLight;

namespace DreamField.WPFInterface.Helpers
{
    class NumericPositivValidator:AbstractValidator<AddRationStatsViewModel>
    {
        public NumericPositivValidator()
        {
            RuleFor(RationData => RationData)
                .NotEmpty()
                .Must((RationData)=>(IsNumericPositiv(RationData)))
                .WithMessage("Please enter a positiv numeric value");
        }

        //TODO: rework this?
        private static bool IsNumericPositiv(AddRationStatsViewModel model)
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
