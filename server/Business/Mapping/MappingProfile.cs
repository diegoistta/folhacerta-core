using AutoMapper;
using FolhaCerta.Model.Dto;
using FolhaCerta.Model.Domain;

namespace FolhaCerta.Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Entity => DTO

            CreateMap<Usuario, UsuarioDto>();

            // DTO => Entity

            CreateMap<UsuarioDto, Usuario>();
        }
    }
}