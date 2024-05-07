using Cet.SmartClient.Client;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TalaadThaiWg.Shell.Commands;

namespace TalaadThaiWg.Shell.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private IUnityContainer container;
        private IRegionManager regionManager;

        private ObservableCollection<MenuItem> menuList;
        public ObservableCollection<MenuItem> MenuList
        {
            get { return menuList; }
        }

        private string ouName;
        public string OuName
        {
            get { return ouName; }
            set
            {
                ouName = value;
                OnPropertyChanged("OuName");
            }
        }

        private string profileName;
        public string ProfileName
        {
            get { return profileName; }
            set
            {
                profileName = value;
                OnPropertyChanged("ProfileName");
            }
        }


        //[Dependency("BGImageUrl")]
        //public string BGImageUrl { get; set; }

        //private ImageSource imageUrl;
        //public ImageSource ImageUrl
        //{
        //    get { return imageUrl; }
        //    set
        //    {
        //        imageUrl = value;
        //        OnPropertyChanged("ImageUrl");
        //    }
        //}


        public MainViewModel(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
            this.menuList = new ObservableCollection<MenuItem>();
            this.container.RegisterInstance("MenuList", menuList);
            this.OuName = "M-SAT";
        }

        public ICommand CloseCommand
        {
            get
            {
                return this.container.Resolve<CloseCommand>();
            }
        }


    }
}
