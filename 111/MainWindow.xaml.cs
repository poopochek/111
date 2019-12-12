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
        TextBox[,] Ta;
        TextBox[] Tb;
        int N = 3;
        public MainWindow()
        {
            InitializeComponent();
            MatrixErstellen(N); 
        
        }

        private void MatrixErstellen (int n)
        {
            Ta = new TextBox[n, n];
            Tb = new TextBox[n];

            for(int zeilen = 0; zeilen<n; zeilen++ )
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(1, GridUnitType.Star);
                Matrix.RowDefinitions.Add(rd);
            }
            for(int spalten = 0; spalten < n + 1; spalten ++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                cd.Width = new GridLength(1, GridUnitType.Star);
                Matrix.ColumnDefinitions.Add(cd);
            }


            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    TextBox x = new TextBox();
                    // x.Text = i + " ; " + j;
                    x.FontSize = 20;
                    Matrix.Children.Add(x);
                    Grid.SetColumn(x, j);
                    Grid.SetRow(x, i);
                    Ta[i, j] = x;
                }
                TextBox y = new TextBox();
                // y.Text = i.ToString();
                y.FontSize = 20;
                Matrix.Children.Add(y);
                Grid.SetColumn(y, n);
                Grid.SetRow(y, i);
                Tb[i] = y;
            }
        }

        private void Btn_Lösen_Click(object sender, RoutedEventArgs e)
        {
            double[,] GS = new double[N, N];
            double[] L  = new double[N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    GS[i, j] = Convert.ToDouble(Ta[i, j].Text);

                }
                L[i] = Convert.ToDouble(Tb[i].Text);
            }
            // Lösung berechnen
            double p0, p1, g;
           
            for (int i = 0; i < N; i++)
            {
                //evtl Zeilen tauschen
                int Zähler = 1;
                while (GS[i, i] == 0.0)
                {
                    if(Zähler == N)
                    {
                        MessageBox.Show("Das Gleichungssystem hat keine eindeutige Lösung.");
                        goto Ende;
                    }
                    ZeileTauschen(GS, L, i, Zähler);
                    Ausgabe(GS, L);
                    MessageBox.Show("Zeilen getauscht.");
                    Zähler++;
                }
                g = GS[i, i];
                double[] p = new double [N - 1];
                for(int k = 1; k <N; k++)
                {
                    p[k -1] = GS[(i + k) % N, i];
                }
               // p0 = GS[(i + 1) % 3, i];
               // p1 = GS[(i + 2) % 3, i];
                for (int j = 0; j < N; j++)
                {
                    GS[i, j] /= g;
                    for (int k=1; k<N; k++)
                    {
                        GS[(i + k) % N, j] += GS[i, j] * (-1) * p[k-1];
                    }
                   // GS[(i + 1) % 3, j] += GS[i, j]*(-1)*p0;
                    //GS[(i + 2) % 3, j] += GS[i, j] * (-1) * p1;
                }
                L[i] /= g;
                for (int k = 1; k < N; k++)
                {
                    L[(i + k) % N] += L[i] * (-1) * p[k-1];
                }
                //L[(i + 1) % 3] +=  L[i]*(-1)*p0;
                //L[(i + 2) % 3] += L[i] * (-1) * p1;
                Ausgabe(GS, L);
                MessageBox.Show("Schritt: " + i);
            }
        Ende:;
            

        }

        private void Ausgabe (double [,] GSA, double [] LA)
        {
            for (int i = 0; i < N; i++ )
            {
                for (int j = 0; j < N; j++)
                {
                    Ta[i, j].Text = GSA[i, j].ToString();
                }
                Tb[i].Text = LA[i].ToString(); 
            }
        }
        private void ZeileTauschen (double[,] GSA, double[] LA, int zeile1, int zeile2)
        {
            double temp = 0;
            for (int j = 0; j < N; j++)
            {
                temp = GSA[zeile1, j];
                GSA[zeile1, j] = GSA[zeile2, j];
                GSA[zeile2, j] = temp;
            }
            temp = LA[zeile1];
            LA[zeile1] = LA[zeile2];
            LA[zeile2] = temp;
        }

        private void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Matrix.Children.Clear();
            Matrix.ColumnDefinitions.Clear();
            Matrix.RowDefinitions.Clear();
            N = Menu.SelectedIndex + 2;
            MatrixErstellen(N);
        }
    }
}
