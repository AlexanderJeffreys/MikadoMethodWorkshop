using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Space
{
    internal class SpaceRunner
    {
        public static Canvas Canvas;
        private int _numberOfObjects = 75;
        private static List<PhysicalObject> _objects = new List<PhysicalObject>();
        public static bool IsBouncingBalls;
        
        private static readonly double G = 6.67428e-11; // m3/kgs2
        public double Seconds { get; set; } = 1;

        private const double AstronomicalUnit = 149597870.7e3;
        public const double EarthWeight = 5.9736e24;

        public static double CenterX = 0.0;
        public static double CenterY = 0.0;
        public static double Scale = 10;
        private readonly Random _random = new Random();


        public SpaceRunner(Canvas canvas, bool isBouncingBalls)
        {
            Canvas = canvas;
            IsBouncingBalls = isBouncingBalls;
            Setup();
        }
        
        public void Run()
        {
            var gameTickTimer = new DispatcherTimer();
            gameTickTimer.Tick += GameTickTimerOnTick;
            gameTickTimer.Interval = TimeSpan.FromMilliseconds(1);
            gameTickTimer.IsEnabled = true;
        }

        private void Setup()
        {
            if (!IsBouncingBalls)
            {
                Seconds = 3600 * 24 * 7;
            }
            
            AddInitialObjects();
            PlaceObjectsOnCanvas();
        }

        private void PlaceObjectsOnCanvas()
        {
            foreach (var physicalObject in _objects)
            {
                physicalObject.Display();
            }
        }

        private void AddInitialObjects()
        {
            if (!IsBouncingBalls)
            {
                var outerLimit = AstronomicalUnit * 20;
                Scale = outerLimit / Canvas.Width;
                
                for (var i = 0; i < _numberOfObjects; i++)
                {
                    var angle = RandSquare() * 2 * Math.PI;
                    var radius = (0.1 + 0.9 * Math.Sqrt(RandSquare())) * outerLimit;
                    var weightKilos = 1e3 * EarthWeight * Math.Pow(0.00001 + 0.99999 * RandSquare(), 12);
                    var x = radius * Math.Sin(angle);
                    var y = radius * Math.Cos(angle);
                    var speedRandom = Math.Sqrt(1 / radius) * 2978000*1500 * (0.4 + 0.6 * RandSquare());

                    var vx = speedRandom * Math.Sin(angle - Math.PI / 2);
                    var vy = speedRandom * Math.Cos(angle - Math.PI / 2);
                    Add(weightKilos, x, y, vx, vy, 1);
                }
                
                Add(EarthWeight * 20000, 0, 0, 0, 0, 1);
            }
        }

        private static void Add(double weightKilos, double x, double y, double vx, double vy, double radius)
        {
            var physicalObject = new PhysicalObject(weightKilos, x, y, vx, vy, radius);
            _objects.Add(physicalObject);
            physicalObject.AddToCanvas(Canvas);
        }

        private double RandSquare()
        {
            var randomDouble = _random.NextDouble(); 
            return randomDouble * randomDouble;
        }

        private void GameTickTimerOnTick(object sender, EventArgs e)
        {
            Step();
        }

        private void Step()
        {
            if (!IsBouncingBalls) {
                foreach (var obj in _objects) {
                    double fx = 0;
                    double fy = 0;
                    foreach (var oth in _objects) {
                        if (obj == oth)
                        {
                            continue;
                        }
                        double[] d = {obj.X - oth.X, obj.Y - oth.Y};
                        var r2 = Math.Pow(d[0], 2) + Math.Pow(d[1], 2);
                        var f = G * obj.Mass * oth.Mass / r2;
                        var sqrtOfR2 = Math.Sqrt(r2);
                        fx += f * d[0] / sqrtOfR2;
                        fy += f * d[1] / sqrtOfR2;
                    }
                    var ax = fx / obj.Mass;
                    var ay = fy / obj.Mass;
                    obj.X += obj.Vx * Seconds - ax * Math.Pow(Seconds, 2) / 2;
                    obj.Y += obj.Vy * Seconds - ay * Math.Pow(Seconds, 2) / 2;
                    obj.Vx -= ax * Seconds;
                    obj.Vy -= ay * Seconds;
                }
            } 
            else {
                foreach (var physicalObject in _objects) 
                {
                    physicalObject.X += physicalObject.Vx * Seconds;
                    physicalObject.Y += physicalObject.Vy * Seconds;
                }
            }
            
            PlaceObjectsOnCanvas();
        }


        public static Brush WeightToColour(double mass)
        {
            return Brushes.Chartreuse;
        }
    }
}