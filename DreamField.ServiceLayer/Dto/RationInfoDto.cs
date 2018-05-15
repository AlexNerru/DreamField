using System.Collections.Generic;
using DreamField.Model;

namespace DreamField.ServiceLayer.Dto
{
    public class RationInfoDto
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Animal { get; set; }
        public string FarmName { get; set; }
        public double EnergyFeedUnit { get; set; }
        public double DigestibleProtein { get; set; }
        public double RoughPercent { get; set; }
        public double JuicyPercent { get; set; }
        public double Consentrates { get; set; }
        public List<RationFeedsDto> Feeds { get; set; }
    }
}