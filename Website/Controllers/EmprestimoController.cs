using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Biblioteca.Data;
using Biblioteca.Data.Context;

namespace Website.Controllers
{
    public class EmprestimoController : Controller
    {
        private ContextoBiblioteca db = new ContextoBiblioteca();

        // GET: Emprestimo
        public async Task<ActionResult> Index()
        {
            var emprestimo = db.Emprestimo.Include(e => e.Exemplar).Include(e => e.Usuario);
            return View(await emprestimo.ToListAsync());
        }

        // GET: Emprestimo/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprestimo emprestimo = await db.Emprestimo.FindAsync(id);
            if (emprestimo == null)
            {
                return HttpNotFound();
            }
            return View(emprestimo);
        }

        // GET: Emprestimo/Create
        public ActionResult Create()
        {
            var livros = BuscarLivrosDisponiveis().ToList();
            var usuarios = BuscarUsuariosDisponiveis();
            var exemplar = BuscarExemplaresDisponiveis();
            ViewBag.IdExemplar = new SelectList(exemplar, "IdLivro", "Edicao");
            ViewBag.IdLivro = new SelectList(livros, "Id", "Titulo");
            ViewBag.MatriculaUsuario = new SelectList(usuarios, "Matricula", "Nome");
            return View();
        }

        // POST: Emprestimo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MatriculaUsuario,IdExemplar,IdLivro,DataPrevista,DataDevolucao,ValorMulta")] Emprestimo emprestimo)
        {
            ViewBag.IdExemplar = new SelectList(db.Exemplar, "IdLivro", "Edicao", emprestimo.IdLivro);
            ViewBag.IdLivro = new SelectList(db.Livro, "Id", "Titulo", emprestimo.IdLivro);
            ViewBag.MatriculaUsuario = new SelectList(db.Usuario, "Matricula", "Nome", emprestimo.MatriculaUsuario);

            if (await VerificaSeLivroRepetido(emprestimo))
            {
                ModelState.AddModelError("erro", "Já existe exemplar emprestado para este usuario");
                return View();
            }

            if (ModelState.IsValid)
            {
                db.Emprestimo.Add(emprestimo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }


            return View(emprestimo);
        }

        private async Task<bool> VerificaSeLivroRepetido(Emprestimo emprestimo)
        {
            return await (from item in db.Emprestimo
                          where item.IdLivro == emprestimo.IdLivro
                             && item.DataDevolucao == null
                             && item.MatriculaUsuario == emprestimo.MatriculaUsuario
                          select item.IdLivro).SingleOrDefaultAsync() > 0;
        }

        // GET: Emprestimo/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var emprestimo = await db.Emprestimo.FindAsync(id);
            if (emprestimo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdExemplar = new SelectList(db.Exemplar, "IdLivro", "Edicao", emprestimo.IdLivro);
            ViewBag.IdLivro = new SelectList(db.Livro, "Id", "Titulo", emprestimo.IdLivro);
            ViewBag.MatriculaUsuario = new SelectList(db.Usuario, "Matricula", "Nome", emprestimo.MatriculaUsuario);
            return View(emprestimo);
        }

        // POST: Emprestimo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdEmprestimo,DataDevolucao,ValorMulta")] Emprestimo emprestimo)
        {
            var item = await db.Emprestimo.FindAsync(emprestimo.IdEmprestimo);
            if (ModelState.IsValid)
            {
                item.DataDevolucao = emprestimo.DataDevolucao;
                item.ValorMulta = emprestimo.ValorMulta;

                db.Entry(item).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdExemplar = new SelectList(db.Exemplar, "Id", "Edicao", emprestimo.IdExemplar);
            ViewBag.MatriculaUsuario = new SelectList(db.Usuario, "Matricula", "Nome", emprestimo.MatriculaUsuario);
            return View(emprestimo);
        }

        // GET: Emprestimo/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprestimo emprestimo = await db.Emprestimo.FindAsync(id);
            if (emprestimo == null)
            {
                return HttpNotFound();
            }
            return View(emprestimo);
        }

        // POST: Emprestimo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Emprestimo emprestimo = await db.Emprestimo.FindAsync(id);
                db.Emprestimo.Remove(emprestimo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("erro", "Não foi possível excluir, favor verificar as dependências.");
                return View();
            }
        }

        public IQueryable<Livro> BuscarLivrosDisponiveis()
        {
            return (from livro in db.Livro
                    join exemplar in db.Exemplar
                      on livro.Id equals exemplar.IdLivro
                    where !exemplar.Locado
                    select livro).Distinct();
        }

        public IQueryable<Exemplar> BuscarExemplaresDisponiveis()
        {
            var emprestimos = db.Emprestimo;

            var idsExemplaresAlugados = (from exemplar in db.Emprestimo
                                         where exemplar.DataDevolucao == null
                                         select exemplar.IdExemplar).ToList();


            return (from exemplar in db.Exemplar
                    where !idsExemplaresAlugados.Contains(exemplar.Id)
                    select exemplar);
        }

        public IQueryable<Usuario> BuscarUsuariosDisponiveis()
        {
            var idsComQuantidadeMaxima = (from emprestimo in db.Emprestimo
                                          join perfil in db.PerfilUsuario
                                            on emprestimo.Usuario.IdPerfilUsuario equals perfil.Id
                                          where emprestimo.DataDevolucao == null
                                          group emprestimo by new { emprestimo.Usuario.Matricula, perfil.QuantidadeLocacoes } into grupo
                                          select new { grupo, count = grupo.Count() }).ToList()
                .Where(x => x.count >= x.grupo.Key.QuantidadeLocacoes)
                .GroupBy(x => x.grupo.Key.Matricula).Select(x => x.Key).ToList();

            return (from usuario in db.Usuario
                    where !idsComQuantidadeMaxima.Contains(usuario.Matricula)
                    select usuario);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
