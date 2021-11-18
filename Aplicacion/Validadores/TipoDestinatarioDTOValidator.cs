using Aplicacion.Destinatarios;
using FluentValidation;

namespace Aplicacion.Validadores
{
    public class TipoDestinatarioDTOValidator : AbstractValidator<TipoDestinatarioDTO>
    {
        public TipoDestinatarioDTOValidator()
        {
            RuleFor(x => x.Descripcion)
            .NotEmpty().WithMessage("Este campo no puede venir vacio")
            .NotNull().WithMessage("No se aceptan valores null");
        }
    }
    
}