using SEBS.Services;
using SEBS.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SEBS.App
{
    public partial class StudentForm : Form
    {
        private readonly BookingService _bookingService;

        public StudentForm(BookingService bookingService)
        {
            InitializeComponent();
            _bookingService = bookingService;
            RefreshEquipmentGrid();
            RefreshBookingsGrid();
        }

        private void RefreshEquipmentGrid()
        {
            dgvEquipment.DataSource = _bookingService.GetAllEquipment();
        }

        private void RefreshBookingsGrid()
        {
            var rows = _bookingService.GetAllBookings().Select(b => new { b.BookingId, Student = b.Student.Name, Equipment = b.Equipment.Name, b.BookingDate, b.DueDate, b.Status }).ToList();
            dgvMyBookings.DataSource = rows;
        }

        private string? GetSelectedEquipmentId() 
        { 
            if (dgvEquipment.CurrentRow == null) return null; 
            return dgvEquipment.CurrentRow.Cells["EquipmentId"].Value?.ToString(); 
        }

        private string? GetSelectedBookingId() 
        { 
            if (dgvMyBookings.CurrentRow == null) return null; 
            return dgvMyBookings.CurrentRow.Cells["BookingId"].Value?.ToString(); 
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            var equipmentId = GetSelectedEquipmentId(); 
            if (equipmentId == null) 
            { 
                lblStatus.Text = "Select an equipment item first."; 
                return; 
            }
            if (string.IsNullOrWhiteSpace(txtStudentId.Text)) 
            { 
                lblStatus.Text = "Enter a student ID."; 
                return; 
            }
            var result = _bookingService.CreateBooking(txtStudentId.Text.Trim(), equipmentId, DateTime.Today, dtpDueDate.Value.Date, out _); 
            lblStatus.Text = result.Message; 
            RefreshEquipmentGrid(); 
            RefreshBookingsGrid();
        }

        private void btnCancelBooking_Click(object sender, EventArgs e)
        {
            var bookingId = GetSelectedBookingId(); if (bookingId == null) 
            { 
                lblStatus2.Text = "Select a booking first."; 
                return; 
            }
            var result = _bookingService.CancelBooking(bookingId); 
            lblStatus2.Text = result.Message; 
            RefreshEquipmentGrid(); 
            RefreshBookingsGrid();
        }
    }
}
