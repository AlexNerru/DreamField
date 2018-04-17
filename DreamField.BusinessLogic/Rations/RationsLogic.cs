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
        private IUnitOfWork _unitOfWork;
        private MilkCowFactorial _cowFactorial;

        public RationsLogic(DbContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }

        public NormIndexLactating CreateNorm (CowDTO dto)
        {
            _cowFactorial = new MilkCowFactorial(dto);
            NormIndexLactating mil = (NormIndexLactating)_cowFactorial.CreateNorm();
            mil.Ration = _unitOfWork.Repository<Ration>().GetById(dto.RationId);
            _unitOfWork.Repository<NormIndexGeneral>().Add(mil);            
            _unitOfWork.SaveChanges();
            return mil;
        }
        public int AddRation(int authorId, int farmId, int animal, string comment)
        {
            Ration ration = new Ration();
            ration.User = _unitOfWork.Repository<User>().GetById(authorId);
            ration.Farm = _unitOfWork.Repository<Farm>().GetById(farmId);
            ration.Animal = animal;
            ration.Comment = comment;
            ration.Creation_datetime = DateTime.Now;
            _unitOfWork.Repository<Ration>().Add(ration);
            _unitOfWork.SaveChanges();
            return ration.Id;
        }
    }
}
