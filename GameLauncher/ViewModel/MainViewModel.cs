using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Media.Imaging;

using System.Windows.Input;

namespace GameLauncher.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public AppState Appstate
        {
            get
            {
                return AppState.GetInstance;
            }
        }

        private ObservableCollection<Model.NavItem> _navItems = new ObservableCollection<Model.NavItem>();
        public ObservableCollection<Model.NavItem> NavItems
        {
            get { return _navItems; }
            set { _navItems = value; }
        }        

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            //Setting up the nav menu buttons
            SetupNavMenu();
        }

        private void SetupNavMenu()
        {
            //Big icon game list
            var bigiconView = new Model.NavItem()
            {
                Caption = "Big Icon view",
                IsSelected = false,
                Index = 1
            };
            NavItems.Add(bigiconView);

            //Details game list
            var detailsListView = new Model.NavItem()
            {
                Caption = "Details list view",
                IsSelected = false,
                Index = 2
            };
            NavItems.Add(detailsListView);

            //Add new game
            var addGame = new Model.NavItem()
            {
                Caption = "Add new game",
                IsSelected = false,
                Index = 3
            };
            NavItems.Add(addGame);
        }

        public void ChangeView(int index)
        {
            //Change the IsSelected to the selected NavItem
            foreach (var item in NavItems)
            {
                if (item.IsSelected == true && item.Index != index)
                {
                    item.IsSelected = false;
                }

                if (item.IsSelected == false && item.Index == index)
                {
                    item.IsSelected = true;
                }
            }

            //Change to the right viewmodel (view)
            switch (index)
            {
                case 1:
                    Appstate.Viewhandler.ChangeView(new ViewModel.GameViewModel(), true);
                    break;
                case 2:
                    break;
                case 3:
                    Appstate.Viewhandler.ChangeView(new ViewModel.EditGameViewModel(), true);
                    break;
                default:
                    break;
            }
        }        

    }
}