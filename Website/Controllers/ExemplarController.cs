using System;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Biblioteca.Data;
using Biblioteca.Data.Context;

namespace Website.Controllers
{
    public class ExemplarController : Controller
    {
        private ContextoBiblioteca db = new ContextoBiblioteca();

        // GET: Exemplar
        public async Task<ActionResult> Index()
        {
            var exemplar = db.Exemplar.Include(e => e.Livro).Include(e => e.Unidade);
            return View(await exemplar.ToListAsync());
        }

        // GET: Exemplar/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exemplar exemplar = await db.Exemplar.FindAsync(id);
            if (exemplar == null)
            {
                return HttpNotFound();
            }
            return View(exemplar);
        }

        // GET: Exemplar/Create
        public ActionResult Create()
        {
            ViewBag.IdLivro = new SelectList(db.Livro, "Id", "Titulo");
            ViewBag.IdUnidade = new SelectList(db.Unidade, "Id", "Nome");
            return View();
        }

        // POST: Exemplar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,IdLivro,IdUnidade,Edicao,AnoEdicao,Locado")] Exemplar exemplar)
        {
            if (ModelState.IsValid)
            {
                db.Exemplar.Add(exemplar);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdLivro = new SelectList(db.Livro, "Id", "Titulo", exemplar.IdLivro);
            ViewBag.IdUnidade = new SelectList(db.Unidade, "Id", "Nome", exemplar.IdUnidade);
            return View(exemplar);
        }

        // GET: Exemplar/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exemplar exemplar = await db.Exemplar.FindAsync(id);
            if (exemplar == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdLivro = new SelectList(db.Livro, "Id", "Titulo", exemplar.IdLivro);
            ViewBag.IdUnidade = new SelectList(db.Unidade, "Id", "Nome", exemplar.IdUnidade);
            return View(exemplar);
        }

        // POST: Exemplar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,IdLivro,IdUnidade,Edicao,AnoEdicao,Locado")] Exemplar exemplar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exemplar).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdLivro = new SelectList(db.Livro, "Id", "Titulo", exemplar.IdLivro);
            ViewBag.IdUnidade = new SelectList(db.Unidade, "Id", "Nome", exemplar.IdUnidade);
            return View(exemplar);
        }

        // GET: Exemplar/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exemplar exemplar = await db.Exemplar.FindAsync(id);
            if (exemplar == null)
            {
                return HttpNotFound();
            }
            return View(exemplar);
        }

        // POST: Exemplar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Exemplar exemplar = await db.Exemplar.FindAsync(id);
                db.Exemplar.Remove(exemplar);
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
