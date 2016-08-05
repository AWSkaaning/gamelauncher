using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System;
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

        public MainViewModel()
        {
            //Setting up the nav menu buttons
            SetupNavMenu();
        }

        private void SetupNavMenu()
        {
            //Game view
            var gameView = new Model.NavItem()
            {
                Caption = "Games",
                IsSelected = false,
                ClassName = typeof(ViewModel.GameViewModel).Name,
                Index = 1
            };
            Appstate.NavItems.Add(gameView);

            //Add new game
            var addGame = new Model.NavItem()
            {
                Caption = "Add new game",
                IsSelected = false,
                ClassName = typeof(ViewModel.EditGameViewModel).Name,
                Index = 2
            };
            Appstate.NavItems.Add(addGame);

            //About view
            var aboutView = new Model.NavItem()
            {
                Caption = "About",
                IsSelected = false,
                ClassName = typeof(ViewModel.AboutViewModel).Name,
                Index = 4
            };
            Appstate.NavItems.Add(aboutView);
        }
        
        public bool SaveData()
        {
            var result = Appstate.GameController.SaveGameData(Appstate.GameDataPath);
            return result;
        }

        public void LoadGameData()
        {
            if (Appstate.GameController.DoesGameDataExist(Appstate.GameDataPath))
            {
                Appstate.GameController.LoadGameData(Appstate.GameDataPath);
            }          
        }

        public void ChangeView(int index)
        {
            //Change to the right viewmodel (view)
            switch (index)
            {
                case 1:
                    Appstate.ChangeView(new ViewModel.GameViewModel(), true);
                    break;
                case 2:
                    Appstate.ChangeView(new ViewModel.EditGameViewModel(), true);
                    break;
                case 4:
                    Appstate.ChangeView(new ViewModel.AboutViewModel(), true);
                    break;
            }
        }

        public void Initialize()
        {
            LoadGameData();
            ChangeView(1);
        } 

    }
}