using System.Collections.ObjectModel;

namespace GameLauncher.Model
{
    public class GameViewData : Model.BaseCollection<GLEngine.Model.Game>
    {
        public AppState Appstate
        {
            get
            {
                return AppState.GetInstance;
            }
        }

        private string _searchQuery;
        public string SearchQuery
        {
            get { return _searchQuery; }
            set
            {
                _searchQuery = value;
                NotifyPropertyChanged("SearchQuery");
                FilterUpdate();
            }
        }

        public GameViewData()
        {
            Items = GetGames();
            UpdateCollection();
        }

        public override bool FilterController(object item)
        {
            var result = false;

            if (SearchFilter(item))
            {
                result = true;
            }

            return result;
        }

        private bool SearchFilter(object item)
        {
            var result = false;

            if (!string.IsNullOrEmpty(SearchQuery))
            {
                var game = item as GLEngine.Model.Game;
                if (game.Title.ToLower().Contains(SearchQuery.ToLower()))
                {
                    result = true;
                }
            }
            else
            {
                result = true;
            }

            return result;
        }

        private void UpdateCollection()
        {
            base.CollectionUpdate();

            //Add sorting to the collection view
            ItemsCollection.SortDescriptions.Clear();
            ItemsCollection.SortDescriptions.Add(new System.ComponentModel.SortDescription("Title", System.ComponentModel.ListSortDirection.Ascending));
        }

        private ObservableCollection<GLEngine.Model.Game> GetGames()
        {
            return new ObservableCollection<GLEngine.Model.Game>(Appstate.GameController.GetAllGames());
        }
    }
}