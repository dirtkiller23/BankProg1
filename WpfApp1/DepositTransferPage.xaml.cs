using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для DepositTransferPage.xaml
    /// </summary>
    public partial class DepositTransferPage : Page
    {
        bankEntities bd = new bankEntities();
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
                DepositFunc();
            }
        }       
        private void Withdraw_Click(object sender, RoutedEventArgs e)
        {
            if (AdminFlagger.AdminFlag)
            {
            if (AdminFlagger.UserFlag == null)
                {
                   AdminUserDialog adminUserDialog = new AdminUserDialog();
                    bool? dialogResult = adminUserDialog.ShowDialog();
                    if (dialogResult == true)
                    {
                        WithdrawFunc();
                    }
                }
            else
                {
                    WithdrawFunc();
                }
            }
            if (AdminFlagger.AdminFlag == false)
            {
            if (AdminFlagger.UserFlag == null)
                {
                    MessageBox.Show("You need to log in to perform this action.");
                    return;
                }
                BankTable u = bd.BankTable.Find(AdminFlagger.UserFlag.Value);
                if (u == null)
                {
                    MessageBox.Show("Account not found.");
                    return;
                }
                if (idBoxFromW.Text == "")
                {
                    MessageBox.Show("No data entered!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                    WithdrawFunc();
            }
            
        }

        private void DepositFunc()
        {                      
                BankTable t = bd.BankTable.Find(Int64.Parse(idBoxFromD.Text));
                var a = t.Deposited;
                if (a == null)
                    t.Deposited = 0;
                t.Deposited = t.Deposited + Math.Round(float.Parse(DepositBox.Text), 2);
                t.Total = t.Total + Math.Round(float.Parse(DepositBox.Text), 2);

                var maxAccID = bd.Database.SqlQuery<long>("SELECT ISNULL(MAX(AccID),0) FROM Transaction_history").FirstOrDefault();
                long newAccID = maxAccID + 1;
                bd.Database.ExecuteSqlCommand("insert into Transaction_history(AccID,Amount,Reciever,DepositTime,FinalOpTime) values ({0},{1},{2},{3},{4})", newAccID, Math.Round(float.Parse(DepositBox.Text), 2), Int64.Parse(idBoxFromD.Text), DateTime.Now,DateTime.Now);
                bd.BankTable.Find(t.AccountNumber).FinalOpTime = Convert.ToDateTime(DateTime.Now);
                bd.BankTable.Find(t.AccountNumber).DocNumb = "Debit";
                bd.SaveChanges();
                MessageBox.Show("Amount Added");           
        }

        
        private void WithdrawFunc()
        {
            {
                {
                    {
                        if (AdminFlagger.UserFlag.Value == Int64.Parse(idBoxFromW.Text))
                        {
                            BankTable t = bd.BankTable.Find(Int64.Parse(idBoxFromW.Text));
                            var a = t.Withdrawn;
                            if (a == null)
                                t.Withdrawn = 0;
                            if (t.Total >= float.Parse(WithdrawBox.Text))
                            {
                                t.Withdrawn = t.Withdrawn + Math.Round(float.Parse(WithdrawBox.Text), 2);
                                t.Total = t.Total - Math.Round(float.Parse(WithdrawBox.Text), 2);

                                var maxAccID = bd.Database.SqlQuery<long>("SELECT ISNULL(MAX(AccID),0) FROM Transaction_history").FirstOrDefault();
                                long newAccID = maxAccID + 1;
                                bd.Database.ExecuteSqlCommand("insert into Transaction_history(AccID,Amount,Sender,WithdrawTime) values ({0},{1},{2},{3})", newAccID, Math.Round(float.Parse(WithdrawBox.Text), 2), Int64.Parse(idBoxFromW.Text), DateTime.Now);
                                bd.BankTable.Find(t.AccountNumber).FinalOpTime = Convert.ToDateTime(DateTime.Now);
                                bd.BankTable.Find(t.AccountNumber).DocNumb = "Credit";
                                bd.SaveChanges();
                                MessageBox.Show("Withdraw complete");
                            }
                            else
                            {
                                MessageBox.Show("Not enough money");
                            }
                        }
                        else
                        {
                            MessageBox.Show("You do not have permission to withdraw from this account.");
                        }

                    }
                }
            }
        }
    }
}
