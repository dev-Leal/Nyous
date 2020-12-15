using System;
using System.Collections.Generic;

#nullable disable

namespace NyousTarde.Domains
{
    public partial class Localizacao
    {
        public Localizacao()
        {
            Eventos = new HashSet<Evento>();
        }

        public int IdLocalizacao { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }

        public virtual ICollection<Evento> Eventos { get; set; }
    }
}
