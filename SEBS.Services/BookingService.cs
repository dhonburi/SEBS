using System;
using System.Collections.Generic;
using System.Linq;
using SEBS.Core;

namespace SEBS.Services
{
    public class ServiceResult
    {
        public bool Success { get; }
        public string Message { get; }

        private ServiceResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public static ServiceResult Ok(string message) => new ServiceResult(true, message);
        public static ServiceResult Fail(string message) => new ServiceResult(false, message);
    }

    public class BookingService
    {
        // The fields below are used to simulate a data store for the purpose of this exercise. In a real-world application, these would be replaced with database access code.
        private readonly List<Equipment> _equipment = new List<Equipment>();
        private readonly List<Student> _students = new List<Student>();
        private readonly List<Booking> _bookings = new List<Booking>();
        private readonly List<StaffMember> _staff = new List<StaffMember>();
        private int _nextBookingNumber = 1;

        // Setup methods to add initial data for testing purposes
        public void AddEquipment(Equipment equipment) => _equipment.Add(equipment);
        public void AddStudent(Student student) => _students.Add(student);
        public void AddStaffMember(StaffMember staff) => _staff.Add(staff);

        // FR1: Query methods to look up equipment and bookings
        public List<Equipment> GetAvailableEquipment() =>
            _equipment.Where(e => e.IsAvailable()).ToList();

        public List<Equipment> GetAllEquipment() => _equipment.ToList();

        // FR2, FR3: Create a booking if the equipment is available and the due date is after the booking date
        public ServiceResult CreateBooking(string studentId, string equipmentId, DateTime bookingDate, DateTime dueDate, out Booking? booking)
        {
            booking = null;

            var student = _students.FirstOrDefault(s => s.StudentId == studentId);
            if (student == null)
                return ServiceResult.Fail("Student not found.");

            var equipment = _equipment.FirstOrDefault(e => e.EquipmentId == equipmentId);
            if (equipment == null)
                return ServiceResult.Fail("Equipment not found.");

            try
            {
                string bookingId = $"B{_nextBookingNumber:D4}";
                booking = new Booking(bookingId, student, equipment, bookingDate, dueDate);
                _bookings.Add(booking);
                _nextBookingNumber++;
                return ServiceResult.Ok($"Booking {bookingId} created.");
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail(ex.Message);
            }
        }

        // FR4: Cancel a booking only if active
        public ServiceResult CancelBooking(string bookingId)
        {
            var booking = _bookings.FirstOrDefault(b => b.BookingId == bookingId);
            if (booking == null)
                return ServiceResult.Fail("Booking not found.");

            try
            {
                booking.Cancel();
                return ServiceResult.Ok($"Booking {bookingId} cancelled.");
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail(ex.Message);
            }
        }

        // FR5: Check in a booking as completed, Only staff can perform this action
        public ServiceResult CheckIn(string bookingId, string staffId)
        {
            var booking = _bookings.FirstOrDefault(b => b.BookingId == bookingId);
            if (booking == null)
                return ServiceResult.Fail("Booking not found.");

            var staff = _staff.FirstOrDefault(s => s.StaffId == staffId);
            if (staff == null)
                return ServiceResult.Fail("Staff member not found or not authorised.");

            try
            {
                staff.CheckIn(booking);
                return ServiceResult.Ok($"Booking {bookingId} checked in.");
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail(ex.Message);
            }
        }

        // FR6: Check in a booking as completed with damage report, Only staff can perform this action
        public ServiceResult CheckInDamaged(string bookingId, string staffId)
        {
            var booking = _bookings.FirstOrDefault(b => b.BookingId == bookingId);
            if (booking == null)
                return ServiceResult.Fail("Booking not found.");

            var staff = _staff.FirstOrDefault(s => s.StaffId == staffId);
            if (staff == null)
                return ServiceResult.Fail("Staff member not found or not authorised.");

            try
            {
                staff.CheckInDamaged(booking);
                return ServiceResult.Ok($"Booking {bookingId} checked in as damaged.");
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail(ex.Message);
            }
        }

        // FR7: Mark damaged equipment as repaired, Only staff can perform this action
        public ServiceResult MarkRepaired(string equipmentId, string staffId)
        {
            var equipment = _equipment.FirstOrDefault(e => e.EquipmentId == equipmentId);
            if (equipment == null)
                return ServiceResult.Fail("Equipment not found.");

            var staff = _staff.FirstOrDefault(s => s.StaffId == staffId);
            if (staff == null)
                return ServiceResult.Fail("Staff member not found or not authorised.");

            staff.MarkEquipmentRepaired(equipment);
            return ServiceResult.Ok($"Equipment {equipmentId} marked as repaired.");
        }

        // FR8: Query methods to look up bookings by status and overdue
        public List<Booking> GetOverdueBookings(DateTime currentDate) =>
            _bookings.Where(b => b.IsOverdue(currentDate)).ToList();

        public List<Booking> GetAllBookings() => _bookings.ToList();
    }
}   