﻿using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        UserBLL bll = new UserBLL();
        // GET: Admin/User
        public ActionResult AddUser()
        {
            UserDTO model = new UserDTO();
            return View(model);
        }
        [HttpPost]
        public ActionResult AddUser(UserDTO model)
        {
            if (model.UserImage == null)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;
            }
            else if (ModelState.IsValid)
            {
                string filename = "";
                HttpPostedFileBase postedFile = model.UserImage;
                Bitmap UserImage = new Bitmap(postedFile.InputStream);
                Bitmap resizeImage = new Bitmap(UserImage, 128, 128);
                string ext = Path.GetExtension(postedFile.FileName);
                if (ext==".jpg"||ext==".jpeg"||ext==".png"||ext==".gif")
                {
                    string uniqueNumber = Guid.NewGuid().ToString();
                    filename = uniqueNumber + postedFile.FileName;
                    resizeImage.Save(Server.MapPath("~/Areas/Admin/Content/userImage/" + filename));
                    model.ImagePath = filename;
                    bll.AddUser(model);
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    model = new UserDTO();
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            return View(model);
        }
        public ActionResult UserList()
        {
            List<UserDTO> model = new List<UserDTO>();
            model = bll.GetUsers();
            return View(model);
        }
        public ActionResult UpdateUser(int ID)
        {
            UserDTO dto = new UserDTO();
            dto = bll.GetUserWithID(ID);
            return View(dto);            
        }
        [HttpPost]
        public ActionResult UpdateUser(UserDTO model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            else
            {
                if (model.UserImage != null)
                {
                    string filename = "";
                    HttpPostedFileBase postedFile = model.UserImage;
                    Bitmap UserImage = new Bitmap(postedFile.InputStream);
                    Bitmap resizeImage = new Bitmap(UserImage, 128, 128);
                    string ext = Path.GetExtension(postedFile.FileName);
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                    {
                        string uniqueNumber = Guid.NewGuid().ToString();
                        filename = uniqueNumber + postedFile.FileName;
                        resizeImage.Save(Server.MapPath("~/Areas/Admin/Content/userImage/" + filename));
                        model.ImagePath = filename;
                    }
                    string oldImagePath = bll.UpdateUser(model);
                    if (model.UserImage != null)
                    {
                        if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/UserImage/" + oldImagePath)))
                        {
                            System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/UserImage/" + oldImagePath));
                        }
                    }
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
                }
            }
            return View(model);
        }
    }
}