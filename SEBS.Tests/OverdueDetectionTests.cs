using SEBS.Core;
using SEBS.Services;

namespace SEBS.Tests;

[TestClass]
public class OverdueDetectionTests
{
    [TestMethod]
    public void TC27_ActiveBookingPastDueDate_IsOverdue()
    {
        // Arrange
        var service = CreateService();
        var dueDate = new DateTime(2026, 8, 30);
        var booking = CreateBooking(service, dueDate);

        // Act
        var overdueBookings =
            service.GetOverdueBookings(dueDate.AddDays(1));

        // Assert
        Assert.HasCount(1, overdueBookings);
        CollectionAssert.Contains(overdueBookings, booking);
    }

    [TestMethod]
    public void TC28_ActiveBookingBeforeDueDate_IsNotOverdue()
    {
        // Arrange
        var service = CreateService();
        var dueDate = new DateTime(2026, 8, 30);

        CreateBooking(service, dueDate);

        // Act
        var overdueBookings =
            service.GetOverdueBookings(dueDate.AddDays(-1));

        // Assert
        Assert.HasCount(0, overdueBookings);
    }

    [TestMethod]
    public void TC29_ActiveBookingOnDueDate_IsNotOverdue()
    {
        // Arrange
        var service = CreateService();
        var dueDate = new DateTime(2026, 8, 30);

        CreateBooking(service, dueDate);

        // Act
        var overdueBookings =
            service.GetOverdueBookings(dueDate);

        // Assert
        Assert.HasCount(0, overdueBookings);
    }

    [TestMethod]
    public void TC30_CompletedBooking_IsNeverOverdue()
    {
        // Arrange
        var service = CreateService();
        var dueDate = new DateTime(2026, 8, 30);
        var booking = CreateBooking(service, dueDate);

        service.CheckIn(booking.BookingId, "ST001");

        // Act
        var overdueBookings =
            service.GetOverdueBookings(dueDate.AddDays(10));

        // Assert
        Assert.AreEqual(BookingStatus.Completed, booking.Status);
        Assert.HasCount(0, overdueBookings);
    }

    [TestMethod]
    public void TC31_CancelledBooking_IsNeverOverdue()
    {
        // Arrange
        var service = CreateService();
        var dueDate = new DateTime(2026, 8, 30);
        var booking = CreateBooking(service, dueDate);

        service.CancelBooking(booking.BookingId);

        // Act
        var overdueBookings =
            service.GetOverdueBookings(dueDate.AddDays(10));

        // Assert
        Assert.AreEqual(BookingStatus.Cancelled, booking.Status);
        Assert.HasCount(0, overdueBookings);
    }

    private static BookingService CreateService()
    {
        var service = new BookingService();

        service.AddStudent(new Student(
            "S001",
            "Test Student",
            "student@aut.ac.nz"));

        service.AddEquipment(new Equipment(
            "E001",
            "Basketball",
            "Balls",
            5));

        service.AddStaffMember(new StaffMember(
            "ST001",
            "Test Staff"));

        return service;
    }

    private static Booking CreateBooking(
        BookingService service,
        DateTime dueDate)
    {
        var bookingDate = dueDate.AddDays(-2);

        var result = service.CreateBooking(
            "S001",
            "E001",
            bookingDate,
            dueDate,
            out var booking);

        Assert.IsTrue(result.Success);
        Assert.IsNotNull(booking);

        return booking;
    }
}