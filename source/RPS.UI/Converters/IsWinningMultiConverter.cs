using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;
using RPS.UI;

namespace RPS.UI.Converters
{
    public class IsWinningMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (((App)App.Current).IsServer) 
                return DependencyProperty.UnsetValue;

            var player1 = (string)values[0];
            var player2 = (string)values[1];
            var player1Wins = (int)values[2];
            var player2Wins = (int)values[3];

            
            if (player1Wins == player2Wins) 
                return DependencyProperty.UnsetValue;

            string winningPlayer;
            if (player1Wins > player2Wins)
                winningPlayer = player1;
            else
                winningPlayer = player2;

            var currentPlayer = ((App)App.Current).XmppConnection.MyJID.Bare.ToString();

            var isWinning = currentPlayer == winningPlayer;

            if (isWinning)
                return App.Current.FindResource("WinningBackgroundBrush");

            return App.Current.FindResource("LosingBackgroundBrush");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
