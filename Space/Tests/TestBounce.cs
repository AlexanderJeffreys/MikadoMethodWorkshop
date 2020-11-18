using System;
using NUnit.Framework;

namespace Space.Tests
{
    public class TestBounce
    {
        [Test]
        public void StraightOn()
        {
            var one = new PhysicalObject(1, 0, 0, 0, 0, 1);
            var two = new PhysicalObject(1, -1, 0, 1, 0, 1);

            one.HitBy(two);

            Assert.That(1.0, Is.EqualTo(one.Vx).Within(0.0001));
            Assert.That(0.0, Is.EqualTo(one.Vy).Within(0.0001));
            Assert.That(0.0, Is.EqualTo(two.Vx).Within(0.0001));
            Assert.That(0.0, Is.EqualTo(two.Vy).Within(0.0001));
            Assert.That(-2.0, Is.LessThan(two.X));
            Assert.That(1.0, Is.EqualTo(one.X).Within(0.0001));
        }

        [Test]
        public void StraightOnVerticalDifferentMass()
        {
            var one = new PhysicalObject(1, 0, 0, 0, -1, 0.5);
            var two = new PhysicalObject(2, 0, -1, 0, 1, 0.5);

            one.HitBy(two);

            Assert.That(5.0 / 3, Is.EqualTo(one.Vy).Within(0.0001));
            Assert.That(-1.0 / 3, Is.EqualTo(two.Vy).Within(0.0001));
        }

        [Test]
        public void StraightOnDifferentMass1()
        {
            var one = new PhysicalObject(1, 0, 0, -1, 0, 0.5);
            var two = new PhysicalObject(2, -1, 0, 1, 0, 0.5);

            one.HitBy(two);

            Assert.That(5.0 / 3, Is.EqualTo(one.Vx).Within(0.0001));
            Assert.That(-1.0 / 3, Is.EqualTo(two.Vx).Within(0.0001));
        }

        [Test]
        public void StraightOnDifferentMass2()
        {
            var one = new PhysicalObject(1, 0, 0, -1, 0, 0.5);
            var two = new PhysicalObject(2, -1, 0, 1, 0, 0.5);

            one.HitBy(two);

            Assert.That(-1.0 / 3, Is.EqualTo(two.Vx).Within(0.0001));
            Assert.That(0, Is.EqualTo(two.Vy).Within(0.0001));
        }

        [Test]
        public void With90degImpactAngle()
        {
            var one = new PhysicalObject(1, 1, 0, 0, 1, Math.Sqrt(0.5));
            var two = new PhysicalObject(1, 0, 1, 1, 0, Math.Sqrt(0.5));

            one.HitBy(two);

            Assert.That(1, Is.EqualTo(one.Vx).Within(0.0001));
            Assert.That(0, Is.EqualTo(one.Vy).Within(0.0001));
            Assert.That(0, Is.EqualTo(two.Vx).Within(0.0001));
            Assert.That(1, Is.EqualTo(two.Vy).Within(0.0001));
        }

        [Test]
        public void With90degImpactAngleTurned()
        {
            var one = new PhysicalObject(1, 0, 0, 1, 1, 0.5);
            var two = new PhysicalObject(1, 1, 0, -1, 1, 0.5);

            one.HitBy(two);

            Assert.That(-1, Is.EqualTo(one.Vx).Within(0.0001));
            Assert.That(1, Is.EqualTo(one.Vy).Within(0.0001));
            Assert.That(1, Is.EqualTo(two.Vx).Within(0.0001));
            Assert.That(1, Is.EqualTo(two.Vy).Within(0.0001));
        }


        [Test]
        public void With45degImpactAngle()
        {
            var one = new PhysicalObject(1, 0, 0, 0, 0, 0.5);
            var two = new PhysicalObject(1, -1, 1, 1, 0, 0.5);

            one.HitBy(two);

            Assert.That(0.5, Is.EqualTo(one.Vx).Within(0.0001));
            Assert.That(-0.5, Is.EqualTo(one.Vy).Within(0.0001));
            Assert.That(0.5, Is.EqualTo(two.Vx).Within(0.0001));
            Assert.That(0.5, Is.EqualTo(two.Vy).Within(0.0001));
        }

        [Test]
        public void With45degImpactAngleFromBelow()
        {
            var one = new PhysicalObject(1, 0, 0, 0, 0, 0.5);
            var two = new PhysicalObject(1, -1, 0, 1, 1, 0.5);

            one.HitBy(two);

            Assert.That(1, Is.EqualTo(one.Vx).Within(0.0001));
            Assert.That(0, Is.EqualTo(one.Vy).Within(0.0001));
            Assert.That(0, Is.EqualTo(two.Vx).Within(0.0001));
            Assert.That(1, Is.EqualTo(two.Vy).Within(0.0001));
        }
    }
}