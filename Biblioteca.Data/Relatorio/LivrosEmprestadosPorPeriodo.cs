using System;

namespace Biblioteca.Data.Relatorio
{
    public class LivrosEmprestadosPorPeriodo
    {
        public DateTime DataEmprestimo { get; set; }
        public string NomeUsuario { get; set; }
        public string NomeLivro { get; set; }
        public int Quantidade { get; set; }
    }
}