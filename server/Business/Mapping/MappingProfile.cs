using AutoMapper;
using FolhaCerta.Business.Dto;
using FolhaCerta.Model.Domain;

namespace FolhaCerta.Business.Mapping
{
    public class MappingProfile : Profile
{
  public MappingProfile()
  {
      CreateMap<Usuario, UsuarioDto>();
      CreateMap<UsuarioDto, Usuario>();
  }
}
}