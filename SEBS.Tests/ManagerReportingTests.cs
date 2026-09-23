using SEBS.Core;
using SEBS.Services;
using System.Collections.Generic;

namespace SEBS.Tests;

[TestClass]
public class ManagerReportingTests
{
    [TestMethod]
    public void TC32_Report_TotalsBookingsCorrectlyPerEquipmentItem()
    {
        // Arrange
        var service = CreateServiceWithEquipment(out _, ("E001", "Basketball"), ("E002", "Volleyball"));
        var dueDate = new DateTime(2026, 8, 30);

        CreateBooking(service, "E001", dueDate);
        CreateBooking(service, "E001", dueDate);
        CreateBooking(service, "E002", dueDate);

        // Act
        var report = service.GenerateManagerReport(dueDate.AddDays(-1));

        // Assert
        Assert.AreEqual(2, report.BookingsPerEquipment["Basketball"]);
        Assert.AreEqual(1, report.BookingsPerEquipment["Volleyball"]);
    }

    [TestMethod]
    public void TC33_Report_ReflectsCurrentOverdueCount()
    {
        // Arrange
        var service = CreateServiceWithEquipment(out _, ("E001", "Basketball"), ("E002", "Volleyball"));
        var pastDueDate = new DateTime(2026, 8, 30);
        var futureDueDate = new DateTime(2026, 9, 30);

        CreateBooking(service, "E001", pastDueDate);
        CreateBooking(service, "E002", futureDueDate);

        // Act
        var report = service.GenerateManagerReport(new DateTime(2026, 9, 1));

        // Assert
        Assert.AreEqual(1, report.OverdueBookingCount);
    }

    [TestMethod]
    public void TC34_Report_ReflectsCurrentDamagedEquipmentCount()
    {
        // Arrange
        var service = CreateServiceWithEquipment(out var equipmentList, ("E001", "Basketball"), ("E002", "Volleyball"));
        equipmentList[0].MarkAsDamaged();

        // Act
        var report = service.GenerateManagerReport(DateTime.Now);

        // Assert
        Assert.AreEqual(1, report.DamagedEquipmentCount);
    }

    [TestMethod]
    public void TC35_Report_OnEmptySystem_ReturnsZeroAndEmptyResults()
    {
        // Arrange
        var service = new BookingService();

        // Act
        var report = service.GenerateManagerReport(DateTime.Now);

        // Assert
        Assert.HasCount(0, report.BookingsPerEquipment);
        Assert.AreEqual(0, report.OverdueBookingCount);
        Assert.AreEqual(0, report.DamagedEquipmentCount);
    }

    private static BookingService CreateServiceWithEquipment(
        out List<Equipment> equipmentList,
        params (string id, string name)[] equipmentItems)
    {
        var service = new BookingService();

        service.AddStudent(new Student(
            "S001",
            "Test Student",
            "student@aut.ac.nz"));

        service.AddStaffMember(new StaffMember(
            "ST001",
            "Test Staff"));

        equipmentList = new List<Equipment>();
        foreach (var (id, name) in equipmentItems)
        {
            var equipment = new Equipment(id, name, "General", 5);
            service.AddEquipment(equipment);
            equipmentList.Add(equipment);
        }

        return service;
    }

    private static Booking CreateBooking(
        BookingService service,
        string equipmentId,
        DateTime dueDate)
    {
        var bookingDate = dueDate.AddDays(-2);

        var result = service.CreateBooking(
            "S001",
            equipmentId,
            bookingDate,
            dueDate,
            out var booking);

        Assert.IsTrue(result.Success);
        Assert.IsNotNull(booking);

        return booking;
    }
}