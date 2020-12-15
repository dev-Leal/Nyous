using System;
using System.Collections.Generic;

#nullable disable

namespace NyousTarde.Domains
{
    public partial class Categorium
    {
        public Categorium()
        {
            Eventos = new HashSet<Evento>();
        }

        public int IdCategoria { get; set; }
        public string Titulo { get; set; }

        public virtual ICollection<Evento> Eventos { get; set; }
    }
}
