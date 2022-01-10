using Dominio;
using AutoMapper;
using Aplicacion.Acciones;
using Aplicacion.Estados;
using Aplicacion.Destinatarios;
using Aplicacion.Tipos;
using Aplicacion.Memos;
using Aplicacion.Contenidos;
using Aplicacion.Adjuntos;

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
         CreateMap<ContenidoMemo, ContenidoDTO>()
        .ForMember(x=> x.Usuario, y=> y.MapFrom(z => z.SistemaUsuario))
        .ForMember(x=> x.ID, y=> y.MapFrom(z => z.Id_Contenido));       
        CreateMap<ContenidoDTO, ContenidoMemo>()
        .ForMember(x=> x.SistemaUsuario, y => y.MapFrom(z => z.Usuario))
        .ForMember(x=> x.Id_Contenido, y => y.MapFrom(z => z.ID));
         CreateMap<Adjunto, AdjuntoDTO>()
        .ForMember(x=> x.Usuario, y=> y.MapFrom(z => z.SistemaUsuario))
        .ForMember(x=> x.Id, y=> y.MapFrom(z => z.Id_Adjuntos));       
        CreateMap<AdjuntoDTO, Adjunto>()
        .ForMember(x=> x.SistemaUsuario, y => y.MapFrom(z => z.Usuario))
        .ForMember(x=> x.Id_Adjuntos, y => y.MapFrom(z => z.Id));
       

        }
       
    }
    
}