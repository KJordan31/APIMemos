using Aplicacion.Estados;
using FluentValidation;

namespace Apliacacion.Validadores
{
    public class EstadoDTOValidator : AbstractValidator<EstadoDTO>
    {
        public EstadoDTOValidator()
        {
            RuleFor(y => y.Descripcion)
            .NotEmpty().WithMessage("Este campo no puede quedar vacio")
            .NotNull().WithMessage("Valor no aceptado");
        }
    }
}