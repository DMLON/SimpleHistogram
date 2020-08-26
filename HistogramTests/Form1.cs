using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HistogramTests
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int N = 1000;
            var data = new double[N];
            Random rand = new Random();
            for (int i = 0; i < N; i++)
            {
                data[i] = Convert.ToDouble(rand.Next(-50, 51));
            }

            //int N = 100;
            //var data = new double[N];
            //Random rand = new Random();
            //for (int i = 0; i < N; i++)
            //{
            //    data[i] = Math.Floor((double)i /3);
            //}
            histogramChart1.Compute(data);
            //histogramChart1.EnableXLabels = false;
            //histogramChart1.InternalChart.ChartAreas[0].AxisX.Minimum = -200;
        }
    }
}
