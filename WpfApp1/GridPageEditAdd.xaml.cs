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
    /// Логика взаимодействия для GridPageEditAdd.xaml
    /// </summary>
    public partial class GridPageEditAdd : Page
    {
        flightEntities1 context;
        public GridPageEditAdd(flightEntities1 cont)
        {
            InitializeComponent();
            context = cont;           
            flag = true;
        }
        bool flag;
        private void SaveCar(object sender, RoutedEventArgs e)
        {
            if (flag == true)
            {
                BankTable flight = new BankTable()
                {
                    AccountNumber = Convert.ToInt64(idBox_Acc.Text),
                    FIO = idBox_FIO.Text,
                    LeavingFrom = idBoxFrom.Text,
                    GoingTo = idBoxTo.Text,
                    DepartureDate = Convert.ToDateTime(idDateD.Text),
                    ArrivalDate = Convert.ToDateTime(idDateA.Text),
                    TravelerAmount = Convert.ToInt64(idTravelers.Text),
                    FlightType = idFType.Text,
                    FlightCost = Convert.ToInt64(idFCost.Text),
                };
                context.BankTable.Add(flight);
                context.SaveChanges();
                NewFrame3Edit.Navigate(new GridPage());
            }
            else
            {
                context.BankTable.Find(greg.AccountNumber).AccountNumber = Convert.ToInt64(idBox_Acc.Text);
                context.BankTable.Find(greg.AccountNumber).FIO = idBox_FIO.Text;
                context.BankTable.Find(greg.AccountNumber).LeavingFrom = idBoxFrom.Text;
                context.BankTable.Find(greg.AccountNumber).GoingTo = idBoxTo.Text;
                context.BankTable.Find(greg.AccountNumber).DepartureDate = Convert.ToDateTime(idDateD.Text);
                context.BankTable.Find(greg.AccountNumber).ArrivalDate = Convert.ToDateTime(idDateA.Text);
                context.BankTable.Find(greg.AccountNumber).TravelerAmount = Convert.ToInt64(idTravelers.Text);
                context.BankTable.Find(greg.AccountNumber).FlightType = idFType.Text;
                context.BankTable.Find(greg.AccountNumber).FlightCost = Convert.ToInt64(idFCost.Text);
                context.SaveChanges();
                NewFrame3Edit.Navigate(new GridPage());
            }
        }
        BankTable greg;

        public GridPageEditAdd(flightEntities1 cont, BankTable grug)
        {
            InitializeComponent();
            context = cont;
            greg = grug;
            idBox_Acc.Text = grug.AccountNumber.ToString();
            idBox_FIO.Text = grug.FIO;
            idBoxFrom.Text = grug.LeavingFrom;
            idBoxTo.Text = grug.GoingTo;
            idDateD.Text = grug.DepartureDate.ToString();
            idDateA.Text = grug.ArrivalDate.ToString();
            idTravelers.Text = grug.TravelerAmount.ToString();
            idFType.Text = grug.FlightType;
            idFCost.Text = grug.FlightCost.ToString();
            flag = false;
        }
    }
}