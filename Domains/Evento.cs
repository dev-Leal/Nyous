using System;
using System.Collections.Generic;

#nullable disable

namespace NyousTarde.Domains
{
    public partial class Evento
    {
        public Evento()
        {
            Convites = new HashSet<Convite>();
            Presencas = new HashSet<Presenca>();
        }

        public int IdEvento { get; set; }
        public DateTime DataEvento { get; set; }
        public byte[] AcessoRestrito { get; set; }
        public int Capacidade { get; set; }
        public string Descricao { get; set; }
        public int? IdLocalizacao { get; set; }
        public int? IdCategoria { get; set; }

        public virtual Categorium IdCategoriaNavigation { get; set; }
        public virtual Localizacao IdLocalizacaoNavigation { get; set; }
        public virtual ICollection<Convite> Convites { get; set; }
        public virtual ICollection<Presenca> Presencas { get; set; }
    }
}
