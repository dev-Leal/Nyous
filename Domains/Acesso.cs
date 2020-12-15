using System;
using System.Collections.Generic;

#nullable disable

namespace NyousTarde.Domains
{
    public partial class Acesso
    {
        public Acesso()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int IdAcesso { get; set; }
        public string Tipo { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
