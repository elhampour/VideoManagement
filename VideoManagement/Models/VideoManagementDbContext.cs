using System.Data.Entity;

namespace VideoManagement.Models
{
    public class VideoManagementDbContext : DbContext
    {
        public VideoManagementDbContext()
            : base("VideoManagementConnection")
        {
        }

        public DbSet<VideoAd> VideoAds { get; set; }

        public DbSet<VideoVastAd> VideoVastAds { get; set; }
    }
}