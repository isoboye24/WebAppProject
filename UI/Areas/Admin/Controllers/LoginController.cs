using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DTO;

namespace UI.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        UserBLL userBLL = new UserBLL();
        // GET: Admin/Login
        public ActionResult Index()
        {
            UserDTO dto = new UserDTO();
            return View(dto);
        }
        [HttpPost]
        public ActionResult Index(UserDTO model)
        {
            if (ModelState.IsValid)
            {
                UserDTO user = userBLL.GetUserWithUsernameAndPassword(model);
                if (user.ID !=0)
                {
                    return RedirectToAction("Index", "Post");
                }
                else
                {
                    return View(model);
                }                
            }
            else
            {
                return View(model);
            }
        }
    }
}