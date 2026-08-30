using SEBS.Core;
using SEBS.Services;

namespace SEBS.Tests;

[TestClass]
public class DamageReportingAndRepairTests
{
    [TestMethod]
    public void TC20_DamagedCheckIn_CompletesBookingAndMarksEquipmentDamaged()
    {
        // Arrange
        var service = CreateService(out var equipment);
        var booking = CreateBooking(service);

        // Act
        var result = service.CheckInDamaged(booking.BookingId, "ST001");

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(BookingStatus.Completed, booking.Status);
        Assert.AreEqual("ST001", booking.CompletedByStaffId);
        Assert.IsTrue(equipment.IsDamaged);
        Assert.AreEqual(1, equipment.AvailableQuantity);
    }

    [TestMethod]
    public void TC21_DamagedCheckInWithInvalidStaffId_IsRejected()
    {
        // Arrange
        var service = CreateService(out var equipment);
        var booking = CreateBooking(service);

        // Act
        var result = service.CheckInDamaged(booking.BookingId, "UNKNOWN");

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual(BookingStatus.Active, booking.Status);
        Assert.IsFalse(equipment.IsDamaged);
        Assert.AreEqual(0, equipment.AvailableQuantity);
    }

    [TestMethod]
    public void TC22_DamagedCheckInOnNonActiveBooking_IsRejected()
    {
        // Arrange
        var service = CreateService(out var equipment);
        var booking = CreateBooking(service);

        service.CheckIn(booking.BookingId, "ST001");

        // Act
        var result = service.CheckInDamaged(booking.BookingId, "ST001");

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual(BookingStatus.Completed, booking.Status);
        Assert.IsFalse(equipment.IsDamaged);
        Assert.AreEqual(1, equipment.AvailableQuantity);
    }

    [TestMethod]
    public void TC23_DamagedCheckInSucceedsWhenEquipmentIsAlreadyDamaged()
    {
        // Arrange
        var service = CreateService(out var equipment);
        var booking = CreateBooking(service);

        equipment.MarkAsDamaged();

        // Act
        var result = service.CheckInDamaged(booking.BookingId, "ST001");

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(BookingStatus.Completed, booking.Status);
        Assert.IsTrue(equipment.IsDamaged);
        Assert.AreEqual(1, equipment.AvailableQuantity);
    }

    [TestMethod]
    public void TC24_MarkingEquipmentAsRepaired_ClearsDamagedFlag()
    {
        // Arrange
        var service = CreateService(out var equipment);
        equipment.MarkAsDamaged();

        // Act
        var result = service.MarkRepaired("E001", "ST001");

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsFalse(equipment.IsDamaged);
        Assert.IsTrue(equipment.IsAvailable());
    }

    [TestMethod]
    public void TC25_MarkingUnknownEquipmentAsRepaired_IsRejected()
    {
        // Arrange
        var service = CreateService(out _);

        // Act
        var result = service.MarkRepaired("UNKNOWN", "ST001");

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual("Equipment not found.", result.Message);
    }

    [TestMethod]
    public void TC26_MarkingEquipmentAsRepairedWithInvalidStaffId_IsRejected()
    {
        // Arrange
        var service = CreateService(out var equipment);
        equipment.MarkAsDamaged();

        // Act
        var result = service.MarkRepaired("E001", "UNKNOWN");

        // Assert
        Assert.IsFalse(result.Success);
        Assert.IsTrue(equipment.IsDamaged);
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