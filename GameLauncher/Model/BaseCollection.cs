/* 
 * A simple base class for implementing a CollectionView and easily adding filters
 */

using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace GameLauncher.Model
{
    public class BaseCollection<T> : PropertyChangedNotification
    {
        public ICollectionView ItemsCollection { get; set; }
        public ObservableCollection<T> Items { get; set; }

        public BaseCollection()
        {
            Items = new ObservableCollection<T>();
        }

        /// <summary>
        /// Used to refresh the collection view after items are added or removed
        /// </summary>
        public void CollectionUpdate()
        {
            CreateCollection();
        }

        /// <summary>
        /// Dummy filter method ment to be overwritten if filtering is needed
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual bool FilterController(object item)
        {
            return true;
        }

        /// <summary>
        /// Updates the collection view and makes sure items are filtered.
        /// </summary>
        public void FilterUpdate()
        {
            ItemsCollection.Refresh();
        }

        private void CreateCollection()
        {
            ItemsCollection = CollectionViewSource.GetDefaultView(Items);
            ItemsCollection.Filter += FilterController;
        }
    }
}
