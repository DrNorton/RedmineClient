using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Redmine.Net.Api.Types;
using RedmineApi;
using RedmineClientW8.Common;

namespace RedmineClientW8.ViewModels
{
    public class ProjectsViewModel:Screen
    {
        private readonly INavigationService _navigationService;
        private readonly IRedmineManager _redmineManager;
        private List<Project> _allProjects;
        private TreeViewPageViewModel _treeViewModel;

        public ProjectsViewModel(INavigationService navigationService,IRedmineManager redmineManager)
        {
            _navigationService = navigationService;
            _redmineManager = redmineManager;
            AllProjects=new List<Project>();
           
            GetAllProjects();
        }

        public List<Project> AllProjects
        {
            get { return _allProjects; }
            set
            {
                _allProjects = value;
                base.NotifyOfPropertyChange(()=>AllProjects);
            }
        }

        public TreeViewPageViewModel TreeViewModel
        {
            get { return _treeViewModel; }
            set
            {
                _treeViewModel = value;
                base.NotifyOfPropertyChange(()=>TreeViewModel);
            }
        }

        private async void GetAllProjects()
        {
            AllProjects=(List<Project>)await _redmineManager.GetObjectList<Project>(null);
            TreeViewModel=new TreeViewPageViewModel(AllProjects);
        }
    }
}
