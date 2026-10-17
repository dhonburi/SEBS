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

        // NFR5: Staff must sign in with their staff ID and PIN before the staff screen opens
        private void btnStaff_Click(object sender, EventArgs e)
        {
            using var login = new StaffLoginForm(_bookingService);
            if (login.ShowDialog() != DialogResult.OK || login.AuthenticatedStaffId == null)
                return;

            new StaffForm(_bookingService, login.AuthenticatedStaffId).ShowDialog();
        }
    }
}
