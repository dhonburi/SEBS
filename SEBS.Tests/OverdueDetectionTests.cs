using SEBS.Core;
using SEBS.Services;

namespace SEBS.Tests;

[TestClass]
public class OverdueDetectionTests
{
    [TestMethod]
    [DataRow(1, true, DisplayName = "TC27_ActiveBookingPastDueDate_IsOverdue")]
    [DataRow(-1, false, DisplayName = "TC28_ActiveBookingBeforeDueDate_IsNotOverdue")]
    [DataRow(0, false, DisplayName = "TC29_ActiveBookingOnDueDate_IsNotOverdue")]
    public void OverdueDetection_ActiveBooking_DueDateBoundaries(int daysAfterDueDate, bool expectedOverdue)
    {
        // Arrange
        var service = CreateService();
        var dueDate = new DateTime(2026, 8, 30);
        var booking = CreateBooking(service, dueDate);

        // Act
        var overdueBookings =
            service.GetOverdueBookings(dueDate.AddDays(daysAfterDueDate));

        // Assert
        Assert.AreEqual(expectedOverdue, overdueBookings.Contains(booking));
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

    [TestMethod]
    public void TC37_ActiveBookingOnDueDate_IsNotOverdue_EvenLaterInTheDay()
    {
        // Arrange
        var service = CreateService();
        var dueDate = new DateTime(2026, 8, 30); // midnight, same as the GUI sends
        CreateBooking(service, dueDate);

        // Act 10am on the due date, like clicking the button with DateTime.Now
        var overdueBookings =
            service.GetOverdueBookings(new DateTime(2026, 8, 30, 10, 0, 0));

        // Assert
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