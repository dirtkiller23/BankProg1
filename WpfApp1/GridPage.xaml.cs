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
    /// Логика взаимодействия для GridPage.xaml
    /// </summary>
    public partial class GridPage : Page
    {
        bankEntities context;
        public GridPage()
        {
            InitializeComponent();
            context = new bankEntities();
            moneytable.ItemsSource = context.BankTable.ToList();

            var markList = context.BankTable.ToList();
            markList.Insert(0, new BankTable() { BankName = "All" });
            MarkBox.ItemsSource = markList;

        }
        public void RefreshData()
        {
            var list = context.BankTable.ToList();
            if (MarkBox.SelectedIndex > 0)
            {
                BankTable greg = MarkBox.SelectedItem as BankTable;
                list = list.Where(x => x.BankName == greg.ToString()).ToList();
            }
            if (!string.IsNullOrWhiteSpace(numaccBox.Text))
            {
                list = list.Where(x => x.AccountNumber.ToString().ToLower().Contains(numaccBox.Text.ToLower())).ToList();
            }
            moneytable.ItemsSource = list;
        }
        private void ChangeMark(object sender, SelectionChangedEventArgs e)
        {
            RefreshData();
        }
        private void ChangeStateNumber(object sender, TextChangedEventArgs e)
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
            MessageBoxResult res = MessageBox.Show("Вы уверены, что хотите удалить машину?", "Подтвердить", MessageBoxButton.YesNo);
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
                    MessageBox.Show("Ошибка, невозможно удалить данные машины!");
                }
            }
        }
    }
}