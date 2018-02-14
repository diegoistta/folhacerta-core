using FolhaCerta.Model.Dto;
using FolhaCerta.Model.Domain;
using FolhaCerta.Model.ModelData;

namespace FolhaCerta.Business.Service.Interfaces
{
    public interface IUsuarioService
    {
        Response Salvar(UsuarioDto usuario);
        Response Autenticar(UsuarioDto usuarioDto);
        Response ListarTodos();
    }
}