using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LibraryManagement // Ensure this namespace matches your project and other forms
{
    public partial class LoginForm : Form
    {
        private const string ConnectionString = @"Server=(localdb)\MSSQLLocalDB;Initial Catalog=LibraryManagement;Integrated Security=True;Connect Timeout=30";

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (AuthenticateUser(username, password))
            {
                MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide(); // Hide the login form
                MainForm mainForm = new MainForm(); // Create an instance of MainForm
                mainForm.Show(); // Show the main form
                // Optionally, if you want to close the login form when MainForm closes:
                // mainForm.FormClosed += (s, args) => this.Close();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    // Assuming you have a Users table with Username and Password columns
                    var command = new SqlCommand(
                        "SELECT COUNT(1) FROM Users WHERE Username = @Username AND Password = @Password",
                        connection);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password); // Consider hashing passwords in a real app

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error during authentication: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Exit the application
        }

        // Placeholder for InitializeComponent - this is typically in the Designer.cs file
        // Ensure your InitializeComponent is correctly set up with your controls (txtUsername, txtPassword, btnLogin, btnExit)
    }
}