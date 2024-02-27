using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;

namespace WpfApp1
{
    public partial class HistoryPage : Page
    {
        bankEntities context;
        public HistoryPage()
        {
            InitializeComponent();
            RichUserDisplay();
            context = new bankEntities();
            bool isAdmin = AdminFlagger.AdminFlag;
            historytable.ItemsSource = context.Transaction_history.ToList();
            grid_data_search.ItemsSource = new List<string> { "Number", "Amount", "Sender", "Reciever", "Withdraw Time", "Deposit Time" };
            grid_data_search.SelectedIndex = 0;
            if (isAdmin == false)
            {
                AdminBlock();
                return;
            }
        }
        private void RichUserDisplay()
        {
            if (AdminFlagger.AdminFlag == true)
            {
                help_label.Content = "Logged in as Admin";
            }
            else if (AdminFlagger.AdminFlag == false)
            {
                help_label.Content = "Logged in as: Username: " + RichText.UserN + "," + "FIO: " + RichText.FIO + "," + "Bank: " + RichText.BankN;
            }
        }
        private void AdminBlock()
        {
            DeleteButton.Visibility = Visibility.Hidden;
            DeleteButton.IsEnabled = false;
            historytable.ContextMenu.IsEnabled = false;
            historytable.ContextMenu.Visibility = Visibility.Hidden;
        }
        public void RefreshData()
        {
            var search = numaccBox.Text.ToLower();
            var selected = grid_data_search.SelectedItem as string;
            var query = context.Transaction_history.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                switch (selected)
                {
                    case "Number":
                        if (long.TryParse(search, out var AccID))
                        {
                            query = query.Where(x => x.AccID.ToString().Contains(search));
                        }
                        break;
                    case "Amount":
                        if (long.TryParse(search, out var Amount))
                        {
                            query = query.Where(x => x.Amount.ToString().Contains(search));
                        }
                        break;
                    case "Sender":
                        if (long.TryParse(search, out var Sender))
                        {
                            query = query.Where(x => x.Sender.ToString().Contains(search));
                        }
                        break;
                    case "Reciever":
                        if (long.TryParse(search, out var Reciever))
                        {
                            query = query.Where(x => x.Reciever.ToString().Contains(search));
                        }
                        break;
                    case "Withdraw Time":
                        if (long.TryParse(search, out var WithdrawTime))
                        {
                            query = query.Where(x => x.WithdrawTime.ToString().Contains(search));
                        }
                        break;
                    case "Deposit Time":
                        if (long.TryParse(search, out var DepositTime))
                        {
                            query = query.Where(x => x.DepositTime.ToString().Contains(search));
                        }
                        break;
                }
            }
            historytable.ItemsSource = query.ToList();
        }
        private void ChangeStateNumber(object sender, TextChangedEventArgs e)
        {
            RefreshData();
        }
        private void grid_data_search_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshData();
        }
        private void DeletePage(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Вы уверены, что хотите удалить данные?", "Подтвердить", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                try
                {
                    Transaction_history history = historytable.ItemsSource as Transaction_history;
                    context.Transaction_history.Remove(history);
                    context.SaveChanges();
                    NewFrame3Edit.Navigate(new GridPage());
                }
                catch
                {
                    MessageBox.Show("Ошибка, невозможно удалить данные!");
                }
            }
        }
        private void DeleteCarPage(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Вы уверены, что хотите удалить данные?", "Подтвердить", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                try
                {
                    Transaction_history history = historytable.SelectedItem as Transaction_history;
                    context.Transaction_history.Remove(history);
                    context.SaveChanges();                  
                }
                catch
                {
                    MessageBox.Show("Ошибка, невозможно удалить данные!");
                }
            }
        }
        private void GridExport(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "CSV File|*.csv|TXT document|*.txt";
            saveFileDialog.Title = "Export to CSV";
            if (saveFileDialog.ShowDialog() == true)
            {
                var dataGridItems = historytable.ItemsSource as IEnumerable<Transaction_history>;
                string filePath = saveFileDialog.FileName;

                if (dataGridItems == null || !dataGridItems.Any())
                {
                    MessageBox.Show("There is no data to export.");
                    return;
                }

                using (StreamWriter file = new StreamWriter(filePath))
                {
                    foreach (var transaction in dataGridItems)
                    {
                        string csvLine = $"Number:{transaction.AccID}, Amount:{transaction.Amount}, Sender:{transaction.Sender}, Reciever:{transaction.Reciever}, Withdraw Time:{transaction.WithdrawTime}, Deposit Time:{transaction.DepositTime}, Transfer Time:{transaction.TransferTime}";
                        file.WriteLine(csvLine);
                    }
                }
                MessageBox.Show("Table exported to " + filePath);
            }
        }
        private void ExportPage(object sender, RoutedEventArgs e)
        {
            var _context = new bankEntities();

            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "CSV File|*.csv|TXT document|*.txt";
            saveFileDialog.Title = "Export to CSV";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                var transactionHistoryList = _context.Transaction_history.ToList();

                using (StreamWriter file = new StreamWriter(filePath))
                {
                    if (transactionHistoryList.Any())
                    {
                        foreach (var transaction in transactionHistoryList)
                        {
                            string csvLine = $"Number:{transaction.AccID}, Amount:{transaction.Amount}, Sender:{transaction.Sender}, Reciever:{transaction.Reciever}, Withdraw Time:{transaction.WithdrawTime}, Deposit Time:{transaction.DepositTime}, Transfer Time:{transaction.TransferTime}";
                            file.WriteLine(csvLine);
                        }
                    }
                }
                MessageBox.Show("Table exported to " + filePath);
            }
        }
    }
}
