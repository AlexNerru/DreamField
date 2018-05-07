using DreamField.BusinessLogic;
using DreamField.Model;
using System.Collections.Generic;


namespace DreamField.ServiceLayer
{
    public interface IRationService
    {
        Ration CreateRation(int userId, int farmId, int animal, string comment = "");
        Norm CalculateNorm(Ration ration, CowDTO dto);
        void CalculateRation(Norm norm, RationStructure structure);
        IEnumerable<Ration> GetAllRations(int userId);
    }
}