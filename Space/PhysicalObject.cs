using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Space
{
    internal class PhysicalObject
    {
        private readonly double _radius;

        private readonly Shape _shape;

        public double Mass { get; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Vx { get; set; }
        public double Vy { get; set; }

        public PhysicalObject(double weightKilos, double x, double y, double vx, double vy, double radius)
        {
            _radius = radius;
            Mass = weightKilos;
            X = x;
            Y = y;
            Vx = vx;
            Vy = vy;
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
                _shape.Fill = SpaceRunner.WeightToColour(Mass);
                var diameter = Mass >= SpaceRunner.EarthWeight * 10000 ? 7 : 2;
                var xtmp = (int) ((X - SpaceRunner.CenterX) / SpaceRunner.Scale + SpaceRunner.Canvas.Width / 2);
                var ytmp = (int) ((Y - SpaceRunner.CenterY) / SpaceRunner.Scale + SpaceRunner.Canvas.Height / 2);
                
                Canvas.SetTop(_shape, xtmp - diameter/2);
                Canvas.SetLeft(_shape, ytmp - diameter/2);
            }
            else // Breakout
            {
                _shape.Fill = Brushes.White;
                
                var xtmp = (int) (X - SpaceRunner.CenterX  + SpaceRunner.Canvas.Width / 2);
                var ytmp = (int) (Y - SpaceRunner.CenterY  + SpaceRunner.Canvas.Height / 2);
                
                Canvas.SetTop(_shape, xtmp - _radius);
                Canvas.SetLeft(_shape, ytmp - _radius);
            }
        }
    }
}