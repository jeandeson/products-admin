using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProjetoProdutos_Api
{
    public partial class dbprojetoprodutosContext : DbContext
    {
        public dbprojetoprodutosContext()
        {
        }

        public dbprojetoprodutosContext(DbContextOptions<dbprojetoprodutosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbCentros> TbCentros { get; set; }
        public virtual DbSet<TbClientes> TbClientes { get; set; }
        public virtual DbSet<TbEnderecos> TbEnderecos { get; set; }
        public virtual DbSet<TbProdutos> TbProdutos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:jean-nas.database.windows.net,1433;Initial Catalog=db-projeto-produtos;Persist Security Info=False;User ID=jean-nas;Password=Minhasenhadoazure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbCentros>(entity =>
            {
                entity.HasKey(e => e.IdCentro)
                    .HasName("PK__tb_centr__7197C04FB6C45810");

                entity.ToTable("tb_centros");

                entity.HasIndex(e => e.CodigoCentro)
                    .HasName("UQ__tb_centr__0A9858A4231490EB")
                    .IsUnique();

                entity.Property(e => e.IdCentro).HasColumnName("id_centro");

                entity.Property(e => e.CodigoCentro).HasColumnName("codigo_centro");

                entity.Property(e => e.IdEndereco).HasColumnName("id_endereco");

                entity.Property(e => e.NomeCentro)
                    .IsRequired()
                    .HasColumnName("nome_centro")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEnderecoNavigation)
                    .WithMany(p => p.TbCentros)
                    .HasForeignKey(d => d.IdEndereco)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_id_endereco");
            });

            modelBuilder.Entity<TbClientes>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PK__tb_clien__677F38F5F0D8188B");

                entity.ToTable("tb_clientes");

                entity.HasIndex(e => e.EmailCliente)
                    .HasName("UQ__tb_clien__A1DF279E380C0AE0")
                    .IsUnique();

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.EmailCliente)
                    .IsRequired()
                    .HasColumnName("email_cliente")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdEndereco).HasColumnName("id_endereco");

                entity.Property(e => e.NomeCliente)
                    .IsRequired()
                    .HasColumnName("nome_cliente")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SobrenomeCliente)
                    .IsRequired()
                    .HasColumnName("sobrenome_cliente")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEnderecoNavigation)
                    .WithMany(p => p.TbClientes)
                    .HasForeignKey(d => d.IdEndereco)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_id_endereco_cliente");
            });

            modelBuilder.Entity<TbEnderecos>(entity =>
            {
                entity.HasKey(e => e.IdEndereco)
                    .HasName("PK__tb_ender__324B959EB8CAF01F");

                entity.ToTable("tb_enderecos");

                entity.Property(e => e.IdEndereco).HasColumnName("id_endereco");

                entity.Property(e => e.Bairro)
                    .IsRequired()
                    .HasColumnName("bairro")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cep).HasColumnName("cep");

                entity.Property(e => e.Cidade)
                    .IsRequired()
                    .HasColumnName("cidade")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rua)
                    .IsRequired()
                    .HasColumnName("rua")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbProdutos>(entity =>
            {
                entity.HasKey(e => e.IdProduto)
                    .HasName("PK__tb_produ__BA38A6B83E6F9600");

                entity.ToTable("tb_produtos");

                entity.HasIndex(e => e.CodigoProduto)
                    .HasName("UQ__tb_produ__48A1CB7C66AD9885")
                    .IsUnique();

                entity.Property(e => e.IdProduto).HasColumnName("id_produto");

                entity.Property(e => e.CodigoProduto).HasColumnName("codigo_produto");

                entity.Property(e => e.IdCentro).HasColumnName("id_centro");

                entity.Property(e => e.ImagemProduto)
                    .IsRequired()
                    .HasColumnName("imagem_produto")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NomeProduto)
                    .IsRequired()
                    .HasColumnName("nome_produto")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.QuantidadeProduto).HasColumnName("quantidade_produto");

                entity.Property(e => e.ValorProduto)
                    .IsRequired()
                    .HasColumnName("valor_produto")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCentroNavigation)
                    .WithMany(p => p.TbProdutos)
                    .HasForeignKey(d => d.IdCentro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_id_centro");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
