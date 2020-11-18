using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Space
{
    internal class PhysicalObject
    {
        public double Radius { get; }

        private Shape _shape;

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
        }

        public void AddToCanvas(Canvas canvas)
        {
            _shape = new Ellipse
            {
                Width = Radius * 2,
                Height = Radius * 2
            };
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
                Canvas.SetLeft(_shape, xTmp - diameter / 2);
                Canvas.SetTop(_shape, yTmp - diameter / 2);
            }
            else // Breakout
            {
                _shape.Fill = Brushes.White;

                var xTmp = (int) (X - SpaceRunner.CenterX + SpaceRunner.Canvas.Width / 2);
                var yTmp = (int) (Y - SpaceRunner.CenterY + SpaceRunner.Canvas.Height / 2);

                Canvas.SetLeft(_shape, xTmp - Radius);
                Canvas.SetTop(_shape, yTmp - Radius);
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
            var s = -SpaceRunner.Seconds / 10;
            //total backstep size to be found incrementally
            var dt = 0.0;
            //vector from this object to the other object
            double[] new12 = {X - other.X, Y - other.Y};
            // new distance
            var d = Math.Sqrt(new12[0] * new12[0] + new12[1] * new12[1]);
            // backstep to find collision point
            while (d < Radius + other.Radius)
            {
                dt += s;
                new12[0] = new12[0] + s * (Vx - other.Vx);
                new12[1] = new12[1] + s * (Vy - other.Vy);
                d = Math.Sqrt(new12[0] * new12[0] + new12[1] * new12[1]);
            }
            
            // simplify variables
            var m1 = other.Mass;
            var vx1 = other.Vx;
            var vy1 = other.Vy;
            // point of impact for other object
            var x1 = other.X + dt * vx1;
            var y1 = other.Y + dt * vy1;

            var m2 = Mass;
            var vx2 = Vx;
            var vy2 = Vy;
            // point of impact for this object
            var x2 = X + dt * vx2;
            var y2 = Y + dt * vy2;

            // direction of impact
            double[] p12 = {x2 - x1, y2 - y1};
            // normalize p12 to length 1
            var p12_abs = Math.Sqrt(p12[0] * p12[0] + p12[1] * p12[1]);
            double[] p12n = {p12[0] / p12_abs, p12[1] / p12_abs};

            // factor in calculation
            var c = p12n[0] * (vx1 - vx2) + p12n[1] * (vy1 - vy2);
            // fully elastic
            var e = 1;
            // new speeds
            double[] v1prim = {vx1 - p12n[0] * (1 + e) * (m2 * c / (m1 + m2)),
                vy1 - p12n[1] * (1 + e) * (m2 * c / (m1 + m2))};
            double[] v2prim = {vx2 + p12n[0] * (1 + e) * (m1 * c / (m1 + m2)),
                vy2 + p12n[1] * (1 + e) * (m1 * c / (m1 + m2))};

            // set variables back
            Vx = v2prim[0];
            Vy = v2prim[1];

            other.Vx = v1prim[0];
            other.Vy = v1prim[1];

            // step forward to where the objects should be
            X += v2prim[0] * (-dt);
            Y += v2prim[1] * (-dt);

            other.X += v1prim[0] * (-dt);
            other.Y += v1prim[1] * (-dt);
        }
    }
}