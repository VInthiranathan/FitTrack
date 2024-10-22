using FitTrack.View;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FitTrack
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

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
        }

        private void SigninBtn_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow();
            homeWindow.Show();
            this.Close();

        }
    }
}