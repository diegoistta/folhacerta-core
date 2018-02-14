using System;
using Newtonsoft.Json;

namespace FolhaCerta.Model.Dto
{
	public class UsuarioDto
	{
		 public Guid Id { get; set; }

        [JsonIgnore]
        public DateTime DataCriacao { get; set; }

        [JsonIgnore]
        public DateTime? DataAlteracao { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

		public string Token { get; set; }
        
		public bool ShouldSerializeSenha()
        {
            return false;
        }

	}
}
