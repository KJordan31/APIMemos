using Aplicacion.Tipos;
using FluentValidation;

namespace Aplicacion.Validadores
{
    public class TipoMemorandumDTOValidator : AbstractValidator<TipoMemorandumDTO>
    {
        public TipoMemorandumDTOValidator()
        {
            RuleFor(x => x.Tipo)
            .NotEmpty().WithMessage("Este campo no puede venir vacio")
            .NotNull().WithMessage("No se aceptan valoress nulos");
        }

    }
    
}