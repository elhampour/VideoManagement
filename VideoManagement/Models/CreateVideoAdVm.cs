using System.ComponentModel.DataAnnotations;
using System.Web;

namespace VideoManagement.Models
{
    public class CreateVideoAdVm
    {
        [Required]
        public VastVersion? VastVersion { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public int DurationInSeconds { get; set; }

        [Required]
        public HttpPostedFileBase File { get; set; }
    }
}