using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Views;

namespace DreamField.WPFInterface
{
    public interface ICustomFrameNavigationService : INavigationService
    {
        object Parameter { get; }
    }
}
