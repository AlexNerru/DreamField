using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamField.BusinessLogic
{
    class MilkCowFactorial
    {
        #region Private Fields
        private double weight;
        private double weightIncrement;
        #endregion

        #region BaseProps
        public double Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public double WeightIncrement
        {
            get { return weightIncrement; }
            set { weightIncrement = value; }
        }
        #endregion

        /// <summary>
        /// Обменная масса А00
        /// </summary>
        public double BulkWeight { get {return Math.Pow(Weight,(double)(3/4)); }  }

        /// <summary>
        /// Минимальная теплопродукция А01
        /// </summary>
        public double HeatProduction { get { return 0.33 * BulkWeight; }  }

        /// <summary>
        /// Приращение теплопродукции А02
        /// </summary>
        public double HeatProductionIncrement { get { return Math.Exp(0.6 * WeightIncrement); } }


        public MilkCowFactorial(double weight, double weightIncrement)
        {
            //Maybe MilkCow DTO?
            //TODO: Think about exceptions
            Weight = weight;
        }
    }
}
