using System.Collections.Generic;
using FolhaCerta.Business.Dto;
using FolhaCerta.Model.Domain;
using FolhaCerta.Model.ModelData;

namespace FolhaCerta.Business.ServiceContract
{
    public interface IUsuarioService
    {
        Response Create(UsuarioDto usuario);
        Response Authenticate(UsuarioDto usuarioDto);
        Response GetAll();
    }
}