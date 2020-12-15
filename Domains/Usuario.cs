using System;
using System.Collections.Generic;

#nullable disable

namespace NyousTarde.Domains
{
    public partial class Usuario
    {
        public Usuario()
        {
            ConviteIdUsuarioConvidadoNavigations = new HashSet<Convite>();
            ConviteIdUsuarioEmissorNavigations = new HashSet<Convite>();
            Presencas = new HashSet<Presenca>();
        }

        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
        public int? IdAcesso { get; set; }

        public virtual Acesso IdAcessoNavigation { get; set; }
        public virtual ICollection<Convite> ConviteIdUsuarioConvidadoNavigations { get; set; }
        public virtual ICollection<Convite> ConviteIdUsuarioEmissorNavigations { get; set; }
        public virtual ICollection<Presenca> Presencas { get; set; }
    }
}
