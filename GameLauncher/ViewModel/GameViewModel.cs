using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GalaSoft.MvvmLight;

namespace GameLauncher.ViewModel
{
    public class GameViewModel : ViewModelBase
    {
        public AppState Appstate
        {
            get
            {
                return AppState.GetInstance;
            }
        }

        public Model.BaseCollection<GLEngine.Model.Game> GamesCollection { get; set; }
        private ObservableCollection<GLEngine.Model.Game> GetGames()
        {
            return new ObservableCollection<GLEngine.Model.Game>(Appstate.GameController.GetAllGames());
        }

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


        public GameViewModel()
        {
            GamesCollection = new Model.BaseCollection<GLEngine.Model.Game>();
            GamesCollection.Items = GetGames();
            GamesCollection.CollectionUpdate();

            BigIconView = true;
        }
    }
}
