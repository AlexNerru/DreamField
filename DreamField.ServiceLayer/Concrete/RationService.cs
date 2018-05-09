using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamField.BusinessLogic;
using System.Data.Entity;
using DreamField.Model;
using DreamField.DataAccessLevel.Interfaces;
using DreamField.ServiceLayer.Exceptions;
using DreamField.ServiceLayer.Validators;


namespace DreamField.ServiceLayer
{
    /// <summary>
    /// Handles all activity related to rations
    /// </summary>
    public class RationService : IRationService
    {
        IUnitOfWork _unitOfWork;
        NumericPositivValidator validator = new NumericPositivValidator();

        public RationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        /// <summary>
        /// Creates, if possible, ration in database
        /// </summary>
        /// <param name="userId">Creator of a ration</param>
        /// <param name="farmId">Farm, which will use this ration</param>
        /// <param name="animal">Type of animal, which is ration for</param>
        /// <param name="comment">Any comment or name for ration</param>
        /// <returns></returns>
        public Ration Create(int userId, int farmId, AnimalTypes animal, string comment)
        {
            Ration ration = new Ration();

            User user = _unitOfWork.UserRepository.GetById(userId);
            Farm farm = _unitOfWork.FarmRepository.GetById(farmId);
            if (user != null && farm != null)
            {
                ration.User = user;
                ration.Farm = farm;
            }
            else
                throw new RationCreationException("Wrong user id or farm id was provided");

            ration.Animal = (int)animal;
            ration.Comment = comment;
            ration.Creation_datetime = DateTime.Now;

            _unitOfWork.RationRepository.Add(ration);
            _unitOfWork.SaveChanges();

            return ration;
        }

        /// <summary>
        /// Adds norm to ration by its id
        /// </summary>
        /// <param name="rationId">id of ration</param>
        /// <param name="norm">norm form ration calculation</param>
        /// <returns></returns>
        public Ration Add(int rationId, Norm norm)
        {
            Ration ration = _unitOfWork.RationRepository.GetById(rationId);

            if (ration != null)
            {
                ration.Norm = norm;
                _unitOfWork.SaveChanges();
            }
            else
                throw new RationNotFoundException("Failed to find ration");

            return ration;
        }

        /// <summary>
        /// Creates new norm using factorial method
        /// </summary>
        /// <param name="cowDTO"></param>
        /// <returns></returns>
        public Norm Create(RationStatsDto cowDTO)
        {
            if (validator.Validate(cowDTO).IsValid)
            {
                MilkCowFactorial cowFactorial = new MilkCowFactorial(cowDTO);

                Norm norm = cowFactorial.Create();

                return norm;
            }
            else
                throw new ArgumentException("Check dto values");
        }

        /// <summary>
        /// Adds structure for ration
        /// </summary>
        /// <param name="rationId">id of ration in database</param>
        /// <param name="rationStructure"><see cref="RationStructure"/></param>
        /// <returns></returns>
        public Ration Add(int rationId, RationStructure rationStructure)
        {
            Ration ration = _unitOfWork.RationRepository.GetById(rationId);

            if (ration != null)
            {
                ration.RationStructure = rationStructure;
                _unitOfWork.SaveChanges();
            }
            else
                throw new RationNotFoundException("Ration was not found");
            return ration;
        }

        /// <summary>
        /// Calculates ration by its id
        /// </summary>
        /// <param name="rationId">id of a ration</param>
        /// <returns></returns>
        public Ration Calculate(int rationId)
        {
            //Todo: rework this, add this to method params
            IEnumerable<Feed>  feeds = _unitOfWork.FeedRepository.GetAll();
            List<string> toOptimise = new List<string>() { "EnergyFeedUnit", "DigestibleProtein", "DryMatter", "Sugar", "Starch" };
            double abnormality = 0.01;

            Ration ration = _unitOfWork.RationRepository.GetById(rationId);

            if (ration != null && feeds !=null)
            {
                if (ration.Norm != null && ration.RationStructure != null)
                {
                    Simplex simplex = new Simplex(feeds.ToList(), ration.Norm, ration.RationStructure);
                    Dictionary<Feed, double> dict = simplex.Calculate(toOptimise, abnormality);

                    foreach (Feed item in dict.Keys)
                    {
                        RationFeed rf = new RationFeed();
                        rf.Ration = ration;
                        rf.Feed = item;
                        rf.amount = dict[item];
                        ration.RationFeeds.Add(rf);
                    }
                    _unitOfWork.SaveChanges();
                }
                else
                    throw new ArgumentException("Rations doesn't have norm or structure");
            }
            else
                throw new RationNotFoundException("Ration was not found or feeds are null");

            return ration;
        }

        public IEnumerable<Ration> GetAllRations(int userId) 
            => _unitOfWork.RationRepository.GetAll().Where(ration => ration.Author_id == userId);

    }
}
