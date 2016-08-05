using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace GameLauncher.ViewModel
{
    public class EditGameViewModel : ViewModelBase
    {
        private bool _changesSaved = false;
        public bool ChangesSaved
        {
            get { return _changesSaved; }
            set
            {
                _changesSaved = value;
                RaisePropertyChanged("ChangesSaved");
            }
        }

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

        private string _statusMessage;
        public string StatusMessage
        {
            get { return _statusMessage; }
            set
            {
                _statusMessage = value;
                RaisePropertyChanged("StatusMessage");
            }
        }

        #region Constructors and helpers
        public EditGameViewModel()
        {
            CreatingNew = true;
            Game = GLEngine.Model.Game.CreateGame("", "", "");

            ConstructorHelper();
        }

        /// <summary>
        /// Ctor used when editing a game.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown if input is null</exception>
        /// <param name="game"></param>
        public EditGameViewModel(GLEngine.Model.Game game)
        {
            if (game == null)
            {
                throw new NullReferenceException("Input game cannot be null!");
            }

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

            foreach (var item in AppState.GetInstance.GameController.GetPlatforms())
            {
                platforms.Add(item);
            }

            return platforms;
        }

        public void Accept()
        {
            if (Game != null)
            {
                if (ChangesSaved == false)
                {
                    if (CreatingNew)
                    {
                        AppState.GetInstance.GameController.AddGame(Game);
                    }
                    else
                    {
                        AppState.GetInstance.GameController.UpdateGame(Game);
                    }

                    ChangesSaved = true;
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