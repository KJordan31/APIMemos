using Dominio;
using AutoMapper;
using Aplicacion.Acciones;
using Aplicacion.Estados;
using Aplicacion.Destinatarios;
using Aplicacion.Tipos;
using Aplicacion.Memos;
using Aplicacion.Contenidos;
using Aplicacion.Adjuntos;
using Aplicacion.Bitacoras;
using Aplicacion.DestinatariosUsu.cs;
using Aplicacion.Firmas;
using Aplicacion.Usuarios;
using Apliacacion.Plantillas;

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
        .ForMember(x=> x.Descripcion, y=> y.MapFrom(z => z.Tipo))
        .ForMember(x=> x.Id, y=> y.MapFrom(z => z.Id_Tipo));       
        CreateMap<TipoMemorandumDTO, TipoMemorandum>()
        .ForMember(x=> x.SistemaUsuario, y => y.MapFrom(z => z.Usuario))
        .ForMember(x=> x.Tipo, y=> y.MapFrom(z => z.Descripcion))
        .ForMember(x=> x.Id_Tipo, y => y.MapFrom(z => z.Id));

        CreateMap<Memorandum, MemorandumDTO>()
        .ForMember(x=> x.Usuario, y=> y.MapFrom(z => z.SistemaUsuario))
        .ForMember(x=> x.Fecha, y=> y.MapFrom(z => z.SistemaFecha));
        

         CreateMap<MemorandumDTO, Memorandum>()
        .ForMember(x=> x.SistemaUsuario, y => y.MapFrom(z => z.Usuario))
        .ForMember(x=> x.SistemaFecha, y => y.MapFrom(z => z.Fecha));


         CreateMap<ContenidoMemo, ContenidoDTO>()
        .ForMember(x=> x.Usuario, y=> y.MapFrom(z => z.SistemaUsuario));
           

        CreateMap<ContenidoDTO, ContenidoMemo>()
        .ForMember(x=> x.SistemaUsuario, y => y.MapFrom(z => z.Usuario));
        
         CreateMap<Adjunto, AdjuntoDTO>()
        .ForMember(x=> x.Usuario, y=> y.MapFrom(z => z.SistemaUsuario))
        .ForMember(x=> x.id_ad, y=> y.MapFrom(z => z.Id_Adjuntos));       
        CreateMap<AdjuntoDTO, Adjunto>()
        .ForMember(x=> x.SistemaUsuario, y => y.MapFrom(z => z.Usuario))
        .ForMember(x=> x.Id_Adjuntos, y => y.MapFrom(z => z.id_ad));
        CreateMap<Bitacora, BitacoraDTO>()
        .ForMember(x=> x.Usuario, y=> y.MapFrom(z => z.SistemaUsuario))
        .ForMember(x=> x.Id, y=> y.MapFrom(z => z.Id_Bitacora));       
        CreateMap<BitacoraDTO, Bitacora>()
        .ForMember(x=> x.SistemaUsuario, y => y.MapFrom(z => z.Usuario))
        .ForMember(x=> x.Id_Bitacora, y => y.MapFrom(z => z.Id));
        CreateMap<Destinatario, DestinatarioDTO>()
        .ForMember(x=> x.Id, y=> y.MapFrom(z => z.Id_Destinatario));       
        CreateMap<DestinatarioDTO, Destinatario>()
        .ForMember(x=> x.Id_Destinatario, y => y.MapFrom(z => z.Id));
        CreateMap<Firma, FirmaDTO>()
        .ForMember(x=> x.Usuario, y=> y.MapFrom(z => z.SistemaUsuario))
        .ForMember(x=> x.FirmaUsu, y=> y.MapFrom(z => z.Firmas))
        .ForMember(x=> x.Id, y=> y.MapFrom(z => z.Id_Firma));       
        CreateMap<FirmaDTO, Firma>()
        .ForMember(x=> x.SistemaUsuario, y => y.MapFrom(z => z.Usuario))
        .ForMember(x=> x.Firmas, y => y.MapFrom(z => z.FirmaUsu))
        .ForMember(x=> x.Id_Firma, y => y.MapFrom(z => z.Id));
         CreateMap<Usuario, UsuarioDTO>()
        .ForMember(x=> x.Id, y=> y.MapFrom(z => z.Id_Usuario));       
        CreateMap<UsuarioDTO, Usuario>()
        .ForMember(x=> x.Id_Usuario, y => y.MapFrom(z => z.Id));

         CreateMap<Plantilla, PlantillaDTO>()
        .ForMember(x=> x.id, y=> y.MapFrom(z => z.Id));       
        CreateMap<PlantillaDTO, Plantilla>()
        .ForMember(x=> x.Id, y => y.MapFrom(z => z.id));


        }
       
    }
    
}