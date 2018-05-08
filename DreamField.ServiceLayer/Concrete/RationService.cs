﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamField.BusinessLogic;
using System.Data.Entity;
using DreamField.Model;
using DreamField.DataAccessLevel.Interfaces;


namespace DreamField.ServiceLayer
{
    public class RationService : IRationService
    {
        RationsLogic _rations;
        IUnitOfWork _unitOfWork;

        public RationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _rations = new RationsLogic(_unitOfWork);
        }
            
        public Ration Create(int userId, int farmId, int animal, string comment = "")
            => _rations.AddRation(userId, farmId, animal, comment);

        public Norm CalculateNorm(Ration ration, CowDTO data) => _rations.CreateNorm(ration, data);

        public void Calculate (Norm norm, RationStructure structure)
        {
            List<Feed> feeds = _unitOfWork.FeedRepository.GetAll().ToList();
            _rations.Calculate(feeds, structure, norm);
        }

        public IEnumerable<Ration> GetAllRations(int userId) 
            => _unitOfWork.RationRepository.GetAll().Where(ration => ration.Author_id == userId);

        //public RationDto Create(RationDto customerDto)
        
    }
}
