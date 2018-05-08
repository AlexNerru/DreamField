using DreamField.BusinessLogic;
using DreamField.Model;
using System.Collections.Generic;


namespace DreamField.ServiceLayer
{
    public interface IRationService
    {
        Ration Create(int userId, int farmId, int animal, string comment = "");
        Norm CalculateNorm(Ration ration, CowDTO dto);
        void Calculate(Norm norm, RationStructure structure);
        IEnumerable<Ration> GetAllRations(int userId);
    }
}