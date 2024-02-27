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
using static WpfApp1.MainWindow;

namespace WpfApp1
{  
    public partial class MainWindow : Window
    {
        bankEntities context;
        public static MainWindow _instance;
        public MainWindow()
        {
            _instance = this;
            bool isAdmin = AdminFlagger.AdminFlag;
            InitializeComponent();            
            Logout();
            return;
        }
        private void LogoutFunc(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Вы уверены, что хотите выйти из аккаунта?", "Подтвердить", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                try
                {
                    Logout();
                    return;
                }
                catch
                {
                    MessageBox.Show("Ошибка!");
                }
            }
        }
        private void NavigateToPage(Page page)
        {
            NewFrame.Navigate(page);
        }
        private void Logout()
        {
            LoginButton.IsEnabled = true;
            LoginButton.Visibility = Visibility.Visible;
            GridButton.IsEnabled = false;
            GridButton.Visibility = Visibility.Hidden;
            MoneyButton.Visibility = Visibility.Hidden;
            MoneyButton.IsEnabled = false;
            DepositWithdrawButton.Visibility = Visibility.Hidden;
            DepositWithdrawButton.IsEnabled = false;
            LogoutButton.IsEnabled = false;
            AdminFlagger.Reset();
            NavigateToPage(new LoginPage());
        }       
        public void LoggedIn()
        {
            GridButton.IsEnabled = true;
            GridButton.Visibility = Visibility.Visible;           
            DepositWithdrawButton.IsEnabled = true;
            DepositWithdrawButton.Visibility = Visibility.Visible;
            LoginButton.IsEnabled = false;
            LogoutButton.IsEnabled = true;
            LoginButton.Visibility = Visibility.Hidden;
        }
        public void LoggedInAsAdmin()
        {
            GridButton.IsEnabled = true;
            GridButton.Visibility = Visibility.Visible;
            MoneyButton.IsEnabled = true;
            MoneyButton.Visibility = Visibility.Visible;
            DepositWithdrawButton.IsEnabled = true;
            DepositWithdrawButton.Visibility = Visibility.Visible;
            LoginButton.IsEnabled = false;
            LogoutButton.IsEnabled = true;
            LoginButton.Visibility = Visibility.Hidden;
        }       
        private void ShowGridPage(object sender, RoutedEventArgs e) => NavigateToPage(new GridPage());                        
        private void ShowMoneyPage(object sender, RoutedEventArgs e) => NavigateToPage(new MoneyPage(context));             
        private void ShowLoginPage(object sender, RoutedEventArgs e) => NavigateToPage(new LoginPage());        
        private void ShowDepositPage(object sender, RoutedEventArgs e) => NavigateToPage(new DepositTransferPage());       
        private void ShowHistoryPage(object sender, RoutedEventArgs e) => NavigateToPage(new HistoryPage());
        private void ExitFunc(object sender, RoutedEventArgs e) => this.Close();                      
    }
}