using System;
using FolhaCerta.Model.Enumerators;

namespace FolhaCerta.Model.Domain
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }

        public DateTime? DataAlteracao { get; set; }
 
        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Email { get; set; }

         public byte[] SenhaHash { get; set; }

        public byte[] SenhaSalt { get; set; }

        public string TokenId { get; set; }

        public string Foto { get; set; }
        
        public bool CadastroConfirmado { get; set; }

        public Enums.OrigemDefinicoes OrigemDefinicoes { get; set; }
       
        public int Perfil { get; set; }

        public Enums.Dashboard? DashboardInicial { get; set; }

        public bool LoginRapido { get; set; }

        public bool ConfirmaPoliticaPrivacidade { get; set; }

        public bool AceitouTermos { get; set; }

        public Enums.StatusUsuario Status { get; set; }

         public  Notificacao Notificacao { get; set; }

         public  Dispositivo DispositivoMovel { get; set; }

         public  Funcionario CadastroFuncionario { get; set; }
       
    }
}