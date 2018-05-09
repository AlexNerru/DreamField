using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using DreamField.WPFInterface.ViewModel;

namespace DreamField.WPFInterface.Helpers
{
    class CreateRationValidator:AbstractValidator<CreateRationViewModel>
    {
        public CreateRationValidator()
        {
            RuleFor(x => x.Juicy).NotEmpty().Must(IsPositivDouble).WithMessage("Must be positiv double");
            RuleFor(x=>x.Roughage).NotEmpty().Must(IsPositivDouble).WithMessage("Must be positiv double");
            RuleFor(x => x.Comment).NotEmpty();
        }

        private bool IsPositivDouble(string value)
        {
            double result;
            if (double.TryParse(value, out result))
                return result >= 0;
            return false;
        }
    }
}
