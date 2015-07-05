namespace Website.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Exemplar")]
    public partial class Exemplar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Exemplar()
        {
            Emprestimo = new HashSet<Emprestimo>();
        }

        public int Id { get; set; }

        public int IdLivro { get; set; }

        public int IdUnidade { get; set; }

        [Required]
        [StringLength(30)]
        public string Edicao { get; set; }

        [Column(TypeName = "numeric")]
        public decimal AnoEdicao { get; set; }

        public bool Locado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Emprestimo> Emprestimo { get; set; }

        public virtual Livro Livro { get; set; }

        public virtual Unidade Unidade { get; set; }
    }
}
