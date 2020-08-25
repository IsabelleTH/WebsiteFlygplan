using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace WebsiteFlygplan.Models
{
    public class ImageModel
    {
        [Key]
        public int ImageId { get; set; }
        public string Title { get; set; }
        [Display(Name = "Upload File")]
        public string ImagePath { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        [NotMapped]
        [Display(Name = "Browse file")]
        public HttpPostedFileBase[] files { get; set; }
    }
}