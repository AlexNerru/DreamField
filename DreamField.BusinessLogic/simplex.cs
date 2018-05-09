using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SolverFoundation.Common;
using Microsoft.SolverFoundation.Services;
using Microsoft.SolverFoundation.Solvers;
using DreamField.DataAccessLevel.Interfaces;
using DreamField.DataAccessLevel.Generics;
using DreamField.Model;
using System.Collections.Specialized;

namespace DreamField.BusinessLogic
{
    /// <summary>
    /// Represents solver for simplex method
    /// </summary>
    public class Simplex
    {
        private List<Feed> _feeds;
        private List<int> feedIds;
        private SimplexSolver solver = new SimplexSolver();
        

        private Norm _norm;
        private RationStructure _rationStructure;

        /// <summary>
        /// Creates new example of simplex
        /// </summary>
        /// <param name="feeds">Feeds to calculate ration</param>
        /// <param name="norm">Norm to calculate ration</param>
        /// <param name="rationStructure">Structure of ration</param>
        public Simplex(List<Feed> feeds, Norm norm, RationStructure rationStructure)
        {
            _feeds = feeds;
            feedIds = new List<int>(_feeds.Count);

            _norm = norm;
            _rationStructure = rationStructure;
        }

        /// <summary>
        /// Calculates ration for feeds and ration structure in costructor
        /// </summary>
        /// <param name="parametersToOptimise">Names of Norm properties to optimise</param>
        /// <param name="abrormality">percent of abnormality 5% = 0.05</param>
        /// <returns>pairs of feed and it's amount in ration</returns>
        public Dictionary<Feed,double> Calculate(List<string> parametersToOptimise, double abrormality)
        {
            Dictionary<Feed, double> rationFeeds = new Dictionary<Feed, double>();

            //setting abnormality
            double upAbnormality = 1 + abrormality;
            double downAbnormality = 1 - abrormality;
            
            //List to store indexes of parameters in solver
            List<int> toOptimiseIndexes = new List<int>();
            List<Feed> roughFeeds = GetFeedsOfType(FeedTypes.Rough);
            List<Feed> juicyFeeds = GetFeedsOfType(FeedTypes.Juicy);


            AddVariablesToSolver(ref solver);

            AddOptimisationRows(ref solver, parametersToOptimise, ref toOptimiseIndexes);


            #region Rough feeds structure
            //Adding row to normalise rough feeds
            int rough;
            solver.AddRow("feedUnitStructure", out rough);

            for (int i = 0; i < roughFeeds.Count; i++)
            {
                int id = _feeds.IndexOf(roughFeeds[i]);
                solver.SetCoefficient(rough, id, _feeds[id].FeedElement.EnergyFeedUnit);
            }

            double roughFeedUnit = _norm.EnergyFeedUnit * _rationStructure.roughage;

            solver.SetBounds(rough, roughFeedUnit * downAbnormality,
                roughFeedUnit * upAbnormality);
            #endregion

            #region Juicy feeds structure
            //Add row to normalise juicy feeds structure
            int juicy;
            solver.AddRow("juicyStructure", out juicy);

            for (int i = 0; i < juicyFeeds.Count; i++)
            {
                int id = _feeds.IndexOf(juicyFeeds[i]);
                solver.SetCoefficient(juicy, id, _feeds[id].FeedElement.EnergyFeedUnit);
            }

            solver.SetBounds(juicy, _norm.EnergyFeedUnit * _rationStructure.juicy_food * downAbnormality,
                _norm.EnergyFeedUnit * _rationStructure.juicy_food * upAbnormality);
            #endregion

            
            //iterate all parameters to optimise to set it to norm
            for (int j = 0; j < parametersToOptimise.Count; j++)
            {
                //for each parameter adding all feeds coefficient
                for (int i = 0; i < _feeds.Count; i++)
                    solver.SetCoefficient(toOptimiseIndexes[j], feedIds[i], (double)_feeds[i].FeedElement[parametersToOptimise[j]]);

                //settings bounds with abnormality
                solver.SetBounds(toOptimiseIndexes[j], (double)(_norm[parametersToOptimise[j]]) * downAbnormality,
                    (double)_norm[parametersToOptimise[j]] * upAbnormality);
            }

            int cost;
            solver.AddRow("cost", out cost);
            AddCost(ref solver, ref cost);
            solver.AddGoal(cost, 1, true);

            //solving
            solver.Solve(new SimplexSolverParams());

            for (int i = 0; i < feedIds.Count; i++)
                if (solver.GetValue(feedIds[i]).ToDouble() != 0)
                    rationFeeds.Add(_feeds[feedIds[i]], solver.GetValue(feedIds[i]).ToDouble());


#if DEBUG
            Console.WriteLine(solver);

            foreach (var item in feedIds)
                Console.WriteLine($"{_feeds[item].name} - {solver.GetValue(item).ToDouble()}\n");

            foreach (var item in toOptimiseIndexes)
                Console.WriteLine($"{parametersToOptimise[item - feedIds.Count]} - {solver.GetValue(item).ToDouble()}\n");

            Console.WriteLine($"{solver.GetValue(cost).ToDouble()}");
#endif

            return rationFeeds;
        }

        //TODO: move to repositoty
        private List<Feed> GetFeedsOfType (FeedTypes type) => _feeds.Where(feed => feed.type == type).ToList();

        /// <summary>
        /// Add cost coefficient to solver
        /// </summary>
        /// <param name="solver">Simplex solver object</param>
        /// <param name="cost">cost index in solver</param>
        private void AddCost(ref SimplexSolver solver, ref int cost)
        {
            for (int i = 0; i < _feeds.Count; i++)
                solver.SetCoefficient(cost, feedIds[i], (double)_feeds[i].price);
        }

        /// <summary>
        /// Adds variables to solver for each feed
        /// </summary>
        /// <param name="simplexSolver">Simplex solver object</param>
        private void AddVariablesToSolver(ref SimplexSolver simplexSolver)
        {
            int a = 0;
            for (int i = 0; i < _feeds.Count; i++)
            {
                solver.AddVariable(_feeds[i], out a);
                feedIds.Add(a);
                solver.SetBounds(feedIds[i], 0, _feeds[i].current_amount);
                
            }
        }

        /// <summary>
        /// Adds rows for every optimisation parameter
        /// </summary>
        /// <param name="simplexSolver">Simplex solver object</param>
        /// <param name="propertiesToOptimise">List of norm/feedElement properties names</param>
        /// <param name="optimisationIndexes">List of indexes of rows in solver</param>
        private void AddOptimisationRows(ref SimplexSolver simplexSolver,
            List<string> propertiesToOptimise,
            ref List<int> optimisationIndexes)
        {
            int b = 0;
            for (int i = 0; i < propertiesToOptimise.Count; i++)
            {
               solver.AddRow(propertiesToOptimise[i], out b);
               optimisationIndexes.Add(b);
            }
        }


    }
}
