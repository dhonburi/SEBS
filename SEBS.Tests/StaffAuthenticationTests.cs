using SEBS.Core;
using SEBS.Services;

namespace SEBS.Tests;

[TestClass]
public class StaffAuthenticationTests
{
    [TestMethod]
    public void TC43_ValidStaffIdAndPin_IsAuthenticated()
    {
        // Arrange
        var service = CreateService();

        // Act
        var result = service.AuthenticateStaff("ST001", "4821");

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual("Signed in as Test Staff.", result.Message);
    }

    [TestMethod]
    public void TC44_WrongPin_IsRejected()
    {
        // Arrange
        var service = CreateService();

        // Act
        var result = service.AuthenticateStaff("ST001", "1111");

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual("Invalid staff ID or PIN.", result.Message);
    }

    [TestMethod]
    public void TC45_UnknownStaffId_GivesSameMessageAsWrongPin()
    {
        // Arrange
        var service = CreateService();

        // Act
        var unknownId = service.AuthenticateStaff("UNKNOWN", "4821");
        var wrongPin = service.AuthenticateStaff("ST001", "0000");

        // Assert
        Assert.IsFalse(unknownId.Success);
        Assert.AreEqual(wrongPin.Message, unknownId.Message);
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    [DataRow("48210")]
    public void TC46_BlankOrNearMissPin_IsRejected(string? pin)
    {
        // Arrange
        var service = CreateService();

        // Act
        var result = service.AuthenticateStaff("ST001", pin!);

        // Assert
        Assert.IsFalse(result.Success);
    }

    [TestMethod]
    public void TC47_PinIsStoredAsSaltedHashNotPlainText()
    {
        // Arrange
        var first = new StaffMember("ST001", "First Staff", "4821");
        var second = new StaffMember("ST002", "Second Staff", "4821");

        // Act
        var pinBytes = System.Text.Encoding.UTF8.GetBytes("4821");

        // Assert
        CollectionAssert.AreNotEqual(pinBytes, first.PinHash);
        CollectionAssert.AreNotEqual(first.PinSalt, second.PinSalt);
        CollectionAssert.AreNotEqual(first.PinHash, second.PinHash);
        Assert.IsTrue(first.VerifyPin("4821"));
        Assert.IsTrue(second.VerifyPin("4821"));
    }

    [TestMethod]
    [DataRow("123")]
    [DataRow("123456789")]
    [DataRow("12a4")]
    [DataRow("")]
    public void TC48_InvalidPinFormat_IsRejectedWhenCreatingStaff(string pin)
    {
        // Act
        var ex = Assert.ThrowsExactly<ArgumentException>(() => new StaffMember("ST001", "Test Staff", pin));

        // Assert
        Assert.AreEqual("PIN must be 4-8 digits.", ex.Message);
    }

    private static BookingService CreateService()
    {
        var service = new BookingService();

        service.AddStaffMember(new StaffMember(
            "ST001",
            "Test Staff",
            "4821"));

        return service;
    }
}
