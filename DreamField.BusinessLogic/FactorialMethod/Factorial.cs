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
        public MilkCowFactorial(CowDTO dto) : base(dto) { }

        /// <summary>
        /// Теплопродукция лактирующих А50
        /// </summary>
        protected override double HeatProduction
        {
            get
            {
                double c = 1;
                if (stats.DayDistance > 0)
                    c += (int)stats.DayDistance * 0.02;
                return (BulkWeight * MinHeatProduction * HeatIncrementBody * c
                    + MilkHeatProduction * stats.DailyMilk
                    + BulkWeight * (EnvironmentHeatProduction + DryFeedIncrement)) * HumidityIncrement;
            }
        }

        /// <summary>
        /// Энергия продукции лактации А52
        /// </summary>
        protected override double EnergyProduction { get { return MilkEnergy * stats.DailyMilk + PregnancyEnergy; } }

        public override double Starch { get { return ReserveAndFeedEnergy * (0.2344 * stats.DailyMilk + 7.625); } }
        public override double Sugar { get { return ReserveAndFeedEnergy * (0.1563 * stats.DailyMilk + 5.048); } }
        public override double RawFat { get { return FeedExchangeEnergy * (0.04375 * stats.DailyMilk + 2.05); } }
        public override double Salt { get { return FeedExchangeEnergy * (0.0313 * stats.DailyMilk + 5.25); } }
        public override double Ka { get { return FeedExchangeEnergy * (0.0032 * stats.DailyMilk + 5.25); } }
        public override double P { get { return Ka * 0.7; } }
        public override double Mg { get { return FeedExchangeEnergy*(0.19 - 0.0125* stats.DailyMilk); } }
        public override double K { get { return FeedExchangeEnergy*0.65; } }
        public override double S { get { return FeedExchangeEnergy*0.21; } }
        public override double Se { get { return FeedExchangeEnergy * (0.25 * stats.DailyMilk + 6.25); } }
        public override double Cu { get { return FeedExchangeEnergy * (0.0103 * stats.DailyMilk + 6.18); } }
        public override double Zn { get { return FeedExchangeEnergy * (0.0625 * stats.DailyMilk + 4.1); } }
        public override double Mn { get { return Zn; } }
        public override double Co { get { return FeedExchangeEnergy * FeedExchangeEnergy * 0.0000725
                    + 0.0631* FeedExchangeEnergy - 1.561; } }
        public override double J { get { return FeedExchangeEnergy * (0.0000938 * stats.DailyMilk + 0.0525); } }
        public override double Carotene { get { return FeedExchangeEnergy * (0.0406 * stats.DailyMilk + 3.076); } }
        public override double VitaminD { get { return FeedExchangeEnergy * 0.09; } }
        public override double VitaminE { get { return FeedExchangeEnergy * (0.0125 * stats.DailyMilk + 3.3); } }

        public override NormIndexGeneral CreateNorm()
        {
            NormIndexLactating norm = new NormIndexLactating();
            throw new NotImplementedException();


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

        protected CowDTO stats;  

        /// <summary>
        /// Обменная масса А00
        /// </summary>
        protected double BulkWeight { get { return Math.Pow(stats.Weight, (double)(3 / 4)); } }

        #region ExcangeEnergy
        /// <summary>
        /// Минимальная теплопродукция А01
        /// </summary>
        protected double MinHeatProduction { get { return 0.33 * BulkWeight; } }

        /// <summary>
        /// Приращение теплопродукции А02
        /// </summary>
        protected double HeatProductionIncrement { get { return Math.Exp(0.6 * stats.WeightIncrement); } }

        /// <summary>
        /// Теплоприращение при изменении температуры А03
        /// </summary>
        protected double EnvironmentHeatProduction { get { return 0.0008 * (stats.BodyTemp - stats.EnvironmentTemp); } }

        /// <summary>
        /// Теплоприращение при варьировании содержания концентрированных кормов А04
        /// </summary>
        protected double DryFeedIncrement { get { return 0.504 - 0.00072 * stats.DryFeedPercent; } }

        /// <summary>
        /// Теплопродукция при повышении влажности воздуха
        /// </summary>
        protected double HumidityIncrement { get { return Math.Exp(0.00446 * (stats.Humidity - 50)); } }

        /// <summary>
        /// Теплоприращение при использовании резервов тела
        /// </summary>
        protected double HeatIncrementBody { get { return Math.Exp(-0.33 * stats.Weight); } }

        /// <summary>
        /// Чистая энергия молока А07
        /// </summary>
        protected double MilkEnergy { get { return 1.337 + 0.444 * stats.MilkFat; } }

        /// <summary>
        /// Выделение тепло МДЖ при биосинтезе 1 кг молока ЭТМ
        /// </summary>
        protected double MilkHeatProduction { get { return 0.99782 * Math.Pow(stats.MilkFat, 0.5332); } }

        /// <summary>
        /// Средние удельные энергетические характеристики прироста массы А08
        /// </summary>
        protected double EnergyStatsOfWeightIncrement { get { return 32 * stats.WeightIncrement; } }

        /// <summary>
        /// Средние удельные характеристики использования массы тела А09
        /// </summary>
        protected double EnergyStatsOfUsingWeight { get { return 24 * stats.WeightIncrement; } }

        /// <summary>
        /// Затраты энергии на рост плода A18
        /// </summary>
        protected double PregnancyEnergy { get { return 1.13 * Math.Exp((0.00001 * stats.Weight + 0.006) * stats.PregnancyDurability); } }

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
                return MilkEnergy * stats.DailyMilk
                    + PregnancyEnergy + EnergyStatsOfUsingWeight * stats.WeightIncrement;
            }
        }

        /// <summary>
        /// Потребность в обменной энергии корма А35
        /// </summary>
        public double FeedExchangeEnergy { get { return ProductionFeedExchangeEnergy + HeatProduction; } }

        /// <summary>
        /// Удой от потребление питательных веществ корма А36
        /// </summary>
        protected double FeedMilkYeild { get { return stats.DailyMilk + (EnergyStatsOfUsingWeight * 0.8) / MilkEnergy; } }

        /// <summary>
        /// Потребность в чистой энергии лактации ЧЭЛ
        /// </summary>
        protected double LactationClearEnergy
        {
            get
            {
                double lce = 0.08 * Math.Pow(stats.Weight, 0.75)
                    + (0.0929 * stats.MilkFat + 0.0547 * stats.ProteinMilkContent + 0.192 * (0.0395 * stats.Ltza)) * stats.DailyMilk
                    + 0.00045 * stats.DayDistance * stats.Weight
                    + ((0.00318 * stats.ProteinMilkContent - 0.0352) * 0.00222 * stats.CalfWeight) / 0.218;
                if (stats.PasturePeriod)
                    return lce + 0.0012 * stats.Weight;
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
                return 114 * BulkWeight + Math.Sqrt(MilkEnergy * stats.DailyMilk) - 3 - 2.828 * Math.Pow(10, -0.0311 * stats.LactDay);
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
                return DryMatterConsumption * 10 * (29.5692 + 0.473822 * stats.DryFeedPercent
                + 0.007701 * Math.Pow(stats.DryFeedPercent,2)
                - 0.00005 * Math.Pow(stats.DryFeedPercent, 3));
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

        public CowNorm(CowDTO dto)
        {
            this.stats = dto;
        }

    }
}
