using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteFlygplan.Models;
using WebsiteFlygplan.Service;

namespace WebsiteFlygplan.Controllers
{
    public class ImageController : Controller
    {
        
        public ActionResult Add()
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
        public ActionResult AddImages(HttpPostedFileBase[] files)
        {
            if(ModelState.IsValid)
            {
                //iterate through multiple file collection
                foreach(HttpPostedFileBase file in files)
                {
                    if(file != null)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        string serverSavePath = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        //save file to server folder
                        file.SaveAs(serverSavePath);
                        //send message to user 
                        ViewBag.Message = files.Count().ToString() + " was successfully uploaded";
                    }
                }
            }

            return View("Add");
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
    }
}