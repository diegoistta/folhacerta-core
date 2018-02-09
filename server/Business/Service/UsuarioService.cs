
namespace FolhaCerta.Business.Service
{
    using FolhaCerta.DataAccess;
    using FolhaCerta.Business.ServiceContract;
    using System;
    using FolhaCerta.Model.Domain;
    using FolhaCerta.Business.Dto;
    using AutoMapper;
    using System.Linq;
    using System.Collections.Generic;
    using FolhaCerta.Model.ModelData;
    using FolhaCerta.Business.Validation;
    using FluentValidation;

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

         public Response Authenticate(UsuarioDto usuarioDto)
        {
            var retorno = new Response();

            var validation = this.validator.Validate(usuarioDto, ruleSet: "Auth");

            if (!validation.IsValid)
            {
                foreach (var validationFailure in validation.Errors)
                {
                    retorno.AddMessage(true, validationFailure.ErrorMessage);
                }

                return retorno;
            }

            var usuario = context.Usuarios.SingleOrDefault(x => x.Login == usuarioDto.Login || x.Email == usuarioDto.Email);

           
            if (usuario == null)
            {
                retorno.AddMessage(true, "Login ou senha invalidos.");
                return retorno;
            }
                
            if (!VerifyPasswordHash(usuarioDto.Senha, usuario.SenhaHash, usuario.SenhaSalt))
            {
                 retorno.AddMessage(true, "Login ou senha invalidos.");
                return retorno;
            }
            retorno.AddMessage(false, $"Usuário {usuario.Login} autenticado com sucesso!");
            retorno.Data = usuario;
            return retorno;
        }


        public Response Create(UsuarioDto usuarioDto)
        {
            var retorno = new Response();

            var validation = this.validator.Validate(usuarioDto, ruleSet: "Insert, Auth");

            if (!validation.IsValid)
            {
                foreach (var validationFailure in validation.Errors)
                {
                    retorno.AddMessage(true, validationFailure.ErrorMessage);
                }

                return retorno;
            }

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(usuarioDto.Senha, out passwordHash, out passwordSalt);

            var usuario = mapper.Map<Usuario>(usuarioDto);
            usuario.SenhaHash = passwordHash;
            usuario.SenhaSalt = passwordSalt;

            context.Usuarios.Add(usuario);
            context.SaveChanges();

            return retorno;
        }

          private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        
         private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
        public Response GetAll()
        {
            var retorno = new Response();

            var usuario = this.context.Usuarios.ToList();
            retorno.Data =  Mapper.Map<IList<UsuarioDto>>(usuario);

            return retorno;
        }

        internal bool IsEmailUnique(string email)
        {
            var any = this.context.Usuarios.Any(x => x.Email.Equals(email));
            return !any;
        }

        internal bool HasUser(UsuarioDto usuario)
        {
            var any = this.context.Usuarios.Any(x => x.Login == usuario.Login || x.Email == usuario.Email);
            return any;
        }

    }
}