using SEBS.Core;
using SEBS.Services;

namespace SEBS.Tests;

[TestClass]
public class EquipmentBrowsingTests
{
    [TestMethod]
    public void TC01_GetAllEquipment_ReturnsSeededList()
    {
        // Arrange
        var service = new BookingService();
        var basketball = new Equipment("E001", "Basketball", "Balls", 3);
        var racket = new Equipment("E002", "Badminton Racket", "Rackets", 2);

        service.AddEquipment(basketball);
        service.AddEquipment(racket);

        // Act
        var equipment = service.GetAllEquipment();

        // Assert
        Assert.HasCount(2, equipment);
        CollectionAssert.Contains(equipment, basketball);
        CollectionAssert.Contains(equipment, racket);
    }

    [TestMethod]
    public void TC02_GetAllBookings_ReturnsAllCreatedBookings()
    {
        // Arrange
        var service = new BookingService();
        var student = new Student(
            "S001",
            "Test Student",
            "student@aut.ac.nz");

        var basketball = new Equipment("E001", "Basketball", "Balls", 1);
        var racket = new Equipment("E002", "Badminton Racket", "Rackets", 1);

        service.AddStudent(student);
        service.AddEquipment(basketball);
        service.AddEquipment(racket);

        var bookingDate = new DateTime(2026, 8, 30);

        service.CreateBooking(
            "S001",
            "E001",
            bookingDate,
            bookingDate.AddDays(1),
            out var firstBooking);

        service.CreateBooking(
            "S001",
            "E002",
            bookingDate,
            bookingDate.AddDays(2),
            out var secondBooking);

        // Act
        var bookings = service.GetAllBookings();

        // Assert
        Assert.HasCount(2, bookings);
        CollectionAssert.Contains(bookings, firstBooking);
        CollectionAssert.Contains(bookings, secondBooking);
    }

    [TestMethod]
    public void TC37_SearchByCategory_ReturnsOnlyThatCategory()
    {
        // Arrange
        var service = CreateServiceWithCatalogue();

        // Act
        var results = service.SearchEquipment("Racket Sports", null);

        // Assert
        Assert.HasCount(2, results);
        Assert.IsTrue(results.All(e => e.Category == "Racket Sports"));
    }

    [TestMethod]
    public void TC38_SearchByName_IsCaseInsensitivePartialMatch()
    {
        // Arrange
        var service = CreateServiceWithCatalogue();

        // Act
        var results = service.SearchEquipment(null, "  RACKET ");

        // Assert
        Assert.HasCount(2, results);
        CollectionAssert.AreEquivalent(
            new[] { "E003", "E004" },
            results.Select(e => e.EquipmentId).ToArray());
    }

    [TestMethod]
    public void TC39_SearchByCategoryAndName_AppliesBothFilters()
    {
        // Arrange
        var service = CreateServiceWithCatalogue();

        // Act
        var results = service.SearchEquipment("ball sports", "volley");

        // Assert
        Assert.HasCount(1, results);
        Assert.AreEqual("E002", results[0].EquipmentId);
    }

    [TestMethod]
    public void TC40_SearchWithNoFilters_ReturnsAllEquipmentIncludingUnavailable()
    {
        // Arrange
        var service = CreateServiceWithCatalogue();
        var damaged = service.GetAllEquipment().First(e => e.EquipmentId == "E001");
        damaged.MarkAsDamaged();

        // Act
        var results = service.SearchEquipment("", "");

        // Assert
        Assert.HasCount(4, results);
        CollectionAssert.Contains(results, damaged);
        Assert.IsFalse(results.First(e => e.EquipmentId == "E001").IsAvailable());
    }

    [TestMethod]
    public void TC41_SearchWithNoMatch_ReturnsEmptyList()
    {
        // Arrange
        var service = CreateServiceWithCatalogue();

        // Act
        var results = service.SearchEquipment("Water Sports", null);

        // Assert
        Assert.HasCount(0, results);
    }

    [TestMethod]
    public void TC42_GetCategories_ReturnsDistinctSortedCategories()
    {
        // Arrange
        var service = CreateServiceWithCatalogue();

        // Act
        var categories = service.GetCategories();

        // Assert
        CollectionAssert.AreEqual(
            new[] { "Ball Sports", "Racket Sports" },
            categories.ToArray());
    }

    private static BookingService CreateServiceWithCatalogue()
    {
        var service = new BookingService();

        service.AddEquipment(new Equipment("E001", "Basketball", "Ball Sports", 2));
        service.AddEquipment(new Equipment("E002", "Volleyball", "Ball Sports", 2));
        service.AddEquipment(new Equipment("E003", "Badminton Racket", "Racket Sports", 2));
        service.AddEquipment(new Equipment("E004", "Table Tennis Racket", "Racket Sports", 2));

        return service;
    }
}