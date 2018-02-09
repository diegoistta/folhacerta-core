using FolhaCerta.Business.Dto;
using FolhaCerta.Business.Service;
using FluentValidation;

namespace FolhaCerta.Business.Validation
{
    public class UsuarioValidation : AbstractValidator<UsuarioDto>
    {
        private readonly UsuarioService service;
        public UsuarioValidation(UsuarioService service)
        {
            this.service = service;

            this.RuleSet("Insert", () => {
                this.RuleFor(x => x.Login).NotEmpty().WithMessage("O login é obrigatório.");
                this.RuleFor(x => x.Nome).NotEmpty().WithMessage("O nome do usuário é obrigatório.");
                this.RuleFor(x => x.Sobrenome).NotEmpty().WithMessage("O sobrenome do usuário é obrigatório.");

                this.RuleFor(x => x.Email).NotEmpty().WithMessage("O e-mail é obrigatório")
                .EmailAddress().WithMessage("E-mail inválido")
                .Must(service.IsEmailUnique).WithMessage("E-mail já cadastrado.");
                
            });

            this.RuleSet("Auth", () => {
                this.RuleFor(x => x.Senha).NotEmpty().WithMessage("Senha Inválida")
                .Matches("(?=.*[a-z])").WithMessage("A senha deve conter pelo menos uma letra minúscula.")
                .Matches("(?=.*[A-Z])").WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
                .Matches("(?=.*[0-9])").WithMessage("A senha deve conter pelo menos um número.")
                .Matches(@"(?=.[!@#\$%\^&])").WithMessage("A senha deve conter pelo menos um caracter especial")
                .Matches("(?=.{6,})").WithMessage("A senha deve conter 6 ou mais caracteres.");
                this.RuleFor(x => x.Email).NotEmpty().WithMessage("Email ou login inválidos");
            });

        }
       
    }
}