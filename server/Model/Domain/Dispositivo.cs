using System;

namespace FolhaCerta.Model.Domain
{
    public class Dispositivo
    {
        public int Id { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string Numero { get; set; }

        public string DeviceId { get; set; } 
        
        public string TokenApp { get; set; }

        public Guid UsuarioId { get; set; }
        
        public  Usuario Usuario {get; set;}
    }
}