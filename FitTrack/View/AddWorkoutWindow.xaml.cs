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
using System.Windows.Shapes;

namespace FitTrack.View
{
    /// <summary>
    /// Interaction logic for AddWorkoutWindow.xaml
    /// </summary>
    public partial class AddWorkoutWindow : Window
    {
        public AddWorkoutWindow()
        {
            InitializeComponent();
        }
        private void menBtn_Click(object sender, RoutedEventArgs e)
        {
            if (menuBorder.Visibility == Visibility.Hidden)
            {
                menuBorder.Visibility = Visibility.Visible;
            }
            else
            {
                menuBorder.Visibility = Visibility.Hidden;
            }
        }
    }
}
