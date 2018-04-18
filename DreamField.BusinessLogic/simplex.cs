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

namespace DreamField.BusinessLogic
{
    class Simplex
    {
        static IUnitOfWork _unitOfWork = new UnitOfWork();

        static public void Calucate(Norm norm)
        {
            SimplexSolver solver = new SimplexSolver();


            int hay, silage, cake, potapo, barley, mesassa;

            solver.AddVariable(_unitOfWork.Repository<Feed>().GetById(1), out hay);
            solver.SetBounds(hay, 0, _unitOfWork.Repository<Feed>().GetById(1).current_amount);
            solver.AddVariable(_unitOfWork.Repository<Feed>().GetById(2), out silage);
            solver.SetBounds(silage, 0, _unitOfWork.Repository<Feed>().GetById(2).current_amount);
            solver.AddVariable(_unitOfWork.Repository<Feed>().GetById(6), out cake);
            solver.SetBounds(cake, 0, _unitOfWork.Repository<Feed>().GetById(6).current_amount);


            int feedUnit, digestibleProtein, cost;
            solver.AddRow("feedUnit", out feedUnit);
            solver.AddRow("digestibleProtein", out digestibleProtein);
            solver.AddRow("cost", out cost);

            solver.SetCoefficient(feedUnit, hay, _unitOfWork.Repository<Feed>().GetById(1).FeedElement.EnergyFeedUnit);
            solver.SetCoefficient(feedUnit, silage, _unitOfWork.Repository<Feed>().GetById(2).FeedElement.EnergyFeedUnit);
            solver.SetCoefficient(feedUnit, cake, _unitOfWork.Repository<Feed>().GetById(6).FeedElement.EnergyFeedUnit);
            solver.SetBounds(feedUnit, norm.EnergyFeedUnit-20, norm.EnergyFeedUnit + 20);

            solver.SetCoefficient(digestibleProtein, hay, _unitOfWork.Repository<Feed>().GetById(1).FeedElement.DigestibleProtein);
            solver.SetCoefficient(digestibleProtein, silage, _unitOfWork.Repository<Feed>().GetById(2).FeedElement.DigestibleProtein);
            solver.SetCoefficient(digestibleProtein, cake, _unitOfWork.Repository<Feed>().GetById(6).FeedElement.DigestibleProtein);
            solver.SetBounds(digestibleProtein, norm.DigestibleProtein-20, norm.DigestibleProtein + 20);



            solver.SetCoefficient(cost, hay, (double)_unitOfWork.Repository<Feed>().GetById(1).price);
            solver.SetCoefficient(cost, silage, (double)_unitOfWork.Repository<Feed>().GetById(2).price);
            solver.SetCoefficient(cost, cake, (double)_unitOfWork.Repository<Feed>().GetById(6).price);
            solver.AddGoal(cost, 1, true);

            solver.Solve(new SimplexSolverParams());

            Console.WriteLine(solver);
            Console.WriteLine($"{solver.GetValue(hay).ToDouble()},{solver.GetValue(silage).ToDouble()},{solver.GetValue(cake).ToDouble()}, эке {solver.GetValue(feedUnit).ToDouble()}, {solver.GetValue(digestibleProtein).ToDouble()}, {solver.GetValue(cost).ToDouble()}");

            Console.WriteLine("sfs");
        }

       static public void CalculateForAll(Norm norm)
        {
            List<string> toOpt = new List<string>() { "EnergyFeedUnit", "DigestibleProtein", "DryMatter" };

            SimplexSolver solver = new SimplexSolver();

            List<Feed> feeds = _unitOfWork.Repository<Feed>().GetAll().ToList();
            
            List<int> feedIds = new List<int>(feeds.Count);

            Dictionary<string, int> optimizationStats = new Dictionary<string, int>();

            for (int i = 0; i < feeds.Count; i++)
            {
                int a = 0;
                solver.AddVariable(feeds[i], out a);
                feedIds.Add(a);
                solver.SetBounds(feedIds[i], 0, feeds[i].current_amount);
            }

            int feedUnit, digestibleProtein, cost;
            solver.AddRow("feedUnit", out feedUnit);
            solver.AddRow("digestibleProtein", out digestibleProtein);
            

            //for (int i = 0; i < toOpt.Count; i++)
            //{
            //    int b = 0;
            //    solver.AddRow(toOpt[i], out b);
            //    optimizationStats[toOpt[i]] = b;
            //}
            solver.AddRow("cost", out cost);


            for (int i = 0; i < feeds.Count; i++)
                solver.SetCoefficient(feedUnit, feedIds[i], feeds[i].FeedElement.EnergyFeedUnit);
            solver.SetBounds(feedUnit, norm.EnergyFeedUnit - (norm.EnergyFeedUnit*0.05), norm.EnergyFeedUnit + (norm.EnergyFeedUnit * 0.05));

            for (int i = 0; i < feeds.Count; i++)
                solver.SetCoefficient(digestibleProtein, feedIds[i], feeds[i].FeedElement.DigestibleProtein);

            solver.SetBounds(digestibleProtein, norm.DigestibleProtein - (norm.DigestibleProtein*0.05),
                norm.DigestibleProtein + (norm.DigestibleProtein*0.05));

            for (int i = 0; i < feeds.Count; i++)
                solver.SetCoefficient(cost, feedIds[i], (double)feeds[i].price);

            solver.Solve(new SimplexSolverParams());

            Console.WriteLine(solver);

            foreach (var item in feedIds)
                Console.WriteLine($"{solver.GetValue(item).ToDouble()}\n");
            Console.WriteLine($"{solver.GetValue(feedUnit).ToDouble()}, {solver.GetValue(digestibleProtein).ToDouble()}, {solver.GetValue(cost).ToDouble()}");
            Console.WriteLine("hgfhg");
        }
    }
}
