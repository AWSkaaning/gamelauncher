using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


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

        public List<Model.SelectionItem> Platforms { get; set; }

        public GameViewData()
        {
            Platforms = new List<Model.SelectionItem>();

            Items = GetGames();
            UpdateCollection();            
        }

        public override bool FilterController(object item)
        {
            var result = false;

            if (SearchFilter(item) && PlatformFilter(item))
            {
                result = true;
            }

            return result;
        }


        public void SelectAllPlatforms()
        {
            foreach (var item in Platforms)
            {
                item.IsSelected = true;
            }

            FilterUpdate();
        }

        public void DeselectAllPlatforms()
        {
            foreach (var item in Platforms)
            {
                item.IsSelected = false;
            }

            FilterUpdate();
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

        private bool PlatformFilter(object item)
        {
            var result = false;

            if (Platforms.Count > 0)
            {
                var game = item as GLEngine.Model.Game;
                var gamePlatform = Platforms.Where(x => x.Caption == game.Platform).SingleOrDefault();

                if (gamePlatform.IsSelected)
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