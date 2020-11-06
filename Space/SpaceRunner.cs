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
        }
        
        public void Run()
        {
            Setup();
            
            var gameTickTimer = new DispatcherTimer();
            gameTickTimer.Tick += GameTickTimerOnTick;
            gameTickTimer.Interval = TimeSpan.FromMilliseconds(5);
            // gameTickTimer.IsEnabled = true;
        }

        private void Setup()
        {
            AddInitialObjects();
            PlaceObjects();
        }

        private void PlaceObjects()
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
                    var weightKilos = 1e3 * EarthWeight * (Math.Pow(0.00001 + 0.99999 * RandSquare(), 12));
                    var x = radius * Math.Sin(angle);
                    var y = radius * Math.Cos(angle);
                    var speedRandom = Math.Sqrt(1 / radius) * 2978000*1500 * (0.4 + 0.6 * RandSquare());

                    var vx = speedRandom * Math.Sin(angle - Math.PI / 2);
                    var vy = speedRandom * Math.Cos(angle - Math.PI / 2);
                    Add(weightKilos, x, y, vx, vy, 1);
                }
            } 
        }

        private static PhysicalObject Add(double weightKilos, double x, double y, double vx, double vy, double radius)
        {
            var physicalObject = new PhysicalObject(weightKilos, x, y, vx, vy, radius);
            _objects.Add(physicalObject);
            physicalObject.AddToCanvas(Canvas);
            return physicalObject;
        }

        private double RandSquare()
        {
            var randomDouble = _random.NextDouble(); 
            return randomDouble * randomDouble;
        }

        private void GameTickTimerOnTick(object sender, EventArgs e)
        {
            PlaceObjects();
        }

        public static Brush WeightToColour(double mass)
        {
            return Brushes.Chartreuse;
        }
    }
}