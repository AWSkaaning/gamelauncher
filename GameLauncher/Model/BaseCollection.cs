using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
