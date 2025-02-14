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

namespace WPF_2025._01._13_B
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Button> gombok = new List<Button>();
        private int[,] tabla = new int[3, 3];
        private int foglalt_mezo;

        Color[] all_c = { Colors.Red, Colors.Blue };
        private int player = 1;
        Brush def_brush;

        public MainWindow()
        {
            InitializeComponent();
            Start();
        }
        void Start()
        {
            //MessageBox.Show(GetColorName(all_c[0]));
            foglalt_mezo = 0;
            for (int i = 0; i < 9; i++)
            {
                //int sor = i / 3;
                //int oszlop = i % 3;
                //Button oneB = new Button() { Content = i.ToString() };
                Button oneB = new Button();
                Grid.SetRow(oneB, i / 3);
                Grid.SetColumn(oneB, i % 3);
                oneB.Click += ClickEvent;
                gombok.Add(oneB);
                gameplace.Children.Add(oneB);
            }
            def_brush = gombok[0].Background;
            //MessageBox.Show($"{def_brush.ToString()}, {gombok[0].Height}, {gombok[0].Width}");
        }
        private int Kiertekel()
        {
            int sum_1, sum_2;
            for (int i = 0; i < 3; i++)
            {
                sum_1 = 0;
                sum_2 = 0;
                for (int j = 0; j < 3; j++)
                {
                    sum_1 += tabla[i, j];
                    sum_2 += tabla[j, i];
                }
                if (Math.Abs(sum_1) == 3 || Math.Abs(sum_2) == 3) { return 1; }
                //if (Math.Abs(sum_1) == 3 || Math.Abs(sum_2) == 3) { return (sum_1 / 3 + sum_2 / 3); }
                //if (sum_1 == -3 || sum_2 == -3) { return -1; }
                //if (sum_2 == 3 || sum_2 == 3) { return 1; }
            }
            sum_1 = 0;
            sum_2 = 0;
            for (int i = 0; i < 3; i++)
            {
                sum_1 += tabla[i, i];
                sum_2 += tabla[2 - i, i];
            }
            if (Math.Abs(sum_1) == 3 || Math.Abs(sum_2) == 3) { return 1; }
            //if (Math.Abs(sum_1) == 3 || Math.Abs(sum_2) == 3) { return (sum_1 / 3 + sum_2 / 3); }
            //if (sum_1 == -3 || sum_2 == -3) { return -1; }
            //if (sum_2 == 3 || sum_2 == 3) { return 1; }
            return 0;
        }
        private void ShowMatrix()
        {
            string temp = "";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    temp += $"{tabla[i, j]};";
                }
                temp += "\n";
            }
            MessageBox.Show(temp);
        }
        private string GetColorName(Color inp_c)
        {
            return typeof(Colors)
            .GetProperties()
            .Where(p => p.PropertyType == typeof(Color))
            .Select(p => new { p.Name, c = (Color)p.GetValue(null) })
            .FirstOrDefault(d => d.c.Equals(inp_c))?.Name;
        }

        private void ClickEvent(Object s, EventArgs e)
        {
            Button a_gomb = s as Button;
            int b_n = gombok.IndexOf(a_gomb);  //MessageBox.Show(b_n.ToString());
            int sor = b_n / 3;
            int oszlop = b_n % 3;
            if (tabla[sor, oszlop] == 0)
            {
                player = ++player % 2;
                tabla[sor, oszlop] = player == 0 ? -1 : 1;
                a_gomb.Background = new SolidColorBrush(all_c[player]);
                //ShowMatrix();
                foglalt_mezo++;
            }
            else
            {
                MessageBox.Show("Nana!");
            }
            int chk = Kiertekel();
            if (chk != 0) { MessageBox.Show($"A' {GetColorName(all_c[player])}' színű játékos nyert!"); }
            else if (foglalt_mezo == 9) { MessageBox.Show("Döntetlen"); }
            else return;

            if (MessageBox.Show("New Game?", "Amőba:", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                //gombok.ForEach(gomb => gomb.Background = new SolidColorBrush(def_brush));
                gombok.ForEach(gomb => gomb.Background = def_brush);
                tabla = tabla = new int[3, 3];
                foglalt_mezo = 0;
                player = 1;
            }
            else
            {
                gombok.ForEach(gomb => gomb.Click -= ClickEvent);
            }
        }

    }
}