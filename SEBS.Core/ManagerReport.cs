using System;
using System.Collections.Generic;
using System.Text;

namespace SEBS.Core
{
    // FR9: Represents a snapshot report for managers - total bookings per equipment item,
    // how many bookings are currently overdue, and how many items are currently damaged.
    public class ManagerReport
    {
        // Keyed by EquipmentId
        public Dictionary<string, int> BookingsPerEquipment { get; }
        public int OverdueBookingCount { get; }
        public int DamagedEquipmentCount { get; }

        public ManagerReport(Dictionary<string, int> bookingsPerEquipment, int overdueBookingCount, int damagedEquipmentCount)
        {
            BookingsPerEquipment = bookingsPerEquipment;
            OverdueBookingCount = overdueBookingCount;
            DamagedEquipmentCount = damagedEquipmentCount;
        }
    }
}