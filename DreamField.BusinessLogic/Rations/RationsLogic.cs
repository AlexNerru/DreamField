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
    public class RationsLogic
    {
        public IGenericUnitOfWork unitOfWork;
        private MilkCowFactorial mcf;
        public RationsLogic(DbContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }
        public NormIndexLactating CreateNorm (CowDTO dto)
        {
            mcf = new MilkCowFactorial(dto);
            NormIndexLactating mil = (NormIndexLactating)mcf.CreateNorm();
            mil.Ration = unitOfWork.Repository<Ration>().GetById(dto.RationId);
            unitOfWork.Repository<NormIndexGeneral>().Add(mil);            
            unitOfWork.SaveChanges();
            return mil;
        }
        public int AddRationToDb(int authorId, int farmId, int animal, string comment)
        {
            Ration ration = new Ration();
            ration.User = unitOfWork.Repository<User>().GetById(authorId);
            ration.Farm = unitOfWork.Repository<Farm>().GetById(farmId);
            ration.Animal = animal;
            ration.Comment = comment;
            ration.Creation_datetime = DateTime.Now;
            unitOfWork.Repository<Ration>().Add(ration);
            return ration.Id;
        }
    }
}
