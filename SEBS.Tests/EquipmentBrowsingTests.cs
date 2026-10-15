using SEBS.Core;
using SEBS.Services;
using System.Diagnostics;

namespace SEBS.Tests;

[TestClass]
public class EquipmentBrowsingTests
{
    [TestMethod]
    public void TC01_GetAllEquipment_ReturnsSeededList()
    {
        // Arrange
        var service = new BookingService();
        var basketball = new Equipment("E001", "Basketball", "Balls", 3);
        var racket = new Equipment("E002", "Badminton Racket", "Rackets", 2);

        service.AddEquipment(basketball);
        service.AddEquipment(racket);

        // Act
        var equipment = service.GetAllEquipment();

        // Assert
        Assert.HasCount(2, equipment);
        CollectionAssert.Contains(equipment, basketball);
        CollectionAssert.Contains(equipment, racket);
    }

    [TestMethod]
    public void TC02_GetAllBookings_ReturnsAllCreatedBookings()
    {
        // Arrange
        var service = new BookingService();
        var student = new Student(
            "S001",
            "Test Student",
            "student@aut.ac.nz");

        var basketball = new Equipment("E001", "Basketball", "Balls", 1);
        var racket = new Equipment("E002", "Badminton Racket", "Rackets", 1);

        service.AddStudent(student);
        service.AddEquipment(basketball);
        service.AddEquipment(racket);

        var bookingDate = new DateTime(2026, 8, 30);

        service.CreateBooking(
            "S001",
            "E001",
            bookingDate,
            bookingDate.AddDays(1),
            out var firstBooking);

        service.CreateBooking(
            "S001",
            "E002",
            bookingDate,
            bookingDate.AddDays(2),
            out var secondBooking);

        // Act
        var bookings = service.GetAllBookings();

        // Assert
        Assert.HasCount(2, bookings);
        CollectionAssert.Contains(bookings, firstBooking);
        CollectionAssert.Contains(bookings, secondBooking);
    }

    [TestMethod]
    public void TC38_GetAvailableEquipment_ExcludesDamagedAndFullyReservedItems()
    {
        // Arrange
        var service = new BookingService();
        var student = new Student("S001", "Test Student", "student@aut.ac.nz");

        var available = new Equipment("E001", "Basketball", "Balls", 2);
        var fullyReserved = new Equipment("E002", "Volleyball", "Balls", 1);
        var damaged = new Equipment("E003", "Badminton Racket", "Rackets", 3);

        service.AddStudent(student);
        service.AddEquipment(available);
        service.AddEquipment(fullyReserved);
        service.AddEquipment(damaged);

        // Take the only unit of E002 and mark E003 as damaged
        var bookingDate = new DateTime(2026, 8, 30);
        service.CreateBooking("S001", "E002", bookingDate, bookingDate.AddDays(1), out _);
        damaged.MarkAsDamaged();

        // Act
        var result = service.GetAvailableEquipment();

        // Assert
        Assert.HasCount(1, result);
        CollectionAssert.Contains(result, available);
    }

    [TestMethod]
    public void TC40_AvailabilityQuery_WithLargeDataset_CompletesWithinTwoSeconds()
    {
        // Arrange - far larger than normal prototype load
        var service = new BookingService();
        service.AddStudent(new Student("S001", "Test Student", "student@aut.ac.nz"));

        for (int i = 1; i <= 1000; i++)
            service.AddEquipment(new Equipment($"E{i:D4}", $"Item {i}", "Balls", 10));

        var bookingDate = new DateTime(2026, 8, 30);
        for (int i = 0; i < 5000; i++)
        {
            var result = service.CreateBooking(
                "S001", $"E{(i % 1000) + 1:D4}", bookingDate, bookingDate.AddDays(1), out _);
            Assert.IsTrue(result.Success);
        }

        // Act
        var stopwatch = Stopwatch.StartNew();
        var available = service.GetAvailableEquipment();
        stopwatch.Stop();
        Console.WriteLine($"Availability query: {stopwatch.ElapsedMilliseconds} ms");

        // Assert
        Assert.HasCount(1000, available);
        Assert.IsTrue(stopwatch.ElapsedMilliseconds < 2000,
            $"Query took {stopwatch.ElapsedMilliseconds} ms");
    }
}