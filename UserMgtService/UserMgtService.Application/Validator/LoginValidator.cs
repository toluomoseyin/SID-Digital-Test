using UserMgtService.Application.DTOs;
using Validot;

namespace UserMgtService.Application.Validator
{
    internal sealed class LoginValidator : ISpecificationHolder<LoginDTO>
    {
        public Specification<LoginDTO> Specification { get; }

        public LoginValidator()
        {
            Specification<LoginDTO> loginCommandSpecification = s => s
                .Member(m => m.Username, m => m
                    .NotEmpty()
                    .NotWhiteSpace())
                .Member(m => m.Password, m => m
                    .NotEmpty()
                    .NotWhiteSpace());

            Specification = loginCommandSpecification;
        }
    }
}
