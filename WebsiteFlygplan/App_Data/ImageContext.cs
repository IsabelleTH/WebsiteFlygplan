using System.Data.Entity;
using WebsiteFlygplan.Models;

namespace WebsiteFlygplan.Service
{
    public class ImageContext : DbContext
    {
        public ImageContext() :base("ImageContext")
        {

        }

        public DbSet<ImageModel> ImageModels { get; set; }
    }
}