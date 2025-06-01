using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LibraryManagement // Changed from MyLibrary
{
    public partial class ReturnBooksForm : Form
    {
        private const string ConnectionString = @"Server=(localdb)\MSSQLLocalDB;Initial Catalog=LibraryManagement;Integrated Security=True;Connect Timeout=30";

        // This property will hold the BorrowerID if this form is opened for a specific borrower
        public int BorrowerId { get; set; } = -1;

        public ReturnBooksForm(int borrowerId = -1) // Modified constructor to accept BorrowerId
        {
            InitializeComponent();
            this.BorrowerId = borrowerId;
            // The grid configuration below is commented out because it's for 'Borrowers' table.
            // If this form is for returning books, you'd configure it to show 'BorrowedBooks'
            // ConfigureGrid(); // You will need to re-evaluate this based on what data you want to display

            // Add the Load event handler here if it's not already added in InitializeComponent()
            this.Load += new System.EventHandler(this.ReturnBooksForm_Load);
        }

        private void ReturnBooksForm_Load(object sender, EventArgs e)
        {
            if (BorrowerId > 0)
            {
                this.Text = $"Return Books for Borrower ID: {BorrowerId}";
                // If this form is meant to display books borrowed by a specific user,
                // you would call a method like this:
                LoadBorrowedBooksForBorrower(BorrowerId);
            }
            else
            {
                this.Text = "Return Books (Select Borrower)";
                // If you intend for this form to initially list all borrowers,
                // and then clicking one shows their borrowed books, keep this:
                LoadAllBorrowers();
            }
        }

        // *** IMPORTANT: You need to implement this method based on your 'BorrowedBooks' table schema ***
        private void LoadBorrowedBooksForBorrower(int borrowerId)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    // This SQL query assumes you have a table like 'BorrowedBooks'
                    // with columns such as BorrowID, BookID, BorrowerID, BorrowDate, DueDate, ReturnDate
                    // and you'd typically join with the Books table to get book titles.
                    var adapter = new SqlDataAdapter(
                        "SELECT bb.BorrowID, b.Title, b.Author, bb.BorrowDate, bb.DueDate " +
                        "FROM BorrowedBooks bb JOIN Books b ON bb.BookID = b.BookID " +
                        "WHERE bb.BorrowerID = @BorrowerID AND bb.ReturnDate IS NULL",
                        connection);
                   // adapter.Parameters.AddWithValue("@BorrowerID", borrowerId);
                    var table = new DataTable();
                    adapter.Fill(table);
                    dgvBorrowers.DataSource = table; // Rename dgvBorrowers to dgvBorrowedBooks if it shows books
                                                     // Also update ConfigureGrid() to define columns for borrowed books
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading borrowed books: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // This method was in your original code. Keep it if this form initially lists all borrowers.
        // If the form's purpose is *only* to manage returns for a pre-selected borrower,
        // you might remove this method and the call to it from Load.
        private void LoadAllBorrowers()
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    var adapter = new SqlDataAdapter(
                        "SELECT BorrowerID, Name, Email, Phone FROM Borrowers",
                        connection);
                    var table = new DataTable();
                    adapter.Fill(table);
                    dgvBorrowers.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading borrowers: {ex.Message}");
            }
        }


        // The original ConfigureGrid method was set up to show 'Borrowers' columns.
        // If dgvBorrowers will display 'Borrowed Books', you need to change the column definitions.
        /*
        private void ConfigureGrid()
        {
            dgvBorrowers.AutoGenerateColumns = false;

            dgvBorrowers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "BorrowerID",
                HeaderText = "ID",
                ReadOnly = true
            });

            dgvBorrowers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = "Name"
            });

            dgvBorrowers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Email",
                HeaderText = "Email"
            });

            dgvBorrowers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Phone",
                HeaderText = "Phone"
            });
        }
        */

        private void btnRemove_Click(object sender, EventArgs e)
        {
            // This button's current logic is to DELETE A BORROWER from the Borrowers table.
            // If this form is for returning books, 'Remove' likely means removing a *borrowed book entry*.
            // Please clarify its intended function. For now, keeping original logic for deleting a borrower.
            if (dgvBorrowers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a borrower to remove");
                return;
            }

            var borrowerId = Convert.ToInt32(dgvBorrowers.SelectedRows[0].Cells["BorrowerID"].Value);

            if (MessageBox.Show("Are you sure you want to remove this borrower? This will also remove any borrowed book records associated with them.", "Confirm Removal",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using (var connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();
                        // Start a transaction to ensure data integrity
                        SqlTransaction transaction = connection.BeginTransaction();
                        try
                        {
                            // First, delete associated borrowed book records
                            using (var commandBorrowedBooks = new SqlCommand("DELETE FROM BorrowedBooks WHERE BorrowerID = @ID", connection, transaction))
                            {
                                commandBorrowedBooks.Parameters.AddWithValue("@ID", borrowerId);
                                commandBorrowedBooks.ExecuteNonQuery();
                            }

                            // Then, delete the borrower
                            using (var commandBorrower = new SqlCommand("DELETE FROM Borrowers WHERE BorrowerID = @ID", connection, transaction))
                            {
                                commandBorrower.Parameters.AddWithValue("@ID", borrowerId);
                                commandBorrower.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Borrower and associated records removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception($"Transaction failed: {ex.Message}");
                        }
                    }
                    LoadAllBorrowers(); // Reload all borrowers if this was showing all of them
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error removing borrower: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (dgvBorrowers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book to return.");
                return;
            }

            // Assuming dgvBorrowers now displays BorrowedBooks with a 'BorrowID' column
            var borrowId = Convert.ToInt32(dgvBorrowers.SelectedRows[0].Cells["BorrowID"].Value);
            var bookId = Convert.ToInt32(dgvBorrowers.SelectedRows[0].Cells["BookID"].Value); // Assuming BookID is also available

            if (MessageBox.Show("Are you sure you want to return the selected book?", "Confirm Return",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (var connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();
                        SqlTransaction transaction = connection.BeginTransaction();
                        try
                        {
                            // 1. Update the BorrowedBooks record to set ReturnDate
                            using (var commandUpdateBorrow = new SqlCommand(
                                "UPDATE BorrowedBooks SET ReturnDate = @ReturnDate WHERE BorrowID = @BorrowID",
                                connection, transaction))
                            {
                                commandUpdateBorrow.Parameters.AddWithValue("@ReturnDate", DateTime.Now);
                                commandUpdateBorrow.Parameters.AddWithValue("@BorrowID", borrowId);
                                commandUpdateBorrow.ExecuteNonQuery();
                            }

                            // 2. Increment the AvailableCopies for the returned book in the Books table
                            using (var commandUpdateBook = new SqlCommand(
                                "UPDATE Books SET AvailableCopies = AvailableCopies + 1 WHERE BookID = @BookID",
                                connection, transaction))
                            {
                                commandUpdateBook.Parameters.AddWithValue("@BookID", bookId);
                                commandUpdateBook.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Book returned successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Reload the list of borrowed books for the current borrower
                            LoadBorrowedBooksForBorrower(this.BorrowerId);
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
                    MessageBox.Show($"Error returning book: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        // This method is not used in the provided code, but might be if you intend to click a borrower
        // in a list and then open *another* form to manage their returns.
        // Given this form is named 'ReturnBooksForm', it's likely meant to handle the returns directly.
        // Keeping it for reference, but consider if it's needed or if its logic should be absorbed into this form.
        /*
        private void ShowReturnBooksForm(int borrowerId)
        {
            using (var returnForm = new ReturnBooksForm(borrowerId))
            {
                returnForm.ShowDialog();
                // After closing the return form, you might want to refresh the main borrower list
                // if this form itself is showing a list of all borrowers initially.
                if (this.BorrowerId == -1) // If this form was showing all borrowers
                {
                    LoadAllBorrowers();
                }
            }
        }
        */

        private void dgvBorrowers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // You can add logic here if you want something to happen when a cell content is clicked.
            // For example, display more details about the selected book or borrower.
        }
    }
}