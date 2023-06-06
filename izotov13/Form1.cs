using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace izotov13
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int ClickCount = 0;

        Random random = new Random();

        double euro;
        double dollar;
        int days;

        double volatilityEuro = 0.01;
        double volatilityDollar = 0.01;
        double drift = 0.000015;
        int dt = 1;
        double standartNormalVal;
        double dw;

        private void btStartStop_Click(object sender, EventArgs e)
        {
            ClickCount++;
            if (ClickCount < 2)
            {
                days = 1;
                euro = (double)edEuro.Value;
                dollar = (double)edDollar.Value;

                chart1.Series[0].Points.Clear();
                chart1.Series[1].Points.Clear();
                chart1.Series[0].Points.AddXY(0, euro);
                chart1.Series[1].Points.AddXY(0, dollar);

                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ClickCount == 2)
            {
                ClickCount = 0;
                timer1.Stop();
            }

            standartNormalVal = Math.Round(Math.Sqrt(-2 * Math.Log(random.NextDouble())) * Math.Cos(2 * Math.PI * random.NextDouble()), 3);
            dw = dt * standartNormalVal;
            euro = euro * Math.Exp((drift - Math.Pow(volatilityEuro, 2) / 2) * dt + volatilityEuro * dw);

            standartNormalVal = Math.Round(Math.Sqrt(-2 * Math.Log(random.NextDouble())) * Math.Cos(2 * Math.PI * random.NextDouble()), 3);
            dw = dt * standartNormalVal;
            dollar = dollar * Math.Exp((drift - Math.Pow(volatilityDollar, 2) / 2) * dt + volatilityDollar * dw);

            chart1.Series[0].Points.AddXY(days, euro);
            chart1.Series[1].Points.AddXY(days, dollar);

            days++;

        }
    }
}
