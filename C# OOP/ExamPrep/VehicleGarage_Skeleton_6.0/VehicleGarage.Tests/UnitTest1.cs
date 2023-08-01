using NUnit.Framework;

namespace VehicleGarage.Tests
{
    public class VehicleTests
    {

        [TestCase("ASD", "DSADW", "SA6647AS")]
        [TestCase("AS2D", "DSfaADW", "SA5447AS")]
        public void ConstructorsShouldInitializeAllValues(string make, string model, string licencePlate)
        {
            Vehicle vehicle = new(make, model, licencePlate);

            Assert.That(make, Is.EqualTo(vehicle.Brand));
            Assert.That(model, Is.EqualTo(vehicle.Model));
            Assert.That(licencePlate, Is.EqualTo(vehicle.LicensePlateNumber));
            Assert.That(false, Is.EqualTo(vehicle.IsDamaged));
            Assert.That(100, Is.EqualTo(vehicle.BatteryLevel));

        }
    }
}