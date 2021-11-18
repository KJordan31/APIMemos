using Aplicacion.Acciones;
using Dominio;
using AutoMapper;
using Aplicacion.Estados;
using Aplicacion.Destinatarios;
using Aplicacion.Tipos;

namespace Infraestructura.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile(){
        CreateMap<Accion, AccionDTO>().ForMember(x=> x.Usuario, y=> y.MapFrom(z => z.SistemaUsuario));       
        CreateMap<AccionDTO, Accion>().ForMember(x=> x.SistemaUsuario, y => y.MapFrom(z => z.Usuario));
        CreateMap<Estado, EstadoDTO>().ForMember(x=> x.Usuario, y=> y.MapFrom(z => z.SistemaUsuario));       
        CreateMap<EstadoDTO, Estado>().ForMember(x=> x.SistemaUsuario, y => y.MapFrom(z => z.Usuario));
        CreateMap<TipoDestinatario, TipoDestinatarioDTO>().ForMember(x=> x.Usuario, y=> y.MapFrom(z => z.SistemaUsuario));       
        CreateMap<TipoDestinatarioDTO, TipoDestinatario>().ForMember(x=> x.SistemaUsuario, y => y.MapFrom(z => z.Usuario));
        CreateMap<TipoMemorandum, TipoMemorandumDTO>().ForMember(x=> x.Usuario, y=> y.MapFrom(z => z.SistemaUsuario));       
        CreateMap<TipoMemorandumDTO, TipoMemorandum>().ForMember(x=> x.SistemaUsuario, y => y.MapFrom(z => z.Usuario));

        }
       
    }
    
}