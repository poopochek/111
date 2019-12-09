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
                Grid.SetColumn(y, 3);
                Grid.SetRow(y, i);
                Tb[i] = y;
            }

           
        }

        private void Btn_Lösen_Click(object sender, RoutedEventArgs e)
        {
            double[,] GS = new double[3, 3];
            double[] L  = new double[3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    GS[i, j] = Convert.ToDouble(Ta[i, j].Text);

                }
                L[i] = Convert.ToDouble(Tb[i].Text);
            }
            // Lösung berechnen
            double p0, p1, g;
           
            for (int i = 0; i < 3; i++)
            {
                //evtl Zeilen tauschen
                int Zähler = 0;
                while (GS[i, i] == 0.0)
                {
                    if(Zähler == 3)
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
                p0 = GS[(i + 1) % 3, i];
                p1 = GS[(i + 2) % 3, i];
                for (int j = 0; j < 3; j++)
                {
                    GS[i, j] /= g;
                    GS[(i + 1) % 3, j] += GS[i, j]*(-1)*p0;
                    GS[(i + 2) % 3, j] += GS[i, j] * (-1) * p1;
                }
                L[i] /= g; 
               
                L[(i + 1) % 3] +=  L[i]*(-1)*p0;
                L[(i + 2) % 3] += L[i] * (-1) * p1;
                Ausgabe(GS, L);
                MessageBox.Show("Schritt: " + i);
            }
        Ende:;
            

        }

        private void Ausgabe (double [,] GSA, double [] LA)
        {
            for (int i = 0; i < 3; i++ )
            {
                for (int j = 0; j < 3; j++)
                {
                    Ta[i, j].Text = GSA[i, j].ToString();
                }
                Tb[i].Text = LA[i].ToString(); 
            }
        }
        private void ZeileTauschen (double[,] GSA, double[] LA, int zeile1, int zeile2)
        {
            double temp = 0;
            for (int j = 0; j < 3; j++)
            {
                temp = GSA[zeile1, j];
                GSA[zeile1, j] = GSA[zeile2, j];
                GSA[zeile2, j] = temp;
            }
            temp = LA[zeile1];
            LA[zeile1] = LA[zeile2];
            LA[zeile2] = temp;
        }
    }
}
