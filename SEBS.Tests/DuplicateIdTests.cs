using SEBS.Core;
using SEBS.Services;

namespace SEBS.Tests;

[TestClass]
public class DuplicateIdTests
{
    [TestMethod]
    public void TC49_AddingEquipmentWithDuplicateId_IsRejectedAndOriginalKept()
    {
        // Arrange
        var service = new BookingService();
        var original = new Equipment("E001", "Basketball", "Ball Sports", 2);
        service.AddEquipment(original);

        // Act
        var ex = Assert.ThrowsExactly<InvalidOperationException>(() =>
            service.AddEquipment(new Equipment("E001", "Volleyball", "Ball Sports", 5)));

        // Assert
        Assert.AreEqual("Equipment ID E001 already exists.", ex.Message);
        Assert.HasCount(1, service.GetAllEquipment());
        Assert.AreSame(original, service.GetAllEquipment()[0]);
    }

    [TestMethod]
    public void TC50_AddingStudentWithDuplicateId_IsRejectedAndOriginalKept()
    {
        // Arrange
        var service = new BookingService();
        service.AddStudent(new Student("S001", "Original Student", "original@aut.ac.nz"));
        service.AddEquipment(new Equipment("E001", "Basketball", "Ball Sports", 1));

        // Act
        var ex = Assert.ThrowsExactly<InvalidOperationException>(() =>
            service.AddStudent(new Student("S001", "Duplicate Student", "duplicate@aut.ac.nz")));

        var bookingDate = new DateTime(2026, 8, 30);
        service.CreateBooking("S001", "E001", bookingDate, bookingDate.AddDays(1), out var booking);

        // Assert
        Assert.AreEqual("Student ID S001 already exists.", ex.Message);
        Assert.IsNotNull(booking);
        Assert.AreEqual("Original Student", booking.Student.Name);
    }

    [TestMethod]
    public void TC51_AddingStaffWithDuplicateId_IsRejectedAndOriginalPinStillWorks()
    {
        // Arrange
        var service = new BookingService();
        service.AddStaffMember(new StaffMember("ST001", "Original Staff", "4821"));

        // Act
        var ex = Assert.ThrowsExactly<InvalidOperationException>(() =>
            service.AddStaffMember(new StaffMember("ST001", "Impostor Staff", "0000")));

        // Assert
        Assert.AreEqual("Staff ID ST001 already exists.", ex.Message);
        Assert.IsTrue(service.AuthenticateStaff("ST001", "4821").Success);
        Assert.IsFalse(service.AuthenticateStaff("ST001", "0000").Success);
    }

    [TestMethod]
    public void TC52_AddingNullRecords_IsRejected()
    {
        // Arrange
        var service = new BookingService();

        // Act / Assert
        Assert.ThrowsExactly<ArgumentNullException>(() => service.AddEquipment(null!));
        Assert.ThrowsExactly<ArgumentNullException>(() => service.AddStudent(null!));
        Assert.ThrowsExactly<ArgumentNullException>(() => service.AddStaffMember(null!));
    }
}
