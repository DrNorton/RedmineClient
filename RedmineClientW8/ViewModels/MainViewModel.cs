using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Redmine.Net.Api.Types;
using RedmineApi;
using RedmineClientW8.Views;

namespace RedmineClientW8.ViewModels
{
    public class MainViewModel:Screen
    {
        private INavigationService _navigationService;
        private readonly IRedmineManager _redmineManager;
        private readonly WinRTContainer _container;
        private List<Issue> _myIssues; 
        

        public MainViewModel(IRedmineManager redmineManager,WinRTContainer container)
        {
            _redmineManager = redmineManager;
            _container = container;
            _myIssues=new List<Issue>();
            GetMyIssues();
        }

        protected override void OnViewReady(object view)
        {
            var vw = view as MainView;
            _container.UnregisterHandler(typeof(INavigationService), null);
            _container.RegisterNavigationService(vw.MainNavigationFrame);
            _navigationService = IoC.Get<INavigationService>();
            base.OnViewReady(view);
        }


        public List<Issue> MyIssues
        {
            get { return _myIssues; }
            set
            {
                _myIssues = value;
                base.NotifyOfPropertyChange(()=>MyIssues);
            }
        }

  

        private async void GetMyIssues()
        {
             var dict = new Dictionary<string, string>();
         //   dict.Add("assigned_to_id", "me");
            MyIssues =(List<Issue>) await _redmineManager.GetObjectList<Issue>(null);
          
        }

        //Menu

        public void NavigateToProjectView()
        {
            _navigationService.UriFor<ProjectsViewModel>().Navigate();
        }

        public void NavigateToMyIssues()
        {
            _navigationService.UriFor<IssuesViewModel>().WithParam(x=>x.AssignedToId,"me").Navigate();
        }

        public void NavigateToNews()
        {
            _navigationService.UriFor<NewsViewModel>().Navigate();
        }
    }
}
