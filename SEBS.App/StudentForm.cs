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
    public partial class StudentForm : Form
    {
        private readonly BookingService _bookingService;
        public StudentForm(BookingService bookingService)
        {
            InitializeComponent();
            _bookingService = bookingService;
        }
    }
}
