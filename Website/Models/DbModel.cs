using Biblioteca.Data;

namespace Website.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DbModel : DbContext
    {
        public DbModel()
            : base("name=DbModel")
        {
        }

        public virtual DbSet<Autor> Autor { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }
        public virtual DbSet<Departamento> Departamento { get; set; }
        public virtual DbSet<Editora> Editora { get; set; }
        public virtual DbSet<Emprestimo> Emprestimo { get; set; }
        public virtual DbSet<Exemplar> Exemplar { get; set; }
        public virtual DbSet<Livro> Livro { get; set; }
        public virtual DbSet<PerfilUsuario> PerfilUsuario { get; set; }
        public virtual DbSet<Unidade> Unidade { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<UsuarioCurso> UsuarioCurso { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autor>()
                .Property(e => e.Nome)
                .IsFixedLength();

            modelBuilder.Entity<Autor>()
                .HasMany(e => e.Livro)
                .WithRequired(e => e.Autor)
                .HasForeignKey(e => e.IdAutor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Curso>()
                .Property(e => e.Nome)
                .IsFixedLength();

            modelBuilder.Entity<Curso>()
                .HasMany(e => e.UsuarioCurso)
                .WithRequired(e => e.Curso)
                .HasForeignKey(e => e.IdCurso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Departamento>()
                .Property(e => e.Descricao)
                .IsFixedLength();

            modelBuilder.Entity<Departamento>()
                .HasMany(e => e.Curso)
                .WithRequired(e => e.Departamento)
                .HasForeignKey(e => e.IdDepartamento)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Editora>()
                .Property(e => e.Nome)
                .IsFixedLength();

            modelBuilder.Entity<Editora>()
                .HasMany(e => e.Livro)
                .WithRequired(e => e.Editora)
                .HasForeignKey(e => e.IdEditora)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Emprestimo>()
                .Property(e => e.ValorMulta)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Exemplar>()
                .Property(e => e.Edicao)
                .IsFixedLength();

            modelBuilder.Entity<Exemplar>()
                .Property(e => e.AnoEdicao)
                .HasPrecision(4, 0);

            modelBuilder.Entity<Exemplar>()
                .HasMany(e => e.Emprestimo)
                .WithRequired(e => e.Exemplar)
                .HasForeignKey(e => e.IdExemplar)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Livro>()
                .Property(e => e.Titulo)
                .IsFixedLength();

            modelBuilder.Entity<Livro>()
                .HasMany(e => e.Exemplar)
                .WithRequired(e => e.Livro)
                .HasForeignKey(e => e.IdLivro)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PerfilUsuario>()
                .Property(e => e.ValorMultaDiario)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PerfilUsuario>()
                .Property(e => e.Descricao)
                .IsFixedLength();

            modelBuilder.Entity<PerfilUsuario>()
                .HasMany(e => e.Usuario)
                .WithRequired(e => e.PerfilUsuario)
                .HasForeignKey(e => e.IdPerfilUsuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Unidade>()
                .Property(e => e.Nome)
                .IsFixedLength();

            modelBuilder.Entity<Unidade>()
                .HasMany(e => e.Curso)
                .WithRequired(e => e.Unidade)
                .HasForeignKey(e => e.IdUnidade)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Unidade>()
                .HasMany(e => e.Exemplar)
                .WithRequired(e => e.Unidade)
                .HasForeignKey(e => e.IdUnidade)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Nome)
                .IsFixedLength();

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Emprestimo)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.MatriculaUsuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.UsuarioCurso)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.MatriculaUsuario)
                .WillCascadeOnDelete(false);
        }
    }
}
