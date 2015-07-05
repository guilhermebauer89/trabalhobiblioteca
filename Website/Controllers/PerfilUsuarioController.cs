using System;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Biblioteca.Data;
using Biblioteca.Data.Context;

namespace Website.Controllers
{
    public class PerfilUsuarioController : Controller
    {
        private ContextoBiblioteca db = new ContextoBiblioteca();

        // GET: PerfilUsuario
        public async Task<ActionResult> Index()
        {
            return View(await db.PerfilUsuario.ToListAsync());
        }

        // GET: PerfilUsuario/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerfilUsuario perfilUsuario = await db.PerfilUsuario.FindAsync(id);
            if (perfilUsuario == null)
            {
                return HttpNotFound();
            }
            return View(perfilUsuario);
        }

        // GET: PerfilUsuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PerfilUsuario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,QuantidadeLocacoes,QuantidadeCursos,ValorMultaDiario,Descricao,PodeSerCoordenador")] PerfilUsuario perfilUsuario)
        {
            if (ModelState.IsValid)
            {
                db.PerfilUsuario.Add(perfilUsuario);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(perfilUsuario);
        }

        // GET: PerfilUsuario/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerfilUsuario perfilUsuario = await db.PerfilUsuario.FindAsync(id);
            if (perfilUsuario == null)
            {
                return HttpNotFound();
            }
            return View(perfilUsuario);
        }

        // POST: PerfilUsuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,QuantidadeLocacoes,QuantidadeCursos,ValorMultaDiario,Descricao,PodeSerCoordenador")] PerfilUsuario perfilUsuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(perfilUsuario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(perfilUsuario);
        }

        // GET: PerfilUsuario/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerfilUsuario perfilUsuario = await db.PerfilUsuario.FindAsync(id);
            if (perfilUsuario == null)
            {
                return HttpNotFound();
            }
            return View(perfilUsuario);
        }

        // POST: PerfilUsuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                PerfilUsuario perfilUsuario = await db.PerfilUsuario.FindAsync(id);
                db.PerfilUsuario.Remove(perfilUsuario);
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
