using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace UserControls
{
    public partial class HistogramChart : UserControl
    {
        double[] data;
        int[] Bins;

        int _numberOfBins;
        int numberOfBins { 
            get { return _numberOfBins; }
            set {
                if(value<=0)
                    throw new ArgumentException("Number of bins must be greater than zero");
                //Calculate BinWidth
                if (data != null)
                {
                    if (data.Length > 0)
                    {
                        _numberOfBins = value;
                        _binWidth = (data.Max() - data.Min()) / value;
                        ComputeData();
                    }
                    else throw new ArgumentNullException("data","Data has no elements");
                }
                else throw new ArgumentException("Data is null!");
            }
         }

        double _binWidth;
        double binWidth {
            get { return _binWidth; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Bin width must be greater than zero");
                //Calculate number of bins
                if (data != null)
                {
                    if (data.Length > 0)
                    {
                        _binWidth = value;
                        //_numberOfBins = Convert.ToInt32(Math.Ceiling(Math.Sqrt(data.Length)));
                        _numberOfBins = 1+Convert.ToInt32((data.Max() - data.Min())/ _binWidth);
                        ComputeData();
                    }
                    else throw new ArgumentNullException("data", "Data has no elements");
                }
                else throw new ArgumentException("Data is null!");
            }
        }

        public bool _EnableXLabels=true;
        public bool EnableXLabels { 
            get
            {
                return _EnableXLabels;
            }
            set
            {
                _EnableXLabels = value;
                if (!_EnableXLabels)
                {
                    chart.ChartAreas[0].AxisX.LabelStyle.Interval = data.Max() - data.Min();
                    chart.ChartAreas[0].AxisX.LabelStyle.IntervalOffset = binWidth;
                }
                ComputeData();
            }
        }

        /// <summary>
        /// Returns the associated chart contoller
        /// </summary>
        public Chart InternalChart { get { return chart; } }
        public HistogramChart()
        {
            InitializeComponent();
        }
        public HistogramChart(double[] data,int numberOfBins)
        {
            InitializeComponent();
            this.data = data;
            this.numberOfBins = numberOfBins;
        }

        public HistogramChart(double[] data, double binWidth)
        {
            InitializeComponent();
            this.data = data;
            this.binWidth = binWidth;
        }

        public void Compute(double[] data, double binWidth)
        {
            this.data = data;
            this.binWidth = binWidth;
        }
        public void Compute(double[] data, int numberOfBins)
        {
            this.data = data;
            this.numberOfBins = numberOfBins;
        }
        public void Compute(double[] data)
        {
            this.data = data;
            if (data != null)
            {
                if (data.Length > 0)
                {
                    _numberOfBins = Convert.ToInt32(Math.Ceiling(Math.Sqrt(data.Length)));
                    _binWidth = (data.Max() - data.Min()) / _numberOfBins;
                    ComputeData();
                }
                else throw new ArgumentNullException("data", "Data has no elements");
            }
            else throw new ArgumentException("Data is null!");
        }
        private void ComputeData()
        {
            chart.Series.First().Points.Clear();
            chart.ChartAreas[0].AxisX.CustomLabels.Clear();
            Bins = new int[numberOfBins];
            Array.Sort(data);
            int binIndex = 0;
            double minVal = data.Min();
            for (int i = 0; i < data.Length; ++i)
            {
                if (data[i] > 45)
                {
                    int a = 5;
                }
                if (!(data[i] >= ((binIndex * binWidth) + minVal) && data[i] < (((binIndex + 1) * binWidth) + minVal)))
                    binIndex++;
                if (binIndex >= 0 && binIndex < numberOfBins) 
                    Bins[binIndex]++;
            }
            chart.ChartAreas[0].AxisX.Minimum = data.Min()-binWidth;
            chart.ChartAreas[0].AxisX.Maximum = data.Max();
            chart.ChartAreas[0].AxisX.Interval = binWidth;
            for (int i = 0; i < numberOfBins; ++i)
            {
                var center = i * binWidth + minVal;
                var From = center - binWidth / 2;
                var To = center + binWidth / 2;
                chart.Series.First().Points.AddXY(center, Bins[i]);
                if(EnableXLabels)
                    chart.ChartAreas[0].AxisX.CustomLabels.Add(new CustomLabel(From,To, $"{From:F0}-{To:F0}",0,LabelMarkStyle.Box));
            }

            
            
        }
    }
}
