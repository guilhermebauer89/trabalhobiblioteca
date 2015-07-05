using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Biblioteca.Data;
using Biblioteca.Data.Context;

namespace Website.Controllers
{
    public class LivroController : Controller
    {
        private ContextoBiblioteca db = new ContextoBiblioteca();

        // GET: Livro
        public async Task<ActionResult> Index()
        {
            var livro = db.Livro.Include(l => l.Autor).Include(l => l.Editora);
            return View(await livro.ToListAsync());
        }

        // GET: Livro/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = await db.Livro.FindAsync(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }

        // GET: Livro/Create
        public ActionResult Create()
        {
            ViewBag.IdAutor = new SelectList(db.Autor, "Id", "Nome");
            ViewBag.IdEditora = new SelectList(db.Editora, "Id", "Nome");
            return View();
        }

        // POST: Livro/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,IdAutor,IdEditora,Titulo")] Livro livro)
        {
            ViewBag.IdAutor = new SelectList(db.Autor, "Id", "Nome", livro.IdAutor);
            ViewBag.IdEditora = new SelectList(db.Editora, "Id", "Nome", livro.IdEditora);

            if (await ExisteTitulo(livro))
            {
                ModelState.AddModelError("erro", "Título já cadastrado.");
                return View();
            }

            if (ModelState.IsValid)
            {
                db.Livro.Add(livro);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(livro);
        }

        private async Task<bool> ExisteTitulo(Livro livro)
        {
            var existe =
                await
                    (from item in db.Livro where item.Titulo == livro.Titulo select item.Titulo).SingleOrDefaultAsync();

            return !string.IsNullOrEmpty(existe);
        }

        // GET: Livro/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = await db.Livro.FindAsync(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAutor = new SelectList(db.Autor, "Id", "Nome", livro.IdAutor);
            ViewBag.IdEditora = new SelectList(db.Editora, "Id", "Nome", livro.IdEditora);
            return View(livro);
        }

        // POST: Livro/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,IdAutor,IdEditora,Titulo")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(livro).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdAutor = new SelectList(db.Autor, "Id", "Nome", livro.IdAutor);
            ViewBag.IdEditora = new SelectList(db.Editora, "Id", "Nome", livro.IdEditora);
            return View(livro);
        }

        // GET: Livro/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = await db.Livro.FindAsync(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }

        // POST: Livro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Livro livro = await db.Livro.FindAsync(id);
                db.Livro.Remove(livro);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("erro", "Não foi possível excluir, favor verificar as dependências.");
                return View();
            }
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
