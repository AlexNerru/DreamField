using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamField.BusinessLogic
{
    /// <summary>
    /// 
    /// </summary>
    struct RationStructure
    {
        /// <summary>
        /// Percent of rough feeds in ration
        /// </summary>
        public double RoughFeedsPercent { get; set; }

        /// <summary>
        /// Percent of juicy feeds in ration
        /// </summary>
        public double JuicyFeedsPercent { get; set; }

        /// <summary>
        /// Percent of concentrates in ration
        /// </summary>
        public double ConcentratesPercent { get; set; }

        public RationStructure(double roughFeedsPercent, double juicyFeedsPercent, double concentratesPercent)
        {
                RoughFeedsPercent = roughFeedsPercent;
                JuicyFeedsPercent = juicyFeedsPercent;
                ConcentratesPercent = concentratesPercent;
        }
    }
}
