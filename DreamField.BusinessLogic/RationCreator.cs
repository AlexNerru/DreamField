using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamField.DataAccessLevel.Generics;
using DreamField.Model;
using System.Data.Entity;
using DreamField.DataAccessLevel;
using DreamField.DataAccessLevel.Interfaces;


namespace DreamField.BusinessLogic
{
    public class RationCreator
    {
        public IGenericUnitOfWork unitOfWork;
        public RationCreator(DbContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }
        public void CreateRation ()
        {
            
            //TODO: Think about input params
        }
        
        
       

    }
}
