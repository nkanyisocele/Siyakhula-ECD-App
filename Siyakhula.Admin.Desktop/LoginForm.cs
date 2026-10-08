using System;
using System.Drawing;
using System.Windows.Forms;
using Siyakhula.Shared.Security;

namespace Siyakhula.Admin.Desktop
{
    public class LoginForm : Form
    {
        private TextBox txtEmail;
        private TextBox txtPassword;
        private TextBox txtTwoFactor;
        private Button btnLogin;
        private Label lblMessage;

        // Mocking a secure database profile anchor for validation testing
        private const string ExpectedUser = "admin@siyakhula.org";
        private const string ExpectedHash = "v/FqZ80B7b8H+199KDuq7o/G5p9D2wIEbC76U37EwRE="; // Hashed "Siyakhula2026!" with salt "NPO_SALT"
        private const string Salt = "NPO_SALT";

        public LoginForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.txtEmail = new TextBox() { Location = new Point(150, 30), Width = 200 };
            this.txtPassword = new TextBox() { Location = new Point(150, 70), Width = 200, PasswordChar = '*' };
            this.txtTwoFactor = new TextBox() { Location = new Point(150, 110), Width = 100, MaxLength = 6 };
            this.btnLogin = new Button() { Text = "Authenticate", Location = new Point(150, 160), Width = 120 };
            this.lblMessage = new Label() { Location = new Point(30, 210), Width = 320, ForeColor = Color.Red };

            var lblEmail = new Label() { Text = "Admin Email:", Location = new Point(30, 33) };
            var lblPassword = new Label() { Text = "Password:", Location = new Point(30, 73) };
            var lbl2FA = new Label() { Text = "6-Digit 2FA Code:", Location = new Point(30, 113) };

            this.Controls.AddRange(new Control[] { lblEmail, lblPassword, lbl2FA, txtEmail, txtPassword, txtTwoFactor, btnLogin, lblMessage });
            this.Size = new Size(400, 300);
            this.Text = "Siyakhula Admin Gate - Secure Access";
            this.StartPosition = FormStartPosition.CenterScreen;

            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Defensive Empty Fields Check
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblMessage.Text = "Validation Error: Credentials cannot be empty.";
                return;
            }

            // Cryptographic Password Hashing Verification Check
            string computedHash = SecurityUtility.HashPassword(txtPassword.Text, Salt);
            bool isIdentityValid = (txtEmail.Text.Trim().ToLower() == ExpectedUser && computedHash == ExpectedHash);

            if (!isIdentityValid)
            {
                lblMessage.Text = "Access Denied: Invalid administrative credentials.";
                return;
            }

            // 2FA Mandatory Token Verification Check
            bool is2FATokenValid = SecurityUtility.VerifyTwoFactorToken("SHARED_SECRET", txtTwoFactor.Text.Trim());

            if (!is2FATokenValid)
            {
                lblMessage.Text = "Access Denied: 2FA Token mismatch. Enter '123456'.";
                return;
            }

            // Authentication Chain Passed Successfully
            this.Hide();
            Form1 mainDashboard = new Form1();
            mainDashboard.ShowDialog();
            this.Close();
        }
    }
}

