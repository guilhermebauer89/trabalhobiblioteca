namespace Biblioteca.Data
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Exemplar")]
    public class Exemplar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Exemplar()
        {
            Emprestimo = new HashSet<Emprestimo>();
        }

        public int Id { get; set; }

        [Display(Name = "Livro")]
        public int IdLivro { get; set; }

        [Display(Name = "Unidade")]
        public int IdUnidade { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Edição")]
        public string Edicao { get; set; }

        [Column(TypeName = "numeric")]
        [Display(Name = "Ano")]
        public short AnoEdicao { get; set; }

        [Display(Name = "Está Locado")]
        public bool Locado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Emprestimo> Emprestimo { get; set; }

        public virtual Livro Livro { get; set; }

        public virtual Unidade Unidade { get; set; }
    }
}
