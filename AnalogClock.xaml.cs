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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf3DCubeWindow
{
    /// <summary>
    /// Логика взаимодействия для AnalogClock.xaml
    /// </summary>
    public partial class AnalogClock : UserControl
    {

        static public int seconds { get; set; }
        static public int hours { get; set; }
        static public int minutes { get; set; }

        static public DateTime stopTime { get; set; }

        private double x0;
        private double y0;

        static public DoubleAnimation SecondAnimation { get; set; }
        static public DoubleAnimation MinuteAnimation { get; set; }
        static public DoubleAnimation HourAnimation { get; set; }

        private const int SECONDS_TO_ANGLE_RATE = 6; // 360 degrees divide 60 seconds equals 6
        public AnalogClock()
        {

            InitializeComponent();
        }

        public void StartClock() {
            scaleArrows();

            //Seconds
            //60s 360d -> 6
            int sAngleFrom = seconds * SECONDS_TO_ANGLE_RATE;
            int sAngleTo = 360;
            Duration sDuration = new Duration(TimeSpan.FromSeconds(60 - seconds));



            SecondAnimation =
            new DoubleAnimation(sAngleFrom, sAngleTo, sDuration);
            SecondAnimation.Completed += new EventHandler(secondAnimationHandler);
            ((RotateTransform)((TransformGroup)secondArrow.RenderTransform).Children[1]).Angle = sAngleFrom;
            ((TransformGroup)secondArrow.RenderTransform).Children[1].BeginAnimation(RotateTransform.AngleProperty, SecondAnimation);





            //Minutes
            int mAngleFrom = minutes * 6;
            int mAngleTo = 360;
            Duration mDuration = new Duration(TimeSpan.FromMinutes(60 - minutes));



            MinuteAnimation = new DoubleAnimation(mAngleFrom, mAngleTo, mDuration);
            MinuteAnimation.Completed += new EventHandler(minuteAnimationHandler);
            ((RotateTransform)((TransformGroup)minuteArrow.RenderTransform).Children[1]).Angle = mAngleFrom;
            ((TransformGroup)minuteArrow.RenderTransform).Children[1].BeginAnimation(RotateTransform.AngleProperty, MinuteAnimation);



            //Hours
            double hAngleFrom = hours * 30;
            double hAngleTo = 360;
            Duration hDuration = new Duration(TimeSpan.FromHours(Math.Abs(12 - hours)));



            HourAnimation = new DoubleAnimation(hAngleFrom, hAngleTo, hDuration);
            HourAnimation.Completed += new EventHandler(hourAnimationHandler);


            ((RotateTransform)((TransformGroup)hoursArrow.RenderTransform).Children[1]).Angle = hAngleFrom;
            ((TransformGroup)hoursArrow.RenderTransform).Children[1].BeginAnimation(RotateTransform.AngleProperty, HourAnimation);
        }

        public void StopClock() {
            stopTime = DateTime.Now;
            StopAnimation(hoursArrow);
            StopAnimation(minuteArrow);
            StopAnimation(secondArrow);
        }

        private void StopAnimation(Line line) {
            var minuteAnimation = new DoubleAnimation();
            minuteAnimation.BeginTime = null;
            ((TransformGroup)line.RenderTransform).Children[1].BeginAnimation(RotateTransform.AngleProperty, minuteAnimation);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            seconds = DateTime.Now.Second;
            minutes = DateTime.Now.Minute + (seconds / 60);
            hours =
               (DateTime.Now.Hour <= 12 ? DateTime.Now.Hour : Math.Abs(DateTime.Now.Hour - 12)) + (minutes / 60);

            StartClock();
        }


        void secondAnimationHandler(object sender, EventArgs e)
        {
            DoubleAnimation secondAnimationForever;


            secondAnimationForever =
               new DoubleAnimation(0, 360, new Duration(TimeSpan.FromSeconds(60)));
            secondAnimationForever.Completed += new EventHandler(secondAnimationHandler);
            ((TransformGroup)secondArrow.RenderTransform).Children[1].BeginAnimation(RotateTransform.AngleProperty, secondAnimationForever);


        }

        void minuteAnimationHandler(object sender, EventArgs e)
        {
            DoubleAnimation minuteAnimationForever =
                   new DoubleAnimation(0, 360, new Duration(TimeSpan.FromSeconds(60)));
            minuteAnimationForever.Completed += new EventHandler(minuteAnimationHandler);
            ((TransformGroup)minuteArrow.RenderTransform).Children[1].BeginAnimation(RotateTransform.AngleProperty, minuteAnimationForever);
        }

        void hourAnimationHandler(object sender, EventArgs e)
        {
            DoubleAnimation hourAnimationForever =
                   new DoubleAnimation(0, 360, new Duration(TimeSpan.FromSeconds(60)));
            hourAnimationForever.Completed += new EventHandler(hourAnimationHandler);
            ((TransformGroup)hoursArrow.RenderTransform).Children[1].BeginAnimation(RotateTransform.AngleProperty, hourAnimationForever);
        }

        void scaleArrows()
        {

            x0 = (this.canvasWindow.ActualWidth % 2 != 0)
                ? this.canvasWindow.ActualWidth / 2
                : this.canvasWindow.ActualWidth / 2 + 1;
            y0 = (this.canvasWindow.ActualHeight % 2 != 0)
                ? this.canvasWindow.ActualHeight / 2
                : this.canvasWindow.ActualHeight / 2 + 1;

            secondArrow.X2 = minuteArrow.X2 = hoursArrow.X2 = x0;
            secondArrow.Y2 = minuteArrow.Y2 = hoursArrow.Y2 = y0;

            secondArrow.X1 = minuteArrow.X1 = hoursArrow.X1 = x0;

            secondArrow.Y1 = y0 - y0 * 0.75;
            minuteArrow.Y1 = y0 - y0 * 0.65;
            hoursArrow.Y1 = y0 - y0 * 0.55;

            ((RotateTransform)((TransformGroup)secondArrow.RenderTransform).Children[1]).CenterX = x0;
            ((RotateTransform)((TransformGroup)secondArrow.RenderTransform).Children[1]).CenterY = y0;

            ((RotateTransform)((TransformGroup)minuteArrow.RenderTransform).Children[1]).CenterX = x0;
            ((RotateTransform)((TransformGroup)minuteArrow.RenderTransform).Children[1]).CenterY = y0;

            ((RotateTransform)((TransformGroup)hoursArrow.RenderTransform).Children[1]).CenterX = x0;
            ((RotateTransform)((TransformGroup)hoursArrow.RenderTransform).Children[1]).CenterY = y0;
        }

        private void canvasWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            scaleArrows();
        }
    }
}
