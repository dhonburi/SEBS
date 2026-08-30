using SEBS.Core;
using SEBS.Services;

namespace SEBS.Tests;

[TestClass]
public class BookingCancellationTests
{
    [TestMethod]
    public void TC11_CancellingActiveBooking_ReleasesUnit()
    {
        // Arrange
        var service = CreateService(out var equipment);
        var booking = CreateBooking(service);

        // Act
        var result = service.CancelBooking(booking.BookingId);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(BookingStatus.Cancelled, booking.Status);
        Assert.AreEqual(1, equipment.AvailableQuantity);
    }

    [TestMethod]
    public void TC12_CancellingCompletedBooking_IsRejected()
    {
        // Arrange
        var service = CreateService(out var equipment);
        var booking = CreateBooking(service);

        service.AddStaffMember(new StaffMember("ST001", "Test Staff"));
        service.CheckIn(booking.BookingId, "ST001");

        // Act
        var result = service.CancelBooking(booking.BookingId);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual(BookingStatus.Completed, booking.Status);
        Assert.AreEqual(1, equipment.AvailableQuantity);
    }

    [TestMethod]
    public void TC13_CancellingAlreadyCancelledBooking_IsRejected()
    {
        // Arrange
        var service = CreateService(out var equipment);
        var booking = CreateBooking(service);

        service.CancelBooking(booking.BookingId);

        // Act
        var result = service.CancelBooking(booking.BookingId);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual(BookingStatus.Cancelled, booking.Status);
        Assert.AreEqual(1, equipment.AvailableQuantity);
    }

    [TestMethod]
    public void TC14_CancellingUnknownBookingId_IsRejected()
    {
        // Arrange
        var service = new BookingService();

        // Act
        var result = service.CancelBooking("UNKNOWN");

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual("Booking not found.", result.Message);
        Assert.HasCount(0, service.GetAllBookings());
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

        service.AddStudent(student);
        service.AddEquipment(equipment);

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