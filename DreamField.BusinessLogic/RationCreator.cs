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
        private MilkCowFactorial mcf;
        public RationCreator(DbContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }
        public NormIndexLactating CreateNorm (CowDTO dto)
        {
            mcf = new MilkCowFactorial(dto);
            NormIndexLactating mil = (NormIndexLactating)mcf.CreateNorm();
            Ration rt = new Ration();
            //rt.User = unitOfWork.Repository<User>().GetById(1);
            //rt.Farm1.id = 1;
            //rt.Id = 4;
            //rt.Comment = "hkfk";
            //rt.Creation_datetime = DateTime.Now;
            //rt.Farm = unitOfWork.Repository<Farm>().GetById(1);
            //unitOfWork.Repository<Ration>().Add(rt);
            mil.Ration = unitOfWork.Repository<Ration>().GetById(4);
            //mil.Ration.Id = 3;
            mil.Id = 2;
            unitOfWork.Repository<NormIndexLactating>().Add(mil);
            unitOfWork.SaveChanges();
            return mil;
        }
        public void AddRationToDb(int authorId, int farmId, int animal, string comment)
        {
            Ration ration = new Ration();
            ration.User = unitOfWork.Repository<User>().GetById(authorId);
            ration.Farm = unitOfWork.Repository<Farm>().GetById(farmId);
            ration.Animal = animal;
            ration.Comment = comment;
            ration.Creation_datetime = DateTime.Now;
            unitOfWork.Repository<Ration>().Add(ration);
            unitOfWork.SaveChanges();
        }


        
        
       

    }
}
