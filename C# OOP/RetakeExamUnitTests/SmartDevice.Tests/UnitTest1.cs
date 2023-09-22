namespace SmartDevice.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Tests
    {

        [Test]
        public void TestTakePhotoShouldReturnSuccess()
        {
            int memoryCapacity = 1000;
            Device device = new Device(memoryCapacity);
            int photoSize = 100;

            bool result = device.TakePhoto(photoSize);

            Assert.IsTrue(result);
            Assert.AreEqual(memoryCapacity - photoSize, device.AvailableMemory);
            Assert.AreEqual(1, device.Photos);
        }

        [Test]
        public void TestTakePhotoShouldReturnFail()
        {
            int memoryCapacity = 100;
            Device device = new Device(memoryCapacity);
            int photoSize = 200;

            bool result = device.TakePhoto(photoSize);

            Assert.IsFalse(result);
            Assert.AreEqual(memoryCapacity, device.AvailableMemory);
            Assert.AreEqual(0, device.Photos);
        }

        [Test]
        public void TestInstallApp_Success()
        {
            int memoryCapacity = 1000;
            Device device = new Device(memoryCapacity);
            string appName = "TestApp";
            int appSize = 200;

            string result = device.InstallApp(appName, appSize);

            Assert.AreEqual($"{appName} is installed successfully. Run application?", result);
            Assert.AreEqual(memoryCapacity - appSize, device.AvailableMemory);
            CollectionAssert.Contains(device.Applications, appName);
        }

        [Test]
        public void TestInstallApp_Failure()
        {
            int memoryCapacity = 100;
            Device device = new Device(memoryCapacity);
            string appName = "LargeApp";
            int appSize = 200;

            Assert.Throws<InvalidOperationException>(()
                => device.InstallApp(appName, appSize));
            Assert.AreEqual(memoryCapacity, device.AvailableMemory);
            CollectionAssert.DoesNotContain(device.Applications, appName);
        }

        [Test]
        public void TestFormatDevice()
        {
            int memoryCapacity = 1000;
            Device device = new Device(memoryCapacity);

            device.TakePhoto(100);
            device.InstallApp("App1", 200);

            device.FormatDevice();

            Assert.AreEqual(0, device.Photos);
            CollectionAssert.AreEqual(new List<string>(), device.Applications);
            Assert.AreEqual(memoryCapacity, device.AvailableMemory);
        }

        [Test]
        public void TestGetDeviceStatus()
        {
            int memoryCapacity = 1000;
            Device device = new Device(memoryCapacity);

            device.TakePhoto(100);
            device.InstallApp("App1", 200);
            device.InstallApp("App2", 150);

            string expectedStatus = $"Memory Capacity: {memoryCapacity} MB, Available Memory: {memoryCapacity - 450} MB" +
                                    $"{Environment.NewLine}Photos Count: 1" +
                                    $"{Environment.NewLine}Applications Installed: App1, App2";

            string status = device.GetDeviceStatus();

            Assert.AreEqual(expectedStatus, status);
        }
    }
}