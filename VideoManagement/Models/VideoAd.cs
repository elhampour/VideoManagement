using System.Collections.Generic;

namespace VideoManagement.Models
{
    public class VideoAd : BaseEntity
    {
        public string Title { get; set; }

        public string VideoAdPath { get; set; }

        public string Description { get; set; }

        public int DurationInSeconds { get; set; }

        public virtual ICollection<VideoVastAd> VideoVastAds { get; set; }
    }
}