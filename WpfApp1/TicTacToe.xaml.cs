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
    /// Логика взаимодействия для TicTacToe.xaml
    /// </summary>
    public partial class TicTacToe : Page
    {

        public TicTacToe()
        {
            InitializeComponent();
        }
        public GameLogic _GameLogic = new GameLogic();

        private void PlayerClicksSpace(object sender, RoutedEventArgs e)
        {
            var space = (Button)sender;
            if (!String.IsNullOrWhiteSpace(space.Content?.ToString())) return;
            space.Content = _GameLogic.CurrentPlayer;


            var coodinates = space.Tag.ToString().Split(',');
            var xValue = int.Parse(coodinates[0]);
            var yValue = int.Parse(coodinates[1]);

            var buttonPosition = new Position() { x = xValue, y = yValue };
            _GameLogic.UpdateBoard(buttonPosition, _GameLogic.CurrentPlayer);

            if (_GameLogic.PlayerWin())
            {
                WinScreen.Text = $"{_GameLogic.CurrentPlayer} WINS!!!";
                WinScreen.Visibility = Visibility.Visible;
            }

            _GameLogic.SetNextPlayer();

        }
        public void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            foreach (var control in gridBoard.Children)
            {
                if (control is Button)
                {
                    ((Button)control).Content = String.Empty;
                }
            }

            resetGameState();
            WinScreen.Visibility = Visibility.Collapsed;
        }

        public void resetGameState()
        {
            _GameLogic = new GameLogic();
        }

    }
}

