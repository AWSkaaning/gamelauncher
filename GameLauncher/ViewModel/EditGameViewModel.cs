
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace GameLauncher.ViewModel
{
    public class EditGameViewModel : ViewModelBase
    {
        public bool CreatingNew { get; set; }
        public GLEngine.Model.Game Game { get; set; }

        public ICommand AcceptCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private ObservableCollection<string> _platformSuggestions = new ObservableCollection<string>();
        public ObservableCollection<string> PlatformSuggestions
        {
            get { return _platformSuggestions; }
            set { _platformSuggestions = value; }
        }

        #region Constructors and helpers
        public EditGameViewModel()
        {
            CreatingNew = true;
            Game = GLEngine.Model.Game.CreateGame("", "", "");

            ConstructorHelper();
        }

        public EditGameViewModel(GLEngine.Model.Game game)
        {
            CreatingNew = false;
            Game = game.Clone();

            ConstructorHelper();
        }

        /// <summary>
        /// Initializes default stuff that is needed no matter what constructor is called
        /// </summary>
        private void ConstructorHelper()
        {            
            SettingUpCommands();
            PlatformSuggestions = GetPlatforms();
        }
        #endregion

        private void SettingUpCommands()
        {
            AcceptCommand = new RelayCommand(Accept);
            CancelCommand = new RelayCommand(Cancel);
        }

        private ObservableCollection<string> GetPlatforms()
        {
            var platforms = new ObservableCollection<string>();

            foreach (var item in AppState.GetInstance.GameController.GetAllGames())
            {
                if (platforms.Contains(item.Platform) == false)
                {
                    platforms.Add(item.Platform);
                }
            }

            return platforms;
        }

        public void Accept()
        {
            if (Game != null)
            {
                if (CreatingNew)
                {
                    AppState.GetInstance.GameController.AddGame(Game);
                }
                else
                {
                    AppState.GetInstance.GameController.UpdateGame(Game);
                }
            }
        }

        public void Cancel()
        {
            var appstate = AppState.GetInstance;
            if (appstate.PreviousView != null)
            {
                appstate.GotoPreviouView();
            }
            else
            {
                appstate.ChangeView(new ViewModel.GameViewModel(), true);
            }
        }
    }
}