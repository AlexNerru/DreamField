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

        public RationsLogic(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public Norm CreateNorm (Ration ration, CowDTO dto)
        {
            _cowFactorial = new MilkCowFactorial(dto);
            Norm norm = _cowFactorial.CreateNorm();
            norm.Ration = ration;
            _unitOfWork.NormRepository.Add(norm);            
            _unitOfWork.SaveChanges();
            return norm;
        }

        public void Calculate(List<Feed> feeds, RationStructure structure, Norm norm)
        {
            List<string> toOptimise = new List<string>() { "EnergyFeedUnit", "DigestibleProtein", "DryMatter", "Sugar", "Starch" };
            double abnormality = 0.01;

            Simplex simplex = new Simplex(feeds, norm, structure);
            Dictionary<Feed, double> dict = simplex.Calculate(toOptimise, abnormality);
            foreach (Feed item in dict.Keys)
            {
                RationFeed rf = new RationFeed();
                rf.Ration = norm.Ration;
                rf.Feed = item;
                rf.amount = dict[item];
                norm.Ration.RationFeeds.Add(rf);
            }
            _unitOfWork.SaveChanges();

        }

        public Ration AddRation(int authorId, int farmId, int animal, string comment)
        {
            Ration ration = new Ration();
            ration.User = _unitOfWork.UserRepository.GetById(authorId);
            ration.Farm = _unitOfWork.FarmRepository.GetById(farmId);
            ration.Animal = animal;
            ration.Comment = comment;
            ration.Creation_datetime = DateTime.Now;

            _unitOfWork.RationRepository.Add(ration);
            _unitOfWork.SaveChanges();

            return ration;
        }

    }
}
