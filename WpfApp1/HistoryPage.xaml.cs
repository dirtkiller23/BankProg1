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
    /// Логика взаимодействия для HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        bankEntities1 context;
        public HistoryPage()
        {
            InitializeComponent();
            context = new bankEntities1();
            historytable.ItemsSource = context.Transaction_history.ToList();
        }
        public void RefreshData()
        {
            var list = context.Transaction_history.ToList();
            if (!string.IsNullOrWhiteSpace(numaccBox.Text))
            {
                list = list.Where(x => x.AccID.ToString().ToLower().Contains(numaccBox.Text.ToLower())).ToList();
            }
            historytable.ItemsSource = list;
        }
        private void ChangeStateNumber(object sender, TextChangedEventArgs e)
        {
            RefreshData();
        }      
    }
}
