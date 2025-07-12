using DispatchOrderSystem.Application.Commands.Products;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispatchOrderSystem.Application.Validators.Products
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre del producto es obligatorio.")
                .MinimumLength(2).WithMessage("El nombre del producto debe tener al menos 2 caracteres.")
                .MaximumLength(100).WithMessage("El nombre del producto no puede superar los 100 caracteres.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("La descripción del producto es obligatoria.")
                .MinimumLength(5).WithMessage("La descripción debe tener al menos 5 caracteres.")
                .MaximumLength(250).WithMessage("La descripción no puede superar los 250 caracteres.");
        }
    }
}
