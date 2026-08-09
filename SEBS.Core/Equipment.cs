using System;

namespace SEBS.Core
{
    public class Equipment
    {
        public string EquipmentId { get; }
        public string Name { get; }
        public string Category { get; }
        public int TotalQuantity { get; }
        public int AvailableQuantity { get; private set; }
        public bool IsDamaged { get; private set; }

        public Equipment(string equipmentId, string name, string category, int totalQuantity)
        {
            if (string.IsNullOrWhiteSpace(equipmentId))
                throw new ArgumentException("Equipment ID cannot be empty.");
            if (totalQuantity <= 0)
                throw new ArgumentException("Total quantity must be greater than zero.");

            EquipmentId = equipmentId;
            Name = name;
            Category = category;
            TotalQuantity = totalQuantity;
            AvailableQuantity = totalQuantity;
            IsDamaged = false;
        }

        public bool IsAvailable()
        {
            return AvailableQuantity > 0 && !IsDamaged;
        }

        public void Reserve()
        {
            if (!IsAvailable())
                throw new InvalidOperationException("Equipment is not available for booking.");
            AvailableQuantity--;
        }

        public void Release()
        {
            if (AvailableQuantity < TotalQuantity)
                AvailableQuantity++;
        }

        public void MarkAsDamaged()
        {
            IsDamaged = true;
        }

        public void MarkAsRepaired()
        {
            IsDamaged = false;
        }
    }
}