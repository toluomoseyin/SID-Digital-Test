using UserMgtService.Application.DTOs;
using FluentValidation;
using Validot;

namespace UserMgtService.Application.Validator
{
    //internal class RegistrationValidator : AbstractValidator<RegistrationDTO>
    //{
    //    public RegistrationValidator()
    //    {
    //        RuleFor(user => user.Email)
    //        .NotEmpty().WithMessage("Email is required.")
    //        .EmailAddress().WithMessage("Invalid email format.");

    //        RuleFor(user => user.Password)
    //            .NotEmpty().WithMessage("Password is required.")
    //            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

    //        RuleFor(user => user.Username)
    //            .Equal(user => user.Password).WithMessage("Username is required");
    //    }
    //}

    internal sealed class RegistrationValidator : ISpecificationHolder<RegistrationDTO>
    {
        public Specification<RegistrationDTO> Specification { get; }

        public RegistrationValidator()
        {
            Specification<RegistrationDTO> registerCommandSpecification = s => s
                .Member(m => m.Username, m => m
                    .NotEmpty()
                    .NotWhiteSpace())
                .Member(m => m.Password, m => m
                    .NotEmpty()
                    .NotWhiteSpace())
                .Member(m => m.Email, m => m
                    .NotEmpty()
                    .NotWhiteSpace()
                    .Email());

            Specification = registerCommandSpecification;
        }
    }
}
