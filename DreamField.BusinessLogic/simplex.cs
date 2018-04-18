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
    class Simplex
    {
        private IUnitOfWork _unitOfWork;
        private List<Feed> feeds;
        private List<int> feedIds;
        private SimplexSolver solver = new SimplexSolver();

        public Simplex(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            feeds = _unitOfWork.Repository<Feed>().GetAll().ToList();
            feedIds = new List<int>(feeds.Count);
        }

        public void Calculate(Norm norm, RationStructure rationStructure, double abrormality)
        {

            double upAbnormality = 1 + abrormality;
            double downAbnormality = 1 - abrormality;

            List<string> toOpt = new List<string>() { "EnergyFeedUnit", "DigestibleProtein", "DryMatter", "Sugar", "Starch" };

            List<int> toOptIndexes = new List<int>();

            AddVariablesToSolver(ref solver);
            AddOptimisationRows(ref solver, toOpt, ref toOptIndexes);

            int cost;

            int roughFeedUnitStructure;
            solver.AddRow("feedUnitStructure", out roughFeedUnitStructure);

            List<Feed> roughFeeds = new List<Feed>();
            roughFeeds = feeds.Where(feed => feed.type == FeedTypes.Rough).ToList();


            for (int i = 0; i < roughFeeds.Count; i++)
                solver.SetCoefficient(roughFeedUnitStructure, feeds.IndexOf(roughFeeds[i]), (double)feeds[feeds.IndexOf(roughFeeds[i])].FeedElement.EnergyFeedUnit);
            solver.SetBounds(roughFeedUnitStructure, norm.EnergyFeedUnit * rationStructure.RoughFeedsPercent * downAbnormality,
                norm.EnergyFeedUnit * rationStructure.RoughFeedsPercent * upAbnormality);


            int juicyFeedUnitStructure;
            solver.AddRow("juicyStructure", out juicyFeedUnitStructure);

            List<Feed> juicyFeeds = new List<Feed>();
            juicyFeeds = feeds.Where(feed => feed.type == FeedTypes.Juicy).ToList();


            for (int i = 0; i < juicyFeeds.Count; i++)
                solver.SetCoefficient(juicyFeedUnitStructure, feeds.IndexOf(juicyFeeds[i]), (double)feeds[feeds.IndexOf(juicyFeeds[i])].FeedElement.EnergyFeedUnit);
            solver.SetBounds(juicyFeedUnitStructure, norm.EnergyFeedUnit * rationStructure.JuicyFeedsPercent * downAbnormality,
                norm.EnergyFeedUnit * rationStructure.JuicyFeedsPercent * upAbnormality);

            solver.AddRow("cost", out cost);


            for (int j = 0; j < toOpt.Count; j++)
            {
                for (int i = 0; i < feeds.Count; i++)
                    solver.SetCoefficient(toOptIndexes[j], feedIds[i], (double)feeds[i].FeedElement[toOpt[j]]);

                solver.SetBounds(toOptIndexes[j], (double)(norm[toOpt[j]]) * downAbnormality,
                    (double)norm[toOpt[j]] * upAbnormality);
            }

            for (int i = 0; i < feeds.Count; i++)
                solver.SetCoefficient(cost, feedIds[i], (double)feeds[i].price);

            solver.AddGoal(cost, 1, true);

            solver.Solve(new SimplexSolverParams());

#if DEBUG
            Console.WriteLine(solver);

            foreach (var item in feedIds)
                Console.WriteLine($"{feeds[item].name} - {solver.GetValue(item).ToDouble()}\n");

            foreach (var item in toOptIndexes)
                Console.WriteLine($"{toOpt[item - feedIds.Count]} - {solver.GetValue(item).ToDouble()}\n");

            Console.WriteLine($"{solver.GetValue(cost).ToDouble()}");
            Console.WriteLine("hgfhg");
#endif
        }


        /// <summary>
        /// Adds variables to solver for each feed
        /// </summary>
        /// <param name="simplexSolver">Simplex solver object</param>
        private void AddVariablesToSolver(ref SimplexSolver simplexSolver)
        {
            int a = 0;
            for (int i = 0; i < feeds.Count; i++)
            {
                solver.AddVariable(feeds[i], out a);
                feedIds.Add(a);
                solver.SetBounds(feedIds[i], 0, feeds[i].current_amount);
            }
        }

        /// <summary>
        /// Adds rows for every optimisation parameter
        /// </summary>
        /// <param name="simplexSolver">Simplex solver object</param>
        /// <param name="propertiesToOptimise">List of norm/feedElement properties names</param>
        /// <param name="optimisationIndexes">List of indexes of rows in solver</param>
        private void AddOptimisationRows(ref SimplexSolver simplexSolver, List<string> propertiesToOptimise, ref List<int> optimisationIndexes)
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
