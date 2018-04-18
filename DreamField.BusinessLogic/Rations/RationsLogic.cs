﻿using System;
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

        public RationsLogic()
        {
            _unitOfWork = new UnitOfWork();
        }

        public Norm CreateNorm (CowDTO dto)
        {
            _cowFactorial = new MilkCowFactorial(dto);
            Norm mil = _cowFactorial.CreateNorm();
            Simplex simplex = new Simplex(_unitOfWork);
            simplex.Calculate(mil, new RationStructure(0.25,0.5,0.25), 0.01);
            mil.Ration = _unitOfWork.Repository<Ration>().GetById(dto.RationId);
            _unitOfWork.Repository<Norm>().Add(mil);            
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
