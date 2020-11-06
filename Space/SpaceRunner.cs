using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Space
{
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
            _canvas.Background = Brushes.Black;
            
            _canvas.Children.Add(_ball.Shape);
            _ball.PlaceObject();
        }
        
        private void GameTickTimerOnTick(object sender, EventArgs e)
        {
            _ball.x += 1;
            _ball.y += 1;
            
            _ball.PlaceObject();
        }
    }
}