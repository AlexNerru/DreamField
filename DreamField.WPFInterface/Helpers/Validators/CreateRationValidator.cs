using DreamField.WPFInterface.ViewModel;
using FluentValidation;

namespace DreamField.WPFInterface.Helpers.Validators
{
    internal class CreateRationValidator:AbstractValidator<CreateRationViewModel>
    {
        public CreateRationValidator()
        {
            RuleFor(x => x.Juicy).NotEmpty().WithMessage("Поле сочных кормов пустое")
                .Must(BePositivDouble).WithMessage("Нужно положительное дробное число");
            RuleFor(x=>x.Roughage).NotEmpty().WithMessage("Поле грубых кормов пустое")
                .Must(BePositivDouble).WithMessage("Нужно положительное дробное число");
            RuleFor(x=>x.Consentrates).NotEmpty().WithMessage("Поле концентратов кормов пустое")
                .Must(BePositivDouble).WithMessage("Нужно положительное дробное число");
            RuleFor(x => x.Comment).NotEmpty().WithMessage("Имя не должно быть пустое");
            RuleFor(x => x).Must(BeRightSumEqualToOne).WithMessage("Сумма должна быть равно 1");
        }

        private bool BePositivDouble(string value)
        {
            if (double.TryParse(value, out var result))
                return result >= 0;
            return false;
        }

        private bool BeRightSumEqualToOne(CreateRationViewModel vm)
        {
            if (double.TryParse(vm.Roughage, out var rough)
                && double.TryParse(vm.Juicy, out var juicy)
                && double.TryParse(vm.Consentrates, out var consentrate))
                return rough + juicy + consentrate == 1;
            return false;
        }

        
    }
}
