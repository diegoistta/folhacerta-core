using FolhaCerta.Model.Dto;
using FolhaCerta.Model.Domain;
using FolhaCerta.Business.Service.Interfaces;
using FolhaCerta.DataAccess.Context;
using AutoMapper;
using System.Linq;
using FolhaCerta.Business.Validation;
using FluentValidation;
using System;
using System.Threading.Tasks;
using FolhaCerta.Model.ModelData;

namespace FolhaCerta.Business.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ApplicationContext context;
        private readonly IMapper mapper;
        private readonly UsuarioValidation validator;

        public UsuarioService(ApplicationContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.validator = new UsuarioValidation(this);
        }

        public Response Salvar(UsuarioDto usuarioDto)
        {
            var retorno = new Response();

            var validation = this.validator.Validate(usuarioDto, ruleSet: "Salvar, Autenticar");

            if (!validation.IsValid)
            {
                foreach (var validationFailure in validation.Errors)
                {
                    retorno.AddMessage(true, validationFailure.ErrorMessage);
                }

                return retorno;
            }

            byte[] senhaHash, senhaSalt;
            CriarHashDeSenha(usuarioDto.Senha, out senhaHash, out senhaSalt);

            var usuario = mapper.Map<Usuario>(usuarioDto);
            usuario.SenhaHash = senhaHash;
            usuario.SenhaSalt = senhaSalt;

            context.Usuarios.Add(usuario);
            context.SaveChanges();

            retorno.AddMessage(false, $"Usuário {usuarioDto.Email} salvo com sucesso!");
            return retorno;
        }
        
        public Response Autenticar(UsuarioDto usuarioDto)
        {
            var retorno = new Response();

            var validation = this.validator.Validate(usuarioDto, ruleSet: "Autenticar, ValidarHash");

            if (!validation.IsValid)
            {
                foreach (var validationFailure in validation.Errors)
                {
                    retorno.AddMessage(true, validationFailure.ErrorMessage);
                }

                return retorno;
            }

            var usuario = this.context.Usuarios.SingleOrDefault(x => x.Email == usuarioDto.Email);
            var map = mapper.Map<UsuarioDto>(usuario);

            retorno.AddMessage(false, $"Usuário {usuario.Email} autenticado com sucesso!");
            retorno.Data = map;
            return retorno;
        }

        public Response ListarTodos()
        {
            var retorno  = new Response();
            return retorno;
        }

        internal bool EmailNaoExiste(string email)
        {
            var any = this.context.Usuarios.Any(x => x.Email.Equals(email));
            return !any;
        }

        private static void CriarHashDeSenha(string senha, out byte[] senhaHash, out byte[] senhaSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                senhaSalt = hmac.Key;
                senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
            }
        }
        
        internal bool VerificarHashDeSenha(string email, string senha)
        {
            var usuario = this.context.Usuarios.SingleOrDefault(x => x.Email == email);

            if (usuario == null)
            {
                return false;
            }

            using (var hmac = new System.Security.Cryptography.HMACSHA512(usuario.SenhaSalt))
            {
                var hashCalculado = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
                for (int i = 0; i < hashCalculado.Length; i++)
                {
                    if (hashCalculado[i] != usuario.SenhaHash[i]) return false;
                }
            }

            return true;
        }
    }
}