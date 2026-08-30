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
        Assert.HasCount(1, service.GetAllBookings());
        Assert.HasCount(1, service.GetAllBookings());
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
    [TestMethod]
    public void TC06_FullyReservedEquipment_IsRejected()
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

        service.CreateBooking(
            "S001",
            "E001",
            bookingDate,
            bookingDate.AddDays(1),
            out var firstBooking);

        // Act
        var result = service.CreateBooking(
            "S001",
            "E001",
            bookingDate,
            bookingDate.AddDays(2),
            out var secondBooking);

        // Assert
        Assert.IsNotNull(firstBooking);
        Assert.IsFalse(result.Success);
        Assert.IsNull(secondBooking);
        Assert.AreEqual(0, equipment.AvailableQuantity);
        Assert.HasCount(1, service.GetAllBookings());
    }

    [TestMethod]
    public void TC07_DamagedEquipment_IsRejected()
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

        equipment.MarkAsDamaged();
        service.AddStudent(student);
        service.AddEquipment(equipment);

        var bookingDate = new DateTime(2026, 8, 30);

        // Act
        var result = service.CreateBooking(
            "S001",
            "E001",
            bookingDate,
            bookingDate.AddDays(1),
            out var booking);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.IsNull(booking);
        Assert.AreEqual(1, equipment.AvailableQuantity);
        Assert.HasCount(0, service.GetAllBookings());
    }

    [TestMethod]
    public void TC08_RepeatedBookingAttempts_DoNotReduceAvailabilityBelowZero()
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

        service.CreateBooking(
            "S001",
            "E001",
            bookingDate,
            bookingDate.AddDays(1),
            out var firstBooking);

        // Act
        for (var attempt = 0; attempt < 3; attempt++)
        {
            var result = service.CreateBooking(
                "S001",
                "E001",
                bookingDate,
                bookingDate.AddDays(2),
                out var rejectedBooking);

            Assert.IsFalse(result.Success);
            Assert.IsNull(rejectedBooking);
        }

        // Assert
        Assert.IsNotNull(firstBooking);
        Assert.AreEqual(0, equipment.AvailableQuantity);
        Assert.HasCount(1, service.GetAllBookings());
    }

    [TestMethod]
    public void TC09_UnknownStudentId_IsRejected()
    {
        // Arrange
        var service = new BookingService();
        var equipment = new Equipment(
            "E001",
            "Basketball",
            "Balls",
            1);

        service.AddEquipment(equipment);

        var bookingDate = new DateTime(2026, 8, 30);

        // Act
        var result = service.CreateBooking(
            "UNKNOWN",
            "E001",
            bookingDate,
            bookingDate.AddDays(1),
            out var booking);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.IsNull(booking);
        Assert.AreEqual("Student not found.", result.Message);
        Assert.AreEqual(1, equipment.AvailableQuantity);
    }

    [TestMethod]
    public void TC10_UnknownEquipmentId_IsRejected()
    {
        // Arrange
        var service = new BookingService();
        var student = new Student(
            "S001",
            "Test Student",
            "student@aut.ac.nz");

        service.AddStudent(student);

        var bookingDate = new DateTime(2026, 8, 30);

        // Act
        var result = service.CreateBooking(
            "S001",
            "UNKNOWN",
            bookingDate,
            bookingDate.AddDays(1),
            out var booking);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.IsNull(booking);
        Assert.AreEqual("Equipment not found.", result.Message);
        Assert.HasCount(0, service.GetAllBookings());
    }
}