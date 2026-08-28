using SEBS.Core;
using SEBS.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SEBS.App
{
    public partial class StaffForm : Form
    {
        private readonly BookingService _bookingService;
        public StaffForm(BookingService bookingService)
        {
            InitializeComponent();
            _bookingService = bookingService;

            RefreshBookingsGrid(); 
            RefreshEquipmentGrid();
        }

        private void RefreshBookingsGrid() 
        { 
            var rows = _bookingService.GetAllBookings().Select(b => new 
            { 
                b.BookingId, 
                Student = b.Student.Name, 
                Equipment = b.Equipment.Name, 
                b.BookingDate, 
                b.DueDate, 
                b.Status 
            }).ToList(); 
            dgvBookings.DataSource = rows; 
        }
        private void RefreshEquipmentGrid() 
        { 
            dgvEquipment.DataSource = _bookingService.GetAllEquipment(); 
        }

        private string? GetSelectedBookingId() 
        { 
            if (dgvBookings.CurrentRow == null) return null; 
            return dgvBookings.CurrentRow.Cells["BookingId"].Value?.ToString(); 
        }

        private string? GetSelectedEquipmentId() 
        { 
            if (dgvEquipment.CurrentRow == null) return null; 
            return dgvEquipment.CurrentRow.Cells["EquipmentId"].Value?.ToString(); 
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            var bookingId = GetSelectedBookingId(); 
            if (bookingId == null) 
            { 
                lblCheckInStatus.Text = "Select a booking first."; 
                return; 
            }
            if (string.IsNullOrWhiteSpace(txtStaffId.Text)) 
            { 
                lblCheckInStatus.Text = "Enter a staff ID."; 
                return; 
            }
            var result = _bookingService.CheckIn(bookingId, txtStaffId.Text.Trim()); 
            lblCheckInStatus.Text = result.Message; 

            RefreshBookingsGrid(); 
            RefreshEquipmentGrid();
        }

        private void btnCheckInDamaged_Click(object sender, EventArgs e)
        {
            var bookingId = GetSelectedBookingId();
            if (bookingId == null)
            {
                lblCheckInStatus.Text = "Select a booking first.";
                return;
            }
            if (string.IsNullOrWhiteSpace(txtStaffId.Text))
            {
                lblCheckInStatus.Text = "Enter a staff ID.";
                return;
            }
            var result = _bookingService.CheckInDamaged(bookingId, txtStaffId.Text.Trim());
            lblCheckInStatus.Text = result.Message;

            RefreshBookingsGrid();
            RefreshEquipmentGrid();
        }

        private void btnMarkRepaired_Click(object sender, EventArgs e)
        {
            var equipmentId = GetSelectedEquipmentId(); 
            if (equipmentId == null) 
            { 
                lblMarkRepairedStatus.Text = "Select an equipment item first."; 
                return; 
            }
            if (string.IsNullOrWhiteSpace(txtStaffId.Text)) 
            { 
                lblMarkRepairedStatus.Text = "Enter a staff ID."; 
                return; 
            }
            var result = _bookingService.MarkRepaired(equipmentId, txtStaffId.Text.Trim()); 
            lblMarkRepairedStatus.Text = result.Message; 

            RefreshEquipmentGrid();
        }
    }
}
