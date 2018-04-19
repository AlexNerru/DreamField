using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamField.BusinessLogic
{
    public class CowDTO
    {
        public CowDTO(
            double weight,
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
            Weight = weight;
            WeightIncrement = weightIncrement;
            EnvironmentTemp = environmentTemperature;
            BodyTemp = bodyTempetature;
            DryFeedPercent = dryFeedPercent;
            Humidity = humidity;
            MilkFat = fatContent;
            PregnancyDurability = pregnancyDurability;
            DailyMilk = dailyMilk;
            DayDistance = dayDistance;
            ProteinMilkContent = proteinMilkContent;
            Ltza = ltza;
            CalfWeight = calfWeight;
            PasturePeriod = pasturePeriod;
            LactDay = lactDay;
        }
        public double Weight { get; set; }
        public double WeightIncrement { get; set; }
        public double EnvironmentTemp { get; set; }
        public double BodyTemp { get; set; }
        public double DryFeedPercent { get; set; }
        public double Humidity { get; set; }
        public double MilkFat { get; set; }
        public int PregnancyDurability { get; set; }
        public double DailyMilk { get; set; }
        public double DayDistance { get; set; }
        public double ProteinMilkContent { get; set; }
        public double Ltza { get; set; }
        public double CalfWeight { get; set; }
        public bool PasturePeriod { get; set; }
        public int LactDay { get; set; }
    }
}
