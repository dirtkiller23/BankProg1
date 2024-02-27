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

namespace WpfApp1
{
    public static class AdminFlagger
    {
        public static long? UserFlag { get; set; }       
        public static long? AdminSID { get; set; }
        public static bool AdminFlag { get; set; }
        public static bool NewUser { get; set; }
        public static void Reset()
        {
            UserFlag = 0;
            AdminSID = 0;
        }
    }
    public static class RichText
    {
        public static string UserN { get; set; }
        public static string BankN { get; set; }
        public static string FIO { get; set; }
        public static string Total { get; set; }
        public static string AccID { get; set; }
    }
    public partial class LoginPage : Page
    {   
        bankEntities context;
        static int counter = 0;
        public LoginPage()
        {
            InitializeComponent();
            context = new bankEntities();
        }             
        private void Button_login(object sender, RoutedEventArgs e)
        {
            counter++;
            string user = login.Text;
            string pass = password.Text;

            BankTable userC = context.BankTable.ToList().Find(x => x.Username == user);           
            if (counter >= 3)
            {
                BlockLoginField();
                return;
            }
            var userCheck = context.BankTable.FirstOrDefault(x => x.Username == user);
            var adminUser = context.AdminUsers.FirstOrDefault(x => x.AdminUsername == user);
            if (userC == null && adminUser == null)
            {
                MessageBox.Show("This user doesn't exist!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (adminUser != null && adminUser.AdminPassword.Equals(pass))
            {
                counter = 0;
                MessageBox.Show("Admin login successful!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                AdminFlagger.AdminSID = adminUser.AdminID;
                AdminFlagger.AdminFlag = true;
                MainWindow._instance.LoggedInAsAdmin();
                NewFrame3Edit.Navigate(new GridPage());
                help_login.Visibility = Visibility.Hidden;
            }
            else if (userC != null && userC.Password.Equals(pass))
            {
                counter = 0;
                MessageBox.Show("Login successful!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                AdminFlagger.UserFlag = userC.AccountNumber;
                {
                    RichText.UserN = userC.Username;
                    RichText.BankN = userC.BankName;
                    RichText.FIO = userC.FIO;
                    RichText.Total = userC.Total.ToString();
                }
                AdminFlagger.AdminFlag = false;               
                MainWindow._instance.LoggedIn();
                NewFrame3Edit.Navigate(new GridPage());
                help_login.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Wrong password entered", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BlockLoginField()
        {
            login.IsEnabled = false;
            login.Text = "";
            password.IsEnabled = false;
            password.Text = "";
            LoginButton.IsEnabled = false;
            MessageBox.Show("Max amount of retries(3) reached", "Критическая Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void Button_register(object sender, RoutedEventArgs e)
        {
            AdminFlagger.NewUser = true;
            NewFrame3Edit.Navigate(new GridPageEditAdd(context));
            help_login.Visibility = Visibility.Hidden;
        }
    }
}