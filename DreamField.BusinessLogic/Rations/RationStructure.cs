using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamField.BusinessLogic.Rations
{
    public class RationStructure
    {
        const double FULL_PERCENT = 1;

        public double RoughFeeds { get; set; }
        public double JuicyFeeds { get; set; }
        public double GreenFeeds { get; set; }
        public double Consentrates { get; set; }


        public RationStructure(double roughFeedsPercent,double juicyFeedsPercent,
                                double greenFeedsPercent, double concentratesPercent)
        {
            if (roughFeedsPercent + juicyFeedsPercent + greenFeedsPercent + concentratesPercent == FULL_PERCENT)
            {
                RoughFeeds = roughFeedsPercent;
                JuicyFeeds = juicyFeedsPercent;
                GreenFeeds = greenFeedsPercent;
                Consentrates = concentratesPercent;
            }
            else throw new ArgumentException("Sum is not 1");
        }


    }
}
