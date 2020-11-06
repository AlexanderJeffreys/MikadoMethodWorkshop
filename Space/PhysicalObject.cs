using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Space
{
    internal class PhysicalObject
    {
        private readonly double _radius;

        private readonly Shape _shape; 

        private readonly double _mass;
        private readonly double _x;
        private readonly double _y;
        private readonly double _vx;
        private readonly double _vy;
        
        public PhysicalObject(double weightKilos, double x, double y, double vx, double vy, double radius)
        {
            _radius = radius;
            _mass = weightKilos;
            _x = x;
            _y = y;
            _vx = vx;
            _vy = vy;
            _shape = new Ellipse
            {
                Width = radius * 2,
                Height = radius * 2
            };
        }

        public void AddToCanvas(Canvas canvas)
        {
            canvas.Children.Add(_shape);
        }

        public void Display()
        {
            if (!SpaceRunner.IsBouncingBalls)
            {
                _shape.Fill = SpaceRunner.WeightToColour(_mass);
                var diameter = _mass >= SpaceRunner.EarthWeight * 10000 ? 7 : 2;
                var xtmp = (int) ((_x - SpaceRunner.CenterX) / SpaceRunner.Scale + SpaceRunner.Canvas.Width / 2);
                var ytmp = (int) ((_y - SpaceRunner.CenterY) / SpaceRunner.Scale + SpaceRunner.Canvas.Height / 2);
                
                Canvas.SetTop(_shape, xtmp - diameter/2);
                Canvas.SetLeft(_shape, ytmp - diameter/2);
            }
            else // Breakout
            {
                _shape.Fill = Brushes.White;
                
                var xtmp = (int) (_x - SpaceRunner.CenterX  + SpaceRunner.Canvas.Width / 2);
                var ytmp = (int) (_y - SpaceRunner.CenterY  + SpaceRunner.Canvas.Height / 2);
                
                Canvas.SetTop(_shape, xtmp - _radius);
                Canvas.SetLeft(_shape, ytmp - _radius);
            }
        }
    }
}