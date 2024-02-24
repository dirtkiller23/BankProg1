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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AdminUserDialog.xaml
    /// </summary>
    public partial class AdminUserDialog : Window
    {
        public AdminUserDialog()
        {           
            InitializeComponent();
            LoadAccounts();
        }
        private void LoadAccounts()
        {
            using (var context = new bankEntities())
            {
                UserComboBox.ItemsSource = context.BankTable.ToList();
            }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserComboBox.SelectedItem != null)
            {
                AdminFlagger.UserFlag = (long)UserComboBox.SelectedValue;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Please select a user.");
            }
        }
    }
}
