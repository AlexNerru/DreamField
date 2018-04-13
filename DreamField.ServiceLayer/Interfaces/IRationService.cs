using DreamField.BusinessLogic;
using DreamField.Model;


namespace DreamField.ServiceLayer
{
    public interface IRationService
    {
        NormIndexLactating CreateRation(CowDTO dto);
    }
}