using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamField.BusinessLogic.Interfaces
{
    /// <summary>
    /// Interface for all factorial methods
    /// </summary>
    interface ICowFaсtorial
    {
        //&&
        double EnergyFeedUnit { get; }

        double ExcangeEnergy { get; }
        double DryMatter { get; }
        double ExchangeProtein { get; }
        double CleavableProtein { get; }
        double UnCleavableProtein { get; }
        double RawProtein { get; }
        double DigestibleProtein { get; }

        double Starch { get; }
        double Sugar { get; }
        double RawFat { get;  }
    }
}
