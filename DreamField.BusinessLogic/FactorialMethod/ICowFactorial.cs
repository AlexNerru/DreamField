using DreamField.Model;

namespace DreamField.BusinessLogic
{
    public interface ICowNorm
    {
        double Carotene { get; }
        double CleavableProtein { get; }
        double Cu { get; }
        double DigestibleProtein { get; }
        double FeedExchangeEnergy { get; }
        double J { get; }
        double K { get; }
        double Сa { get; }
        double Co { get; }
        double Mg { get; }
        double Mn { get; }
        double P { get; }
        double RawCellulose { get; }
        double RawFat { get; }
        double RawProtein { get; }
        double S { get; }
        double Salt { get; }
        double Se { get; }
        double Starch { get; }
        double Sugar { get; }
        double UncleavablaProtein { get; }
        double VitaminD { get; }
        double VitaminE { get; }
        double Zn { get; }
        NormIndexGeneral CreateNorm();
    }
}