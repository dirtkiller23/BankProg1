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
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
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
            int AccountNum = Convert.ToInt32(login.Text);
            string pass = password.Text;

            BankTable accnumber = context.BankTable.ToList().Find(x => x.AccountNumber == AccountNum);
            if (counter >= 3)
            {
                login.IsEnabled = false;
                login.Text = "";
                password.IsEnabled = false;
                password.Text = "";
                LoginButton.IsEnabled = false;
                MessageBox.Show("Max amount of retries(3) reached", "Критическая Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            if (accnumber == null)
            {
                MessageBox.Show("This user doesn't exist!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                if (accnumber.Password.Equals(pass))
                {
                    MessageBox.Show("Login successful!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow._instance.Eban();
                    NewFrame.Navigate(new GridPage());
                }
                else
                {
                    MessageBox.Show("Wrong password entered", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
