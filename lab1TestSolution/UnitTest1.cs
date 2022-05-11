using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace VehicleTest
{
    [TestClass]
    public class UnitTest1 
    {
        private VehicleTracker testTracker;
        private Vehicle vehicle1;
        private Vehicle vehicle2;
        private Vehicle vehicle3;
        private Vehicle vehicle4;
        private int capacity = 6;
        [TestInitialize]
        public void TestInitialize()
        {
            vehicle1 = new Vehicle("MB100", false);
            vehicle2 = new Vehicle("BC200", true);
            vehicle3 = new Vehicle("AB300", true);
            vehicle4 = new Vehicle("MB400", true);
            testTracker = new VehicleTracker(capacity, "Winnipeg");
        }
        // GenerateSlots Function
        [TestMethod]
        public void checkIfGenerateSlotCorrectly()
        {
            //Arrange
            var expectedNumberofSlot = capacity;
            int Slotavailable = testTracker.VehicleList.Where(v => v.Value == null).Count();
            //Assert
            Assert.AreEqual(expectedNumberofSlot, Slotavailable);
        }
        [TestMethod]
        public void LessThan1CapacityinGenerate()
        {
            // Arrange
            var capacity1 = -20;
            
            var address = "winnipeg";

            //Act and Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                var testTracker = new VehicleTracker(capacity1, address);
            });

           
        }
        //AddVehicle Function
        [TestMethod]
        public void AddVehicletoSlot()
        {
            //Arrange
            var Expectedslotfilled = 2;

            //Act
            testTracker.AddVehicle(vehicle1);
            testTracker.AddVehicle(vehicle2);
            int slotfilled = testTracker.VehicleList.Where((v) => v.Value != null).Count();
            //Assert
            Assert.AreEqual(Expectedslotfilled, slotfilled);

        }
        [TestMethod]
        public void OutOfRangeExceptipnInAddVehicle()
        {
            //Act and Assert
            Assert.ThrowsException<IndexOutOfRangeException>(() =>
            {
                //6 slosts == null
                testTracker.AddVehicle(vehicle1);
                // 5 slots ==null
                testTracker.AddVehicle(vehicle2);
                testTracker.AddVehicle(vehicle3); 
                testTracker.AddVehicle(vehicle4);
                testTracker.AddVehicle(vehicle4);
                testTracker.AddVehicle(vehicle4);// full
                //0slot ==null
                testTracker.AddVehicle(vehicle4);// expected throw exception

            });
        }
        //RemoveVehicle Function
        [TestMethod]
        public void TestRemoveVehiclebyLicence()
        {
            //Arrange 
            testTracker.AddVehicle(vehicle1);
            var licenceOfVehicle1 = vehicle1.Licence;

            //Act
            testTracker.RemoveVehicle(licenceOfVehicle1);

            //Arrange

            Assert.AreEqual(false, testTracker.VehicleList.Values.Contains(vehicle1));
        }
        [TestMethod]
        public void InvalidlicenceInRemoveVehicle()
        {
            //Arrange
            var invalidLicence = "aaaaa";

            //Act Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                testTracker.RemoveVehicle(invalidLicence);
            });
        }
        [TestMethod]
        public void InvalidSlotNumberToRemoveVehicle()
        {
            //slot number > capacity
            var slotnumber1 = capacity + 10;
            //slot number <= 0 
            var slotnumber2 = 0;
            //VehicleList[slotnumber] == null
            var slotnumber3 = 3;

            //Act & Assert
            Assert.IsFalse(testTracker.RemoveVehicle(slotnumber1));
            Assert.IsFalse(testTracker.RemoveVehicle(slotnumber2));
            Assert.IsFalse(testTracker.RemoveVehicle(slotnumber3));
        }

    }
}