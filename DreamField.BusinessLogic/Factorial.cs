using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamField.Model;
using DreamField.DataAccessLevel.Generics;
using DreamField.DataAccessLevel.Interfaces;
using System.Data.Entity;

namespace DreamField.BusinessLogic
{
    public class MilkCowFactorial : CowFactorial
    {
        public MilkCowFactorial(double weight, double weightIncrement,
            double fatContent, double dailyMilk, double proteinMilkContent, int lactDay, double bodyTempetature = 38.5,
            double environmentalTemperature = 15, bool pasturePeriod = false,
            int pregnancyDurability = 285, double ltza = 4.85, double dayDistance = 0, double calfWeight = 0) : base(weight, weightIncrement,
            fatContent, dailyMilk, proteinMilkContent, lactDay, bodyTempetature,
            environmentalTemperature, pasturePeriod = false,
            pregnancyDurability, ltza, dayDistance, calfWeight) { }

        public MilkCowFactorial(LactatingDTO dto) : base (dto) { }

        /// <summary>
        /// Теплопродукция лактирующих А50
        /// </summary>
        protected override double HeatProduction
        {
            get
            {
                double c = 1;
                if (dayDistance > 0)
                    c += (int)dayDistance * 0.02;
                return (BulkWeight * MinHeatProduction * HeatIncrementBody * c
                    + MilkHeatProduction * DailyMilk
                    + BulkWeight * (EnvironmentHeatProduction + DryFeedIncrement)) * HumidityIncrement;
            }
        }

        /// <summary>
        /// Энергия продукции лактации А52
        /// </summary>
        protected override double EnergyProduction { get { return MilkEnergy * DailyMilk + PregnancyEnergy; } }

        public override NormIndexGeneral CreateNorm()
        {
            NormIndexLactating norm = new NormIndexLactating();
            norm.ExcahngeEnergy = 2.15F;
            norm.ExcahngeEnergy = 2.34F;


            return norm;
        }
    }

    class DryCowFactorial : CowFactorial
    {
        public DryCowFactorial(double weight, double weightIncrement,
            double fatContent, double dailyMilk, double proteinMilkContent, int lactDay, double bodyTempetature = 38.5,
            double environmentalTemperature = 15, bool pasturePeriod = false,
            int pregnancyDurability = 285, double ltza = 4.85, double dayDistance = 0, double calfWeight = 0) : base(weight, weightIncrement,
            fatContent, dailyMilk, proteinMilkContent, lactDay, bodyTempetature,
            environmentalTemperature, pasturePeriod = false,
            pregnancyDurability, ltza, dayDistance, calfWeight) { }
        
        /// <summary>
        /// Теплопродукция сухостойных коров А50
        /// </summary>
        protected override double HeatProduction
        {
            get
            {
                double c = 1;
                if (dayDistance > 0)
                    c += (int)dayDistance * 0.02;
                return (BulkWeight * MinHeatProduction * HeatProductionIncrement * c
                    + BulkWeight * (EnvironmentHeatProduction + DRYPERCENT)) * HumidityIncrement;
            }
        }

        /// <summary>
        /// Энергия продукции сухостойнх А52
        /// </summary>
        protected override double EnergyProduction { get { return EnergyStatsOfWeightIncrement + PregnancyEnergy; } }

        public override NormIndexGeneral CreateNorm()
        {
            throw new NotImplementedException();
        }
    }
    
    class GrowCowFactorial : CowFactorial
    {
        public GrowCowFactorial(double weight, double weightIncrement,
            double fatContent, double dailyMilk, double proteinMilkContent, int lactDay, double bodyTempetature = 38.5,
            double environmentalTemperature = 15, bool pasturePeriod = false,
            int pregnancyDurability = 285, double ltza = 4.85, double dayDistance = 0, double calfWeight = 0) : base(weight, weightIncrement,
            fatContent, dailyMilk, proteinMilkContent, lactDay, bodyTempetature,
            environmentalTemperature, pasturePeriod = false,
            pregnancyDurability, ltza, dayDistance, calfWeight) { }

