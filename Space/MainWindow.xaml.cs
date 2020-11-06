using System;
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
        

        public MainWindow()
        {
            InitializeComponent();
            var space = new SpaceRunner(GameArea);
            space.Run();
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

    internal class SpaceRunner
    {
        private readonly Canvas _canvas;
        private readonly PhysicalObject _ball = new PhysicalObject(100, 100);

        public SpaceRunner(Canvas canvas)
        {
            _canvas = canvas;
        }
        
        public void Run()
        {
            Setup();
            
            var gameTickTimer = new DispatcherTimer();
            gameTickTimer.Tick += GameTickTimerOnTick;
            gameTickTimer.Interval = TimeSpan.FromMilliseconds(5);
            gameTickTimer.IsEnabled = true;
        }

        private void Setup()
        {
            _canvas.Children.Add(_ball.Shape);
            _ball.PlaceObject();
        }
        
        private void GameTickTimerOnTick(object sender, EventArgs e)
        {
            Step();
        }
        
        private void Step()
        {
            MoveObjects();
        }
        
        private void MoveObjects()
        {
            _ball.x += 1;
            _ball.y += 1;
            
            _ball.PlaceObject();
        }
    }
}