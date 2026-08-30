using SEBS.Core;
using SEBS.Services;

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
}