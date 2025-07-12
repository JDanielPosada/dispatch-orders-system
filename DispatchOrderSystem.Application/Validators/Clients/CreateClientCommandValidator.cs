using DispatchOrderSystem.Application.Commands.Clients;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispatchOrderSystem.Application.Validators.Clients
{
    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommandRequest>
    {
        public CreateClientCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre del cliente es obligatorio.")
                .MinimumLength(2).WithMessage("El nombre del cliente debe tener al menos 2 caracteres.")
                .MaximumLength(100).WithMessage("El nombre del cliente no puede superar los 100 caracteres.");
        }
    }
}
