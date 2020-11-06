using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Space
{
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