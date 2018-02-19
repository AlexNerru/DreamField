﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamField.BusinessLogic;
using System.Data.Entity;
using DreamField.Model;


namespace DreamField.ServiceLayer
{
    public class RationService
    {
        public RationCreator rationCreator;
        public RationService(DbContext context)
        {
            rationCreator = new RationCreator(context);
        }
        public NormIndexLactating CreateRation (LactatingDTO dto)
        {
            NormIndexLactating nil= rationCreator.CreateRation(dto);
            Console.WriteLine("Что-то получилось");
            return nil;
        }
    }
}
