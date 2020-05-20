using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Wpf3DCubeWindow
{
    /// <summary>
    /// Логика взаимодействия для SetTimeWindow.xaml
    /// </summary>
    public partial class SetTimeWindow : Window
    {
        public SetTimeWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AnalogClock.seconds = 1;
            AnalogClock.hours = Convert.ToInt32(SetMyHour.Text);
            AnalogClock.minutes = Convert.ToInt32(SetMyMin.Text);
            DialogResult = true;
            this.Close();
        }
    }
}
