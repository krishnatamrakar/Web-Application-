using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application.Models;

namespace Application.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ActionName("PostLogin")]
        public JsonResult PostLogin(UserModel UsrModel)
        {
            JsonResult objJsonResult = null;
            if (UsrModel != null)
            {
                if (UsrModel.UserName.ToUpper() == "ADMIN" && UsrModel.Password.ToUpper() == "ADMIN")
                {
                    UsrModel.IsUserLogin = true;
                    UsrModel.UserName = "Amir";
                }
                UsrModel.IsUserLogin = true;
                objJsonResult = Json(UsrModel, JsonRequestBehavior.AllowGet);
            }
            return objJsonResult;
        }
	}
}