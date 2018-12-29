namespace WebMvc.ViewModels
{
    using System.Collections.Generic;
    using WebMvc.ViewModels;
    using WebMvc.ViewModels.Pagination;
    using WebMvc.ViewModels.Annotations;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    public class CampaignViewModel
    {
        public IEnumerable<CampaignItem> CampaignItems { get; set; }
        public PaginationInfo PaginationInfo { get; set; }

        [LongitudeCoordinate, Required]
        public double Lon { get; set; } = -122.315752;
        [LatitudeCoordinate, Required]
        public double Lat { get; set; } = 47.604610;
    }
}