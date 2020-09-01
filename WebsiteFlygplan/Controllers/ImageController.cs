using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebsiteFlygplan.Models;
using WebsiteFlygplan.Models.Dtos;
using WebsiteFlygplan.Service;
using ActionResult = System.Web.Mvc.ActionResult;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace WebsiteFlygplan.Controllers
{
    public class ImageController : System.Web.Mvc.Controller
    {
        
        public System.Web.Mvc.ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(ImageModel imageModel)
        {
            if (ModelState.IsValid)
            {
                //name of the image uploaded from the view and saving it in fileName
                string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
                //get the file type
                string extension = Path.GetExtension(imageModel.ImageFile.FileName);
                //update the file name with name, date, extension
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                imageModel.ImagePath = "~/Images/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                imageModel.ImageFile.SaveAs(fileName);
                using (ImageContext context = new ImageContext())
                {
                    context.ImageModels.Add(imageModel);
                    context.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = "File uploaded successfully";
            }
            return View();
        }

        public ActionResult AddImages()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddImages(ImageModel model)
        {
            
            if(ModelState.IsValid)
            {
                //iterate through multiple file collection
                foreach(HttpPostedFileBase file in model.files)
                {
                    if(file != null)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        model.ImagePath = fileName;
                        string serverSavePath = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        //save file to server folder
                        file.SaveAs(serverSavePath);
                        //send message to user 
                    }
                }

                using (ImageContext context = new ImageContext())
                {
                    for (int i = 0; i < model.files.Length; i++)
                    {
                        context.ImageModels.Add(model);
                        context.SaveChanges();
                    }
                }

                ViewBag.Message = model.files.Count().ToString() + " files was successfully uploaded";
            }

            return View("Add");
        }

        public ActionResult DisplayImages()
        {
            ImageContext context = new ImageContext();
            var images = (from i in context.ImageModels
                          select new ImageModelDto
                          {
                              ImageId = i.ImageId,
                              ImagePath = i.ImagePath
                          }).ToList();
            return View(images);
        }

        public ActionResult View(int id)
        {
            ImageModel model = new ImageModel();
            using(ImageContext context = new ImageContext())
            {
              model = context.ImageModels.Where(m => m.ImageId == id).FirstOrDefault();
            }
            return View(model);
        }

       

        public ActionResult ViewImages(int? id)
        {
            ImageContext db = new ImageContext();

            if(id == null)
            {
                return View("Index");
            }

           
            return View();
        }

        public IEnumerable<ImageModel> ViewImageModels()
        {
            ImageContext context = new ImageContext();
            var a = from o in context.ImageModels
                    select new ImageModel
                    {
                        ImagePath = o.ImagePath
                    };
            return a.ToList();
        }
    }
}