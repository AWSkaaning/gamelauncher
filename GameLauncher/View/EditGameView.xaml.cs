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

    }
}
