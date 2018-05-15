using DreamField.WPFInterface.ViewModel;
using FluentValidation;

namespace DreamField.WPFInterface.Helpers.Validators
{
    internal class CreateRationValidator:AbstractValidator<CreateRationViewModel>
    {
        public CreateRationValidator()
        {
            RuleFor(x => x.Juicy).NotEmpty().Must(IsPositivDouble).WithMessage("Must be positiv double");
            RuleFor(x=>x.Roughage).NotEmpty().Must(IsPositivDouble).WithMessage("Must be positiv double");
            RuleFor(x => x.Comment).NotEmpty();
        }

        private bool IsPositivDouble(string value)
        {
            if (double.TryParse(value, out var result))
                return result >= 0;
            return false;
        }
    }
}
