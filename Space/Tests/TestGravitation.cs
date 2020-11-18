using NUnit.Framework;

namespace Space.Tests
{
    [TestFixture]
    public class TestGravitation
    {
        [Test]
        public void GravitationalFormulaIsCorrect()
        {
            var s = new SpaceRunner(false, false);
            SpaceRunner.Seconds = 1;
            const double earthsWeight = 5.9736e24;
            const int earthsRadius = 6371000;
            var earth = SpaceRunner.Add(earthsWeight, 0, -earthsRadius, 0, 0, 1);
            var lump = SpaceRunner.Add(1, 0, 10, 0, 0, 1);

            s.Step();

            Assert.That(10 - 9.82 / 2, Is.EqualTo(lump.Y).Within(0.02));
            Assert.That(-9.82, Is.EqualTo(lump.Vy).Within(0.02));
            Assert.That(-earthsRadius, Is.EqualTo(earth.Y).Within(0.02));
            Assert.That(0, Is.EqualTo(earth.Vy).Within(0.02));

            s.Step();

            Assert.That(10 - 4 * 9.82 / 2, Is.EqualTo(lump.Y).Within(0.02));
            Assert.That(-9.82 * 2, Is.EqualTo(lump.Vy).Within(0.02));
            Assert.That(-earthsRadius, Is.EqualTo(earth.Y).Within(0.02));
            Assert.That(0, Is.EqualTo(earth.Vy).Within(0.02));
        }

        [Test]
        public void MergeWithoutSpeed()
        {
            var one = new PhysicalObject(1, 1, 0, 0, 0, 1);
            var other = new PhysicalObject(1, 0, 1, 0, 0, 1);
            
            var merge = one.Absorb(other);
            
            Assert.That(0.5, Is.EqualTo(merge.X).Within(0.00001));
            Assert.That(0.5, Is.EqualTo(merge.Y).Within(0.00001));
            Assert.That(0.0, Is.EqualTo(merge.Vx).Within(0.00001));
            Assert.That(0.0, Is.EqualTo(merge.Vy).Within(0.00001));
        }

        [Test]
        public void MergeWithSpeed()
        {
            var one = new PhysicalObject(1, 1, 0, 1, 0, 1);
            var other = new PhysicalObject(1, 0, 1, 0, 1, 1);
            
            var merge = one.Absorb(other);
            
            Assert.That(0.5, Is.EqualTo(merge.X).Within(0.00001));
            Assert.That(0.5, Is.EqualTo(merge.Y).Within(0.00001));
            Assert.That(0.5, Is.EqualTo(merge.Vx).Within(0.00001));
            Assert.That(0.5, Is.EqualTo(merge.Vy).Within(0.00001));
            Assert.That(2, Is.EqualTo(merge.Mass).Within(0.00001));
        }

        [Test]
        public void MergeWithSpeedAndDifferentMasses()
        {
            var one = new PhysicalObject(1, 1, 1, 1, 0, 1);
            var other = new PhysicalObject(4, 0, 0, 0, 1, 1);
            
            var merge = one.Absorb(other);
            
            Assert.That(0.2, Is.EqualTo(merge.X).Within(0.00001));
            Assert.That(0.2, Is.EqualTo(merge.Y).Within(0.00001));
            Assert.That(0.2, Is.EqualTo(merge.Vx).Within(0.00001));
            Assert.That(0.8, Is.EqualTo(merge.Vy).Within(0.00001));
            Assert.That(5, Is.EqualTo(merge.Mass).Within(0.00001));
        }

        [Test]
        public void HeadsOnMergeConservesZeroSumMomentum()
        {
            var one = new PhysicalObject(10, 0, 0, 100, 100, 1);
            var other = new PhysicalObject(100, 0, 0, -10, -10, 1);
            
            var merge = one.Absorb(other);
            
            Assert.That(0, Is.EqualTo(merge.X).Within(0.00001));
            Assert.That(0, Is.EqualTo(merge.Y).Within(0.00001));
            Assert.That(0, Is.EqualTo(merge.Vx).Within(0.00001));
            Assert.That(0, Is.EqualTo(merge.Vy).Within(0.00001));
            Assert.That(110, Is.EqualTo(merge.Mass).Within(0.00001));
        }

        [Test]
        public void HeadsOnMergeConservesMomentum()
        {
            var one = new PhysicalObject(10, 0, 0, 10, 10, 1);
            var other = new PhysicalObject(100, 0, 0, 0, 0, 1);
            
            var merge = one.Absorb(other);
            
            Assert.That(0, Is.EqualTo(merge.X).Within(0.00001));
            Assert.That(0, Is.EqualTo(merge.Y).Within(0.00001));
            Assert.That(100 / 110.0, Is.EqualTo(merge.Vx).Within(0.00001));
            Assert.That(100 / 110.0, Is.EqualTo(merge.Vy).Within(0.00001));
            Assert.That(110, Is.EqualTo(merge.Mass).Within(0.00001));
        }
    }
}