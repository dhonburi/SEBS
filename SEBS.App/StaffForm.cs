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
        }
    }
}
