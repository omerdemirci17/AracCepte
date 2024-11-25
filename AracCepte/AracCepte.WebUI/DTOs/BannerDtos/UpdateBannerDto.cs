using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracCepte.WebUI.DTOs.BannerDtos
{
    public class UpdateBannerDto
    {
        public int BannerID { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
    }
}
