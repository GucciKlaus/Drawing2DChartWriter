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

namespace Drawing2DChartWriter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {

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
