using System;
using System.Linq;
using System.Security.Cryptography;

namespace SEBS.Core
{
    public class StaffMember
    {
        // NFR5: The PIN is never stored. Only a random salt and a PBKDF2 hash of the PIN are kept.
        private const int MinPinLength = 4;
        private const int MaxPinLength = 8;
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int HashIterations = 100_000;

        public string StaffId { get; }
        public string Name { get; }
        internal byte[] PinSalt { get; }
        internal byte[] PinHash { get; }

        public StaffMember(string staffId, string name, string pin)
        {
            if (string.IsNullOrWhiteSpace(staffId))
                throw new ArgumentException("Staff ID cannot be empty.");
            if (!IsValidPinFormat(pin))
                throw new ArgumentException($"PIN must be {MinPinLength}-{MaxPinLength} digits.");

            StaffId = staffId;
            Name = name;
            PinSalt = RandomNumberGenerator.GetBytes(SaltSize);
            PinHash = HashPin(pin, PinSalt);
        }

        // NFR5: Compares in constant time so the check does not leak how much of the PIN matched
        public bool VerifyPin(string? pin)
        {
            if (!IsValidPinFormat(pin))
                return false;

            return CryptographicOperations.FixedTimeEquals(HashPin(pin!, PinSalt), PinHash);
        }

        private static bool IsValidPinFormat(string? pin) =>
            pin != null &&
            pin.Length >= MinPinLength &&
            pin.Length <= MaxPinLength &&
            pin.All(char.IsAsciiDigit);

        private static byte[] HashPin(string pin, byte[] salt) =>
            Rfc2898DeriveBytes.Pbkdf2(pin, salt, HashIterations, HashAlgorithmName.SHA256, HashSize);

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