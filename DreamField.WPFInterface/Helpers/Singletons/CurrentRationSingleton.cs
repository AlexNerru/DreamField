using System;

namespace DreamField.WPFInterface.Helpers.Singletons
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
        private static readonly Lazy<CurrentRationSingleton> Lazy =
            new Lazy<CurrentRationSingleton>(() => new CurrentRationSingleton());

        /// <summary>
        /// Returns the instanse of singleton
        /// </summary>
        public static CurrentRationSingleton Source => Lazy.Value;
    }
}
