using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace RedmineClientW8.ViewModels
{
    public class TestViewModel:Screen
    {
        private readonly INavigationService _navigationService;

        public TestViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
