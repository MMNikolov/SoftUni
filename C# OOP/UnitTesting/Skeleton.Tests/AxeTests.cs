using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class AxeTests
    {
        [Test]
        public void AttackMethodShouldDereaseDurabilityPoints()
        {
            Dummy target = new Dummy(10, 10);
            Axe axe = new Axe(100, 10);

            axe.Attack(target);

            Assert.AreEqual(9, axe.DurabilityPoints);
        }

        [Test]
        public void AttackMethodShouldThrowAnExceptionIfDurabilityIsZero()
        {
            Axe axe = new Axe(10, 0);
            Dummy dummy = new Dummy(100, 100);

            Assert.Throws<InvalidOperationException>(() => { axe.Attack(dummy); });
        }
    }
}