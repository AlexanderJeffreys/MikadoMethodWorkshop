using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Space
{
    internal class PhysicalObject
    {
        public double Radius { get; }

        private readonly Shape _shape;

        public double Mass { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Vx { get; set; }
        public double Vy { get; set; }

        public PhysicalObject(double weightKilos, double x, double y, double vx, double vy, double radius)
        {
            Radius = radius;
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

        public void RemoveFromCanvas(Canvas canvas)
        {
            canvas.Children.Remove(_shape);
        }

        public void Display()
        {
            if (!SpaceRunner.IsBouncingBalls)
            {
                _shape.Fill = SpaceRunner.WeightToColour(Mass);
                var diameter = Mass >= SpaceRunner.EarthWeight * 10000 ? 10 : 5;
                var xTmp = (int) ((X - SpaceRunner.CenterX) / SpaceRunner.Scale + SpaceRunner.Canvas.Width / 2);
                var yTmp = (int) ((Y - SpaceRunner.CenterY) / SpaceRunner.Scale + SpaceRunner.Canvas.Height / 2);

                _shape.Height = diameter;
                _shape.Width = diameter;
                Canvas.SetTop(_shape, xTmp - diameter / 2);
                Canvas.SetLeft(_shape, yTmp - diameter / 2);
            }
            else // Breakout
            {
                _shape.Fill = Brushes.White;

                var xTmp = (int) (X - SpaceRunner.CenterX + SpaceRunner.Canvas.Width / 2);
                var yTmp = (int) (Y - SpaceRunner.CenterY + SpaceRunner.Canvas.Height / 2);

                Canvas.SetTop(_shape, xTmp - Radius);
                Canvas.SetLeft(_shape, yTmp - Radius);
            }
        }

        public void Absorb(PhysicalObject other)
        {
            var totalMass = Mass + other.Mass;
            X = (X * Mass + other.X * other.Mass) / totalMass;
            Y = (Y * Mass + other.Y * other.Mass) / totalMass;
            Vx = (Vx * Mass + other.Vx * other.Mass) / totalMass;
            Vy = (Vy * Mass + other.Vy * other.Mass) / totalMass;
            Mass = totalMass;
        }

        public void HitBy(PhysicalObject other)
        {
            // find collision point by backstepping

            //backstep increment
            double s = -SpaceRunner.Seconds / 10;
            //total backstep size to be found incrementally
            double dt = 0;
            //vector from this object to the other object
            double[] new12 = {X - other.X, Y - other.Y};
            // new distance
            double d = Math.Sqrt(new12[0] * new12[0] + new12[1] * new12[1]);
            // backstep to find collision point
            while (d < Radius + other.Radius)
            {
                dt += s;
                new12[0] = new12[0] + s * (Vx - other.Vx);
                new12[1] = new12[1] + s * (Vy - other.Vy);
                d = Math.Sqrt(new12[0] * new12[0] + new12[1] * new12[1]);
            }
        }
    }
}