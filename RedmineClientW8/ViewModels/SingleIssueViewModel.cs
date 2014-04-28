using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Redmine.Net.Api.Types;
using RedmineApi;

namespace RedmineClientW8.ViewModels
{
    public class SingleIssueViewModel:Screen
    {
        public int IssueId { get; set; }

        public Issue CurrentIssue
        {
            get { return _currentIssue; }
            set
            {
                _currentIssue = value;
                base.NotifyOfPropertyChange(()=>CurrentIssue);
            }
        }

        private Issue _currentIssue;

        private readonly IRedmineManager _redmineManager;

        public SingleIssueViewModel(IRedmineManager redmineManager)
        {
            _redmineManager = redmineManager;
        }

        protected override void OnInitialize()
        {
            if (IssueId != 0)
            {
                GetIssue(IssueId);
            }
            base.OnInitialize();
        }


        private async void GetIssue(int? issueId)
        {
            CurrentIssue = await _redmineManager.GetObject<Issue>("5099", null);
        }
    }
}
