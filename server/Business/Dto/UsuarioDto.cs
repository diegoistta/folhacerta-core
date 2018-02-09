using System;

namespace FolhaCerta.Business.Dto
{
    public class UsuarioDto
    {
         public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

    }
}