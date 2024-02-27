using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Principal;
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
    public partial class DepositTransferPage : Page
    {
        bankEntities bd = new bankEntities();
        public DepositTransferPage()
        {
            InitializeComponent();
            RichUserDisplay();
        }
        private void RichUserDisplay()
        {
            if (AdminFlagger.AdminFlag == true)
            {
                help_label.Content = "Logged in as Admin";
            }
            else if (AdminFlagger.AdminFlag == false)
            {
                username_label.Content = "Account Number:" + AdminFlagger.UserFlag;
                help_label.Content = "Logged in as: Username: " + RichText.UserN + "," + "FIO: " + RichText.FIO + "," + "Bank: " + RichText.BankN;
            }
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
            long userId = Int64.Parse(idBoxFromD.Text);
            double depo = Math.Round(double.Parse(DepositBox.Text), 2);
            BankTable account = bd.BankTable.Find(userId);
            if (AdminFlagger.UserFlag.Value == userId)
            {
                account.Deposited = (account.Deposited ?? 0) + depo;
                account.Total = account.Total + depo;
                account.FinalOpTime = DateTime.Now;
                account.DocNumb = "Debit";
                long newAccID = 0;
                var maxAccID = bd.Transaction_history.Max(th => (long?)th.AccID);
                var transaction = new Transaction_history
                {
                    AccID = newAccID,
                    Amount = depo,
                    Reciever = userId,
                    DepositTime = DateTime.Now,
                    FinalOpTime = DateTime.Now
                };
                bd.Transaction_history.Add(transaction);
                account.FinalOpTime = DateTime.Now;
                account.DocNumb = "Debit";
                bd.SaveChanges();
            }
            else
            {
                MessageBox.Show($"Account with ID {userId} not found.");
                return;
            }
            MessageBox.Show("Amount Added");
        }
        private void WithdrawFunc()
        {
            long userId = Int64.Parse(idBoxFromW.Text);
            double withdrAmount = double.Parse(WithdrawBox.Text);
            withdrAmount = Math.Round(withdrAmount, 2);

            if (AdminFlagger.UserFlag.Value == userId)
            {
                {
                    var account = bd.BankTable.Find(userId);
                    if (account == null)
                    {
                        MessageBox.Show("Account not found.");
                        return;
                    }
                    account.Withdrawn = account.Withdrawn ?? 0;
                    if (account.Total >= withdrAmount)
                    {
                        account.Withdrawn += withdrAmount;
                        account.Total -= withdrAmount;
                        long newAccID = 0;
                        var maxAccID = bd.Transaction_history.Max(th => (long?)th.AccID);
                        newAccID = (maxAccID ?? 0) + 1;
                        var transaction = new Transaction_history
                        {
                            AccID = newAccID,
                            Amount = withdrAmount,
                            Sender = userId,
                            WithdrawTime = DateTime.Now,
                            FinalOpTime = DateTime.Now
                        };
                        bd.Transaction_history.Add(transaction);
                        account.FinalOpTime = DateTime.Now;
                        account.DocNumb = "Credit";
                        bd.SaveChanges();
                        MessageBox.Show("Withdraw complete");
                    }
                    else
                    {
                        MessageBox.Show("Not enough money");
                    }
                }
            }
            else
            {
                MessageBox.Show("You do not have permission to withdraw from this account.");
            }
        }
    }
}
