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
        public RationsLogic rationCreator;
        public RationService()
        {
            rationCreator = new RationsLogic(new DreamFieldEntities());
        }
        public NormIndexLactating CreateRation (CowDTO dto)
        {
            NormIndexLactating nil= rationCreator.CreateNorm(dto);
            Console.WriteLine("Что-то получилось");
            return nil;
        }
    }
}
