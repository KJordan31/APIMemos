using Aplicacion.Acciones;
using FluentValidation;

namespace Aplicacion.Validadores
{
    public class AccionDTOValidator: AbstractValidator<AccionDTO>
    {
        public AccionDTOValidator()
        {
            RuleFor(x => x.Descripcion)
            .NotEmpty().WithMessage("Este campo no puede venir vacio")
            .NotNull().WithMessage("No se aceptan valores null");

            RuleFor(x => x.Usuario)
            .NotEmpty().WithMessage("Se debe ingresar un usuario")
            .NotNull().WithMessage("No se aceptan valores null");
        }
    }
}