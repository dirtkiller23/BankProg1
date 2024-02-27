using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
    public partial class GridPageEditAdd : Page
    {
        bankEntities context;
        bankEntities bd = new bankEntities();
        public GridPageEditAdd(bankEntities cont)
        {
            InitializeComponent();
            context = cont;
            flag = true;
            bool isNew = AdminFlagger.NewUser;
            bool isAdmin = AdminFlagger.AdminFlag;
            if (isNew == true)
            {
                NewUserGreeting();
                return;
            }
            if (isAdmin == false)
            {
                AdminBlock();
                return;
            }
        }
        bool flag;
        bool admin;
        private void NewUserGreeting()
        {
            grid_help.Content = "HELP: Welcome! Register here on this page.";
            data_text.Content = "Registration";
            accBox.IsEnabled = false;
            accBox.Visibility = Visibility.Hidden;
            acc_label.Visibility = Visibility.Hidden;
            total_label.Visibility = Visibility.Hidden;
            totalTextBox.Visibility = Visibility.Hidden;
        }
        private void AdminBlock()
        {
            accBox.IsEnabled = false;
            accBox.Visibility = Visibility.Hidden;
            acc_label.Visibility = Visibility.Hidden;           
            totalTextBox.Visibility = Visibility.Hidden;
        }      
        private void SaveCar(object sender, RoutedEventArgs e)
        {
        if (flag == true && admin == true)
            {
                BankTable bank = new BankTable()
                {
                    AccountNumber = Convert.ToInt64(accBox.Text),
                    Username = idBox.Text,
                    FIO = fioBox.Text,
                    BankName = bankNameTextBox.Text,
                    Deposited = Convert.ToDouble(depositBox.Text),
                    C1 = Convert.ToDouble(depositBox.Text),
                    Total = Convert.ToDouble(depositBox.Text),
                    Withdrawn = Convert.ToDouble(withdrBox.Text),
                    Password = passBox.Text,
                };               
                context.BankTable.Add(bank);
                context.SaveChanges();
                MessageBox.Show("Changes saved!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        else
            if (flag == true)
            {
                Random rnd = new Random();
                Int64 generate = rnd.Next(10000, 19999);
                double depo = double.TryParse(depositBox.Text,out double dep) ? dep : 0;
                double withdr = double.TryParse(withdrBox.Text, out double wth) ? wth : 0;
                BankTable bank = new BankTable()
                {
                    AccountNumber = generate,
                    Username = idBox.Text,
                    FIO = fioBox.Text,                  
                    BankName = bankNameTextBox.Text,
                    Deposited = depo,
                    C1 = depo,
                    C2 = depo,
                    Total = depo,
                    Withdrawn = withdr,
                    Password = passBox.Text,
                };
                context.BankTable.Add(bank);
                context.SaveChanges();
                MessageBox.Show("Changes saved!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else           
            {
                context.BankTable.Find(greg.AccountNumber).Username = idBox.Text;
                context.BankTable.Find(greg.AccountNumber).FIO = fioBox.Text;               
                context.BankTable.Find(greg.AccountNumber).BankName = bankNameTextBox.Text;              
                context.BankTable.Find(greg.AccountNumber).Deposited = Convert.ToDouble(depositBox.Text);
                context.BankTable.Find(greg.AccountNumber).Withdrawn = Convert.ToDouble(withdrBox.Text);
                context.BankTable.Find(greg.AccountNumber).Password = passBox.Text;
                context.BankTable.Find(greg.AccountNumber).Total = Convert.ToDouble(totalTextBox.Text);
                context.SaveChanges();
                MessageBox.Show("Changes saved!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        BankTable greg;
        public GridPageEditAdd(bankEntities cont, BankTable grug)
        {
            InitializeComponent();
            context = cont;
            greg = grug;           
            totalTextBox.Text = grug.Total.ToString();  
            bankNameTextBox.Text = grug.BankName;
            idBox.Text = grug.Username;
            fioBox.Text = grug.FIO;
            depositBox.Text = grug.Deposited.ToString();
            withdrBox.Text = grug.Withdrawn.ToString();
            passBox.Text = grug.Password;           
            flag = false;
        }
    }
}