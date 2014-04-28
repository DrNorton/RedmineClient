using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227
using Caliburn.Micro;
using RedmineApi;
using RedmineClientW8.ViewModels;
using RedmineClientW8.Views;

namespace RedmineClientW8
{
    public sealed partial class App
    {
      
        private WinRTContainer container;
        

        public App()
        {
            InitializeComponent();
        }

        public WinRTContainer Container
        {
            get { return container; }
        }

        protected override void Configure()
        {
            container = new WinRTContainer();
            Container.RegisterWinRTServices();
            Container.PerRequest<MainViewModel>();
            Container.PerRequest<ProjectsViewModel>();
            Container.PerRequest<TestViewModel>();
            Container.PerRequest<IssuesViewModel>();
            Container.PerRequest<SingleIssueViewModel>();
            Container.PerRequest<NewsViewModel>();
            Container.PerRequest<SingleNewsViewModel>();
            Container.RegisterInstance(typeof(IRedmineManager), null, new RedmineManager("434013178d5a1fcf3aca653f96de99f8d47a453d", "http://91.228.153.167:8002"));
        }

        protected override object GetInstance(Type service, string key)
        {
            var instance = Container.GetInstance(service, key);
            if (instance != null)
                return instance;
            throw new Exception("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return Container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            Container.BuildUp(instance);
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
           Container.RegisterNavigationService(rootFrame);
        }
        
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            DisplayRootView<MainView>();
        }
    }
}