        /// <summary>
        /// Теплопродукция для положительного прироста массы А50
        /// </summary>
        protected override double HeatProduction
        {
            get
            {
                double c = 1;
                if (dayDistance > 0)
                    c += (int)dayDistance * 0.02;
                return (BulkWeight * MinHeatProduction * HeatProductionIncrement * c
                    + 2.2 * DailyMilk + BulkWeight * (EnvironmentHeatProduction + DryFeedIncrement)) * HumidityIncrement;
            }
        }

        /// <summary>
        /// Энергия продукции плода А52
        /// </summary>
        protected override double EnergyProduction { get { return MilkEnergy * DailyMilk + EnergyStatsOfWeightIncrement + PregnancyEnergy; } }

        public override NormIndexGeneral CreateNorm()
        {
            throw new NotImplementedException();
        }
    }   

    /// <summary>
    /// Факториальный метод книги "Оптимизация рационов кормления высокопродуктивных коров
    /// </summary>
    public abstract class CowFactorial
    {

        #region Protected Fields
        protected readonly double BODY_TEMPERATURE;
        protected readonly double ENVIRONMENT_TEMPERATURE;
        protected readonly int PREGNANCY_DURABILITY;
        protected readonly double DRYPERCENT = 3;
        protected readonly double HUMIDITY = 70;

        protected DbContext context;
        protected int lactDay;
        protected double ltza;
        protected bool pasturePeriod;
        protected double dayDistance;
        protected double calfWeight;

        #endregion

        #region BaseProps
        protected double Weight { get; set; }

        protected double WeightIncrement { get; set; }

        protected double MilkFatContent { get; set; }

        protected double MilkProteinContent { get; set; }

        protected double DailyMilk { get; set; }

        #endregion


        /// <summary>
        /// Обменная масса А00
        /// </summary>
        protected double BulkWeight { get { return Math.Pow(Weight, (double)(3 / 4)); } }

        #region ExcangeEnergy
        /// <summary>
        /// Минимальная теплопродукция А01
        /// </summary>
        protected double MinHeatProduction { get { return 0.33 * BulkWeight; } }

        /// <summary>
        /// Приращение теплопродукции А02
        /// </summary>
        protected double HeatProductionIncrement { get { return Math.Exp(0.6 * WeightIncrement); } }

        /// <summary>
        /// Теплоприращение при изменении температуры А03
        /// </summary>
        protected double EnvironmentHeatProduction { get { return 0.0008 * (BODY_TEMPERATURE - ENVIRONMENT_TEMPERATURE); } }

        /// <summary>
        /// Теплоприращение при варьировании содержания концентрированных кормов А04
        /// </summary>
        protected double DryFeedIncrement { get { return 0.504 - 0.00072 * DRYPERCENT; } }

        /// <summary>
        /// Теплопродукция при повышении влажности воздуха
        /// </summary>
        protected double HumidityIncrement { get { return Math.Exp(0.00446 * (HUMIDITY - 50)); } }

        /// <summary>
        /// Теплоприращение при использовании резервов тела
        /// </summary>
        protected double HeatIncrementBody { get { return Math.Exp(-0.33 * Weight); } }

        /// <summary>
        /// Чистая энергия молока А07
        /// </summary>
        protected double MilkEnergy { get { return 1.337 + 0.444 * MilkFatContent; } }

        /// <summary>
        /// Выделение тепло МДЖ при биосинтезе 1 кг молока ЭТМ
        /// </summary>
        protected double MilkHeatProduction { get { return 0.99782 * Math.Pow(MilkFatContent, 0.5332); } }

        /// <summary>
        /// Средние удельные энергетические характеристики прироста массы А08
        /// </summary>
        protected double EnergyStatsOfWeightIncrement { get { return 32 * WeightIncrement; } }

        /// <summary>
        /// Средние удельные характеристики использования массы тела А09
        /// </summary>
        protected double EnergyStatsOfUsingWeight { get { return 24 * WeightIncrement; } }

