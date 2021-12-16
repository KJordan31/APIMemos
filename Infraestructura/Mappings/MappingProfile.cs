using Aplicacion.Acciones;
using Dominio;
using AutoMapper;
using Aplicacion.Estados;
using Aplicacion.Destinatarios;
using Aplicacion.Tipos;
using Aplicacion.Memorandums;

namespace Infraestructura.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile(){
        CreateMap<Accion, AccionDTO>()
        .ForMember(x=> x.Usuario, y=> y.MapFrom(z => z.SistemaUsuario))
        .ForMember(x=> x.ID, y=> y.MapFrom(z => z.Id_Accion));       
        CreateMap<AccionDTO, Accion>()
        .ForMember(x=> x.SistemaUsuario, y => y.MapFrom(z => z.Usuario))
        .ForMember(x=> x.Id_Accion, y => y.MapFrom(z => z.ID));
        CreateMap<Estado, EstadoDTO>()
        .ForMember(x=> x.Usuario, y=> y.MapFrom(z => z.SistemaUsuario))
        .ForMember(x=> x.Id, y=> y.MapFrom(z => z.Id_Estado));       
        CreateMap<EstadoDTO, Estado>()
        .ForMember(x=> x.SistemaUsuario, y => y.MapFrom(z => z.Usuario))
        .ForMember(x=> x.Id_Estado, y => y.MapFrom(z => z.Id));
        CreateMap<TipoDestinatario, TipoDestinatarioDTO>()
        .ForMember(x=> x.Usuario, y=> y.MapFrom(z => z.SistemaUsuario))
        .ForMember(x=> x.Id, y=> y.MapFrom(z => z.Id_Tipo_Destinatario));       
        CreateMap<TipoDestinatarioDTO, TipoDestinatario>()
        .ForMember(x=> x.SistemaUsuario, y => y.MapFrom(z => z.Usuario))
        .ForMember(x=> x.Id_Tipo_Destinatario, y => y.MapFrom(z => z.Id));
        CreateMap<TipoMemorandum, TipoMemorandumDTO>()
        .ForMember(x=> x.Usuario, y=> y.MapFrom(z => z.SistemaUsuario))
        .ForMember(x=> x.Id, y=> y.MapFrom(z => z.Id_Tipo));       
        CreateMap<TipoMemorandumDTO, TipoMemorandum>()
        .ForMember(x=> x.SistemaUsuario, y => y.MapFrom(z => z.Usuario))
        .ForMember(x=> x.Id_Tipo, y => y.MapFrom(z => z.Id));
        CreateMap<Memorandum, MemorandumDTO>()
        .ForMember(x=> x.Usuario, y=> y.MapFrom(z => z.SistemaUsuario));
         CreateMap<MemorandumDTO, Memorandum>()
        .ForMember(x=> x.SistemaUsuario, y => y.MapFrom(z => z.Usuario));

        }
       
    }
    
}