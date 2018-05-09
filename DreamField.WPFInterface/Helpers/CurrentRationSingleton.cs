﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamField.WPFInterface.Helpers
{
    /// <summary>
    /// Class for passing created ration id between viewmodels
    /// </summary>
    public class CurrentRationSingleton
    {
        public int RationId { get;  set; }
        private CurrentRationSingleton() { }

        /// <summary>
        /// Lazy realization of singleton <see cref="Lazy{T}"/>
        /// </summary>
        private static readonly Lazy<CurrentRationSingleton> lazy =
            new Lazy<CurrentRationSingleton>(() => new CurrentRationSingleton());

        /// <summary>
        /// Returns the instanse of singleton
        /// </summary>
        public static CurrentRationSingleton Source => lazy.Value;
    }
}
