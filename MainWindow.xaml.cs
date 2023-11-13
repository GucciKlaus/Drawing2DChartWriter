using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Drawing2DChartWriter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const int cMaxNumofMV = 300;
        public const double cMinMV = 0.0;
        public const double cMaxMV = 100.0;

        public const double cBorderTop = 0.05;
        public const double cBorderBottom = 0.15;
        public const double cBorderLeft = 0.15;
        public const double cBorderRight = 0.05;
        public List<double> MVList { get; set; }
        public DispatcherTimer SamplingTimer { get; set; }
        public Random MVGenerator { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MVGenerator = new Random();
            MVList = new List<double>();
            SamplingTimer = new DispatcherTimer { IsEnabled = false, Interval = new TimeSpan(0, 0, 0, 0, 50) };
            SamplingTimer.Tick += SamplingTimer_Tick;
        }

        private void SamplingTimer_Tick(object? sender, EventArgs e)
        {
            canvas.Children.Clear();
            //Wert was ausgwählt wird von radio
            double newMV = MVGenerator.NextDouble() * (cMaxMV - cMinMV) + cMinMV;
            MVList.Add(newMV);
            while (MVList.Count > cMaxNumofMV)
            {
                MVList.RemoveAt(0);
            }

            PointCollection pc = new PointCollection();
            for(int index = 0; index < MVList.Count; index++)
            {
                pc.Add(new Point(Index2PixX(index) , Mv2PixY(MVList[index])));
            }

            Polyline plChart = new Polyline
            {
                Stroke = new SolidColorBrush(Colors.White),
                Points = pc
            };

            canvas.Children.Add(plChart);
            
        }

        private double Mv2PixY(double mv)
        {
            double percent = (mv - cMinMV) / (cMaxMV - cMinMV);
            return canvas.ActualHeight -
                (percent * canvas.ActualHeight * (1.0 - cBorderTop - cBorderBottom) + canvas.ActualHeight*cBorderBottom);
        }

        private double Index2PixX(double index)
        {
            double percent = index / cMaxNumofMV;
            return percent *  canvas.ActualWidth * (1.0 - cBorderLeft - cBorderRight) + canvas.ActualWidth* cBorderLeft;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            SamplingTimer.IsEnabled = !SamplingTimer.IsEnabled;
        }

        private void ValueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            PrgValue.Value = ValueSlider.Value;
            valueLabel.Content = ValueSlider.Value.ToString();
        }

        private void SamplingSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SamplingTimer.Interval = SamplingSlider.Value;
            SamplingLabel.Content = SamplingSlider.Value.ToString() + " ms";
        }


    }
}
