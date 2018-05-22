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
        public MilkCowFactorial(RationStatsDto dto) : base(dto) { }

        /// <summary>
        /// Теплопродукция лактирующих А50
        /// </summary>
        protected override double HeatProduction
        {
            get
            {
                //TODO: Change it fot another coef
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
        protected override double EnergyProduction => MilkEnergy * stats.DailyMilk + PregnancyEnergy;
                

        public override Norm Create()
        {
            Norm norm = new Norm
            {
                EnergyFeedUnit = FeedExchangeEnergy / 10,
                DryMatter = DryMatterConsumption,
                RawProtein = RawProtein,
                ClevableProtein = CleavableProtein,
                UnclevableProtein = UncleavablaProtein,
                RawCellulose = RawCellulose,
                DigestibleProtein = DigestibleProtein,

                RawFat = FeedExchangeEnergy * (0.04375 * stats.DailyMilk + 2.05),
                Ca = FeedExchangeEnergy * (0.0032 * stats.DailyMilk + 5.25),
                Carotene = FeedExchangeEnergy * (0.0406 * stats.DailyMilk + 3.076),
                Co = FeedExchangeEnergy * FeedExchangeEnergy * 0.0000725
                                     + 0.0631 * FeedExchangeEnergy
                                     - 1.561,
                Cu = FeedExchangeEnergy * (0.0103 * stats.DailyMilk + 6.18),
                J = FeedExchangeEnergy * (0.0000938 * stats.DailyMilk + 0.0525),
                K = FeedExchangeEnergy * 0.65,
                //TODO: поправлено -  на +
                Mg = FeedExchangeEnergy * (0.19 + 0.0125 * stats.DailyMilk),
                Zn = FeedExchangeEnergy * (0.0625 * stats.DailyMilk + 4.1),
                Mn = FeedExchangeEnergy * (0.0625 * stats.DailyMilk + 4.1),
                P = (FeedExchangeEnergy * (0.0032 * stats.DailyMilk + 5.25)) * 0.7,
                S = FeedExchangeEnergy * 0.21,
                Se = FeedExchangeEnergy * (0.25 * stats.DailyMilk + 6.25),
                Starch = ReserveAndFeedEnergy * (0.2344 * stats.DailyMilk + 7.625),
                Sugar = ReserveAndFeedEnergy * (0.1563 * stats.DailyMilk + 5.048),
                Salt = FeedExchangeEnergy * (0.0313 * stats.DailyMilk + 5.25),
                VitaminD = FeedExchangeEnergy * 0.09,
                VitaminE = FeedExchangeEnergy * (0.0125 * stats.DailyMilk + 3.3),

                NeutralDetergentFiber = 0,
                ExcahngeEnergyDryMatter = 0,
                ExchangeProtein = 0,
                Fe = 0,
                Na = 0,
                VitaminA = 0,
                AcidDetergentFiber = 0

            };
            return norm;
        }
    }


    /// <summary>
    /// Факториальный метод книги "Оптимизация рационов кормления высокопродуктивных коров
    /// </summary>
    public abstract class CowNorm : ICowNorm
    {

        protected RationStatsDto stats;

        /// <summary>
        /// Обменная масса А00
        /// </summary>
        protected double BulkWeight => Math.Pow(stats.Weight, 0.75);

        #region ExcangeEnergy

        /// <summary>
        /// Минимальная теплопродукция А01
        /// </summary>
        protected double MinHeatProduction => 0.33 * BulkWeight;

        /// <summary>
        /// Приращение теплопродукции А02
        /// </summary>
        protected double HeatProductionIncrement => Math.Exp(0.6 * stats.WeightIncrement);

        /// <summary>
        /// Теплоприращение при изменении температуры А03
        /// </summary>
        protected double EnvironmentHeatProduction => 0.0008 * (stats.BodyTemp - stats.EnvironmentTemp);

        /// <summary>
        /// Теплоприращение при варьировании содержания концентрированных кормов А04
        /// </summary>
        protected double DryFeedIncrement => 0.504 - 0.00072 * stats.DryFeedPercent;

        /// <summary>
        /// Теплопродукция при повышении влажности воздуха
        /// </summary>
        protected double HumidityIncrement => Math.Exp(0.00446 * (stats.Humidity - 50));

        /// <summary>
        /// Теплоприращение при использовании резервов тела
        /// </summary>
        protected double HeatIncrementBody => Math.Exp(-0.33 * stats.Weight);

        /// <summary>
        /// Чистая энергия молока А07
        /// </summary>
        protected double MilkEnergy => 1.337 + 0.444 * stats.MilkFat;

        /// <summary>
        /// Выделение тепло МДЖ при биосинтезе 1 кг молока ЭТМ
        /// </summary>
        protected double MilkHeatProduction => 0.99782 * Math.Pow(stats.MilkFat, 0.5332);

        /// <summary>
        /// Средние удельные энергетические характеристики прироста массы А08
        /// </summary>
        protected double EnergyStatsOfWeightIncrement => 32 * stats.WeightIncrement;

        /// <summary>
        /// Средние удельные характеристики использования массы тела А09
        /// </summary>
        protected double EnergyStatsOfUsingWeight => 24 * stats.WeightIncrement;

        /// <summary>
        /// Затраты энергии на рост плода A18
        /// </summary>
        protected double PregnancyEnergy => 1.13 * Math.Exp((0.00001 * stats.Weight + 0.006) * stats.PregnancyDurability);

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
        protected double ReserveAndFeedEnergy => HeatProduction + EnergyProduction;

        /// <summary>
        /// Затраты обменной энергии за счет потребление кормления А33
        /// </summary>
        protected double ProductionFeedExchangeEnergy => MilkEnergy * stats.DailyMilk
                    + PregnancyEnergy + EnergyStatsOfUsingWeight * stats.WeightIncrement;

        /// <summary>
        /// Потребность в обменной энергии корма А35
        /// </summary>
        public double FeedExchangeEnergy => ProductionFeedExchangeEnergy + HeatProduction;

        /// <summary>
        /// Удой от потребление питательных веществ корма А36
        /// </summary>
        protected double FeedMilkYeild => stats.DailyMilk + (EnergyStatsOfUsingWeight * 0.8) / MilkEnergy;

        /// <summary>
        /// Потребность в чистой энергии лактации ЧЭЛ
        /// </summary>
        protected double LactationClearEnergy
        {
            get
            {
                //TODO: Вес теленка
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
        protected double DryMatterConsumption => (114 * BulkWeight 
            + Math.Sqrt(MilkEnergy * stats.DailyMilk) 
            - 3 - 2.828 * Math.Pow(10, -0.0311 * stats.LactDay))/300;

        #endregion

        #region RawCellulose
        /// <summary>
        /// Потребность в сырой клетчатке
        /// </summary>
        protected double RawCellulose => DryMatterConsumption * 10 * (29.5692 + 0.473822 * stats.DryFeedPercent
                + 0.007701 * Math.Pow(stats.DryFeedPercent, 2)
                - 0.00005 * Math.Pow(stats.DryFeedPercent, 3));
        #endregion

        #region Protein
        /// <summary>
        /// Потребность в доступном белке PDBP
        /// </summary>
        protected double AvailableProteinLiving => 2.2 / (0.7 * BulkWeight);

        /// <summary>
        /// Потребность в белке на прирост PBPT
        /// </summary>
        protected double AvailableProteinIncrement => stats.WeightIncrement > 0 ? (150 * stats.WeightIncrement) / 0.5 : (100 * stats.WeightIncrement) / 0.5;

        /// <summary>
        /// Потребность в доступном белке на биосинтез белка
        /// </summary>
        protected double AvailableProteinMilk => stats.DailyMilk * (34 / 0.72);

        /// <summary>
        /// Потребность в белке на формирование плода PBKP
        /// </summary>
        protected double CalfProtein => (-4.494734 + 0.2515092 * stats.PregnancyDurability
                    - 0.00294698 * Math.Pow(stats.PregnancyDurability, 2)
                    + 0.0000145 * Math.Pow(stats.PregnancyDurability, 3)) / 0.5;

        /// <summary>
        /// Доступный протеин Db
        /// </summary>
        protected double AvailableProtein => (AvailableProteinLiving
                        + AvailableProteinIncrement
                        + AvailableProteinMilk + CalfProtein) * 1.1;

        /// <summary>
        /// Что-то микробиологическое
        /// </summary>
        protected double MicroProtein => FeedExchangeEnergy * 7.16 * 0.8 * 0.8;

        /// <summary>
        /// Доступный нерасщепляемы протеин
        /// </summary>
        protected double UncleavablaProtein =>  AvailableProtein - MicroProtein;

        /// <summary>
        /// Сырой расщепляемый протеин
        /// </summary>
        protected double CleavableProtein => (FeedExchangeEnergy * 7.16) / 0.8;

        /// <summary>
        /// Потребность в сыром протеине
        /// </summary>
        protected double RawProtein => (UncleavablaProtein / 0.7) + CleavableProtein;


        protected double DigestibleProtein => (AvailableProteinLiving
                        + AvailableProteinIncrement
                        + AvailableProteinMilk + CalfProtein) * 1.1;


        #endregion

        #region Methods
        public abstract Norm Create();
        #endregion

        public CowNorm(RationStatsDto dto) => this.stats = dto;

    }
}
