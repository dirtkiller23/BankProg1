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
using System.Xml.XPath;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bankEntities context;

        public static MainWindow _instance;
        public MainWindow()
        {
            _instance = this;
            InitializeComponent();
            GridButton.IsEnabled = false;
            MoneyButton.IsEnabled = false;           
            NewFrame.Navigate(new LoginPage());
        }

        public void Eban()
        {
            GridButton.IsEnabled = true;
            MoneyButton.IsEnabled = true;          
        }
        
        private void ShowGridPage(object sender, RoutedEventArgs e)
        {
            NewFrame.Navigate(new GridPage());
        }
        private void ShowMoneyPage(object sender, RoutedEventArgs e)
        {          
            NewFrame.Navigate(new MoneyPage(context)); 
        }
        private void ShowTicTacPage(object sender, RoutedEventArgs e)
        {
            NewFrame.Navigate(new TicTacToe());
        }
        private void ShowCaesarPage(object sender, RoutedEventArgs e)
        {
            NewFrame.Navigate(new CaesarPage());
        }
    }
}
