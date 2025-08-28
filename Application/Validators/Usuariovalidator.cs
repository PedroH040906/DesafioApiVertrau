using FluentValidation;
using desafio.DTOS;      // <- valida o DTO que o controller recebe
using desafio.Entities;  // por causa do enum Genero
using System;

namespace desafio.Validators
{
    public class UsuarioValidator : AbstractValidator<UsuarioDTO>
    {
        public UsuarioValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .Length(2, 150).WithMessage("O nome deve ter entre 2 e 150 caracteres.");

            RuleFor(x => x.SobreNome)
                .NotEmpty().WithMessage("O sobrenome é obrigatório.")
                .Length(2, 150).WithMessage("O sobrenome deve ter entre 2 e 150 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("E-mail inválido.");

            RuleFor(x => x.Genero)
                .IsInEnum().WithMessage("Gênero inválido.");

            RuleFor(x => x.DataNascimento)
                .Must(d => d == default || d <= DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("Data de nascimento não pode ser no futuro.");
        }
    }
}
