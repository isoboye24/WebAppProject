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
        // The program or the view page starts with this controller (empty at first)
        public ActionResult Index()
        {
            UserDTO dto = new UserDTO();
            return View(dto);
        }
        [HttpPost]
        public ActionResult Index(UserDTO model) // when an action is made in the login page, this action processes it and redirect to the post page.
        {
            if (ModelState.IsValid) // If the state is valid as it is in the validation (in the model UserDTO) (i.e. required properties are satisfied).
            {
                UserDTO user = userBLL.GetUserWithUsernameAndPassword(model);
                if (user.ID !=0)
                {
                    UserStatic.UserID = user.ID;
                    UserStatic.isAdmin = user.isAdmin;
                    UserStatic.NameSurname = user.Name;
                    UserStatic.ImagePath = user.ImagePath;
                    LogBLL.AddLog(General.ProcessType.Login, General.TableName.Login, 12);
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