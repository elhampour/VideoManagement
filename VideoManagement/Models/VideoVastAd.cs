namespace VideoManagement.Models
{
    public class VideoVastAd : BaseEntity
    {
        public VastVersion VastVersion { get; set; }

        public VideoVsatAdStatus VideoVsatAdStatus { get; set; }

        public string Path { get; set; }

        public int VideoAdId { get; set; }

        public virtual VideoAd VideoAd { get; set; }

    }
}