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
        public const double cMinMV = 0.0;
        public const double cMaxMV = 100.0;
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

            PointCollection pc = new PointCollection();
            for(int index = 0; index < MVList.Count; index++)
            {
                pc.Add(new Point(index *3 , MVList[index] * 5));
            }

            Polyline plChart = new Polyline
            {
                Stroke = new SolidColorBrush(Colors.White),
                Points = pc
            };

            canvas.Children.Add(plChart);
            
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
            SamplingLabel.Content = SamplingSlider.Value.ToString() + " ms";
        }


    }
}
