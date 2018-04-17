using DreamField.BusinessLogic;
using DreamField.Model;


namespace DreamField.ServiceLayer
{
    public interface IRationService
    {
        void CalculateNorm(CowDTO dto);
        int CreateRation(int userId, int farmId, int animal, string comment = "");
    }
}