using GalaSoft.MvvmLight;

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

        public GameViewModel()
        {
            BigIconView = true;
            GameData.SearchQuery = "";
        }

        public void EditGame(GLEngine.Model.Game game)
        {
            var appstate = AppState.GetInstance;
            appstate.ChangeView(new ViewModel.EditGameViewModel(game), false);
        }

    }    
}