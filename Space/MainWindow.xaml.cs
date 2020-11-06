using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Space
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private static PhysicalObject ball = new PhysicalObject(100, 100);
        
        private DispatcherTimer gameTickTimer = new DispatcherTimer();
        
        public MainWindow()
        {
            InitializeComponent();
            
            GameArea.Children.Add(ball.Shape);
            ball.PlaceObject();
            RunGame();
        }
        
        private void RunGame()
        {
            gameTickTimer.Tick += GameTickTimerOnTick;
            gameTickTimer.Interval = TimeSpan.FromMilliseconds(5);
            gameTickTimer.IsEnabled = true;
        }

        private void GameTickTimerOnTick(object sender, EventArgs e)
        {
            MoveObjects();
        }

        private void MoveObjects()
        {
            ball.x += 1;
            ball.y += 1;
            
            ball.PlaceObject();
        }
    }

    internal class PhysicalObject
    {
        public double x;
        public double y;

        public Shape Shape; 

        private int _radius = 30;

        public PhysicalObject(double x, double y)
        {
            this.x = x;
            this.y = y;
            
            Shape = new Ellipse
            {
                Width = _radius,
                Height = _radius,
                Fill = Brushes.Blue
            };
        }

        public void PlaceObject()
        {
            Canvas.SetTop(Shape, x);
            Canvas.SetLeft(Shape, y);
        }
    }
}