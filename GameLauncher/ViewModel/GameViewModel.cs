using System.Diagnostics;
using GalaSoft.MvvmLight;
using System;
using System.Linq;

namespace GameLauncher.ViewModel
{
    public class GameViewModel : ViewModelBase
    {
        private bool _bigIconView;
        public bool BigIconView
        {
            get { return _bigIconView; }
            set
            {
                _bigIconView = value;
                RaisePropertyChanged("BigIconView");
            }
        }

        private Model.GameViewData _gameData = new Model.GameViewData();
        public Model.GameViewData GameData
        {
            get { return _gameData; }
            set { _gameData = value; }
        }

        AppState appstate = AppState.GetInstance;

        public GameViewModel()
        {
            BigIconView = true;
            GameData.SearchQuery = "";
        }

        public void EditGame(GLEngine.Model.Game game)
        {            
            appstate.ChangeView(new ViewModel.EditGameViewModel(game), false);
        }

        public void EditGame(Guid gameId)
        {
            var game = appstate.GameController.GetAllGames().Where(x => x.Id == gameId).SingleOrDefault();
            if (game != null)
            {
                appstate.ChangeView(new ViewModel.EditGameViewModel(game), false);
            }
        }

        public void LaunchGame(GLEngine.Model.Game game)
        {
            /***************************************************
             * TODO: This needs to be refactored!!             *
             * *************************************************/
            var emuApp = new ProcessStartInfo();
            var inputString = game.StartCommand.Split(' ');
            
            var argString = "";
            for (int i = 1; i < inputString.Length; i++)
            {
                argString += inputString[i] + " ";
            }

            emuApp.FileName = inputString[0];
            emuApp.Arguments = argString;

            Process.Start(emuApp);
        }
    }    
}