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

namespace GameLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Image_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            var index = Int32.Parse(((Image)sender).Tag.ToString());
            ChangeView(index);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var dc = this.DataContext as ViewModel.MainViewModel;
            dc.Initialize();
        }

        private void ChangeView(int index)
        {
            var dc = this.DataContext as ViewModel.MainViewModel;
            dc.ChangeView(index);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Save gamedata?", "Save?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var dc = this.DataContext as ViewModel.MainViewModel;
                var result = dc.SaveData();

                if (result == false)
                {
                    //TODO: Throw error and cancel closing so data loss is hopefully prevented.
                    //e.Cancel = true; //Good error logic must be in place!!!
                }
            }
        }

    }
}
