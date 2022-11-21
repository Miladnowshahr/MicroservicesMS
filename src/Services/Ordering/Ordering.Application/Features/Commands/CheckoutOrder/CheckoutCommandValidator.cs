using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Commands.CheckoutOrder
{
    public class CheckoutCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutCommandValidator()
        {
            RuleFor(p => p.UserName).NotEmpty().WithMessage("{userName} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{username} must not exceed 50 characters.");

            RuleFor(p => p.EmailAddress)
                .NotEmpty().WithMessage("");

            RuleFor(p => p.TotalPrice)
                .NotEmpty().WithMessage("{}")
                .GreaterThan(0).WithMessage("");
        }

    }
}
