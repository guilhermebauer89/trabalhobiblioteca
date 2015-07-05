using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Biblioteca.Service;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Website.Controllers
{
    public class GridController<T> : Controller, IGridController<T> where T : class
    {
        internal readonly ICrudService<T> CrudService;

        public GridController(ICrudService<T> crudService)
        {
            CrudService = crudService;
        }
        public virtual ActionResult Read(DataSourceRequest request)
        {
            return BuildReturn(CrudService.GetAll(), request, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(DataSourceRequest request, T model)
        {
            if (model != null && ModelState.IsValid)
            {
                model = CrudService.Insert(model);
            }

            return BuildReturn(request, model);
        }

        public ActionResult Update(DataSourceRequest request, T model)
        {
            if (model != null && ModelState.IsValid)
            {
                CrudService.Update(model);
            }

            return BuildReturn(request, model);
        }

        public ActionResult Destroy(DataSourceRequest request, int? id)
        {
            if (id != null && ModelState.IsValid)
            {
                CrudService.Delete(id);
            }

            return BuildReturn(request, id);
        }

        internal ActionResult BuildReturn(IEnumerable<object> models, DataSourceRequest request, JsonRequestBehavior jsonRequestBehavior)
        {
            return Json(models.ToDataSourceResult(request), jsonRequestBehavior);
        }
        internal JsonResult BuildReturn(DataSourceRequest request, object model)
        {
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }


    }
}