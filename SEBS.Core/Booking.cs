using System;

namespace SEBS.Core
{
    public class Booking
    {
        public string BookingId { get; }
        public Student Student { get; }
        public Equipment Equipment { get; }
        public DateTime BookingDate { get; }
        public DateTime DueDate { get; }
        public BookingStatus Status { get; private set; }
        public string CompletedByStaffId { get; private set; }

        public Booking(string bookingId, Student student, Equipment equipment, DateTime bookingDate, DateTime dueDate)
        {
            if (string.IsNullOrWhiteSpace(bookingId))
                throw new ArgumentException("Booking ID cannot be empty.");
            if (dueDate <= bookingDate)
                throw new ArgumentException("Due date must be after the booking date.");

            BookingId = bookingId;
            Student = student;
            Equipment = equipment;
            BookingDate = bookingDate;
            DueDate = dueDate;

            equipment.Reserve();
            Status = BookingStatus.Active;
        }

        public void Cancel()
        {
            if (Status != BookingStatus.Active)
                throw new InvalidOperationException("Only active bookings can be cancelled.");

            Equipment.Release();
            Status = BookingStatus.Cancelled;
        }

        public void Complete(string staffId)
        {
            CompleteInternal(staffId);
        }

        public void CompleteWithDamageReport(string staffId)
        {
            CompleteInternal(staffId);
            Equipment.MarkAsDamaged();
        }

        private void CompleteInternal(string staffId)
        {
            if (Status != BookingStatus.Active)
                throw new InvalidOperationException("Only active bookings can be completed.");
            if (string.IsNullOrWhiteSpace(staffId))
                throw new ArgumentException("Staff ID is required to complete a check-in.");

            Equipment.Release();
            Status = BookingStatus.Completed;
            CompletedByStaffId = staffId;
        }

        public bool IsOverdue(DateTime currentDate)
        {
            return Status == BookingStatus.Active && currentDate > DueDate;
        }
    }
}