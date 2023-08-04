using NUnit.Framework;
using System.Linq;

namespace RobotFactory.Tests
{
    public class FactoryTests
    {
        private Factory factory;

        [SetUp]
        public void SetUp()
        {
            factory = new("Ivan", 2);
        }


        [Test]
        public void ConstructorShoulWorkProperly()
        {
            string expectedName = "Ivan";
            int expectedCapacity = 2;

            Assert.AreEqual(expectedName, factory.Name);
            Assert.AreEqual(expectedCapacity, factory.Capacity);
            Assert.NotNull(factory.Robots);
            Assert.NotNull(factory.Supplements);
        }

        [Test]
        public void ProduceRobotShouldHaveValidParameters()
        {
            string actualResult = factory.ProduceRobot("Robocop", 400, 13);
            string excpectedResult = "Produced --> Robot model: Robocop IS: 13, Price: 400.00";

            Assert.AreEqual(actualResult, excpectedResult);
        }

        [Test]
        public void ProduceRobotShouldAdAccordingly()
        { 
            int expectedCountBeforeProduce = 0;
            int actualCountBeforeProduce = factory.Robots.Count;

            factory.ProduceRobot("Robocop", 400, 13);

            int expectedCountAfterProduce = 1;
            int actualCountAfterProduce = factory.Robots.Count;

            Assert.AreEqual(expectedCountBeforeProduce, actualCountBeforeProduce);
            Assert.AreEqual(expectedCountAfterProduce, actualCountAfterProduce);

        }

        [Test]
        public void ProduceRobotShouldKnowIfCapacityIsFull()
        {


            factory.ProduceRobot("Robocop", 400, 13);
            factory.ProduceRobot("Robocop", 400, 13);
            string actualResult = factory.ProduceRobot("Robocop", 400, 13);
            string expectedResult = "The factory is unable to produce more robots for this production day!";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ProduceSupplementShouldHaveValidParameters()
        {
            Factory factory = new Factory("SpaceX", 2);

            string actualResult = factory.ProduceSupplement("SpecializedArm", 8);

            string expectedResult = "Supplement: SpecializedArm IS: 8";
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ProduceSupplementShouldAdd()
        {
            Factory factory = new Factory("SpaceX", 10);

            int expectedCountBeforeProduce = 0;
            int actualCountBeforeProduce = factory.Supplements.Count;

            factory.ProduceSupplement("SpecializedArm", 8);

            int expectedCountAfterProduce = 1;
            int actualCountAfterProduce = factory.Supplements.Count;

            Assert.AreEqual(expectedCountBeforeProduce, actualCountBeforeProduce);
            Assert.AreEqual(expectedCountAfterProduce, actualCountAfterProduce);

        }

        [Test]
        public void UpgradeRobotForFirstTime()
        {
            factory.ProduceRobot("Robocop", 400, 13);
            factory.ProduceSupplement("SpecializedArm", 13);

            var actualResult = factory.UpgradeRobot(factory.Robots.FirstOrDefault(),
            factory.Supplements.FirstOrDefault());

            Assert.IsTrue(actualResult);
        }

        [Test]
        public void UpgradeRobotShouldBeFalse()
        {
            factory.ProduceRobot("Robocop", 400, 13);
            factory.ProduceSupplement("SpecializedArm", 13);

            factory.UpgradeRobot(factory.Robots.FirstOrDefault(),
            factory.Supplements.FirstOrDefault());

            var actualResult = factory.UpgradeRobot(factory.Robots.FirstOrDefault(),
            factory.Supplements.FirstOrDefault());

            Assert.IsFalse(actualResult);

        }

        [Test]
        public void UpgradeRobotNotMatching()
        {
            factory.ProduceRobot("Robocop", 400, 13);
            factory.ProduceSupplement("SpecializedArm", 1234);

            var actualResult = factory.UpgradeRobot(factory.Robots.FirstOrDefault(),
                factory.Supplements.FirstOrDefault());

            Assert.IsFalse(actualResult);

        }

        [Test]
        public void SellRobotShouldBeSold()
        {
            factory.ProduceRobot("Robocop", 25423, 143);
            factory.ProduceRobot("Robocop", 400, 53);
            factory.ProduceRobot("Robocop", 534, 1234);

            Robot robot = factory.Robots.FirstOrDefault(r => r.Price == 400);

            var robotSold = factory.SellRobot(400);

            Assert.AreSame(robot, robotSold);

        }


    }
}
