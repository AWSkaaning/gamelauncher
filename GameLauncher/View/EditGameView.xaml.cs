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
    /// Interaction logic for EditGameView.xaml
    /// </summary>
    public partial class EditGameView : UserControl
    {
        private int _errors = 0;

        public EditGameView()
        {
            InitializeComponent();
        }

        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                _errors++;
            }
            else
            {
                _errors--;
            }

            if (_errors == 0)
            {
                okBtn.IsEnabled = true;
            }
            else
            {
                okBtn.IsEnabled = false;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadThumbnail();
        }
        
        private void CoverImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fileDia = new Microsoft.Win32.OpenFileDialog();
            fileDia.Title = "Select cover image";
            fileDia.Filter = "Images|*.jpg;*.png";
            fileDia.FilterIndex = 0;
            fileDia.Multiselect = false;

            if (fileDia.ShowDialog() == true)
            {
                var dc = this.DataContext as ViewModel.EditGameViewModel;
                dc.Game.CoverImagePath = fileDia.FileName;
                LoadThumbnail();
            }
        }

        private void LoadThumbnail()
        {
            var dc = this.DataContext as ViewModel.EditGameViewModel;
            if (dc.Game.HasCoverImage)
            {
                CoverImage.Source = ImageHelper.GenerateThumbnail(dc.Game.CoverImagePath, 200);
            }
        }
    }
}