        /// <summary>
        /// Затраты энергии на рост плода A18
        /// </summary>
        protected double PregnancyEnergy { get { return 1.13 * Math.Exp((0.00001 * Weight + 0.006) * PREGNANCY_DURABILITY); } }

        /// <summary>
        /// Теплопродукция для положительного прироста массы А50
        /// </summary>
        protected abstract double HeatProduction { get; }

        /// <summary>
        /// Энергия продукции плода А52
        /// </summary>
        protected abstract double EnergyProduction { get; }

        /// <summary>
        /// Обменная энергия веществ корма и использования резервов тела А53
        /// </summary>
        protected double ReserveAndFeedEnergy { get { return HeatProduction + EnergyProduction; } }

        /// <summary>
        /// Затраты обменной энергии за счет потребление кормления А33
        /// </summary>
        protected double FeedExcangeEnergy
        {
            get
            {
                return MilkEnergy * DailyMilk
                    + PregnancyEnergy + EnergyStatsOfUsingWeight * WeightIncrement;
            }
        }

        /// <summary>
        /// Потребность в обменной энергии корма А35
        /// </summary>
        protected double FeedExhangeEnergy { get { return FeedExcangeEnergy + HeatProduction; } }
        
        /// <summary>
        /// Удой от потребление питательных веществ корма А36
        /// </summary>
        protected double FeedMilkYeild { get { return DailyMilk + (EnergyStatsOfUsingWeight * 0.8) / MilkEnergy; } }

        /// <summary>
        /// Потребность в чистой энергии лактации ЧЭЛ
        /// </summary>
        protected double LactationClearEnergy
        {
            get
            {
                double lce = 0.08 * Math.Pow(Weight, 0.75)
                    + (0.0929 * MilkFatContent + 0.0547 * MilkProteinContent + 0.192 * (0.0395 * ltza)) * DailyMilk
                    + 0.00045 * dayDistance * Weight
                    + ((0.00318 * MilkProteinContent - 0.0352) * 0.00222 * calfWeight) / 0.218;
                if (pasturePeriod)
                    return lce + 0.0012 * Weight;
                else
                    return lce;
            }
        }
        #endregion

        #region DryMatterConsuption

        /// <summary>
        /// Потребление суховго в-ва для коров с удоем меньше 6000кг
        /// </summary>
        protected double DryMatterConsumption
        {
            get
            {
                return 114 * BulkWeight + Math.Sqrt(MilkEnergy * DailyMilk) - 3 - 2.828 * Math.Pow(10, -0.0311 * lactDay);
            }
        }

        //TODO: Свойство для высокоудойных коров

        #endregion

        #region RawCellulose

        //public double RawCellulose { get { } }
        #endregion

        #region Methods
        public abstract NormIndexGeneral CreateNorm();
        #endregion

        public CowFactorial(double weight, double weightIncrement,
            double fatContent, double dailyMilk, double proteinMilkContent, int lactDay=2, double bodyTempetature = 38.5,
            double environmentalTemperature = 15, bool pasturePeriod = false,
            int pregnancyDurability = 285, double ltza = 4.85, double dayDistance = 0, double calfWeight = 0)
        {
            
            //Maybe MilkCow DTO?
            //TODO: Think about exceptions
            Weight = weight;
            WeightIncrement = weightIncrement;
            MilkFatContent = fatContent;
            DailyMilk = dailyMilk;
            MilkProteinContent = proteinMilkContent;
            this.lactDay = lactDay;
            this.BODY_TEMPERATURE = bodyTempetature;
            this.ENVIRONMENT_TEMPERATURE = environmentalTemperature;
            this.pasturePeriod = pasturePeriod;
            this.PREGNANCY_DURABILITY = pregnancyDurability;
            this.ltza = ltza;
            this.dayDistance = dayDistance;
            this.calfWeight = calfWeight;
        }

        public CowFactorial(LactatingDTO dto)
        {
            Weight = dto.Weight;
            DailyMilk = dto.DailyMilk;
            MilkFatContent = dto.Fat;
            MilkProteinContent = dto.Protein;
        }

    }
}
