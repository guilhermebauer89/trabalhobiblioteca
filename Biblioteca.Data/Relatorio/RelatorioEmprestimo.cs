using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Data.Relatorio
{
    public class RelatorioEmprestimo
    {
        [Display(Name = "Data De")]
        public DateTime? DataDe { get; set; }
        [Display(Name = "Data Até")]
        public DateTime? DataAte { get; set; }

        public int? MatriculaUsuario { get; set; }

        public virtual Usuario Usuario { get; set; }

        IEnumerable<Emprestimo> Emprestimos { get; set; }
    }
}
