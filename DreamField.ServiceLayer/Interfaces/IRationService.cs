using DreamField.BusinessLogic;
using DreamField.Model;
using System.Collections.Generic;
using DreamField.ServiceLayer.Dto;


namespace DreamField.ServiceLayer
{
    /// <summary>
    /// Objects that can act like service for rations
    /// </summary>
    public interface IRationService
    {
        //TODO: Change return type to dto
        Ration Create(int userId, int farmId, AnimalTypes animal, string comment);

        Norm Create(RationStatsDto dto);

        Ration Add(int rationId, Norm norm);

        Ration Add(int rationId, RationStructure rationStructure);

        Ration Calculate(int rationId);

        IEnumerable<RationDto> GetAllRations(int userId);
    }
}