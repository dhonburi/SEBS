using SEBS.Services;

namespace SEBS.App
{
    // NFR5: Staff sign-in screen. The staff screen is only opened once a valid staff ID and PIN are entered.
    public partial class StaffLoginForm : Form
    {
        private readonly BookingService _bookingService;

        public string? AuthenticatedStaffId { get; private set; }

        public StaffLoginForm(BookingService bookingService)
        {
            InitializeComponent();
            _bookingService = bookingService;
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            var staffId = txtLoginStaffId.Text.Trim();
            var result = _bookingService.AuthenticateStaff(staffId, txtPin.Text);

            // Clear the PIN after every attempt so it is not left in the text box
            txtPin.Clear();

            if (!result.Success)
            {
                lblLoginStatus.Text = result.Message;
                txtPin.Focus();
                return;
            }

            AuthenticatedStaffId = staffId;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
