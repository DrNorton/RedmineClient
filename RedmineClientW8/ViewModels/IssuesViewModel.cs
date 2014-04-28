using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Caliburn.Micro;
using Redmine.Net.Api.Types;
using RedmineApi;

namespace RedmineClientW8.ViewModels
{
    public class IssuesViewModel:Screen
    {
        private readonly IRedmineManager _redmineManager;
        private readonly INavigationService _navigationService;
        public string ProjectId { get; set; }
        public string AssignedToId { get; set; }
        private ObservableCollection<Issue> _issues;
        private ObservableCollection<Issue> _filteredIssues;
        private Issue _selectedIssue;

        public ObservableCollection<Issue> Issues
        {
            get { return _issues; }
            set
            {
                _issues = value;
                FilteredIssues = _issues;
                base.NotifyOfPropertyChange(()=>Issues);
            }
        }

        public ObservableCollection<Issue> FilteredIssues
        {
            get { return _filteredIssues; }
            set
            {
                _filteredIssues = value;
                base.NotifyOfPropertyChange(()=>FilteredIssues);
            }
        }

        public Issue SelectedIssue
        {
            get { return _selectedIssue; }
            set
            {
                _selectedIssue = value;
                if (value != null)
                {
                    NavigateToSelectedIssue(value);
                }
                base.NotifyOfPropertyChange(() => SelectedIssue);
            }
        }

        private void NavigateToSelectedIssue(Issue value)
        {
           _navigationService.UriFor<SingleIssueViewModel>().WithParam(x=>x.IssueId,value.Id).Navigate();
        }


        public IssuesViewModel(IRedmineManager redmineManager,INavigationService navigationService)
        {
            _redmineManager = redmineManager;
            _navigationService = navigationService;
        }

        protected override void OnInitialize()
        {
            LoadIssues();
            base.OnInitialize();
        }

        public async void LoadIssues()
        {
            var dict = new Dictionary<string, string>();
            if (!String.IsNullOrEmpty(ProjectId))
            {
                dict.Add("project_id",ProjectId);
            }
            if (!String.IsNullOrEmpty(AssignedToId))
            {
                dict = new Dictionary<string, string>();
                dict.Add("assigned_to_id", AssignedToId);
            }
            if (dict.Count == 0)
            {
                dict = null;
            }

            Issues = new ObservableCollection<Issue>(await _redmineManager.GetObjectList<Issue>(dict));
        }
    }
}
