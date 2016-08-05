using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameLauncher.View
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        public GameView()
        {
            InitializeComponent();
        }

        private void ClearSearch_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var dc = this.DataContext as ViewModel.GameViewModel;
            dc.GameData.SearchQuery = "";
        }

        private void PlatformFilter_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var dc = this.DataContext as ViewModel.GameViewModel;
            dc.ShowFilterMenu = !dc.ShowFilterMenu;
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var dc = this.DataContext as ViewModel.GameViewModel;
            dc.GameData.FilterUpdate();
        }

        private void FilterMenu_MouseLeave(object sender, MouseEventArgs e)
        {
            var dc = this.DataContext as ViewModel.GameViewModel;
            dc.ShowFilterMenu = false;
        }

        private void DeselectAllBtn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var dc = this.DataContext as ViewModel.GameViewModel;
            dc.GameData.DeselectAllPlatforms();
        }

        private void SelectAllPlatformsBtn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var dc = this.DataContext as ViewModel.GameViewModel;
            dc.GameData.SelectAllPlatforms();
        }
    }
}
