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
    public partial class MoneyPage : Page
    {
        bankEntities bd = new bankEntities();
        public MoneyPage(bankEntities cont)
        {
            InitializeComponent();          
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (idBoxFrom.Text == "" || idBoxTo.Text == "")
            {
                MessageBox.Show("No data entered!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                {                   
                    {
                        BankTable t = bd.BankTable.Find(Int64.Parse(idBoxFrom.Text));
                        BankTable t2 = bd.BankTable.Find(Int64.Parse(idBoxTo.Text));
                        var a = t2.Total;
                        if (a == null)
                            t2.Total = 0;
                        if (t.Total >= float.Parse(SendAmountBox.Text))
                        {
                            t.Total = t.Total - Math.Round(float.Parse(SendAmountBox.Text), 2);
                            t2.Total = t2.Total + Math.Round(float.Parse(SendAmountBox.Text), 2);
                            var maxAccID = bd.Database.SqlQuery<long>("SELECT ISNULL(MAX(AccID),0) FROM Transaction_history").FirstOrDefault();
                            long newAccID = maxAccID + 1; // get AccID value + 1 every operation                         
                            bd.Database.ExecuteSqlCommand("insert into Transaction_history(AccID,Amount,Sender,Reciever,TransferTime) values ({0},{1},{2},{3},{4})", newAccID, Math.Round(float.Parse(SendAmountBox.Text), 2), Int64.Parse(idBoxFrom.Text), Int64.Parse(idBoxTo.Text), DateTime.Now);
                            bd.SaveChanges();   
                            MessageBox.Show("Transfer Successful");
                        }
                        else { MessageBox.Show("Not enough money"); }
                    };
                    }
            }
        }
    }
}                          