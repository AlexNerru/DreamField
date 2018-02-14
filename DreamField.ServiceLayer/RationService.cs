using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamField.BusinessLogic;

namespace DreamField.ServiceLayer
{
    public class RationService
    {
        public RationCreator rationCreator;
        public RationService()
        {
            rationCreator = new RationCreator();
        }
        public static void CreateRation ()
        {            
            Console.WriteLine("Что-то получилось");
        }
    }
}
