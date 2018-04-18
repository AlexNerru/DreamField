using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamField.Model;

namespace DreamField.BusinessLogic.Rations
{
    public class RationStructure
    {
        const double FULL_PERCENT = 1;

        public double RoughFeeds { get; set; }
        public double JuicyFeeds { get; set; }
        public double GreenFeeds { get; set; }
        public double Consentrates { get; set; }
        public Norm Norm { get; set; }
            

        public RationStructure(Norm norm, double roughFeedsPercent,double juicyFeedsPercent,
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

        public double EnergyFeedUnitRough => Norm.EnergyFeedUnit * RoughFeeds * 100;

        public double EnergyFeedUnitJuicy => Norm.EnergyFeedUnit * JuicyFeeds * 100;

        public double EnergyFeedUnitRoots => EnergyFeedUnitJuicy / 4;

        public double EnergyFeedUnitSilage => EnergyFeedUnitRoots * 3;


    }
}
