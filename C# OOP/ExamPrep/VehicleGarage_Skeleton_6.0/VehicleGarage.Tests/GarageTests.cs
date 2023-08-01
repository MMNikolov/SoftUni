using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleGarage.Tests
{
    public class GarageTests
    {
        [Test]

        [TestCase(10)]
        [TestCase(1)]
        public void ConstructorsShouldInitializeAllValues(int capacity)
        {
            Garage garage = new(capacity);

            Assert.AreEqual(garage.Capacity, capacity);
            Assert.IsNotNull(garage.Vehicles);
        }


        [TestCase(5, "VW", "Golf", "FD9042JA")]
        public void AddVehiclesShouldAddIfGarageHasEnoughCapacityAndVehicleDoesntExist(
            int capacity, string brand, string model, string plateNumber)
        {
            Garage garage = new(capacity);
            Vehicle vehicle = new(brand, model, plateNumber);

            var result = garage.AddVehicle(vehicle);

            Assert.IsTrue(result);
            Assert.Contains(vehicle, garage.Vehicles);
        }


        [TestCase(5, "VW", "Golf", "FD9042JA")]
        public void AddVehicleShouldReturnFalseIfCarAlreadyExists(
            int capacity, string brand, string model, string plateNumber)
        {
            Garage garage = new(capacity);
            Vehicle vehicle = new(brand, model, plateNumber);

            garage.AddVehicle(vehicle);
            var result = garage.AddVehicle(vehicle);

            Assert.IsFalse(result);
            Assert.Contains(vehicle, garage.Vehicles);
        }



        public void AddVehicleShouldReturnFalseIfGarageIsFull()
        {
            Garage garage = new(2);

            Vehicle vehicle = new("CW", "SDAdaU", "SA0000DA");
            Vehicle vehicle2 = new("CWASD", "DASDAdaU", "SA2000DA");
            Vehicle vehicle3 = new("CW@", "SDAdADaU", "SA0040DA");

            garage.AddVehicle(vehicle);
            garage.AddVehicle(vehicle2);

            var result = garage.AddVehicle(vehicle3);
            var contains = garage.Vehicles.Any(x => x.Brand == vehicle3.Brand
            && x.Model == vehicle3.Model
            && x.LicensePlateNumber == vehicle3.LicensePlateNumber);

            Assert.IsFalse(result);
            Assert.IsFalse(contains);
        }

        [Test]

        public void CharheVehiclesShouldWork()
        {
            Garage garage = new(5);
        
            Vehicle vehicle = new("CW", "SDAdaU", "SA0000DA");
            Vehicle vehicle2 = new("CWASD", "DASDAdaU", "SA2000DA");
            Vehicle vehicle3 = new("CW@", "SDAdADaU", "SA0040DA");

            garage.AddVehicle(vehicle);
            garage.AddVehicle(vehicle2); 
            garage.AddVehicle(vehicle3);

            garage.DriveVehicle(vehicle.LicensePlateNumber, 90, false);
            garage.DriveVehicle(vehicle2.LicensePlateNumber, 70, false);
            garage.DriveVehicle(vehicle3.LicensePlateNumber, 10, false);

            var chargedVehicles = garage.ChargeVehicles(50);

            Assert.AreEqual(chargedVehicles, 2);
            Assert.AreEqual(vehicle.BatteryLevel, 100);
            Assert.AreEqual(vehicle2.BatteryLevel, 100);
            Assert.AreEqual(vehicle3.BatteryLevel, 90);

        }
    } 
}
