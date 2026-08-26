using SEBS.Services;

namespace SEBS.App
{
    public partial class MainForm : Form
    {
        readonly BookingService _bookingService;
        public MainForm(BookingService bookingService)
        {
            InitializeComponent();
            _bookingService = bookingService;
        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            new StudentForm(_bookingService).ShowDialog();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            new StaffForm(_bookingService).ShowDialog();
        }
    }
}
