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
    public class MilkCowFactorial : CowNorm
    {
        public MilkCowFactorial(double weight, double weightIncrement,
            double fatContent, double dailyMilk, double proteinMilkContent, int lactDay, double bodyTempetature = 38.5,
            double environmentalTemperature = 15, bool pasturePeriod = false,
            int pregnancyDurability = 285, double ltza = 4.85, double dayDistance = 0, double calfWeight = 0) : base(weight, weightIncrement,
            fatContent, dailyMilk, proteinMilkContent, lactDay, bodyTempetature,
            environmentalTemperature, pasturePeriod = false,
            pregnancyDurability, ltza, dayDistance, calfWeight)
        { }

        public MilkCowFactorial(CowDTO dto) : base(dto) { }

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

        public override double Starch { get { return ReserveAndFeedEnergy * (0.2344 * DailyMilk + 7.625); } }
        public override double Sugar { get { return ReserveAndFeedEnergy * (0.1563 * DailyMilk + 5.048); } }
        public override double RawFat { get { return FeedExchangeEnergy * (0.04375 * DailyMilk + 2.05); } }
        public override double Salt { get { return FeedExchangeEnergy * (0.0313 * DailyMilk + 5.25); } }
        public override double Ka { get { return FeedExchangeEnergy * (0.0032 * DailyMilk + 5.25); } }
        public override double P { get { return Ka * 0.7; } }
        public override double Mg { get { return FeedExchangeEnergy*(0.19 - 0.0125*DailyMilk); } }
        public override double K { get { return FeedExchangeEnergy*0.65; } }
        public override double S { get { return FeedExchangeEnergy*0.21; } }
        public override double Se { get { return FeedExchangeEnergy * (0.25 * DailyMilk + 6.25); } }
        public override double Cu { get { return FeedExchangeEnergy * (0.0103 * DailyMilk + 6.18); } }
        public override double Zn { get { return FeedExchangeEnergy * (0.0625 * DailyMilk + 4.1); } }
        public override double Mn { get { return Zn; } }
        public override double Co { get { return FeedExchangeEnergy * FeedExchangeEnergy * 0.0000725
                    + 0.0631* FeedExchangeEnergy - 1.561; } }
        public override double J { get { return FeedExchangeEnergy * (0.0000938 * DailyMilk + 0.0525); } }
        public override double Carotene { get { return FeedExchangeEnergy * (0.0406 * DailyMilk + 3.076); } }
        public override double VitaminD { get { return FeedExchangeEnergy * 0.09; } }
        public override double VitaminE { get { return FeedExchangeEnergy * (0.0125 * DailyMilk + 3.3); } }

        public override NormIndexGeneral CreateNorm()
        {
            NormIndexLactating norm = new NormIndexLactating();
            norm.ExcahngeEnergy = 2.15F;
            norm.ExcahngeEnergy = 2.34F;


            return norm;
        }
    }

    /*class DryCowFactorial : CowNorm
    {
        public DryCowFactorial(double weight, double weightIncrement,
            double fatContent, double dailyMilk, double proteinMilkContent, int lactDay, double bodyTempetature = 38.5,
            double environmentalTemperature = 15, bool pasturePeriod = false,
            int pregnancyDurability = 285, double ltza = 4.85, double dayDistance = 0, double calfWeight = 0) : base(weight, weightIncrement,
            fatContent, dailyMilk, proteinMilkContent, lactDay, bodyTempetature,
            environmentalTemperature, pasturePeriod = false,
            pregnancyDurability, ltza, dayDistance, calfWeight)
        { }

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

    class GrowCowFactorial : CowNorm
    {
        public GrowCowFactorial(double weight, double weightIncrement,
            double fatContent, double dailyMilk, double proteinMilkContent, int lactDay, double bodyTempetature = 38.5,
            double environmentalTemperature = 15, bool pasturePeriod = false,
            int pregnancyDurability = 285, double ltza = 4.85, double dayDistance = 0, double calfWeight = 0) : base(weight, weightIncrement,
            fatContent, dailyMilk, proteinMilkContent, lactDay, bodyTempetature,
            environmentalTemperature, pasturePeriod = false,
            pregnancyDurability, ltza, dayDistance, calfWeight)
        { }

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
    }*/

    /// <summary>
    /// Факториальный метод книги "Оптимизация рационов кормления высокопродуктивных коров
    /// </summary>
    public abstract class CowNorm : ICowNorm
    {

        CowDTO stats;
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
        protected double ProductionFeedExchangeEnergy
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
        public double FeedExchangeEnergy { get { return ProductionFeedExchangeEnergy + HeatProduction; } }

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
        /// Потребление сухого в-ва для коров с удоем меньше 6000кг
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
        /// <summary>
        /// Потребность в сырой клетчатке
        /// </summary>
        public double RawCellulose
        {
            get
            {
                return DryMatterConsumption * 10 * (29.5692 + 0.473822 * DRYPERCENT
                + 0.007701 * DRYPERCENT * DRYPERCENT
                - 0.00005 * Math.Pow(DRYPERCENT, 3));
            }
        }
        #endregion

        #region Elements
        public abstract double Starch { get; }
        public abstract double Sugar { get; }
        public abstract double RawFat { get; }
        public abstract double Salt { get; }
        public abstract double Ka { get; }
        public abstract double P { get; }
        public abstract double Mg { get; }
        public abstract double K { get; }
        public abstract double S { get; }
        public abstract double Se { get; }
        public abstract double Cu { get; }
        public abstract double Zn { get; }
        public abstract double Mn { get; }
        public abstract double Co { get; }
        public abstract double J { get; }
        public abstract double Carotene { get; }
        public abstract double VitaminD { get; }
        public abstract double VitaminE { get; }
        #endregion

        #region Protein
        public double RawProtein { get; }
        public double DigestibleProtein { get; }
        public double CleavablaProtein { get; }
        public double UncleavablaProtein { get; }

        #endregion

        #region Methods
        public abstract NormIndexGeneral CreateNorm();
        #endregion

        public CowNorm(double weight,
            double weightIncrement,
            double environmentTemperature,
            double bodyTempetature,
            double dryFeedPercent,
            double humidity,
            double fatContent,
            int pregnancyDurability,
            double dailyMilk,
            double dayDistance,
            double proteinMilkContent,
            double ltza,
            double calfWeight,
            bool pasturePeriod,
            int lactDay)
        {

            //TODO: Think about exceptions
            Weight = weight;
            WeightIncrement = weightIncrement;
            MilkFatContent = fatContent;
            DailyMilk = dailyMilk;
            MilkProteinContent = proteinMilkContent;
            this.lactDay = lactDay;
            this.BODY_TEMPERATURE = bodyTempetature;
            this.ENVIRONMENT_TEMPERATURE = environmentTemperature;
            this.pasturePeriod = pasturePeriod;
            this.PREGNANCY_DURABILITY = pregnancyDurability;
            this.ltza = ltza;
            this.dayDistance = dayDistance;
            this.calfWeight = calfWeight;
        }

        public CowNorm(CowDTO dto)
        {
            Weight = dto.Weight;
            DailyMilk = dto.DailyMilk;
            MilkFatContent = dto.Fat;
            MilkProteinContent = dto.Protein;
        }

    }
}
