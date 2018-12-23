using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgorithmsProject.ViewModels;

namespace AlgorithmsProject.Infrastructure
{
    public class ViewModelLocator
    {
        private MainWindowViewModel _mainWindowModel;
        public MainWindowViewModel MainWindowModel
        {
            get
            {
                if(_mainWindowModel == null)
                {
                    _mainWindowModel = new MainWindowViewModel();
                }

                return _mainWindowModel;
            }
        }
    }
}
