using System;

namespace SEBS.Core
{
    public class StaffMember
    {
        public string StaffId { get; }
        public string Name { get; }

        public StaffMember(string staffId, string name)
        {
            if (string.IsNullOrWhiteSpace(staffId))
                throw new ArgumentException("Staff ID cannot be empty.");

            StaffId = staffId;
            Name = name;
        }

        public void CheckIn(Booking booking)
        {
            booking.Complete(StaffId);
        }

        public void CheckInDamaged(Booking booking)
        {
            booking.CompleteWithDamageReport(StaffId);
        }

        public void MarkEquipmentRepaired(Equipment equipment)
        {
            equipment.MarkAsRepaired();
        }
    }
}