using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Caliburn.Micro;
using Redmine.Net.Api.Types;
using WinRTXamlToolkit.Imaging;
using Windows.UI;
using WinRTXamlToolkit.Tools;

namespace RedmineClientW8.Common
{
    public class TreeViewPageViewModel :Screen
    {
        #region TreeItems
        private ObservableCollection<TreeItemViewModel> _treeItems;
        public ObservableCollection<TreeItemViewModel> TreeItems
        {
            get { return _treeItems; }
            set
            {
                _treeItems = value;
                base.NotifyOfPropertyChange(()=>TreeItems);
            }
        }
        #endregion

        private int _branchId;
        private Random _rand = new Random();
        private List<Color> _namedColors = ColorExtensions.GetNamedColors();

        public TreeViewPageViewModel(IEnumerable<Project> projects)
        {
            _branchId = 1;
            TreeItems = BuildTree(projects);
        }

        public TreeViewPageViewModel()
        {
            
        }
        private ObservableCollection<TreeItemViewModel> BuildTree(IEnumerable<Project> projects)
        {
            var tree = new ObservableCollection<TreeItemViewModel>();
           
                foreach (var rootProject in projects.Where(x => x.Parent == null))
                {
                    tree.Add(
                        new TreeItemViewModel
                        {
                            Text = rootProject.Name,
                            Brush = new SolidColorBrush(_namedColors[_rand.Next(0, _namedColors.Count)]),
                            Children = GetChildProjects(projects,rootProject.Id)
                        });
                }

            return tree;
        }

        private ObservableCollection<TreeItemViewModel> GetChildProjects(IEnumerable<Project> projects,int parentId)
        {
            var childProjects=projects.Where(x => x.Parent != null && x.Parent.Id == parentId).ToList();
            var result= new ObservableCollection<TreeItemViewModel>();
            foreach (var childProject in childProjects)
            {
                result.Add(new TreeItemViewModel
                {
                    Text = childProject.Name,
                    Brush = new SolidColorBrush(_namedColors[_rand.Next(0, _namedColors.Count)]),
                });
            }
            return result;
        } 
    }

    public class TreeItemViewModel:Screen
    {
        #region Text
        private string _text;
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                base.NotifyOfPropertyChange(()=>Text);
            }
        }
        #endregion

        #region Children
        private ObservableCollection<TreeItemViewModel> _children = new ObservableCollection<TreeItemViewModel>();
        /// <summary>
        /// Gets or sets the child items.
        /// </summary>
        public ObservableCollection<TreeItemViewModel> Children
        {
            get { return _children; }
            set
            {
                _children = value;
                base.NotifyOfPropertyChange(()=>Children);
            }
        }
        #endregion

        #region Brush
        private SolidColorBrush _brush;
        /// <summary>
        /// Gets or sets the brush.
        /// </summary>
        public SolidColorBrush Brush
        {
            get { return _brush; }
            set
            {
                _brush = value;
                base.NotifyOfPropertyChange(()=>Brush);
            }
        }
        #endregion
    }
}
