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
    public partial class GridPage : Page
    {
        bankEntities context;     
        public class BothTables
        {
            public long AccountNumber { get; set; }
            public string FIO { get; set; }
            public string BankName { get; set; }
            public DateTime? FinalOpTime { get; set; }
            public string DocNumb { get; set; }
            public double? C1 { get; set; }
            public double? Deposited { get; set; }
            public double? Withdrawn { get; set; }
            public double? Total { get; set; }
        }      
        public void DataGridJoin()          
        {
            using (var context = new bankEntities())
            {
                var query = from bk in context.BankTable
                            join tr in context.Transaction_history on bk.AccountNumber equals tr.AccID
                            select new BothTables
                            {
                                AccountNumber = bk.AccountNumber,
                                FIO = bk.FIO,
                                BankName = bk.BankName,
                                FinalOpTime = tr.FinalOpTime,
                                DocNumb = bk.DocNumb,
                                C1 = bk.C1,
                                Deposited = bk.Deposited,
                                Withdrawn = bk.Withdrawn,
                                Total = bk.Total
                            };
                var mt = query.ToList();
                moneytable.ItemsSource = mt;
            }
        }
        public void LegacyDataGridJoin()
        {
            moneytable.ItemsSource = context.BankTable.ToList();
        }
        public GridPage()
        {
            InitializeComponent();                       
            bool isAdmin = AdminFlagger.AdminFlag;
            context = new bankEntities();
            RichUserDisplay();
            LegacyDataGridJoin();
            grid_data_search.ItemsSource = new List<string> { "ID Number", "FIO", "BankName", "Date Time", "Doc №", "C1", "Debit", "Credit", "C2" };
            grid_data_search.SelectedIndex = 0;
            if ( isAdmin == false )
            {
                AdminBlock();
                return;
            }      
        }         
        private void RichUserDisplay()
        {                 
            if ( AdminFlagger.AdminFlag == true)
            {
                help_label.Content = "Logged in as Admin";
            }
            else if (AdminFlagger.AdminFlag == false )
            {
                help_label.Content = "Logged in as: Username: "+ RichText.UserN +","+"FIO: "+RichText.FIO +","+"Bank: "+RichText.BankN;
            }
        }
        private void AdminBlock()
        {
            AddButton.Visibility = Visibility.Hidden;
            AddButton.IsEnabled = false;
            moneytable.ContextMenu.IsEnabled = false;
            moneytable.ContextMenu.Visibility = Visibility.Hidden;                         
        }
        public void RefreshData()
        {
           var search = numaccBox.Text.ToLower();
           var selected = grid_data_search.SelectedItem as string;
           var query = context.BankTable.AsQueryable();
           if (!string.IsNullOrEmpty( search ) ) 
            { 
             switch (selected)  
                {
                    case "ID Number":
                        if (long.TryParse(search, out var accountNumber))
                        {
                            query = query.Where(x => x.AccountNumber.ToString().Contains(search));
                        }
                        break;
                    case "FIO":
                        query = query.Where(x => x.FIO.ToLower().Contains(search));
                        break;
                    case "BankName":
                        query = query.Where(x => x.BankName.ToLower().Contains(search));
                        break;
                    case "Date Time":
                        if (DateTime.TryParse(search, out var FinalOpTime))
                        {
                            query = query.Where(x => x.FinalOpTime.ToString().Contains(search));
                        }
                        break;
                    case "Doc №":
                        query = query.Where(x => x.DocNumb.ToLower().Contains(search));
                        break;
                    case "Debit":
                        if (long.TryParse(search, out var Deposited))
                        {
                            query = query.Where(x => x.Deposited.ToString().Contains(search));
                        }
                        break;
                    case "Credit":
                        if (long.TryParse(search, out var Withdrawn))
                        {
                            query = query.Where(x => x.Withdrawn.ToString().Contains(search));
                        }
                        break;
                    case "C1":
                        if (long.TryParse(search, out var C1))
                        {
                            query = query.Where(x => x.C1.ToString().Contains(search));
                        }
                        break;
                    case "C2":
                        if (long.TryParse(search, out var total))
                        {
                            query = query.Where(x => x.Total.ToString().Contains(search));
                        }
                        break;
                }
            }
           moneytable.ItemsSource = query.ToList();
        }       
        private void ChangeStateNumber(object sender, TextChangedEventArgs e)
        {
            RefreshData();
        }
        private void grid_data_search_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshData();
        }
        private void AddCarPage(object sender, RoutedEventArgs e)
        {
            NewFrame3Edit.Navigate(new GridPageEditAdd(context));
        }
        private void EditCarPage(object sender, RoutedEventArgs e)
        {
            BankTable greg = moneytable.SelectedItem as BankTable;
            NewFrame3Edit.Navigate(new GridPageEditAdd(context, greg));
        }
        private void DeleteCarPage(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Вы уверены, что хотите удалить данные?", "Подтвердить", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                try
                {
                    BankTable bankTable = moneytable.SelectedItem as BankTable;
                    context.BankTable.Remove(bankTable);
                    context.SaveChanges();
                    NewFrame3Edit.Navigate(new GridPage());
                }
                catch
                {
                    MessageBox.Show("Ошибка, невозможно удалить данные!");
                }
            }
        }      
    }
}