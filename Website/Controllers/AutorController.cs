using System.Linq;
using System.Web.Mvc;
using Biblioteca.Data;
using Biblioteca.Data.Context;
using Biblioteca.Data.Repository;
using Biblioteca.Service;
using Kendo.Mvc.UI;

namespace Website.Controllers
{
    public class AutorController : GridController<Autor>
    {
        public AutorController() : base(new CrudService<Autor>(new Repository<Autor>(new ContextoBiblioteca())))
        {
            
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: Autor
        public override ActionResult Read(DataSourceRequest request)
        {
            var autores = CrudService.GetAll().Select(x => new {x.Id, x.Nome}).ToList();
            return BuildReturn(autores, request, JsonRequestBehavior.AllowGet);
        }

    }
}
