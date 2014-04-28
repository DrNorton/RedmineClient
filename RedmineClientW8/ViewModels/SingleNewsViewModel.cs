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
    public class SingleNewsViewModel:Screen
    {
        private readonly IRedmineManager _redmineManager;
        private object _currentNews;
        public int NewsId { get; set; }

        public object CurrentNews
        {
            get { return _currentNews; }
            set
            {
                _currentNews = value;
                base.NotifyOfPropertyChange(()=>CurrentNews);
            }
        }

        public SingleNewsViewModel(IRedmineManager redmineManager)
        {
            _redmineManager = redmineManager;
        }

        protected async override void OnInitialize()
        {
           CurrentNews=await _redmineManager.GetObject<News>(NewsId.ToString(),null);
           base.OnInitialize();
        }
    }
}
