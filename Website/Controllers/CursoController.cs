using System;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Biblioteca.Data;
using Biblioteca.Data.Context;

namespace Website.Controllers
{
    public class CursoController : Controller
    {
        private ContextoBiblioteca db = new ContextoBiblioteca();

        // GET: Curso
        public async Task<ActionResult> Index()
        {
            var curso = db.Curso.Include(c => c.Departamento).Include(c => c.Unidade);
            return View(await curso.ToListAsync());
        }

        // GET: Curso/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curso curso = await db.Curso.FindAsync(id);
            if (curso == null)
            {
                return HttpNotFound();
            }
            return View(curso);
        }

        // GET: Curso/Create
        public ActionResult Create()
        {
            ViewBag.IdDepartamento = new SelectList(db.Departamento, "Id", "Descricao");
            ViewBag.IdUnidade = new SelectList(db.Unidade, "Id", "Nome");
            return View();
        }

        // POST: Curso/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,IdUnidade,IdDepartamento,Nome")] Curso curso)
        {
            if (ModelState.IsValid)
            {
                db.Curso.Add(curso);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdDepartamento = new SelectList(db.Departamento, "Id", "Descricao", curso.IdDepartamento);
            ViewBag.IdUnidade = new SelectList(db.Unidade, "Id", "Nome", curso.IdUnidade);
            return View(curso);
        }

        // GET: Curso/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curso curso = await db.Curso.FindAsync(id);
            if (curso == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDepartamento = new SelectList(db.Departamento, "Id", "Descricao", curso.IdDepartamento);
            ViewBag.IdUnidade = new SelectList(db.Unidade, "Id", "Nome", curso.IdUnidade);
            return View(curso);
        }

        // POST: Curso/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,IdUnidade,IdDepartamento,Nome")] Curso curso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(curso).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdDepartamento = new SelectList(db.Departamento, "Id", "Descricao", curso.IdDepartamento);
            ViewBag.IdUnidade = new SelectList(db.Unidade, "Id", "Nome", curso.IdUnidade);
            return View(curso);
        }

        // GET: Curso/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curso curso = await db.Curso.FindAsync(id);
            if (curso == null)
            {
                return HttpNotFound();
            }
            return View(curso);
        }

        // POST: Curso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Curso curso = await db.Curso.FindAsync(id);
                db.Curso.Remove(curso);
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
