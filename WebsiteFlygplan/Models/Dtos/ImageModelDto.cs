using System.Web;

namespace WebsiteFlygplan.Models.Dtos
{
    public class ImageModelDto
    {
        public int ImageId { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public HttpPostedFileBase[] files { get; set; }
    }
}