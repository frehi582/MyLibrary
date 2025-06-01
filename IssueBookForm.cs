using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LibraryManagement
{
    public partial class IssueBookForm : Form
    {
        private int? preSelectedBookId;
        private SqlConnection connection;
        private string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=LibraryManagement;Integrated Security=True;";

        public IssueBookForm(int? bookId = null)
        {
            InitializeComponent();
            preSelectedBookId = bookId;
            InitializeDatabaseConnection();
            WireUpEvents();
            LoadBorrowers();
            LoadAvailableBooks();
        }

        private void InitializeDatabaseConnection()
        {
            connection = new SqlConnection(connectionString);
        }

        private void WireUpEvents()
        {
            cmbBooks.SelectedIndexChanged += CmbBooks_SelectedIndexChanged;
            btnIssue.Click += BtnIssue_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void LoadBorrowers()
        {
            try
            {
                connection.Open();
                string query = "SELECT BorrowerID, Name FROM Borrowers";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    cmbBorrowers.DisplayMember = "Name";
                    cmbBorrowers.ValueMember = "BorrowerID";
                    cmbBorrowers.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                ShowError($"Error loading borrowers: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void LoadAvailableBooks()
        {
            try
            {
                connection.Open();
                string query = "SELECT BookID, Title, AvailableCopies FROM Books WHERE AvailableCopies > 0";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    cmbBooks.DisplayMember = "Title";
                    cmbBooks.ValueMember = "BookID";
                    cmbBooks.DataSource = table;

                    // Select pre-selected book if available
                    if (preSelectedBookId.HasValue)
                    {
                        foreach (DataRowView item in cmbBooks.Items)
                        {
                            if (Convert.ToInt32(item["BookID"]) == preSelectedBookId.Value)
                            {
                                cmbBooks.SelectedItem = item;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError($"Error loading books: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void CmbBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBooks.SelectedItem != null)
            {
                DataRowView row = (DataRowView)cmbBooks.SelectedItem;
                lblAvailableCount.Text = row["AvailableCopies"].ToString();
            }
        }

        private void BtnIssue_Click(object sender, EventArgs e)
        {
            if (cmbBorrowers.SelectedItem == null || cmbBooks.SelectedItem == null)
            {
                ShowError("Please select both a borrower and a book");
                return;
            }

            int borrowerId = (int)cmbBorrowers.SelectedValue;
            int bookId = (int)cmbBooks.SelectedValue;
            DateTime dueDate = dtpDueDate.Value;

            try
            {
                connection.Open();

                // Start transaction
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // 1. Insert into IssuedBooks
                    string issueQuery = @"INSERT INTO IssuedBooks (BookID, BorrowerID, IssueDate, DueDate) 
                                       VALUES (@BookID, @BorrowerID, @IssueDate, @DueDate)";

                    using (SqlCommand cmd = new SqlCommand(issueQuery, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@BookID", bookId);
                        cmd.Parameters.AddWithValue("@BorrowerID", borrowerId);
                        cmd.Parameters.AddWithValue("@IssueDate", DateTime.Today);
                        cmd.Parameters.AddWithValue("@DueDate", dueDate);
                        cmd.ExecuteNonQuery();
                    }

                    // 2. Decrement AvailableCopies in Books
                    string updateQuery = "UPDATE Books SET AvailableCopies = AvailableCopies - 1 WHERE BookID = @BookID";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@BookID", bookId);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    MessageBox.Show("Book issued successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ShowError($"Error issuing book: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                ShowError($"Database error: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            lblError.Visible = true;
        }
    }
}