using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Biblioteca.Data;
using Biblioteca.Data.Context;
using Biblioteca.Data.Relatorio;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Website.Controllers
{
    public class RelatoriosController : Controller
    {
        private ContextoBiblioteca db = new ContextoBiblioteca();

        #region Index
        [HttpPost]
        public ActionResult Index([Bind(Include = "DataDe,DataAte")] RelatorioEmprestimo relatorioEmprestimo)
        {
            if (relatorioEmprestimo.DataDe > relatorioEmprestimo.DataAte)
            {
                ModelState.AddModelError("erro", "Data De deve ser menor ou igual a Data Até");
                return View();
            }

            ViewBag.Grid = LivrosEmprestadosPorPeriodo(relatorioEmprestimo);
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region LivrosPorPeriodoUsuario

        public async Task<ViewResult> LivrosPorPeriodoUsuario()
        {
            await PopulaViewBagUsuario();
            return View();
        }

        [HttpPost]
        public async Task<ViewResult> LivrosPorPeriodoUsuario([Bind(Include = "DataDe,DataAte,MatriculaUsuario")] RelatorioEmprestimo relatorioEmprestimo)
        {
            await PopulaViewBagUsuario();
            if (relatorioEmprestimo.DataDe > relatorioEmprestimo.DataAte)
            {
                ModelState.AddModelError("erro", "Data De deve ser menor ou igual a Data Até");
                return View();
            }

            if (relatorioEmprestimo.MatriculaUsuario == null)
            {
                ModelState.AddModelError("erro", "Selecione um locátario");
                return View();
            }

            ViewBag.Grid = LivrosEmprestadosPorPeriodoUsuario(relatorioEmprestimo);
            return View();
        }
        #endregion

        #region TopDezLivros

        public async Task<ViewResult> TopDezLivros()
        {
            return View();
        }

        [HttpPost]
        public async Task<ViewResult> TopDezLivros([Bind(Include = "DataDe,DataAte")] RelatorioEmprestimo relatorioEmprestimo)
        {
            await PopulaViewBagUsuario();
            if (relatorioEmprestimo.DataDe > relatorioEmprestimo.DataAte)
            {
                ModelState.AddModelError("erro", "Data De deve ser menor ou igual a Data Até");
                return View();
            }

            ViewBag.Grid = TopDezLivrosPorPeriodo(relatorioEmprestimo);
            return View();
        }
        #endregion

        #region TopUsuarioLivro
        public async Task<ViewResult> TopUsuarioLivro()
        {
            return View();
        }

        [HttpPost]
        public ViewResult TopUsuarioLivro([Bind(Include = "DataDe,DataAte")] RelatorioEmprestimo relatorioEmprestimo)
        {
            if (relatorioEmprestimo.DataDe > relatorioEmprestimo.DataAte)
            {
                ModelState.AddModelError("erro", "Data De deve ser menor ou igual a Data Até");
                return View();
            }

            ViewBag.Grid = TopUsuarioLivroPorPeriodo(relatorioEmprestimo);
            return View();
        }
        #endregion

        #region LivrosAtrasados
        public async Task<ViewResult> LivrosAtrasados()
        {
            await PopulaViewBagUsuario();
            return View();
        }

        [HttpPost]
        public async Task<ViewResult> LivrosAtrasados([Bind(Include = "DataDe,DataAte,MatriculaUsuario")] RelatorioEmprestimo relatorioEmprestimo)
        {
            await PopulaViewBagUsuario();

            var temUsuario = (relatorioEmprestimo.MatriculaUsuario != null || relatorioEmprestimo.MatriculaUsuario <= 0);
            var temPeriodo = relatorioEmprestimo.DataDe != null && relatorioEmprestimo.DataAte != null;

            if (!temPeriodo && temUsuario)
            {
                ViewBag.Grid = LivrosAtrasadosPorUsuario(relatorioEmprestimo);
                return View();
            }

            if (relatorioEmprestimo.DataDe > relatorioEmprestimo.DataAte)
            {
                ModelState.AddModelError("erro", "Data De deve ser menor ou igual a Data Até");
                return View();
            }

            if (temPeriodo && !temUsuario)
            {
                ViewBag.Grid = LivrosAtrasadosPorPeriodo(relatorioEmprestimo);
                return View();
            }

            ViewBag.Grid = LivrosAtrasadosPorPeriodoUsuario(relatorioEmprestimo);
            return View();
        }
        #endregion

        private async Task PopulaViewBagUsuario()
        {
            var usuarios = await db.Usuario.ToListAsync();
            ViewBag.MatriculaUsuario = new SelectList(usuarios, "Matricula", "Nome");
        }

        private IEnumerable<LivrosEmprestadosPorPeriodo> LivrosEmprestadosPorPeriodo(RelatorioEmprestimo relatorioEmprestimo)
        {
            relatorioEmprestimo.DataAte = relatorioEmprestimo.DataAte.GetValueOrDefault().AddDays(1).AddMilliseconds(-1);
            return (from emprestimo in db.Emprestimo
                    where (emprestimo.DataEmprestimo >= relatorioEmprestimo.DataDe ||
                          emprestimo.DataEmprestimo <= relatorioEmprestimo.DataAte)
                    select new LivrosEmprestadosPorPeriodo
                    {
                        DataEmprestimo = emprestimo.DataEmprestimo,
                        NomeLivro = emprestimo.Exemplar.Livro.Titulo + " - " + emprestimo.Exemplar.Edicao,
                        NomeUsuario = emprestimo.MatriculaUsuario + " - " + emprestimo.Usuario.Nome
                    });
        }

        private IEnumerable<LivrosEmprestadosPorPeriodo> LivrosEmprestadosPorPeriodoUsuario(RelatorioEmprestimo relatorioEmprestimo)
        {
            relatorioEmprestimo.DataAte = relatorioEmprestimo.DataAte.GetValueOrDefault().AddDays(1).AddMilliseconds(-1);
            return (from emprestimo in db.Emprestimo
                    where (emprestimo.DataEmprestimo >= relatorioEmprestimo.DataDe ||
                          emprestimo.DataEmprestimo <= relatorioEmprestimo.DataAte) &&
                          emprestimo.MatriculaUsuario == relatorioEmprestimo.MatriculaUsuario
                    select new LivrosEmprestadosPorPeriodo
                    {
                        DataEmprestimo = emprestimo.DataEmprestimo,
                        NomeLivro = emprestimo.Exemplar.Livro.Titulo + " - " + emprestimo.Exemplar.Edicao,
                        NomeUsuario = emprestimo.MatriculaUsuario + " - " + emprestimo.Usuario.Nome
                    });
        }

        private IEnumerable<LivrosEmprestadosPorPeriodo> TopDezLivrosPorPeriodo(RelatorioEmprestimo relatorioEmprestimo)
        {
            relatorioEmprestimo.DataAte = relatorioEmprestimo.DataAte.GetValueOrDefault().AddDays(1).AddMilliseconds(-1);
            var livros = (from emprestimo in db.Emprestimo
                          join livro in db.Livro
                            on emprestimo.IdLivro equals livro.Id
                          where (emprestimo.DataEmprestimo >= relatorioEmprestimo.DataDe ||
                                  emprestimo.DataEmprestimo <= relatorioEmprestimo.DataAte)
                          group emprestimo by new { emprestimo.IdLivro, livro.Titulo }
                          into grupo
                          select new { grupo, count = grupo.Count() }).OrderByDescending(x => x.count).ToList();

            return livros.Take(10).Select(x => new LivrosEmprestadosPorPeriodo
            {
                NomeLivro = x.grupo.Key.Titulo,
                Quantidade = x.count
            }).ToList();

        }

        private IEnumerable<LivrosEmprestadosPorPeriodo> TopUsuarioLivroPorPeriodo(RelatorioEmprestimo relatorioEmprestimo)
        {
            relatorioEmprestimo.DataAte = relatorioEmprestimo.DataAte.GetValueOrDefault().AddDays(1).AddMilliseconds(-1);
            var topUsuarios = (from emprestimo in db.Emprestimo
                               join usuario in db.Usuario
                                 on emprestimo.MatriculaUsuario equals usuario.Matricula
                               where (emprestimo.DataEmprestimo >= relatorioEmprestimo.DataDe ||
                                       emprestimo.DataEmprestimo <= relatorioEmprestimo.DataAte)
                               group emprestimo by new { usuario.Matricula, usuario.Nome }
                          into grupo
                               select new { grupo, count = grupo.Count() }).OrderByDescending(x => x.count).ToList();

            return topUsuarios.Select(x => new LivrosEmprestadosPorPeriodo
            {
                NomeUsuario = x.grupo.Key.Matricula + " - " + x.grupo.Key.Nome,
                Quantidade = x.count
            }).ToList();

        }

        private IEnumerable<LivrosEmprestadosPorPeriodo> LivrosAtrasadosPorPeriodo(RelatorioEmprestimo relatorioEmprestimo)
        {
            relatorioEmprestimo.DataAte = relatorioEmprestimo.DataAte.GetValueOrDefault().AddDays(1).AddMilliseconds(-1);
            return (from emprestimo in db.Emprestimo
                    where (emprestimo.DataEmprestimo >= relatorioEmprestimo.DataDe ||
                          emprestimo.DataEmprestimo <= relatorioEmprestimo.DataAte) &&
                          ((emprestimo.DataDevolucao == null && emprestimo.DataPrevista < DateTime.Now) ||
                          (emprestimo.DataDevolucao > emprestimo.DataPrevista))
                    select new LivrosEmprestadosPorPeriodo
                    {
                        DataEmprestimo = emprestimo.DataEmprestimo,
                        NomeLivro = emprestimo.Exemplar.Livro.Titulo + " - " + emprestimo.Exemplar.Edicao,
                        NomeUsuario = emprestimo.MatriculaUsuario + " - " + emprestimo.Usuario.Nome
                    });
        }

        private IEnumerable<LivrosEmprestadosPorPeriodo> LivrosAtrasadosPorUsuario(RelatorioEmprestimo relatorioEmprestimo)
        {
            return (from emprestimo in db.Emprestimo
                    where emprestimo.MatriculaUsuario == relatorioEmprestimo.MatriculaUsuario &&
                          ((emprestimo.DataDevolucao == null && emprestimo.DataPrevista < DateTime.Now) ||
                          (emprestimo.DataDevolucao > emprestimo.DataPrevista))
                    select new LivrosEmprestadosPorPeriodo
                    {
                        DataEmprestimo = emprestimo.DataEmprestimo,
                        NomeLivro = emprestimo.Exemplar.Livro.Titulo + " - " + emprestimo.Exemplar.Edicao,
                        NomeUsuario = emprestimo.MatriculaUsuario + " - " + emprestimo.Usuario.Nome
                    });
        }

        private IEnumerable<LivrosEmprestadosPorPeriodo> LivrosAtrasadosPorPeriodoUsuario(RelatorioEmprestimo relatorioEmprestimo)
        {
            relatorioEmprestimo.DataAte = relatorioEmprestimo.DataAte.GetValueOrDefault().AddDays(1).AddMilliseconds(-1);
            return (from emprestimo in db.Emprestimo
                    where emprestimo.MatriculaUsuario == relatorioEmprestimo.MatriculaUsuario &&
                          (emprestimo.DataEmprestimo >= relatorioEmprestimo.DataDe ||
                          emprestimo.DataEmprestimo <= relatorioEmprestimo.DataAte) &&
                          ((emprestimo.DataDevolucao == null && emprestimo.DataPrevista < DateTime.Now) ||
                          (emprestimo.DataDevolucao > emprestimo.DataPrevista))
                    select new LivrosEmprestadosPorPeriodo
                    {
                        DataEmprestimo = emprestimo.DataEmprestimo,
                        NomeLivro = emprestimo.Exemplar.Livro.Titulo + " - " + emprestimo.Exemplar.Edicao,
                        NomeUsuario = emprestimo.MatriculaUsuario + " - " + emprestimo.Usuario.Nome
                    });
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
