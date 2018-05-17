using System;
using System.Linq;
using System.Text.RegularExpressions;
using DreamField.WPFInterface.ViewModel;
using FluentValidation;

namespace DreamField.WPFInterface.Helpers.Validators
{
    class NumericPositivValidator:AbstractValidator<AddRationStatsViewModel>
    {
        public NumericPositivValidator()
        {
            RuleFor(rationData => rationData)
                .NotEmpty()
                .WithMessage("Какое-то из полей пустое")
                .Must((rationData)=>(IsNumericPositiv(rationData)))
                .WithMessage("Нужно положительное дробное число");
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
