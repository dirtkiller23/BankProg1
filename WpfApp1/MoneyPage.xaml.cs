using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using WpfApp1;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MoneyPage.xaml
    /// </summary>
    public partial class MoneyPage : Page
    {
        bankEntities context;
        public MoneyPage(bankEntities cont)
        {
            InitializeComponent();
            context = cont;
            flag = true;
        }
        bool flag;
        private void Initialize(object sender, RoutedEventArgs e)
        {
            if (flag == true)
            {
                BankTable bank = new BankTable()
                {
                    AccountNumber = Convert.ToInt64(idBoxFrom.Text),                   
                    FIO = fioBoxFrom.Text,                    
                    BankName = (bankNameComboBoxFrom.SelectedItem as BankTable).BankName,                  
                    Total = Convert.ToDouble(SendAmountBox.Text),                   
                };             
                context.SaveChanges();
                NewFrame3Edit.Navigate(new GridPage());
            }
            else
            {
                context.BankTable.Find(greg.AccountNumber).AccountNumber = Convert.ToInt64(idBoxFrom.Text);
                context.BankTable.Find(greg.FIO).FIO = fioBoxFrom.Text;
                context.BankTable.Find(greg.FIO).FIO = fioBoxTo.Text;
                context.BankTable.Find(greg.Total).Total = Convert.ToDouble(SendAmountBox.Text);                
                context.BankTable.Find(greg.BankName).BankName = (bankNameComboBoxFrom.SelectedItem as BankTable).BankName;               
                context.SaveChanges();
                NewFrame3Edit.Navigate(new GridPage());
            }
        }
        BankTable greg;

        public MoneyPage(bankEntities cont, BankTable grug)
        {
            InitializeComponent();
            context = cont;
            greg = grug;
            bankNameComboBoxFrom.ItemsSource = context.BankTable.ToList();
            SendAmountBox.Text = greg.Total.ToString();           
            idBoxFrom.Text = greg.AccountNumber.ToString();
            fioBoxFrom.Text = greg.FIO;         
            flag = false;
        }
    }
}





//{
// var markList = context.BankTable.ToList();
//markList.Insert(0, new BankTable() { BankName = "All" });
//bankNameComboBoxFrom.ItemsSource = markList;
//bankNameComboBoxTo.ItemsSource = markList;
//var list = context.BankTable.ToList();
// if (bankNameComboBoxFrom.SelectedIndex > 0)
// {
//    BankTable greg = bankNameComboBoxFrom.SelectedItem as BankTable;
//     list = list.Where(x => x.BankName == greg.ToString()).ToList();
// }
// if (bankNameComboBoxTo.SelectedIndex > 0)
//{
//  BankTable greg = bankNameComboBoxTo.SelectedItem as BankTable;
//   list = list.Where(x => x.BankName == greg.ToString()).ToList();
//}
//}