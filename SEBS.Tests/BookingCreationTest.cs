using SEBS.Core;
using SEBS.Services;

namespace SEBS.Tests;

[TestClass]
public class BookingCreationTest
{
    [TestMethod]
    public void TC03_ValidBooking_CreatesActiveBookingAndReducesAvailability()
    {
        // Arrange
        var service = new BookingService();
        var student = new Student(
            "S001",
            "Test Student",
            "student@aut.ac.nz");

        var equipment = new Equipment(
            "E001",
            "Basketball",
            "Balls",
            2);

        service.AddStudent(student);
        service.AddEquipment(equipment);

        var bookingDate = new DateTime(2026, 8, 30);
        var dueDate = bookingDate.AddDays(2);

        // Act
        var result = service.CreateBooking(
            "S001",
            "E001",
            bookingDate,
            dueDate,
            out var booking);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(booking);
        Assert.AreEqual(BookingStatus.Active, booking.Status);
        Assert.AreEqual(1, equipment.AvailableQuantity);
    }

    [TestMethod]
    public void TC04_DueDateOnOrBeforeBookingDate_IsRejected()
    {
        // Arrange
        var service = new BookingService();
        var student = new Student(
            "S001",
            "Test Student",
            "student@aut.ac.nz");

        var equipment = new Equipment(
            "E001",
            "Basketball",
            "Balls",
            2);

        service.AddStudent(student);
        service.AddEquipment(equipment);

        var bookingDate = new DateTime(2026, 8, 30);

        // Act
        var result = service.CreateBooking(
            "S001",
            "E001",
            bookingDate,
            bookingDate,
            out var booking);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.IsNull(booking);
        Assert.AreEqual(2, equipment.AvailableQuantity);
        Assert.HasCount(0, service.GetAllBookings());
    }

    [TestMethod]
    public void TC05_DueDateExactlyOneDayAfterBookingDate_Succeeds()
    {
        // Arrange
        var service = new BookingService();
        var student = new Student(
            "S001",
            "Test Student",
            "student@aut.ac.nz");

        var equipment = new Equipment(
            "E001",
            "Basketball",
            "Balls",
            1);

        service.AddStudent(student);
        service.AddEquipment(equipment);

        var bookingDate = new DateTime(2026, 8, 30);
        var dueDate = bookingDate.AddDays(1);

        // Act
        var result = service.CreateBooking(
            "S001",
            "E001",
            bookingDate,
            dueDate,
            out var booking);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(booking);
        Assert.AreEqual(BookingStatus.Active, booking.Status);
        Assert.AreEqual(dueDate, booking.DueDate);
        Assert.AreEqual(0, equipment.AvailableQuantity);
    }
}