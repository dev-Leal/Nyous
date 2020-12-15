using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NyousTarde.Domains;

#nullable disable

namespace NyousTarde.Contexts
{
    public partial class NyousContext : DbContext
    {
        public NyousContext()
        {
        }

        public NyousContext(DbContextOptions<NyousContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Acesso> Acessos { get; set; }
        public virtual DbSet<Categorium> Categoria { get; set; }
        public virtual DbSet<Convite> Convites { get; set; }
        public virtual DbSet<Evento> Eventos { get; set; }
        public virtual DbSet<Localizacao> Localizacaos { get; set; }
        public virtual DbSet<Presenca> Presencas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public object Usuario { get; internal set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.\\SqlExpress3; Initial Catalog= NyousTarde; User Id=sa; Password=sa132");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Acesso>(entity =>
            {
                entity.HasKey(e => e.IdAcesso)
                    .HasName("PK__Acesso__CDF01DA11244D57D");

                entity.ToTable("Acesso");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Categorium>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PK__Categori__A3C02A1033A52C5C");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Convite>(entity =>
            {
                entity.HasKey(e => e.IdConvite)
                    .HasName("PK__Convite__318FC5544C19F028");

                entity.ToTable("Convite");

                entity.HasOne(d => d.IdEventoNavigation)
                    .WithMany(p => p.Convites)
                    .HasForeignKey(d => d.IdEvento)
                    .HasConstraintName("FK__Convite__IdEvent__48CFD27E");

                entity.HasOne(d => d.IdUsuarioConvidadoNavigation)
                    .WithMany(p => p.ConviteIdUsuarioConvidadoNavigations)
                    .HasForeignKey(d => d.IdUsuarioConvidado)
                    .HasConstraintName("FK__Convite__IdUsuar__4AB81AF0");

                entity.HasOne(d => d.IdUsuarioEmissorNavigation)
                    .WithMany(p => p.ConviteIdUsuarioEmissorNavigations)
                    .HasForeignKey(d => d.IdUsuarioEmissor)
                    .HasConstraintName("FK__Convite__IdUsuar__49C3F6B7");
            });

            modelBuilder.Entity<Evento>(entity =>
            {
                entity.HasKey(e => e.IdEvento)
                    .HasName("PK__Evento__034EFC04077DDE35");

                entity.ToTable("Evento");

                entity.Property(e => e.AcessoRestrito)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasDefaultValueSql("((0))")
                    .IsFixedLength(true);

                entity.Property(e => e.DataEvento).HasColumnType("datetime");

                entity.Property(e => e.Descricao)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Eventos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK__Evento__IdCatego__412EB0B6");

                entity.HasOne(d => d.IdLocalizacaoNavigation)
                    .WithMany(p => p.Eventos)
                    .HasForeignKey(d => d.IdLocalizacao)
                    .HasConstraintName("FK__Evento__IdLocali__403A8C7D");
            });

            modelBuilder.Entity<Localizacao>(entity =>
            {
                entity.HasKey(e => e.IdLocalizacao)
                    .HasName("PK__Localiza__C96A5BF6847A5311");

                entity.ToTable("Localizacao");

                entity.Property(e => e.Bairro)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cep)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CEP");

                entity.Property(e => e.Cidade)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Complemento)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Logradouro)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Numero)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Uf)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("UF")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Presenca>(entity =>
            {
                entity.HasKey(e => e.IdPresenca)
                    .HasName("PK__Presenca__50FB6F5D8906908E");

                entity.ToTable("Presenca");

                entity.HasOne(d => d.IdEventoNavigation)
                    .WithMany(p => p.Presencas)
                    .HasForeignKey(d => d.IdEvento)
                    .HasConstraintName("FK__Presenca__IdEven__440B1D61");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Presencas)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Presenca__IdUsua__44FF419A");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuario__5B65BF97D90B6BD2");

                entity.ToTable("Usuario");

                entity.Property(e => e.DataNascimento).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdAcessoNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdAcesso)
                    .HasConstraintName("FK__Usuario__IdAcess__3C69FB99");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
