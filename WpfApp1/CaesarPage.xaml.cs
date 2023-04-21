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
    /// Логика взаимодействия для CaesarPage.xaml
    /// </summary>
    public partial class CaesarPage : Page
    {
        byte[] b = new byte[100];
        public CaesarPage()
        {
            InitializeComponent();
        }

        private void Button_execute(object sender, RoutedEventArgs e)
        {
            b = Encoding.ASCII.GetBytes(inputTextBox.Text);
            foreach (byte element in b)
            {
                outputTextBox.Text += element + " - " + (char)element + "\r\n";
            }
        }

        private void Button_Encode(object sender, RoutedEventArgs e)
        {
            foreach (byte element in b)
            {
                encodeTextbox.Text += (char)(element + Convert.ToInt16(keyTextbox.Text));

            }
        }

        private void Button_Decode(object sender, RoutedEventArgs e)
        {
            b = Encoding.ASCII.GetBytes(encodeTextbox.Text);
            foreach (byte element in b)
            {
                decodeTextbox.Text += (char)(element - Convert.ToInt16(keyTextbox.Text));
            }
        }
    }
}
