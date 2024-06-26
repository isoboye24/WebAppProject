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
    public class SocialMediaController : Controller
    {
        // GET: Admin/SocialMedia
        SocialMediaBLL bll = new SocialMediaBLL();
        public ActionResult AddSocialMedia()
        {
            SocialMediaDTO model = new SocialMediaDTO();
            return View(model);
        }
        [HttpPost]
        public ActionResult AddSocialMedia(SocialMediaDTO model)
        {
            if (model.SocialImage == null) 
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;
            }
            else if (ModelState.IsValid)
            {
                HttpPostedFileBase postedFile = model.SocialImage;
                Bitmap SocialMedia = new Bitmap(postedFile.InputStream);
                string ext = Path.GetExtension(postedFile.FileName);
                string filename = "";
                if (ext == ".jpg"||ext == ".jpeg"||ext == ".png"||ext == ".gif")
                {
                    string uniqueNumber = Guid.NewGuid().ToString();
                    filename = uniqueNumber + postedFile.FileName;
                    SocialMedia.Save(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + filename));
                    model.ImagePath = filename;
                    if (bll.AddSocialMedia(model))
                    {
                        ViewBag.ProcessState = General.Messages.AddSuccess;
                        model = new SocialMediaDTO();
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.ProcessState = General.Messages.GeneralError;
                    }
                }
                else
                {
                    ViewBag.ProcessState = General.Messages.ExtensionError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            return View(model);
        }
        public ActionResult SocialMediaList()
        {
            List<SocialMediaDTO> dtoList = new List<SocialMediaDTO>();
            dtoList = bll.GetSocialMedias();
            return View(dtoList);
        }
        public ActionResult UpdateSocialMedia(int ID)
        {
            SocialMediaDTO dto = bll.GetSocialMediaWithID(ID);
            return View(dto);
        }
        [HttpPost]
        public ActionResult UpdateSocialMedia(SocialMediaDTO model)
        {
            if (ModelState.IsValid)
            {
                if (model.SocialImage !=null)
                {
                    HttpPostedFileBase postedFile = model.SocialImage;
                    Bitmap SocialMedia = new Bitmap(postedFile.InputStream);
                    string ext = Path.GetExtension(postedFile.FileName);
                    string filename = "";
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                    {
                        string uniqueNumber = Guid.NewGuid().ToString();
                        filename = uniqueNumber + postedFile.FileName;
                        SocialMedia.Save(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + filename));
                        model.ImagePath = filename;
                    }
                }
                string oldImagePath = bll.UpdateSocialMedia(model);
                if (model.SocialImage !=null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + oldImagePath)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + oldImagePath));
                    }
                }
                ViewBag.ProcessState = General.Messages.UpdateSuccess;
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            return View(model);
        }
    }
}