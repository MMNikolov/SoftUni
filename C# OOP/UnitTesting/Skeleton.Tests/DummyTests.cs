using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class DummyTests
    {
        [Test]
        public void ConstructorsShouldInitializeCorrectly()
        {
            Dummy dummy = new Dummy(100, 100);

            Assert.AreEqual(100, dummy.Health);
        }

        [Test]
        public void TakeAttackShouldDecreaaseHelth()
        {
            Dummy dummy = new Dummy(100, 100);

            dummy.TakeAttack(50);
            Assert.AreEqual(50, dummy.Health);
        }

        [Test]
        public void TakeAttackShouldThrowExceptionIfTheDummyIsDead()
        {
            Dummy dummy = new Dummy(100, 100);

            dummy.TakeAttack(100);
            
            Assert.Throws<InvalidOperationException>(
                () => dummy.TakeAttack(50),
                "Dummy is dead.");

        }

        [Test]
        public void GiveExpShouldReturnCurrentExperienceIfDummyIsDead()
        {
            Dummy dummy = new Dummy(100, 100);

            dummy.TakeAttack(100);

            Assert.AreEqual(100, dummy.GiveExperience());
        }

        [Test]
        public void GiveExpShouldThrowAnExceptionIfTheDummyIsNotDead()
        {
            Dummy dummy = new Dummy(100, 100);

            dummy.TakeAttack(50);

            Assert.Throws<InvalidOperationException>(
                () => dummy.GiveExperience(),
                "Target is not dead.");
        }

        [Test]
        public void IsDeadShouldCheckIfHealthIsBelowOrEqualToZero()
        {
            Dummy dummy = new Dummy(100, 100);

            dummy.TakeAttack(100);

            Assert.IsTrue(dummy.IsDead());
        }
    }
}