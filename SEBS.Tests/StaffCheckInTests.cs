using SEBS.Core;
using SEBS.Services;

namespace SEBS.Tests;

[TestClass]
public class StaffCheckInTests
{
    [TestMethod]
    public void TC15_ValidStaffCheckIn_CompletesBooking()
    {
        // Arrange
        var service = CreateService(out var equipment);
        var booking = CreateBooking(service);

        // Act
        var result = service.CheckIn(booking.BookingId, "ST001");

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(BookingStatus.Completed, booking.Status);
        Assert.AreEqual("ST001", booking.CompletedByStaffId);
        Assert.AreEqual(1, equipment.AvailableQuantity);
    }

    [TestMethod]
    public void TC16_InvalidStaffId_IsRejectedAndBookingStaysActive()
    {
        // Arrange
        var service = CreateService(out var equipment);
        var booking = CreateBooking(service);

        // Act
        var result = service.CheckIn(booking.BookingId, "UNKNOWN");

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual(BookingStatus.Active, booking.Status);
        Assert.AreEqual(0, equipment.AvailableQuantity);
    }

    [TestMethod]
    public void TC17_CheckInWithUnknownBookingId_IsRejected()
    {
        // Arrange
        var service = CreateService(out var equipment);

        // Act
        var result = service.CheckIn("UNKNOWN", "ST001");

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual("Booking not found.", result.Message);
        Assert.AreEqual(1, equipment.AvailableQuantity);
    }

    [TestMethod]
    public void TC18_CheckInOnCompletedBooking_IsRejected()
    {
        // Arrange
        var service = CreateService(out var equipment);
        var booking = CreateBooking(service);

        service.CheckIn(booking.BookingId, "ST001");

        // Act
        var result = service.CheckIn(booking.BookingId, "ST001");

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual(BookingStatus.Completed, booking.Status);
        Assert.AreEqual(1, equipment.AvailableQuantity);
    }

    [TestMethod]
    public void TC19_CheckInOnCancelledBooking_IsRejected()
    {
        // Arrange
        var service = CreateService(out var equipment);
        var booking = CreateBooking(service);

        service.CancelBooking(booking.BookingId);

        // Act
        var result = service.CheckIn(booking.BookingId, "ST001");

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual(BookingStatus.Cancelled, booking.Status);
        Assert.AreEqual(1, equipment.AvailableQuantity);
    }

    private static BookingService CreateService(out Equipment equipment)
    {
        var service = new BookingService();

        var student = new Student(
            "S001",
            "Test Student",
            "student@aut.ac.nz");

        equipment = new Equipment(
            "E001",
            "Basketball",
            "Balls",
            1);

        var staff = new StaffMember(
            "ST001",
            "Test Staff");

        service.AddStudent(student);
        service.AddEquipment(equipment);
        service.AddStaffMember(staff);

        return service;
    }

    private static Booking CreateBooking(BookingService service)
    {
        var bookingDate = new DateTime(2026, 8, 30);

        var result = service.CreateBooking(
            "S001",
            "E001",
            bookingDate,
            bookingDate.AddDays(1),
            out var booking);

        Assert.IsTrue(result.Success);
        Assert.IsNotNull(booking);

        return booking;
    }
}