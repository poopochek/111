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

namespace _111
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TextBox[,] Ta = new TextBox[3, 3];
        TextBox[] Tb = new TextBox[3];
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < 3; i++ )
            {
                for ( int j = 0; j < 3; j++)
                {
                    TextBox x = new TextBox();
                    x.Text = i + " ; " + j;
                    x.FontSize = 20;
                    Matrix.Children.Add(x);
                    Grid.SetColumn(x, j);
                    Grid.SetRow(x, i);
                    Ta[i, j] = x;
                }
            }

        }
    }
}
