using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamField.BusinessLogic
{
    public class LactatingDTO
    {
        public LactatingDTO(double weight, double milk, double fat, double protein)
        {
            //TODO: Add checking
            Weight = weight;
            DailyMilk = milk;
            Fat = fat;
            Protein = protein;

        }
        public double Weight { get; set; }
        public double DailyMilk { get; set; }
        public double Fat { get; set; }
        public double Protein { get; set; }

    }
}
