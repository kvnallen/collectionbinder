using collectionbinder.Models;
using System.Web.Mvc;

namespace collectionbinder.Controllers
{
    public class PersonController : Controller
    {

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public void Create(Person person)
        {
        }

    }
}
