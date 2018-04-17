using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamField.BusinessLogic;
using System.Data.Entity;
using DreamField.Model;
using DreamField.DataAccessLevel;


namespace DreamField.ServiceLayer
{
    public class RationService : IRationService
    {
        RationsLogic _rationCreator;

        public RationService()
        {
            _rationCreator = new RationsLogic(new DreamFieldEntities());
        }
        
        public int CreateRation(int userId, int farmId, int animal, string comment = "")
            => _rationCreator.AddRation(userId, farmId, animal, comment);

        public void CalculateNorm(CowDTO data) => _rationCreator.CreateNorm(data);
        
    }
}
