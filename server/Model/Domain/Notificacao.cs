namespace FolhaCerta.Model.Domain
{
    using System;
    public class Notificacao
    {
        public int Id { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime? DataAlteracao { get; set; }
        
        public bool HabilitarAds { get; set; }

        public bool Ferias { get; set; }

        public bool DayOff { get; set; }

        public bool Recesso { get; set; }

        public bool AvisoAusencia { get; set; }

        public bool MensagemChat { get; set; }

        public bool Afastamento { get; set; }

        public Guid UsuarioId { get; set; }
        public  Usuario Usuario { get; set; }
    }
}