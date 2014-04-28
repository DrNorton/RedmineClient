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
    public class NewsViewModel:Screen
    {
        private readonly IRedmineManager _redmineManager;
        private readonly INavigationService _navigationService;
        private News _selectedNews;
        private List<News> _news;

        public NewsViewModel(IRedmineManager redmineManager,INavigationService navigationService)
        {
            _redmineManager = redmineManager;
            _navigationService = navigationService;
        }

        public List<News> News
        {
            get { return _news; }
            set
            {
                _news = value;
                base.NotifyOfPropertyChange(()=>News);
            }
        }

        public News SelectedNews
        {
            get { return _selectedNews; }
            set
            {
                _selectedNews = value;
                NavigateToSingleNews(value);
                base.NotifyOfPropertyChange(()=>SelectedNews);
            }
        }

        private void NavigateToSingleNews(News value)
        {
            _navigationService.UriFor<SingleNewsViewModel>().WithParam(x=>x.NewsId,value.Id).Navigate();
        }

        protected async override void OnInitialize()
        {
            News=(List<News>) await _redmineManager.GetObjectList<News>(null);
            base.OnInitialize();
        }
    }
}
