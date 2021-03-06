//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DreamField.Model
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    
    public partial class FeedElement
    {
        public int Id { get; set; }
        public double EnergyFeedUnit { get; set; }
        public double DryMatter { get; set; }
        public double RawProtein { get; set; }
        public double ClevableProtein { get; set; }
        public double UnclevableProtein { get; set; }
        public double DigestibleProtein { get; set; }
        public double RawCellulose { get; set; }
        public double Salt { get; set; }
        public double Ca { get; set; }
        public double P { get; set; }
        public double Mg { get; set; }
        public double Fe { get; set; }
        public double Cu { get; set; }
        public double Zn { get; set; }
        public double Co { get; set; }
        public double K { get; set; }
        public double S { get; set; }
        public double Mn { get; set; }
        public double J { get; set; }
        public double Carotene { get; set; }
        public double VitaminD { get; set; }
        public double VitaminE { get; set; }
        public double VitaminA { get; set; }
        public double Starch { get; set; }
        public double Sugar { get; set; }
        public double RawFat { get; set; }
        public double Na { get; set; }
        public double Se { get; set; }
        public double ExchangeProtein { get; set; }
        public double AcidDetergentFiber { get; set; }
        public double NeutralDetergentFiber { get; set; }
        public double ExcahngeEnergyDryMatter { get; set; }
    
        public virtual Feed Feed { get; set; }

        public object this[string propertyName]
        {
            get
            {
                Type myType = typeof(FeedElement);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                return myPropInfo.GetValue(this, null);
            }
            set
            {
                Type myType = typeof(FeedElement);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                myPropInfo.SetValue(this, value, null);
            }
        }
    }
}
