using SEBS.Core;
using SEBS.Services;

namespace SEBS.App
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            var bookingService = new BookingService();

            bookingService.AddEquipment(new Equipment("E001", "Basketball", "Ball Sports", 5));
            bookingService.AddEquipment(new Equipment("E002", "Volleyball", "Ball Sports", 3));
            bookingService.AddEquipment(new Equipment("E003", "Table Tennis Racket", "Racket Sports", 10));
            bookingService.AddEquipment(new Equipment("E004", "Table Tennis Ball", "Racket Sports", 5));
            bookingService.AddEquipment(new Equipment("E005", "Badminton Shuttlecock", "Racket Sports", 20));
            bookingService.AddEquipment(new Equipment("E006", "Badminton Racket", "Racket Sports", 6));
            bookingService.AddStudent(new Student("S001", "Dhon Lao", "dhonl@aut.ac.nz"));
            bookingService.AddStudent(new Student("S002", "Bela Calma", "belac@aut.ac.nz"));
            bookingService.AddStudent(new Student("S003", "Hansith Perera", "hansithp@aut.ac.nz"));
            bookingService.AddStaffMember(new StaffMember("ST01", "Coach Dylan"));

            Application.Run(new MainForm(bookingService));
        }
    }
}