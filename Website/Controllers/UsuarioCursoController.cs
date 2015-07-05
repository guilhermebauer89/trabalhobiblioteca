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
    public class UsuarioCursoController : Controller
    {
        private ContextoBiblioteca db = new ContextoBiblioteca();

        public async Task<ActionResult> Index()
        {
            var usuarioCurso = db.UsuarioCurso.Include(u => u.Curso).Include(u => u.Usuario);
            return View(await usuarioCurso.ToListAsync());
        }

        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuarioCurso = await BuscaUsuarioCursoPorId(id);
            if (usuarioCurso == null)
            {
                return HttpNotFound();
            }
            return View(usuarioCurso);
        }

        private async Task<UsuarioCurso> BuscaUsuarioCursoPorId(string id)
        {
            var ids = id.Split('|');
            var idCurso = Convert.ToInt32(ids[0]);
            var matricula = Convert.ToInt32(ids[1]);

            var usuarioCurso = await (from usuario in db.UsuarioCurso
                where usuario.IdCurso == idCurso && usuario.MatriculaUsuario == matricula
                select usuario).SingleAsync();
            return usuarioCurso;
        }

        public ActionResult Create()
        {
            ViewBag.IdCurso = new SelectList(db.Curso, "Id", "Nome");
            ViewBag.MatriculaUsuario = new SelectList(db.Usuario, "Matricula", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdCurso,MatriculaUsuario,Coordenador")] UsuarioCurso usuarioCurso)
        {
            if (ModelState.IsValid)
            {
                db.UsuarioCurso.Add(usuarioCurso);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdCurso = new SelectList(db.Curso, "Id", "Nome", usuarioCurso.IdCurso);
            ViewBag.MatriculaUsuario = new SelectList(db.Usuario, "Matricula", "Nome", usuarioCurso.MatriculaUsuario);
            return View(usuarioCurso);
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var usuarioCurso = await BuscaUsuarioCursoPorId(id);
            if (usuarioCurso == null)
            {
                return HttpNotFound();
            }

            ViewBag.TemCoordenador = await (from item in db.UsuarioCurso
                where item.MatriculaUsuario != usuarioCurso.MatriculaUsuario
                      && item.IdCurso == usuarioCurso.IdCurso
                      && item.Coordenador
                select item.Coordenador).SingleOrDefaultAsync();
            ViewBag.IdCurso = new SelectList(db.Curso, "Id", "Nome", usuarioCurso.IdCurso);
            ViewBag.MatriculaUsuario = new SelectList(db.Usuario, "Matricula", "Nome", usuarioCurso.MatriculaUsuario);
            return View(usuarioCurso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdCurso,MatriculaUsuario,Coordenador")] UsuarioCurso usuarioCurso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarioCurso).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdCurso = new SelectList(db.Curso, "Id", "Nome", usuarioCurso.IdCurso);
            ViewBag.MatriculaUsuario = new SelectList(db.Usuario, "Matricula", "Nome", usuarioCurso.MatriculaUsuario);
            return View(usuarioCurso);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var usuarioCurso = await BuscaUsuarioCursoPorId(id);
            if (usuarioCurso == null)
            {
                return HttpNotFound();
            }
            return View(usuarioCurso);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                UsuarioCurso usuarioCurso = await db.UsuarioCurso.FindAsync(id);
                db.UsuarioCurso.Remove(usuarioCurso);
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
