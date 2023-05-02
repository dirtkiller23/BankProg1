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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для DepositTransferPage.xaml
    /// </summary>
    public partial class DepositTransferPage : Page
    {
        bankEntities1 bd = new bankEntities1();
        public DepositTransferPage()
        {
            InitializeComponent();
        }

        private void Deposit_Click(object sender, RoutedEventArgs e)
        {
            if (idBoxFromD.Text == "")
            {
                MessageBox.Show("No data entered!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else 
            {
                {
                    {
                        BankTable t = bd.BankTable.Find(Int64.Parse(idBoxFromD.Text));
                        var a = t.Deposited;                       
                        if (a == null)
                            t.Deposited = 0;                    
                        t.Deposited = t.Deposited + Math.Round(float.Parse(DepositBox.Text), 2);
                        t.Total = t.Total    + Math.Round(float.Parse(DepositBox.Text), 2);
                        bd.SaveChanges();                       
                        MessageBox.Show("Amount Added");
                    }
                }
            }
        }

        private void Withdraw_Click(object sender, RoutedEventArgs e)
        {
            if (idBoxFromW.Text == "")
            {
                MessageBox.Show("No data entered!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                {
                    {
                        BankTable t = bd.BankTable.Find(Int64.Parse(idBoxFromW.Text));
                        var a = t.Withdrawn;
                        if (a == null)
                            t.Withdrawn = 0;
                        if (t.Total >= float.Parse(WithdrawBox.Text))
                        {
                            t.Withdrawn = t.Withdrawn + Math.Round(float.Parse(WithdrawBox.Text), 2);
                            t.Total = t.Total - Math.Round(float.Parse(WithdrawBox.Text), 2);
                            bd.SaveChanges();
                            MessageBox.Show("Withdraw complete");
                        }
                        else
                        {
                            MessageBox.Show("Not enough money");
                        }
                    }                  
                }
            }
        }
    }
}
