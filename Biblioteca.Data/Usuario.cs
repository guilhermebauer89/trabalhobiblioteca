using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Biblioteca.Data
{
    [Table("Usuario")]
    public sealed class Usuario
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            Emprestimo = new HashSet<Emprestimo>();
            UsuarioCurso = new HashSet<UsuarioCurso>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Matricula { get; set; }

        [Display(Name = "Perfil")]
        public int IdPerfilUsuario { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nome Usúario")]
        public string Nome { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Emprestimo> Emprestimo { get; set; }

        public PerfilUsuario PerfilUsuario { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<UsuarioCurso> UsuarioCurso { get; set; }
    }
}
