using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions; // Still needed for email validation
using System.Windows.Forms;

namespace LibraryManagement // Ensure this namespace matches your project (e.g., LibraryManagement)
{
    public partial class BorrowerForm : Form
    {
        private const string ConnectionString = @"Server=(localdb)\MSSQLLocalDB;Initial Catalog=LibraryManagement;Integrated Security=True;Connect Timeout=30";
        public int BorrowerId { get; set; } = -1;

        public BorrowerForm()
        {
            InitializeComponent();
        }

        private void BorrowerForm_Load(object sender, EventArgs e)
        {
            if (BorrowerId > 0) LoadBorrowerDetails();
        }

        private void LoadBorrowerDetails()
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    // Selecting only Name and Email, removed Phone from the SELECT statement
                    var command = new SqlCommand(
                        "SELECT Name, Email FROM Borrowers WHERE BorrowerID = @ID",
                        connection);
                    command.Parameters.AddWithValue("@ID", BorrowerId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtName.Text = reader["Name"].ToString();
                            txtEmail.Text = reader["Email"].ToString();
                            // The line `txtPhone.Text = reader["Phone"].ToString();` was removed
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading borrower: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand command;

                    if (BorrowerId > 0)
                    {
                        // Update query for Name and Email only
                        command = new SqlCommand(
                            "UPDATE Borrowers SET Name = @Name, Email = @Email WHERE BorrowerID = @ID",
                            connection);
                        command.Parameters.AddWithValue("@ID", BorrowerId);
                    }
                    else
                    {
                        // Insert query for Name and Email only
                        command = new SqlCommand(
                            "INSERT INTO Borrowers (Name, Email) VALUES (@Name, @Email)",
                            connection);
                    }

                    command.Parameters.AddWithValue("@Name", txtName.Text);
                    command.Parameters.AddWithValue("@Email", txtEmail.Text);
                    // Removed: command.Parameters.AddWithValue("@Phone", txtPhone.Text);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Borrower saved successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving borrower: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (BorrowerId < 0) return;

            if (MessageBox.Show("Are you sure you want to delete this borrower? This will also remove any borrowed book records associated with them.", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using (var connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();
                        SqlTransaction transaction = connection.BeginTransaction();
                        try
                        {
                            // First, delete associated borrowed book records (if any)
                            using (var commandBorrowedBooks = new SqlCommand("DELETE FROM BorrowedBooks WHERE BorrowerID = @ID", connection, transaction))
                            {
                                commandBorrowedBooks.Parameters.AddWithValue("@ID", BorrowerId);
                                commandBorrowedBooks.ExecuteNonQuery();
                            }

                            // Then, delete the borrower
                            using (var commandBorrower = new SqlCommand("DELETE FROM Borrowers WHERE BorrowerID = @ID", connection, transaction))
                            {
                                commandBorrower.Parameters.AddWithValue("@ID", BorrowerId);
                                commandBorrower.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Borrower and associated records removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception($"Transaction failed: {ex.Message}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting borrower: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter a name", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Please enter a valid email address", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // Removed phone number validation block entirely
            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Removed IsValidPhoneNumber method entirely
        // Removed txtPhone_TextChanged event handler entirely
    }
}