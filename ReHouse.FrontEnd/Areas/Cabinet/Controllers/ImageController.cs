using ReHouse.FrontEnd.Helpers;
using ReHouse.Utils.BusinessOperations.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReHouse.FrontEnd.Areas.Cabinet.Controllers
{
    public class ImageController : Controller
    {
        [HttpPost]
        public ActionResult Delete(int imageId)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");

            if (imageId < 1)
                return HttpNotFound();
            var operation = new DeleteImageOperation(imageId);
            operation.ExcecuteTransaction();
            return Json(new { IsSuccess = operation.Success });
        }

        [HttpPost]
        public ActionResult DeletePlan(int imageId)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");

            if (imageId < 1)
                return HttpNotFound();
            var operation = new DeletePlanImageOperation(imageId);
            operation.ExcecuteTransaction();
            return Json(new { IsSuccess = operation.Success });
        }
    }
}