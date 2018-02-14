using System;
using FolhaCerta.Model.Enumerators;

namespace FolhaCerta.Model.Domain
{
    public class Funcionario
    {
        public int Id { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public DateTime DataAdmissao { get; set; }

        public DateTime? DataDemissao { get; set; }

        public DateTime? DataNascimento { get; set; }

        public Usuario Login { get; set; }

        public long? Cpf { get; set; }

        public string Rg { get; set; }               
        
        public string TelefoneComercial {get; set;}

        public DateTime? DataInicioBancoHorasSindicatao { get; set; }

        public string Pis { get; set; }

        public int Empresa { get; set; }

        public string Cargo { get; set; }

        public bool? FuncionarioRh { get; set; }
    
        public Enums.Regime? Regime { get; set; } 

        public int CentroCusto { get; set; }

        public int Departamento { get; set; }

        // public IList<DefinicaoJornada> DefinicaoJornadas { get; set; }

        // public IList<DefinicaoLocalTrabalho> DefinicaoLocaisTrabalho { get; set; }

        // public IList<DefinicaoConfiguracao> DefinicaoConfiguracoes { get; set; }

        // public IList<DepartamentoGestao> DepartamentosGestao { get; set; }

        // public IList<UsuarioGestao> UsuariosGestao { get; set; }

        public Guid UsuarioId { get; set; }
        public  Usuario Usuario{ get; set; }

    }
}