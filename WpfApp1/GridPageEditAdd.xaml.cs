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
    /// <summary>
    /// Логика взаимодействия для GridPageEditAdd.xaml
    /// </summary>
    public partial class GridPageEditAdd : Page
    {
        bankEntities1 context;
        public GridPageEditAdd(bankEntities1 cont)
        {
            InitializeComponent();
            context = cont;           
            flag = true;
        }
        bool flag;
        private void SaveCar(object sender, RoutedEventArgs e)
        {
            if (flag == true)
            {
                BankTable bank = new BankTable()
                {
                    AccountNumber = Convert.ToInt64(idBox.Text),
                    FIO = fioBox.Text,                  
                    BankName = bankNameTextBox.Text,                   
                    Deposited = Convert.ToDouble(depositBox.Text),
                    Withdrawn = Convert.ToDouble(withdrBox.Text),
                    Password = passBox.Text,
                    Total = Convert.ToDouble(totalTextBox.Text),
                };
                context.BankTable.Add(bank);
                context.SaveChanges();
                NewFrame3Edit.Navigate(new GridPage());
            }
            else
            {
                context.BankTable.Find(greg.AccountNumber).AccountNumber = Convert.ToInt64(idBox.Text);
                context.BankTable.Find(greg.AccountNumber).FIO = fioBox.Text;               
                context.BankTable.Find(greg.AccountNumber).BankName = bankNameTextBox.Text;              
                context.BankTable.Find(greg.AccountNumber).Deposited = Convert.ToDouble(depositBox.Text);
                context.BankTable.Find(greg.AccountNumber).Withdrawn = Convert.ToDouble(withdrBox.Text);
                context.BankTable.Find(greg.AccountNumber).Password = passBox.Text;
                context.BankTable.Find(greg.AccountNumber).Total = Convert.ToDouble(totalTextBox.Text);
                context.SaveChanges();
                NewFrame3Edit.Navigate(new GridPage());
            }
        }
        BankTable greg;

        public GridPageEditAdd(bankEntities1 cont, BankTable grug)
        {
            InitializeComponent();
            context = cont;
            greg = grug;         
            totalTextBox.Text = grug.Total.ToString();  
            bankNameTextBox.Text = grug.BankName;
            idBox.Text = grug.AccountNumber.ToString();
            fioBox.Text = grug.FIO;
            depositBox.Text = grug.Deposited.ToString();
            withdrBox.Text = grug.Withdrawn.ToString();
            passBox.Text = grug.Password;           
            flag = false;
        }
    }
}