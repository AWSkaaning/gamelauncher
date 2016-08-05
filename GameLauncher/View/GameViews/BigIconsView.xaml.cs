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

namespace GameLauncher.View.GameViews
{
    /// <summary>
    /// Interaction logic for BigIconsView.xaml
    /// </summary>
    public partial class BigIconsView : UserControl
    {
        public BigIconsView()
        {
            InitializeComponent();
        }

        private void LaunchGameBtn_Click(object sender, RoutedEventArgs e)
        {
            var dc = this.DataContext as ViewModel.GameViewModel;
            var btn = sender as Button;
            var gameId = Guid.Parse(btn.Tag.ToString());
            var game = dc.GameData.Items.Where(x => x.Id == gameId).SingleOrDefault();

            if (game != null)
            {
                dc.LaunchGame(game);
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var dc = this.DataContext as ViewModel.GameViewModel;
            var gameId = Guid.Parse(((Button)sender).Tag.ToString());
            dc.EditGame(gameId);
        }
    }
}
