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
            ChangeView(1); //Default start-up view
        }

        private void ChangeView(int index)
        {
            var dc = this.DataContext as ViewModel.MainViewModel;
            dc.ChangeView(index);
        }
    }
}
