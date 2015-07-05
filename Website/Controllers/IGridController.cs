using System.Threading.Tasks;
using System.Web.Mvc;
using Kendo.Mvc.UI;

namespace Website.Controllers
{
    public interface IGridController<T> where T : class
    {
        ActionResult Read([DataSourceRequest] DataSourceRequest request);

        [AcceptVerbs(HttpVerbs.Post)]
        ActionResult Create([DataSourceRequest] DataSourceRequest request, T model);

        [AcceptVerbs(HttpVerbs.Post)]
        ActionResult Update([DataSourceRequest] DataSourceRequest request, T product);

        [AcceptVerbs(HttpVerbs.Post)]
        ActionResult Destroy([DataSourceRequest] DataSourceRequest request, int? product);
    }
}